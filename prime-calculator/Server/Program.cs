using Grpc.Core;
using Prime;

namespace grpc.server
{
    internal class Program
    {
        const int port = 50054;
        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server()
                {
                    Services = { PrimeNumberDecompositionService.BindService(new PrimeNumberDecompositionServiceImp()) },
                    Ports = { new ServerPort("localhost", port, ServerCredentials.Insecure) }
                };

                server.Start();
                Console.WriteLine($"The server is listening to port: {port}");
                Console.ReadKey();
            }
            catch (IOException e)
            {
                Console.WriteLine("Server faild to start");
                throw;
            }
            finally
            {
                server?.ShutdownTask.Wait();
            }
        }
    }
}
