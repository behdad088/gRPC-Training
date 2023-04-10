using Calculator;
using Grpc.Core;
using static Calculator.CalculatorService;

namespace calculator.server
{
    internal class CalculatorServiceImp : CalculatorServiceBase
    {
        public override Task<SumResponse> Sum(SumRequest request, ServerCallContext context)
        {
            var result = request.Num1 + request.Num2;

            return Task.FromResult(new SumResponse
            {
                Result = result
            });
        }


    }
}
