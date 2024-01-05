using API.Configurations.Filters;
using API.Configurations.Middlewares;
using API.Configurations.Swagger;
using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Domain.Interfaces.CQS;
using Infra.CrossCutting.IoC;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCommandHandler(typeof(ICommandHandler<,>));
builder.Services.AddQueryHandler(typeof(IQueryHandler<,>));
builder.Services.AddServices();
builder.Services.AddRepositories(builder.Configuration);


builder.Services.AddMvc(options => options.Filters.Add<NotificationsFilter>());
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    options.SchemaFilter<EnumSchemaFilter>();
    var layers = new List<string>() { "Application", "Domain" };
    
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    foreach (var item in layers)
    {
        xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        xmlFilename = xmlFilename.Replace("API", item);
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }

});

builder.Services.AddApiVersioning(opt =>
{
    opt.DefaultApiVersion = new ApiVersion(1, 0);
    opt.AssumeDefaultVersionWhenUnspecified = true;
    opt.ReportApiVersions = true;
    opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                    new HeaderApiVersionReader("x-api-version"),
                                                    new MediaTypeApiVersionReader("x-api-version"));
})
.AddMvc()
.AddApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

    app.UseReDoc(c =>
    {
        c.DocumentTitle = "QuickBite API";
        c.SpecUrl = "/swagger/v1/swagger.json";
    });
}

app.UseExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
