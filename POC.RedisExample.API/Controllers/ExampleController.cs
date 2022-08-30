using Microsoft.AspNetCore.Mvc;
using POC.DataAccess;
using POC.DataAccess.Models;
using POC.DataAccess.Redis;
using POC.RedisExample.API.Models;
using System.Text.Json;

namespace POC.RedisExample.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : Controller
    {
        private readonly IRedisService _redisService;
        private readonly ExampleDbContext _context;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExampleController(IRedisService redisService, ExampleDbContext context, IServiceScopeFactory serviceScopeFactory)
        {
            _redisService = redisService;
            _context = context;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExampleTableModelBatch model)
        {
            foreach(var item in model.ExampleTables)
            {
                AddExample(item);

            }
           
            return Ok(new
            {
                Success = true
            });
        }

        private async Task AddExample(ExampleTableModel item)
        {
            var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ExampleDbContext>();
            var example = new ExampleTable
            {
                Name = item.Name,
                BusinessId = item.BusinessId,
                Amount1 = item.Amount1,
                Amount2 = item.Amount2,
                Amount3 = item.Amount3,
            };
            await context.ExampleTables.AddAsync(example);
            await context.SaveChangesAsync();

            await _redisService.PublishAsync("example", JsonSerializer.Serialize(example));
        }
    }
}
