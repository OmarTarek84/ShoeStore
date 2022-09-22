

using Core.Dtos;

namespace Core.Helpers
{
    public class Pagination<outDto> where outDto : OutDto
    {
        public IReadOnlyList<outDto> List { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }

        public Pagination(IReadOnlyList<outDto> list, int pageNumber, int pageSize, int count)
        {
            List = list;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Count = count;
        }
    }
}
