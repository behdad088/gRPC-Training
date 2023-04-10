using Calculator;
using Grpc.Core;

namespace calculator.server
{
    internal class Program
    {
        const int port = 50053;
        static void Main(string[] args)
        {
            Server server = null;

            try
            {
                server = new Server()
                {
                    Services = { CalculatorService.BindService(new CalculatorServiceImp()) },
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
