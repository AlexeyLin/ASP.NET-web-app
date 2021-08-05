using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lesson1
{
    class Program
    {
        private readonly HttpClient client = new HttpClient();

        static async Task Main()
        {
            var file = Environment.CurrentDirectory;
            for (var idPost = 4; idPost < 14; idPost++)
            {
                var post = await GetPostInfo(idPost);
                await File.ReadAllTextAsync(File, post.ToString() + Environment.NewLine);
            }

            Console.ReadKey();
        }

        public async Task<PostInfo> GetPostInfo(int id)
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://jsonplaceholder.typicode.com/posts/{id}");
            try
            {
                HttpResponseMessage response = await client.SendAsync(httpRequest);

                using var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<PostInfo>(responseStream);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
