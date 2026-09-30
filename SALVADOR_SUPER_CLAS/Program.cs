using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Globalization;

namespace SALVADOR_SUPER_CLAS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AuthorizeFilter());

                options.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    campo => $"El campo {campo} debe ser un número (use punto para los decimales, ej. 150.50).");
                options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                    (valor, campo) => $"El valor '{valor}' no es válido para {campo}.");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    campo => $"El campo {campo} es obligatorio.");
            });

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Cuenta/Login";
                    options.AccessDeniedPath = "/Cuenta/AccesoDenegado";
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                    options.SlidingExpiration = true;
                });

            var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]!;
            builder.Services.AddHttpClient("SalvadorApi", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            var app = builder.Build();

            var cultura = new CultureInfo("es-BO");
            cultura.NumberFormat.NumberDecimalSeparator = ".";
            cultura.NumberFormat.NumberGroupSeparator = ",";
            cultura.NumberFormat.CurrencyDecimalSeparator = ".";
            cultura.NumberFormat.CurrencyGroupSeparator = ",";
            cultura.NumberFormat.CurrencySymbol = "Bs.";
            CultureInfo.DefaultThreadCurrentCulture = cultura;
            CultureInfo.DefaultThreadCurrentUICulture = cultura;

            var opcionesCultura = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultura),
                SupportedCultures = new List<CultureInfo> { cultura },
                SupportedUICultures = new List<CultureInfo> { cultura }
            };
            opcionesCultura.RequestCultureProviders.Clear();
            app.UseRequestLocalization(opcionesCultura);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            app.Run();
        }
    }
}
