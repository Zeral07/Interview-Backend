using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moduit.Interview.Dto;
using Moduit.Interview.Helper;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Moduit.Interview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IConfiguration _config;
        private const string url1 = @"backend/question/one";
        private const string url2 = @"backend/question/two";
        private const string url3 = @"backend/question/three";

        public AnswersController(IConfiguration config)
        {
            _config = config;
        }

        [Route("/Answer1")]
        [HttpGet]
        public async Task<IActionResult> Answer1Async()
        {
            var data = await HttpHelper.Get<Output1Dto>(_config.GetValue<string>("apiBasicUri"), url1);
            return Ok(data);
        }

        [Route("/Answer2")]
        [HttpGet]
        public async Task<IActionResult> Answer2Async()
        {
            var data = await HttpHelper.Get<List<Output1Dto>>(_config.GetValue<string>("apiBasicUri"), url2);
            data = data.Where(w =>
                    w.description.ToLower().Contains("ergonomics") || 
                    w.title.ToLower().Contains("ergonomics") ||
                    w.tags != null && w.tags.Any(a=> a.ToLower().Contains("sports")))
                .OrderByDescending(o => o.id).TakeLast(3).ToList();
            return Ok(data);
        }

        [Route("/Answer3")]
        [HttpGet]
        public async Task<IActionResult> Answer3Async()
        {
            var data = await HttpHelper.Get<List<Output2Dto>>(_config.GetValue<string>("apiBasicUri"), url3);
            var output = data.Where(w=> w.items != null).SelectMany(s => s.items.Select(o => new 
            {
                s.id,
                s.category,
                o.title,
                o.description,
                o.footer,
                s.createdAt
            }));
            return Ok(output);
        }
    }
}
