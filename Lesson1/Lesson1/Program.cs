using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Lesson1
{
    class Program
    {
        private static HttpClient client = new HttpClient();

        static async Task Main()
        {
            var file = Path.Combine(Environment.CurrentDirectory, "result.txt");
            for (var idPost = 4; idPost < 14; idPost++)
            {
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"https://jsonplaceholder.typicode.com/posts/{idPost}");
                HttpResponseMessage response = await client.SendAsync(httpRequest);
                HttpContent responseContent = response.Content;
                var json = await responseContent.ReadAsStringAsync();
                var post = JsonSerializer.Deserialize<PostInfo>(json);
                await File.AppendAllTextAsync(file, $"{post.userId}\n{post.id}\n{post.title}\n{post.body}\n\n");
            }
            Console.ReadKey();
        }
    }
}
