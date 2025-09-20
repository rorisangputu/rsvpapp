using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rsvp.data.Queries
{
    public class EventQuery
    {
        public bool? IsPrivate { get; set; }
        public int? CategoryId { get; set; }
        public string? SearchTerm { get; set; }
        public string? SortBy { get; set; } = null;
        public bool IsDecsending { get; set; } = false;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int Skip => (Page - 1) * PageSize;
    }
}