using Application.Search.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace SearchFight.Controllers
{
    public class SearchController : BaseController
    {
        [HttpPost("search-engine")]
        public async Task<ActionResult<SearchEngineResponse>> Search(SearchEngineQuery query)
        {

            var result = await Mediator.Send(query);

            return result;
        }

    }
}
