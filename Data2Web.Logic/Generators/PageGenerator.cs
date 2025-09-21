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
            // Crear instancia de Handlebars
            _handlebars = Handlebars.Create();

            // Registrar helper ifEquals (para condiciones en las plantillas)
            _handlebars.RegisterHelper("ifEquals", (writer, context, parameters) =>
            {
                if (parameters.Length == 2 && parameters[0]?.ToString() == parameters[1]?.ToString())
                {
                    writer.WriteSafeString("true");
                }
                else
                {
                    writer.WriteSafeString("false");
                }
            });

            // Otro helper opcional: formato de fecha
            _handlebars.RegisterHelper("formatDate", (writer, context, parameters) =>
            {
                if (parameters.Length == 1 && DateTime.TryParse(parameters[0]?.ToString(), out var date))
                {
                    writer.WriteSafeString(date.ToString("dd/MM/yyyy"));
                }
            });
        }

        public async Task GenerateAsync<T>(string templatePath, T model, string outputPath)
        {
            if (!File.Exists(templatePath))
                throw new FileNotFoundException($"❌ No se encontró la plantilla: {templatePath}");

            // Leer plantilla
            string templateContent = await File.ReadAllTextAsync(templatePath);

            if (string.IsNullOrWhiteSpace(templateContent))
                throw new InvalidOperationException($"❌ La plantilla '{templatePath}' está vacía.");

            // Compilar plantilla
            var template = _handlebars.Compile(templateContent);

            // Renderizar modelo
            string result = template(model);

            // Crear carpeta de salida si no existe
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

            // Guardar archivo HTML
            await File.WriteAllTextAsync(outputPath, result);

            Console.WriteLine($"✅ Página generada: {outputPath}");
        }
    }
}

