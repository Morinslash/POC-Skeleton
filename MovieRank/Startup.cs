using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MovieRank.Libs;
using MovieRank.Libs.Mappers;
using MovieRank.Libs.Repositories;
using MovieRank.Services;

namespace MovieRank
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddAWSService<IAmazonDynamoDB>();
            services.AddDefaultAWSOptions(
                new AWSOptions
                {
                    Region = RegionEndpoint.GetBySystemName("eu-west-3")
                });

            services.AddSingleton<IMovieRankService, MovieRankService>();
            services.AddSingleton<IMovieRankRepository, MovieRankRepository>();
            services.AddSingleton<IMapper, Mapper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
    }
}