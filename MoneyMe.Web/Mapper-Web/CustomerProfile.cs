using AutoMapper;
using MoneyMe.Application.Dto;
using MoneyMe.Domain.Entities;
using MoneyMe.Web.ViewModel;

namespace MoneyMe.Web.Mapper_Web
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerEntity, CustomerViewModel>();
        }
    }
}
