using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;

namespace API.Configurations.Swagger
{
    public class EnumSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema model, SchemaFilterContext context)
        {
            if (context.Type.IsEnum)
            {
                model.Enum.Clear();

                Enum.GetNames(context.Type)
                    .ToList()
                    .ForEach(name =>
                    {
                        var field = context.Type.GetField(name);
                        var description = string.Empty;

                        if (field != null && Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                        {
                            description = attribute.Description;
                        }

                        model.Enum.Add(new OpenApiString($"{Convert.ToInt64(Enum.Parse(context.Type, name))} - {(string.IsNullOrEmpty(description) ? name : description)}"));
                    });
            }
        }
    }
}
