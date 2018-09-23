using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace LinkLibrary.Helpers
{
    public static class QueryableExtensions
    {
        // TODO BP: nie przeprowadzasz żadnej walidacji na stringu sort.
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string sort)
        {
            // TODO BP:                                         nameof(source), pomaga przy potencjalnym refaktoringu w przyszłości
            if (source == null) throw new ArgumentNullException("source");
            if (sort == null) return source;

            var lsSort = sort.Split(',');
            string sortingPhrase = "";
            foreach (var item in lsSort)
            {
                // TODO BP: a co jak zaczyna się od plusa?
                if (item.StartsWith("-"))
                {
                    sortingPhrase += item.Remove(0, 1) + " descending,";
                }
                else
                {
                    sortingPhrase += item + ",";
                }
            }

            if (!string.IsNullOrWhiteSpace(sortingPhrase))
            {
                source = source.OrderBy(sortingPhrase.Remove(sortingPhrase.Count() - 1));
            }

            return source;
        }
    }
}
