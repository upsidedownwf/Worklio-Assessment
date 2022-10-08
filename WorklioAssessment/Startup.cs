using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using WorklioAssessment.Helpers;
using WorklioAssessment.Pages;
using WorklioAssessment.Repositories;
using WorklioAssessment.Services;

namespace WorklioAssessment
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
            services.AddRazorPages();
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient("internalApi", (serviceProvider, httpClient) =>
            {
                var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                var request = httpContextAccessor.HttpContext.Request;
                var baseAddress = string.Concat(request.Scheme, Uri.SchemeDelimiter, request.Host.ToUriComponent(), request.PathBase);
                httpClient.BaseAddress = new Uri(baseAddress);
            });

            services.AddHttpClient<ICountryRepository, CountryRepository>(httpClient =>
            {
                httpClient.BaseAddress = new Uri(Configuration["CountriesApi"]);
            });

            var emailSettings = Configuration.GetSection("EmailSettings").Get<EmailSettings>();
            services.AddSingleton(emailSettings);
            services.AddSingleton<IEmailer, Emailer>();
            services.AddSingleton<IMyLogger, MyLogger>();

            services.AddScoped<ICountryService, CountryService>();

        }
        public RequestLocalizationOptions ConfigureLocalizationOptions()
        {
            var cultures = Configuration.GetSection("Cultures").GetChildren().ToDictionary(x => x.Key, x => x.Value);
            var supportedCultures = cultures.Values.ToArray();
            var localizationOptions = new RequestLocalizationOptions()
                .SetDefaultCulture(supportedCultures[0])
                .AddSupportedCultures(supportedCultures)
                .AddSupportedUICultures(supportedCultures);
            return localizationOptions;
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRequestLocalization(ConfigureLocalizationOptions());
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}