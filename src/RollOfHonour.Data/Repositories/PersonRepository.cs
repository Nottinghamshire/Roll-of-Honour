﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RollOfHonour.Core;
using RollOfHonour.Core.Enums;
using RollOfHonour.Core.Models;
using RollOfHonour.Core.Models.Search;
using RollOfHonour.Core.Shared;
using RollOfHonour.Data.Context;
using Person = RollOfHonour.Core.Models.Person;
using War = RollOfHonour.Core.Enums.War;

namespace RollOfHonour.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly ILogger<PersonRepository> _logger;
    private RollOfHonourContext _dbContext { get; }
    
    private readonly Storage _storage;

    public PersonRepository(ILogger<PersonRepository> logger, 
        RollOfHonourContext dbContext, IOptions<Storage> storageSettings)
    {
        _logger = logger;
        _dbContext = dbContext;
        _storage = storageSettings.Value;
    }

    public async Task<Person?> GetById(int id)
    {
        try
        {
            var dbPerson = await _dbContext.People
                .Include(p => p.Photos)
                .Include(p => p.Decorations)
                .Include(p => p.RecordedNames).ThenInclude(rn => rn.WarMemorial)
                .Include(p => p.SubUnit).ThenInclude(unit => unit!.Regiment)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (dbPerson is null)
            {
                // TODO: Is this necessary?
                return null;
            }

            return dbPerson.ToDomainModel(_storage.ImageUrlPrefix);
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }
    private async Task<Models.DB.Person?> GetEntityById(int id)
    {
        try
        {
            var dbPerson = await _dbContext.People
                .Include(_ => _.Photos)
                .Include(_ => _.Decorations)
                .Include(_ => _.RecordedNames).ThenInclude(rn => rn.WarMemorial)
                .Include(_ => _.SubUnit).ThenInclude(unit => unit!.Regiment)
                .Include(_ => _.War)
                .FirstOrDefaultAsync(p => p.Id == id);

            return dbPerson ?? null;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public async Task<IEnumerable<Person>> DiedOnDay(DateTime date)
    {
        var diedOnDate = _dbContext.People
            .Where(p =>
                p.DateOfDeath.HasValue &&
                (p.DateOfDeath.Value.Day == date.Day && p.DateOfDeath.Value.Month == date.Month))
            .Include(p => p.Photos)
            .AsNoTracking();

        var deathCount = diedOnDate.Count();
        var dbPeople = new List<Models.DB.Person>();

        if (deathCount < 4)
        {
            var countOfPeople = deathCount;
            var totalIds = Count() - 1;
            var random = new Random((int)date.Ticks);

            while (countOfPeople < 4)
            {
                var randomId = random.Next(0, totalIds);
                var person = await _dbContext.People
                    .Include(p => p.Photos)
                    .FirstOrDefaultAsync(p => p.Id == randomId && p.Deleted == false);
                if (person is not null)
                {
                    dbPeople.Add(person);
                    countOfPeople++;
                }
            }
        }
        else if (deathCount > 4)
        {
            dbPeople.AddRange(diedOnDate.Take(4).ToList());
        }
        else
        {
            dbPeople.AddRange(diedOnDate.ToList());
        }

        IEnumerable<Person> people =
            dbPeople.Select(p => p.ToDomainModel(_storage.ImageUrlPrefix));
        return people;
    }

    public async Task<PaginatedList<Person>> SearchPeople(PersonQuery query, Filters filters, int pageIndex,
        int pageSize)
    {
        var dbPeople = GetPeopleByName(query);

        if (filters.IsFiltered)
        {
            dbPeople = FilterPeople(dbPeople, filters);
        }

        var resultCount = dbPeople.Count();
        if (resultCount == 0)
        {
            return new PaginatedList<Person>();
        }

        dbPeople = dbPeople
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).Distinct()
            .OrderBy(p => p.LastName)
            .AsNoTracking();

        var results = await dbPeople
            .Select(p => p.ToDomainModel(_storage.ImageUrlPrefix))
            .ToListAsync();
        return new PaginatedList<Person>(results, resultCount, pageIndex, pageSize);
    }

    public async Task<PaginatedList<Person>> SearchPeopleByRegimentName(RegimentPersonQuery query, Filters filters,
        int pageIndex, int pageSize)
    {
        var dbPeople = GetPeopleByRegimentName(query);

        if (filters.IsFiltered)
        {
            dbPeople = FilterPeople(dbPeople, filters);
        }

        var resultCount = dbPeople.Count();
        if (resultCount == 0)
        {
            return new PaginatedList<Person>();
        }

        dbPeople = dbPeople
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize).Distinct()
            .OrderBy(p => p.LastName)
            .AsNoTracking();

        var results = await dbPeople
            .Select(p => p.ToDomainModel(_storage.ImageUrlPrefix))
            .ToListAsync();
        return new PaginatedList<Person>(results, resultCount, pageIndex, pageSize);
    }

    public async Task<List<RegimentFilter>> GetRegimentFiltersForSearch(ISearchQuery query)
    {
        var dbPeople = GetPeopleByName(query);
        var regiments = await dbPeople
            .Where(p => p.SubUnit != null && p.SubUnit.RegimentId.HasValue && p.SubUnit.Regiment != null &&
                        !string.IsNullOrEmpty(p.SubUnit.Regiment.Name))
            .Select(p => new RegimentFilter((int)p.SubUnit!.RegimentId!, p.SubUnit!.Regiment!.Name!))
            .AsNoTracking()
            .Distinct()
            .ToListAsync();

        return regiments;
    }

    public async Task<List<RegimentFilter>> GetRegimentFiltersForSearchByRegimentName(ISearchQuery query)
    {
        var dbPeople = GetPeopleByRegimentName(query);
        var regiments = await dbPeople
            .Where(p => p.SubUnit != null && p.SubUnit.RegimentId.HasValue && p.SubUnit.Regiment != null &&
                        !string.IsNullOrEmpty(p.SubUnit.Regiment.Name))
            .Select(p => new RegimentFilter((int)p.SubUnit!.RegimentId!, p.SubUnit!.Regiment!.Name!))
            .AsNoTracking()
            .Distinct()
            .ToListAsync();

        return regiments;
    }

    public async Task Update(Person person)
    {
        try
        {
            _logger.LogInformation($"Started to update person with id {person.Id}", new { person });

            var personEntity = await GetEntityById(person.Id);
            if (personEntity is null)
            {
                _logger.LogError($"Unable to find person with id {person.Id}");
                return;
            }

            _logger.LogInformation($"Found person with id {person.Id}");

            personEntity.WarId = person.WarId;

            personEntity.Rank = person.Rank;
            personEntity.ServiceNumber = person.ServiceNumber;
            personEntity.MilitaryHistory = person.MilitaryHistory;

            if (person.Unit.IsNullOrEmpty() is false && person.Regiment.IsNullOrEmpty() is false)
            {
                if (personEntity.SubUnit != null)
                {
                    personEntity.SubUnit.Name = person.Unit;
                    personEntity.SubUnit.Regiment = new Models.DB.Regiment { Name = person.Regiment };
                }
                
                _logger.LogInformation($"Assigned sub unit to person with id {person.Id}");

            }

            personEntity.FirstNames = person.FirstNames;
            personEntity.LastName = person.LastName;
            personEntity.PlaceOfBirth = person.PlaceOfBirth;
            personEntity.EmploymentHobbies = person.EmploymentHobbies;
            personEntity.FamilyHistory = person.FamilyHistory ?? "";
            personEntity.ExtraInfo = person.ExtraInfo ?? "";

            await _dbContext.SaveChangesAsync();
            
            _logger.LogInformation($"Saved changes to person with id {person.Id}");
         
        }
        catch (Exception exception)
        {
            _logger.LogError("An exception was caught in PersonRepository.Update", new { exception });
        }
    }

    public async Task<PaginatedList<Person>> GetPageOfPeople(int pageIndex, int pageSize)
    {
        var dbPeople = await _dbContext.People
            .Include(p => p.Photos)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Where(p => p.Deleted == false)
            .AsNoTracking()
            .ToListAsync();

        if (!dbPeople.Any())
        {
            return new PaginatedList<Person>();
        }

        return new PaginatedList<Person>(dbPeople.Select(p =>
                p.ToDomainModel(_storage.ImageUrlPrefix)).ToList(),
            _dbContext.People.Count(),
            pageIndex, pageSize);
    }

    public int Count()
    {
        return _dbContext.People.Count();
    }

    private IQueryable<Models.DB.Person> FilterPeople(IQueryable<Models.DB.Person> people, Filters filters)
    {
        if (filters.WarIsSelected)
        {
            people = ByWar(people, filters.War);
        }

        // Default is Military
        people = PersonTypeFilter(people, filters.SelectedPersonType);

        if (filters.DateRangeUsed)
        {
            people = DiedBefore(people, filters.DiedBefore);
            people = BornAfter(people, filters.BornAfter);
        }

        if (filters.HasRegiments)
        {
            people = ByRegiment(people, filters.Regiments);
        }

        return people;
    }


    private IQueryable<Models.DB.Person> ByWar(IQueryable<Models.DB.Person> people, War? filtersWar)
    {
        return people.Where(p =>
            p.WarId != null && p.WarId.HasValue && filtersWar!.Value == (War)p.WarId);
    }

    private IQueryable<Models.DB.Person> ByRegiment(IQueryable<Models.DB.Person> people, HashSet<int> regimentIds)
    {
        return people.Where(p =>
            p.SubUnit != null && p.SubUnit.RegimentId.HasValue && regimentIds.Contains((int)p.SubUnit.RegimentId));
    }

    private IQueryable<Models.DB.Person> DiedBefore(IQueryable<Models.DB.Person> people, DateTime date)
    {
        return people.Where(p => p.DateOfDeath <= date);
    }

    private IQueryable<Models.DB.Person> BornAfter(IQueryable<Models.DB.Person> people, DateTime date)
    {
        return people.Where(p => p.DateOfBirth >= date);
    }

    private IQueryable<Models.DB.Person> GetPeopleByName(ISearchQuery query)
    {
        var dbPeople = _dbContext.People
            .Include(p => p.Photos)
            .Include(p => p.Decorations)
            .Include(p => p.RecordedNames)
            .ThenInclude(rn => rn.WarMemorial)
            .Include(p => p.SubUnit)
            .ThenInclude(unit => unit!.Regiment)
            .Where(p =>
                p.Deleted == false &&
                p.FullName!.Contains(query.SearchTerm)
            );

        return dbPeople;
    }

    private IQueryable<Models.DB.Person> GetPeopleByRegimentName(ISearchQuery query)
    {
        var dbPeople = _dbContext.People
            .Include(p => p.Photos)
            .Include(p => p.Decorations)
            .Include(p => p.RecordedNames)
            .ThenInclude(rn => rn.WarMemorial)
            .Include(p => p.SubUnit)
            .ThenInclude(unit => unit!.Regiment)
            .Where(p => p.SubUnit != null
                        && p.SubUnit.Regiment != null
                        && p.SubUnit.Regiment.Name != null
                        && p.SubUnit.Regiment.Name.Contains(query.SearchTerm)
                        && p.Deleted == false
            );

        return dbPeople;
    }

    private IQueryable<Models.DB.Person> PersonTypeFilter(IQueryable<Models.DB.Person> people,
        PersonType? filtersSelectedPersonType)
    {
        if (filtersSelectedPersonType == PersonType.Civilian)
        {
            // Criteria to determine a civilian
            return people
                    .Where(p => p.Rank == null || p.Rank.Equals(""))
                    .Where(p => !p.SubUnitId.HasValue)
                ;
        }

        // Default is Military
        return people
                .Where(p => p.Rank != null && !p.Rank.Equals(""))
                .Where(p => p.SubUnitId.HasValue)
            ;
    }
}
