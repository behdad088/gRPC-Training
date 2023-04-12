using Greeting;
using Grpc.Core;

namespace greeting_with_deadline.server
{
    internal class Program
    {
        const int port = 50057;
        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server()
                {
                    Services = { GreetingService.BindService(new GreetingServiceImpl()) },
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
