using AutoMapper;
using AutoMapperDemo.Models;

namespace AutoMapperDemo.Mapping
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {            
            CreateMap<Customer, CustomerModel>()
                .ForMember(dest =>
                   dest.FullName,
                    opt => opt.MapFrom(src => src.FirstName + " " + src.MiddleName + " " + src.LastName));
        }
    }
}
