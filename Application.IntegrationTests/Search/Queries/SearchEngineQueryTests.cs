using Application.Search.Queries;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Search.Queries
{
    using static Testing;

    public class SearchEngineQueryTests
    {
        [Test]
        public async Task ShouldReturnDataResults()
        {

            var query = new SearchEngineQuery()
            {
                Query="Java .Net"
            };

            var result = await SendAsync(query);

            result.SearchResults.Should().NotBeEmpty();

        }

    }
}
