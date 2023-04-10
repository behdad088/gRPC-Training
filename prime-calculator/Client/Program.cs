using Grpc.Core;
using Prime;

namespace Client
{
    internal class Program
    {
        const string target = "localhost:50054";

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

                var client = new PrimeNumberDecompositionService.PrimeNumberDecompositionServiceClient(channel);


                var primeNumberDecompositionRequest = new PrimeNumberDecompositionRequest()
                {
                    Num = 120
                };

                var primeNumberDecompositionResponse = client.PrimeNumberDecomposition(primeNumberDecompositionRequest);

                while (await primeNumberDecompositionResponse.ResponseStream.MoveNext())
                {
                    Console.WriteLine(primeNumberDecompositionResponse.ResponseStream.Current.Result);
                    await Task.Delay(200);
                }

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
