using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReposController : ControllerBase
    {
        private static readonly HttpClient Client = new HttpClient();

        // GET: api/Repos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Repo>>> GetRepos()
        {
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            Client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stringTask = Client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");

            var msg = await stringTask;
            var repositories = await JsonSerializer.DeserializeAsync<List<Repo>>(msg);
            return repositories;
        }
    }
}
