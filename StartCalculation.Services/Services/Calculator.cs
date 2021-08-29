using StartCalculation.Domain.Domain.Entities;

namespace StartCalculation.Services
{
    public static class Calculator
    {
        public static double? Calculate(double input1, double input2, OperationType operation)
        {
            try
            {
                switch (operation)
                {
                    case OperationType.Plus:
                        return input1 + input2;

                    case OperationType.Minus:
                        return input1 - input2;

                    case OperationType.Multiply:
                        return input1 * input2;

                    case OperationType.Divide:
                        return input1 / input2;

                    default:
                        return default;
                }
            }
            catch
            {
                return default;
            }
        }
    }
}