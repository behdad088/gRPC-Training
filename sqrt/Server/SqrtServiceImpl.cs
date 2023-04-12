using Grpc.Core;
using Max;
using static Max.SqrtService;

namespace sqrt.server
{
    public class SqrtServiceImpl : SqrtServiceBase
    {
        public override async Task<SqrtResponse> sqrt(SqrtRequest request, ServerCallContext context)
        {
            int number = request.Number;

            if (number >= 0)
                return await Task.FromResult(new SqrtResponse() { SquareRoot = Math.Sqrt(number) });
            else
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Value cannot be below 1"));
        }
    }
}
