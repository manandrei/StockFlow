using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using StockFlow.Application;
using StockFlow.Web.Client.Pages;
using StockFlow.Web.Components;
using StockFlow.Infrastructure;
using Microsoft.OpenApi.Models;
using StockFlow.Web.Utilities;
using Swashbuckle.AspNetCore.Versioning;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration.GetConnectionString("Default"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddVersionedApiExplorer(o =>
{
    o.GroupNameFormat = "'v'VVV";
    o.SubstituteApiVersionInUrl = true;
});
builder.Services.AddSwaggerGen(options =>
{
    // options.SwaggerDoc("v1", new OpenApiInfo
    // {
    //     Version = "v1",
    //     Title = "StockFlow API",
    //     Description = "An ASP.NET Core Web API for managing stock items"
    // });
    //
    // options.SwaggerDoc("v2", new OpenApiInfo
    // {
    //     Version = "v2",
    //     Title = "StockFlow New API",
    //     Description = "An ASP.NET Core Web API for managing stock items"
    // });
    
    foreach (string version in ApiVersions.GetVersions())
    {
        options.SwaggerDoc(version, new OpenApiInfo
        {
            Version = version,
            Title = "StockFlow API " + version.ToUpperInvariant(),
            Description = "An ASP.NET Core Web API for managing stock items"
        });
    }

    options.EnableAnnotations();
    options.CustomSchemaIds(x => x.FullName);
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);
});

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseWebAssemblyDebugging();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (ApiVersionDescription description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", "StockFlow API " + description.GroupName.ToUpperInvariant());
        }
    });
}
else
{
    app.UseExceptionHandler("/Error", true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.MapControllers();

app.Run();