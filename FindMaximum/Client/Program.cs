using Grpc.Core;
using Max;

namespace Client
{
    internal class Program
    {
        const string target = "localhost:50056";
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

            var client = new FindMaxService.FindMaxServiceClient(channel);
            var stream = client.FindMaximum();
            var responseReaderTask = Task.Run(async () =>
            {
                while (await stream.ResponseStream.MoveNext())
                {
                    Console.WriteLine("Received :" + stream.ResponseStream.Current.Max);
                }
            });

            var findMaxRequsets = new List<FindMaxRequest>
            {
                new FindMaxRequest { Number = 1 },
                new FindMaxRequest { Number = 5 },
                new FindMaxRequest { Number = 3 },
                new FindMaxRequest { Number = 6 },
                new FindMaxRequest { Number = 2 },
                new FindMaxRequest { Number = 20 },
            };

            foreach (var findMaxRequset in findMaxRequsets)
            {
                await stream.RequestStream.WriteAsync(findMaxRequset);
            }

            await stream.RequestStream.CompleteAsync();
            await responseReaderTask;


            Console.ReadKey();

        }
    }
}
