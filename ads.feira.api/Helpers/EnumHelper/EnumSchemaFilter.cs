using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var enumType = context.Type;
            var enumNames = Enum.GetNames(enumType);
            var enumValues = Enum.GetValues(enumType);

            schema.Enum.Clear();
            schema.Description += " ( ";
            foreach (var enumValue in enumValues)
            {
                var name = enumValue.ToString();
                var field = enumType.GetField(name);
                var displayAttribute = field?.GetCustomAttribute<DisplayAttribute>();

                var displayName = displayAttribute != null ? displayAttribute.Name : name;
                var enumMember = new OpenApiString($"{displayName}");

                schema.Enum.Add(enumMember);
                schema.Description += $"{displayName}, ";
            }
            schema.Description = schema.Description.TrimEnd(' ', ',') + " )";
        }
    }
}