using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace TextCommandFramework.Services
{
    public class PictureService
    {
        private readonly HttpClient _http;

        public PictureService(HttpClient http)
            => _http = http;

        public async Task<Stream> GetCatPictureAsync()
        {
            var resp = await _http.GetAsync("https://cataas.com/cat");
            return await resp.Content.ReadAsStreamAsync();
        }
        public async Task<Stream> GetCatSayPictureAsync(string message)
        {
            var resp = await _http.GetAsync("https://cataas.com/cat/says/" + message);
            return await resp.Content.ReadAsStreamAsync();
        }
        public async Task<Stream> GetCatTagPictureAsync(string message)
        {
            var resp = await _http.GetAsync("https://cataas.com/cat/" + message);
            return await resp.Content.ReadAsStreamAsync();
        }
        public async Task<Stream> GetCatTagSayPictureAsync(string message)
        {
            var resp = await _http.GetAsync("https://cataas.com/cat/" + message.Split("/")[0] + "/says/" + message.Split("/")[1]);
            return await resp.Content.ReadAsStreamAsync();
        }
        public async Task<Stream> GetCatGifAsync()
        {
            var resp = await _http.GetAsync("https://cataas.com/cat/gif");
            return await resp.Content.ReadAsStreamAsync();
        }

    }
}
