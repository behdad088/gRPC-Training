using Average;
using Grpc.Core;

namespace Client
{
    internal class Program
    {
        const string target = "localhost:50055";

        static async Task Main(string[] args)
        {
            var channel = new Channel(target, ChannelCredentials.Insecure);

            await channel.ConnectAsync().ContinueWith((t) =>
            {
                if (t.Status == TaskStatus.RanToCompletion)
                {
                    Console.WriteLine("The client connected successfully.");
                }
            });

            var client = new AverageService.AverageServiceClient(channel);
            channel.ShutdownAsync().Wait();
        }
    }
}
