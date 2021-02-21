using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Questionnaires.DataSeeding;
using Questionnaires.Infrastructure.Repository;

namespace Questionnaires.Api.HostedServices
{
    public class DbSeedingService : IHostedService
    {
        private readonly IServiceProvider _services;

        public DbSeedingService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<QuestionnaireContext>();
            {
                DbSeeder.Seed(context);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}