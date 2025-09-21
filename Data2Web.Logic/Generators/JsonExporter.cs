using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data2Web.Logic.Generators
{
    public class JsonExporter
    {
        public async Task ExportAsync<T>(IEnumerable<T> data, string outputPath)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true 
            };

            string json = JsonSerializer.Serialize(data, options);

            Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);
            await File.WriteAllTextAsync(outputPath, json);
        }
    }
}
