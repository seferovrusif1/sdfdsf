using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Twitter.Business.Repositories.Interfaces;

namespace Twitter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        ITopicRepository _repo { get; }

        public TopicsController(ITopicRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(_repo.GetAll(false));
        }
        [HttpGet]
        public async Task<IActionResult> IsExist()
        {
            return Ok(_repo.IsExistAsync(t => t.Id == 2));
        }

    }
}
