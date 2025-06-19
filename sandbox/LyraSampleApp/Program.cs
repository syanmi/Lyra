using Lyra;

namespace LyraSampleApp
{
    internal class Program
    {
        static async Task Main(string[] _)
        {
            var app = new LyraApp();

            app.Get("/hello", _ => LyraResult.Text("Hello from Lyra!"));

            await app.RunAsync();
        }
    }
}
