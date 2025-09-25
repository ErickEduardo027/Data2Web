using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Data2Web.Logic.Utils
{
    public class ImageDownloader
    {
        private readonly HttpClient _http;

        public ImageDownloader(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task DownloadImageAsync(string url, string savePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    Console.WriteLine("⚠️ La URL está vacía, no se intentará descargar.");
                    return;
                }

                if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    Console.WriteLine($"⚠️ URL inválida: {url}");
                    return;
                }

                if (File.Exists(savePath))
                    return; // ya está descargada

                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

                var bytes = await _http.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(savePath, bytes);

                Console.WriteLine($"✅ Imagen descargada: {savePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Error al descargar {url}: {ex.Message}");
            }
        }
    }
}
