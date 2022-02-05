using Application.Common;
using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Models;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Search.Queries
{
    public class SearchEngineQuery : IRequest<SearchEngineResponse>
    {
        public string Query { get; set; }

    }

    public class SearchEngineQueryHandler : IRequestHandler<SearchEngineQuery, SearchEngineResponse>
    {
        private readonly IEnumerable<ISearchClientAsync> _searchClientAsync;

        public SearchEngineQueryHandler(IEnumerable<ISearchClientAsync> searchClientAsync)
        {
            _searchClientAsync = searchClientAsync;
        }

        public async Task<SearchEngineResponse> Handle(SearchEngineQuery request, CancellationToken cancellationToken)
        {
            if(request.Query.Trim().Equals(string.Empty))
                throw new ApiException(SearchFightResource.msg_exception_empty);


            SearchEngineResponse response = new SearchEngineResponse();

            var wordsToFind = request.Query.Split(' ');

            var allResults = await GetFirstReport(wordsToFind);

            var winnersByEngine = GetWinnersByEngine(allResults);

            var totalWinner = GetTotalWinner(allResults);

            response.SearchResults = allResults;
            response.Winners = winnersByEngine;
            response.TotalWinner = totalWinner;

            return response;
        }

        private async Task<List<EngineResult>> GetFirstReport(string[] wordsToFind)
        {
            var results = new List<EngineResult>();

            foreach (var item in wordsToFind)
            {

                foreach (var searchEngine in _searchClientAsync)
                {
                    results.Add(new EngineResult
                    {
                        ClientEngine = searchEngine.EngineName,
                        Query = item,
                        TotalResults = await searchEngine.GetResultsByEngineAsync(item)
                    });
                }

            }

            return results;
        }

        private static IEnumerable<Winner> GetWinnersByEngine(List<EngineResult> results)
        {
            var winnersByEngine = results.OrderBy(p => p.ClientEngine).GroupBy(p => p.ClientEngine, p => p,
                (key, result) => new Winner
                {
                    EngineName = key,
                    WinnerQuery = result.GetMaxValue(p => p.TotalResults).Query
                });

            return winnersByEngine;

        }

        private static string GetTotalWinner(List<EngineResult> results)
        {
            var totalWinner = results
                .OrderBy(result => result.ClientEngine)
                .GroupBy(result => result.Query, result => result,
                    (key, result) => new { Query = key, Total = result.Sum(r => r.TotalResults) })
                .GetMaxValue(r => r.Total).Query;

            return totalWinner;
        }

    }
}
