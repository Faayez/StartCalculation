namespace StartCalculation.Domain.Domain.Entities
{
    public enum CalculationStatus : byte
    {
        Running = 1,

        Failed = 2,

        Completed = 4
    }
}
