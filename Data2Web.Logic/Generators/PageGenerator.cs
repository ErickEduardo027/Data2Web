using HandlebarsDotNet;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Data2Web.Logic.Generators
{
    public class PageGenerator
    {
        private readonly IHandlebars _handlebars;
        private string? _layoutContent;

        public PageGenerator()
        {
            _handlebars = Handlebars.Create();

            // 🔹 Registrar helper ifEquals
            _handlebars.RegisterHelper("ifEquals", (writer, options, context, arguments) =>
            {
                if (arguments.Length == 2 && arguments[0]?.ToString() == arguments[1]?.ToString())
                {
                    options.Template(writer, context);
                }
                else
                {
                    options.Inverse(writer, context);
                }
            });

            _handlebars.RegisterHelper("json", (writer, context, parameters) =>
            {
                var value = parameters[0];
                string json = System.Text.Json.JsonSerializer.Serialize(value);
                writer.WriteSafeString(json);
            });

            // 🔹 Cargar el layout
            string layoutPath = Path.Combine(AppContext.BaseDirectory, "Templates", "_Layout.hbs");
            if (File.Exists(layoutPath))
            {
                _layoutContent = File.ReadAllText(layoutPath);
            }
        }

        public async Task GenerateAsync<T>(string templatePath, T model, string outputPath)
        {
            // Leer contenido del template específico
            string templateContent = await File.ReadAllTextAsync(templatePath);

            // Compilar y renderizar el contenido de la página
            var template = _handlebars.Compile(templateContent);
            string bodyContent = template(model);

            string finalHtml;

            // Si tenemos layout, lo envolvemos
            if (!string.IsNullOrEmpty(_layoutContent))
            {
                var layoutTemplate = _handlebars.Compile(_layoutContent);

                finalHtml = layoutTemplate(new
                {
                    title = Path.GetFileNameWithoutExtension(templatePath),
                    active = Path.GetFileNameWithoutExtension(outputPath)?.ToLower(),
                    body = bodyContent
                });
            }
            else
            {
                // Sin layout → devolvemos solo el contenido
                finalHtml = bodyContent;
            }

            // Guardar archivo HTML
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            await File.WriteAllTextAsync(outputPath, finalHtml);
        }
    }
}
