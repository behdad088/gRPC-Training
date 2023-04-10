using Calculator;
using Grpc.Core;

namespace Client
{
    internal class Program
    {
        const string target = "localhost:50053";

        static async Task Main(string[] args)
        {
            try
            {
                var channel = new Channel(target, ChannelCredentials.Insecure);

                await channel.ConnectAsync().ContinueWith((t) =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        Console.WriteLine("The client connected successfully.");
                    }
                });

                var client = new CalculatorService.CalculatorServiceClient(channel);

                var sumRequest = new SumRequest()
                {
                    Num1 = 3,
                    Num2 = 12
                };

                var sumResult = client.Sum(sumRequest);
                Console.WriteLine(sumResult.Result);
                channel.ShutdownAsync().Wait();
            }
            catch (IOException e)
            {

                throw;
            }


            Console.ReadKey();
        }
    }
}
