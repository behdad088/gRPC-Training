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
            var stream = client.ComputeAverage();

            for (int i = 1; i <= 4; i++)
            {
                await stream.RequestStream.WriteAsync(new AverageRequest() { Num = i });
            }

            await stream.RequestStream.CompleteAsync();
            var response = await stream.ResponseAsync;

            Console.WriteLine(response.Result);
            channel.ShutdownAsync().Wait();
        }
    }
}
