using Movie_API.Models.Value_Object;

namespace Movie_API.Models
{
    public class PagingResultModel<T> : List<T>
    {
        public PagingQuery Params { get; }
        public PagingResponse Result { get; set; }
        public PagingResultModel(PagingQuery query)
        {
            Params = query;
            Result = new PagingResponse();
        }

        public void GetData(IQueryable<T> query)
        {
            Result.TotalCount = query.Count();
            Result.TotalPages = (int)Math.Ceiling(Result.TotalCount / (double)Params.PageSize);
            Result.CurrentPage = Params.Page;
            Result.NextPage = Result.CurrentPage <= Result.TotalPages ? Result.CurrentPage + 1 : Result.CurrentPage;
            Result.PreviousPage = Result.CurrentPage == 1 ? Result.CurrentPage : Result.CurrentPage - 1;
            var result = query.Skip((Params.Page - 1) * Params.PageSize).Take(Params.PageSize).ToList();

            if (!string.IsNullOrWhiteSpace(Params.Sort))
            {

                var entity = typeof(T);
                var property = entity.GetProperty(Params.Sort);
                if ((int)Params.SortingDirection == 1)
                {
                    result = result.OrderBy(x => property.GetValue(x, null)).ToList();
                }
                else
                {
                    result = result.OrderByDescending(x => property.GetValue(x, null)).ToList();
                }

            }


            AddRange(result);
        }

    }
}
