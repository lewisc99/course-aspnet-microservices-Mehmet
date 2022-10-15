using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ordering.application.Contracts.Infrastructure;
using ordering.application.Contracts.Persistence;
using ordering.application.Models;
using ordering.infrastructure.Mail;
using ordering.infrastructure.Persistence;
using ordering.infrastructure.Repositories;
using Ordering.Application.Contracts.Persistence;

namespace ordering.infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration )
        {


            services.AddDbContext<OrderContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));



            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();


            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();


            return services;

        }
    }
}
