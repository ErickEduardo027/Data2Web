using HandlebarsDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data2Web.Logic.Generators
{
    public class PageGenerator
    {
        public async Task GenerateAsync<T>(string templatePath, T model, string outputPath)
        {
            // Leer plantilla
            string templateContent = await File.ReadAllTextAsync(templatePath);

            // Compilar plantilla
            var template = Handlebars.Compile(templateContent);

            //Renderizar modelo
            string result = template(model);

            // guardar archivo HTML
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            await File.WriteAllTextAsync(outputPath, result);
        }
    }
}
