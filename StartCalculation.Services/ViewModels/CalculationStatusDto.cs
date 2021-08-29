using System;

namespace StartCalculation.Services.ViewModels
{
    public class CalculationStatusDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Expression { get; set; }

        public double? Result { get; set; }

        public int ProcessEstimate { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Progess
        {
            get
            {
                var now = DateTime.Now;
                if ((now - CreatedOn).Ticks > new TimeSpan(0, 0, ProcessEstimate).Ticks)
                {
                    return 100;
                }
                else
                {
                    return (int)(100.0 * (now - CreatedOn).Ticks / new TimeSpan(0, 0, ProcessEstimate).Ticks);
                }
            }
        }
    }
}
