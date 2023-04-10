using Grpc.Core;
using Prime;
using static Prime.PrimeNumberDecompositionService;

namespace grpc.server
{
    internal class PrimeNumberDecompositionServiceImp : PrimeNumberDecompositionServiceBase
    {
        public override async Task PrimeNumberDecomposition(PrimeNumberDecompositionRequest request, IServerStreamWriter<PrimeNumberDecompositionResponse> responseStream, ServerCallContext context)
        {
            int devision = 2;
            var number = request.Num;

            while (number > 1)
            {
                if (number % devision == 0)
                {
                    await responseStream.WriteAsync(new PrimeNumberDecompositionResponse() { Result = devision });
                    number /= devision;
                }
                else
                    devision++;
            }
        }
    }
}
