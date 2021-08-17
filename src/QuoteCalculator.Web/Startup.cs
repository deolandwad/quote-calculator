using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuoteCalculator.App.Loans.Commands;
using QuoteCalculator.App.Loans.Queries;
using QuoteCalculator.App.Mapping;
using QuoteCalculator.App.Quotes.Commands;
using QuoteCalculator.App.Quotes.Queries;
using QuoteCalculator.Data;
using QuoteCalculator.Data.Repositories;
using QuoteCalculator.Domain;
using QuoteCalculator.Web.Data;
using System.Reflection;

namespace QuoteCalculator.Web
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

            services.AddTransient<QuoteCalculatorSeeder>();

            services.AddAutoMapper(typeof(MappingProfile).GetTypeInfo().Assembly);

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
