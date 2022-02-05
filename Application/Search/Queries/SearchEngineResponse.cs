using Application.Models;
using System.Collections.Generic;

namespace Application.Search.Queries
{
    public class SearchEngineResponse
    {
        public List<EngineResult> SearchResults { get; set; }
        public IEnumerable<Winner> Winners { get; set; }
        public string TotalWinner { get; set; }
    }
}
