using AutoMapper;
using DutchTreat.Entity;
using DutchTreat.ViewModel;

namespace DutchTreat
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
            .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
            .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
            .ReverseMap();
        }
    }
}
