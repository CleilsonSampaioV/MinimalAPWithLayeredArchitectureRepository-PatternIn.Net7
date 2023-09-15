using MyTrainingSheet.Api.Extension;
using MyTrainingSheet.Api.Middleware;
using MyTrainingSheet.Business;
using MyTrainingSheet.Domain;
using MyTrainingSheet.Infra;
using Serilog;

Log.Logger = MyTrainingSheet.Api.LoggerFactory.CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    Log.Information("Configuring services");
    ConfigureServices(
        builder.Services,
        builder.Configuration
        );

    Log.Information("Building App");
    var app = builder.Build();

    Log.Information("Configuring App");
    ConfigureApp(
        app, 
        app.Environment, 
        app.Configuration
        );

    Log.Information("Mapping Routes");
    var liftsEndPoint = app.MapGroup("/lifts");

    liftsEndPoint.MapGet("/", GetAllLifts);
    liftsEndPoint.MapGet("/getByLiftName", GetLiftByName);
    liftsEndPoint.MapGet("/{id}", GetLift);
    liftsEndPoint.MapPost("/", CreateLift);
    liftsEndPoint.MapPut("/{id}", UpdateLift);
    liftsEndPoint.MapDelete("/{id}", DeleteLift);
    app.MapGet("/fakeError", FakeError);

    Log.Information("Runnig App");

    app.MigrateDatabase<Program>();

    app.Run();

    static void ConfigureServices(
        IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddBusinnesServices();
        services.AddInfraServices(configuration);
    }

    static void ConfigureApp(
        IApplicationBuilder app,
        IHostEnvironment env,
        IConfiguration configuration
        )
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseRouting();

        app.UseEndpoints(endpoint => endpoint.MapControllerRoute(name: "default", pattern: "{controller}/{action=index}/{id}"));

        app.UseExceptionHandleMiddleware();
    }

    static async Task<IResult> GetAllLifts(ILiftService liftService)
    {
        var result = await liftService.GetAllAsync();
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> GetLiftByName(ILiftService liftService, string name)
    {
        var result = await liftService.GetByLiftName(name);
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> GetLift(int id, ILiftService liftService)
    {
        var result = await liftService.GetAsync(new Lift { Id = id });
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> CreateLift(Lift lift, ILiftService liftService)
    {
        var result = await liftService.CreateAsync(lift);
        return result.Success ? TypedResults.Created($"/lifts/{lift.Id}", result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> UpdateLift(Lift inputLift, ILiftService liftService)
    {
        var result = await liftService.UpdateAsync(inputLift);
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);
    }

    static async Task<IResult> DeleteLift(int id, ILiftService liftService)
    {
        var result = await liftService.DeleteAsync(id);
        return result.Success ? TypedResults.Ok(result) : TypedResults.BadRequest(result);

    }

    static Task<IResult> FakeError()
    {
        throw new InvalidOperationException("Fake error");
    }
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}