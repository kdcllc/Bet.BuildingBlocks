using System.Collections.Generic;

using Bet.BuildingBlocks.Domain.Abstractions.Specifications.Interfaces;
using Bet.BuildingBlocks.Domain.Abstractions.Specifications.Query;
using Bet.BuildingBlocks.UnitTest.Models;

namespace Bet.BuildingBlocks.UnitTest
{
    public class IncludeQueryBuilder
    {
        public IncludeQuery<Person, List<Person>> WithCollectionAsPreviousProperty()
        {
            var pathMap = new Dictionary<IIncludeQuery, string>();
            var query = new IncludeQuery<Person, List<Person>>(pathMap);
            pathMap[query] = nameof(Person.Friends);

            return query;
        }

        public IncludeQuery<Book, Person> WithObjectAsPreviousProperty()
        {
            var pathMap = new Dictionary<IIncludeQuery, string>();
            var query = new IncludeQuery<Book, Person>(pathMap);
            pathMap[query] = nameof(Book.Author);

            return query;
        }
    }
}
