using CapaDatos;
using CapaNegocio;
using Crud.Controllers;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

try
{
    /**********************************************
    * HABILITAR LOGGER - SERILOG
    ***********************************************/
    //serilog
    IConfigurationRoot configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
    Log.Information("La aplicacion esta corriendo");
    builder.Host.UseSerilog();


    //agregamos los servicios para que funcione el serilog por cada capa
    builder.Services.AddScoped<ContactoDAL>();
    builder.Services.AddScoped<ContactoBL>();
    builder.Services.AddScoped<ContactoController>();

    /***********************************************
     * FIN HABILITAR LOGGER - SERILOG
     ***********************************************/

    // Agregar servicios al contenedor.
    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configurar el pipeline de solicitudes HTTP
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthorization();

    /*****************************************************
     * HABILITAR LOGGER SERILOG
     *****************************************************/
    app.UseSerilogRequestLogging();
    /*****************************************************
     * FIN HABILITAR LOGGER SERILOG
     *****************************************************/

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{
    // Log cualquier excepción durante la configuración
    Log.Fatal(ex, "La aplicación falló al iniciar");
}
finally
{
    Log.CloseAndFlush();
}

