using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RollOfHonour.Core.Enums;
using RollOfHonour.Core.Models.Search;
using RollOfHonour.Core.Search;

namespace RollOfHonour.Web.Pages.Search
{
    public class ResultsModel : PageModel
    {

        private ISuperSearchService _searchService;

        [BindProperty(SupportsGet = false)]
        [FromQuery]
        public War War {get; set;}

        [BindProperty(SupportsGet = false)]
        [FromQuery]
        public PersonType SelectedPersonType { get; set; }

        [BindProperty(SupportsGet = false)]
        [FromQuery]
        public QueryType SelectedQueryType { get; set; }
        public List<MemorialSearchResult> MemorialSearchResults = new();
        public List<PersonSearchResult> PersonSearchResults = new();
        public bool HasResults => (MemorialSearchResults.Count() > 0 || PersonSearchResults.Count() > 0);
        public string SearchString = String.Empty;

        public ResultsModel(ISuperSearchService searchService)
        {
           _searchService = searchService;
        }

        public async Task OnGet()
        {
            PersonSearchResults = new();
            MemorialSearchResults = new();

            if (SelectedQueryType == QueryType.Person)
            {
                var searchQuery = new PersonQuery
                {
                    SearchTerm = SearchString ?? String.Empty,
                    SelectedWar = War,
                    PersonType = SelectedPersonType,
                };

                Result<List<PersonSearchResult>> results = await _searchService.PersonSearch(searchQuery);

                if (results.IsSuccess is not true)
                {
                    // Do something
                }

                if (!results.Value.Any())
                {
                    // Do something
                }

                PersonSearchResults = results.Value;
            }

            else if (SelectedQueryType == QueryType.Memorial)
            {
                var searchQuery = new MemorialQuery
                {
                    SearchTerm = SearchString ?? String.Empty
                };

                Result<List<MemorialSearchResult>> results = await _searchService.MemorialSearch(searchQuery);

                if (results.IsSuccess is not true)
                {
                    // Do something
                }

                if (!results.Value.Any())
                {
                    // Do something
                }

                MemorialSearchResults = results.Value;

            }
        }
    }
}