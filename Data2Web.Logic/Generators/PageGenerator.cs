using HandlebarsDotNet;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Data2Web.Logic.Generators
{
    public class PageGenerator
    {
        private readonly IHandlebars _handlebars;

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

            // 🔹 Registrar partial _Layout (lo cargamos desde Templates)
            string layoutPath = Path.Combine(AppContext.BaseDirectory, "Templates", "_Layout.hbs");
            if (File.Exists(layoutPath))
            {
                string layoutContent = File.ReadAllText(layoutPath);
                _handlebars.RegisterTemplate("_Layout", layoutContent);
            }
        }

        public async Task GenerateAsync<T>(string templatePath, T model, string outputPath)
        {
            // Leer plantilla
            string templateContent = await File.ReadAllTextAsync(templatePath);

            // Compilar plantilla
            var template = _handlebars.Compile(templateContent);

            // Renderizar con modelo
            string result = template(model);

            // Guardar archivo HTML
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            await File.WriteAllTextAsync(outputPath, result);
        }
    }
}


