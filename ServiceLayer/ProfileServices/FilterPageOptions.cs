namespace ServiceLayer.ProfileServices
{
    public class FilterPageOptions
    {
        public const int DefaultPageSize = 10; //default page size is 10

        //-----------------------------------------
        //Paging parts, which require the use of the method

        private int _pageNum = 1;

        private int _pageSize = DefaultPageSize;

        /// <summary>
        ///     This holds the possible page sizes
        /// </summary>
        public int[] PageSizes = new[] { 5, DefaultPageSize, 20, 50, 100, 500, 1000 };

        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public string CityId { get; set; }
        public string GenderId { get; set; }

        public int PageNum
        {
            get { return _pageNum; }
            set { _pageNum = value; }
        }

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        public int Size { get; set; }
        public bool IsEnd { get; private set; }

        /// <summary>
        ///     This is set to the number of pages available based on the number of entries in the query
        /// </summary>
        public int NumPages { get; private set; }

        public void SetupRestOfDto<T>(IQueryable<T> query)
        {
            NumPages = (int)Math.Ceiling(
                (double)query.Count() / PageSize);
            PageNum = Math.Max(1, Math.Min(
                PageNum, NumPages));
            IsEnd = PageNum >= NumPages;
        }
    }
}