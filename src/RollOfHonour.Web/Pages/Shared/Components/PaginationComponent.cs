using Microsoft.AspNetCore.Mvc;

namespace RollOfHonour.Web.Pages.Shared.ViewComponents
{
    public class PaginationComponent : ViewComponent
    {
        public PaginationComponent()
        {

        }

        public IViewComponentResult Invoke(int currentPage, int pageCount, string slug)
        {
            var model = new PaginationViewModel(currentPage, pageCount, slug);
            return View(model);
        }
    }

    public class PaginationViewModel
    {
        public int[] PageRange { get; private set; } = new int[] { 0 };
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < PageCount;
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public string Slug { get; set; } = "";

        public PaginationViewModel(int currentPage, int pageCount, string slug)
        {
           Slug = slug;
           CurrentPage = currentPage;
           PageCount = pageCount;
           SetPageRange();
        }

        private void SetPageRange()
        {
            if (CurrentPage == 1)
            {
                this.PageRange = Enumerable.Range(1, 9).Append(PageCount).ToArray();
                return;
            }

            int minRange = Math.Max(1, CurrentPage - 4);
            int maxRange = Math.Min(PageCount, CurrentPage + 5);
            if (minRange <= 1)
            {
                this.PageRange = Enumerable.Range(1, 9).Append(PageCount).ToArray();
                return;
            }

            var count = maxRange - minRange;
            if (maxRange == PageCount)
            {
                this.PageRange = Enumerable.Range(minRange, count).Prepend(1).ToArray();
                return;
            }

            this.PageRange = Enumerable.Range(minRange, count).Prepend(1).Append(PageCount).ToArray();
        }
    }
}
