using Grpc.Core;
using Max;

namespace Client
{
    internal class Program
    {
        const string target = "localhost:50057";
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

            var client = new SqrtService.SqrtServiceClient(channel);

            var request = new SqrtRequest
            {
                Number = -16
            };

            try
            {
                var response = client.sqrt(request);
                Console.WriteLine(response.SquareRoot);
            }
            catch (RpcException e)
            {
                Console.WriteLine("Error Detail : " + e.Status.Detail);
            }

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
