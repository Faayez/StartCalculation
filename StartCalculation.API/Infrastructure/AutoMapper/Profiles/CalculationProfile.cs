using AutoMapper;
using StartCalculation.Core.Utility;
using StartCalculation.Domain.Domain.Entities;
using StartCalculation.Services.ViewModels;

namespace StartCalculation.Api.Infrastructure.AutoMapper
{
    public class CalculationProfile : Profile
    {
        public CalculationProfile()
        {
            CreateMap<CalculationInsertDto, Calculation>()
                .ForMember(d => d.Status, s => s.MapFrom(p => CalculationStatus.Running));

            CreateMap<Calculation, CalculationStatusDto>()
                .ForMember(d => d.Expression, s => s.MapFrom(p => $"{p.Input1} {p.Operator.GetDisplayValue()} {p.Input2}"));
        }
    }
}
