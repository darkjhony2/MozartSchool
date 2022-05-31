namespace ColegioMozart.Application.Common.Models
{
    public class PageModelFilterDTO
    {

        public PageModelFilterDTO(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}
