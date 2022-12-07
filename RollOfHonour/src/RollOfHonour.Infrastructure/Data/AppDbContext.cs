﻿using System.Reflection;
using RollOfHonour.Core.ProjectAggregate;
using RollOfHonour.SharedKernel;
using RollOfHonour.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;
using RollOfHonour.Core.MemorialAggregate;

namespace RollOfHonour.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
      : base(options)
  {
    _dispatcher = dispatcher;
  }

  public DbSet<Person> People => Set<Person>();
  public DbSet<RecordedName> Names => Set<RecordedName>();
  public DbSet<Memorial> Memorials => Set<Memorial>();
  public DbSet<RecordedName> RecordedNames => Set<RecordedName>(); 

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
  {
    int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null) return result;

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
        .Select(e => e.Entity)
        .Where(e => e.DomainEvents.Any())
        .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
