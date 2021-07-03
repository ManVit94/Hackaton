using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;

using Hackaton.Business.Interfaces;
using Hackaton.Business.Services;
using Hackaton.DataAccess.Entities;
using Hackaton.DataAccess.Interfaces;
using Hackaton.DataAccess.Repositories;
using Hackaton.DataSeed;

namespace Hackaton.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterMongoDb(services);

            RegisterDataSeeding(services);

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        void RegisterMongoDb(IServiceCollection services)
        {
            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = _configuration["Mongo:ConnectionString"];

                return new MongoClient(connectionString);
            });

            services.AddScoped(sp =>
            {
                var mongoClient = sp.GetRequiredService<IMongoClient>();

                return mongoClient.GetDatabase(_configuration["Mongo:Database"]);
            });

            services.AddScoped(sp =>
            {
                var db = sp.GetRequiredService<IMongoDatabase>();

                return db.GetCollection<UserEntity>("Users");
            });
        }

        void RegisterDataSeeding(IServiceCollection services)
        {
            services.AddSingleton<IDataSeedingService>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                var db = client.GetDatabase(_configuration["Mongo:Database"]);
                var collection = db.GetCollection<UserEntity>("Users");

                var repo = new UserRepository(collection);
                return new DataSeedingService(repo, _configuration);
            });
        }
    }
}
