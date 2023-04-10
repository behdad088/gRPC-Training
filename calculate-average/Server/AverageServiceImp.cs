using Average;
using Grpc.Core;
using static Average.AverageService;

namespace grpc.server
{
    internal class AverageServiceImp : AverageServiceBase
    {
        public override async Task<AverageResponse> ComputeAverage(IAsyncStreamReader<AverageRequest> requestStream, ServerCallContext context)
        {
            var numbers = new List<int>();

            while (await requestStream.MoveNext())
            {
                numbers.Add(requestStream.Current.Num);
            }

            var average = numbers.Average();

            return new AverageResponse
            {
                Result = average
            };
        }
    }
}
