using System;
using System.Text.Json.Serialization;

namespace StartCalculation.Services.ViewModels
{
    public class CalculationStatusDto
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Expression { get; set; }

        public double? Result { get; set; }

        [JsonIgnore]
        public int ProcessEstimate { get; private set; }

        [JsonIgnore]
        public DateTime CreatedOn { get; private set; }

        public int Progess
        {
            get
            {
                if ((DateTime.Now - CreatedOn).Ticks > new TimeSpan(0, 0, ProcessEstimate).Ticks)
                {
                    return 100;
                }
                else
                {
                    return (int)(100.0 * (DateTime.Now - CreatedOn).Ticks / new TimeSpan(0, 0, ProcessEstimate).Ticks);
                }
            }
        }
    }
}
