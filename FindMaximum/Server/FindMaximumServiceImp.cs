using Grpc.Core;
using Max;
using static Max.FindMaxService;

namespace grpc.server
{
    internal class FindMaximumServiceImp : FindMaxServiceBase
    {
        public override async Task FindMaximum(IAsyncStreamReader<FindMaxRequest> requestStream, IServerStreamWriter<FindMaxResponse> responseStream, ServerCallContext context)
        {
            var numbers = new List<int>();
            while (await requestStream.MoveNext())
            {
                var number = requestStream.Current.Number;

                numbers.Add(number);

                if (number > numbers.Max())
                    await responseStream.WriteAsync(new FindMaxResponse() { Max = number });
            }
        }
    }
}
