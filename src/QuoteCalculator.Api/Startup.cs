using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuoteCalculator.App.Loans.Commands;
using QuoteCalculator.App.Loans.Queries;
using QuoteCalculator.App.Mapping;
using QuoteCalculator.App.Quotes.Commands;
using QuoteCalculator.App.Quotes.Queries;
using QuoteCalculator.Data;
using QuoteCalculator.Data.Repositories;
using QuoteCalculator.Domain;
using System.Reflection;

namespace QuoteCalculator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<QuoteCalculatorContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("QuoteCalculator"));
            });

            services.AddScoped<IRepository<Loan>, LoanRepository>();
            services.AddScoped<IUnitOfWork, QuoteCalculatorUnitOfWork>();

            services.AddScoped<IQuoteCommand, QuoteCommand>();
            services.AddScoped<IQuoteQuery, QuoteQuery>();
            services.AddScoped<ILoanCommand, LoanCommand>();
            services.AddScoped<ILoanQuery, LoanQuery>();

            services.AddAutoMapper(typeof(MappingProfile).GetTypeInfo().Assembly);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuoteCalculator.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuoteCalculator.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
