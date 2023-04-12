using Greeting;
using Grpc.Core;

namespace Client
{
    internal class Program
    {
        const string target = "localhost:50058";
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

            var client = new GreetingService.GreetingServiceClient(channel);

            try
            {
                var response = client.GreetWithDeadline(new GreetingRequest { Name = "Behdad" }, deadline: DateTime.UtcNow.AddMilliseconds(100));
                Console.WriteLine(response.Result);
            }
            catch (RpcException e)
            {
                Console.WriteLine("Error : " + e.Status.Detail);
            }

            channel.ShutdownAsync().Wait();
            Console.ReadKey();
        }
    }
}
