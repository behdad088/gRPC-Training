using Greeting;
using Grpc.Core;
using static Greeting.GreetingService;

namespace greeting_with_deadline.server
{
    internal class GreetingServiceImpl : GreetingServiceBase
    {
        public override async Task<GreetingResponse> GreetWithDeadline(GreetingRequest request, ServerCallContext context)
        {
            await Task.Delay(300);

            return new GreetingResponse
            {
                Result = "Hello " + request.Name
            };
        }
    }
}
