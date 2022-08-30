using POC.DataAccess;
using POC.DataAccess.Models;
using POC.DataAccess.Redis;
using System.Text.Json;

namespace POC.RedisExample
{
    internal class ExampleHostedService : IHostedService
    {
        private readonly IRedisService _redisService;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        
        public ExampleHostedService(IRedisService redisService, IServiceScopeFactory serviceScopeFactory)
        {
            _redisService = redisService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _redisService.Suscribe("example", async (channel, value) =>
            {
                var context = _serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ExampleDbContext>();
                
                var example = JsonSerializer.Deserialize<ExampleTable>(value);
                if (example != null)
                {
                    try
                    {
                        //Hacer algo de procesamiento
                        var total = example.Amount1 + example.Amount2 + example.Amount3;

                        example.Processed = true;
                        context.ExampleTables.Attach(example);
                        context.Entry(example).Property(x => x.Processed).IsModified = true;
                        await context.SaveChangesAsync();

                        Console.WriteLine("Procesado: " + value.ToString());

                    }
                    catch (Exception ex)
                    {
                        //Hacer algo con el error
                        Console.WriteLine(ex.ToString());

                        example.HasError = true;
                        context.ExampleTables.Update(example);
                        await context.SaveChangesAsync();

                        Console.WriteLine("Error en entidad: " + value.ToString());
                    }
                } else
                {
                    Console.WriteLine("Entidad no encontrada: " + value.ToString());
                }
                

                
            });
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }
    }
}
