using System.ComponentModel.DataAnnotations;

namespace StartCalculation.Domain.Domain.Entities
{
    public enum OperationType : byte
    {
        [Display(Name = "+")]
        Plus = 1,

        [Display(Name = "-")]
        Minus = 2,

        [Display(Name = "*")]
        Multiply = 4,

        [Display(Name = "/")]
        Divide = 8
    }
}
