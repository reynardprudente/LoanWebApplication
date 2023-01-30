using AutoMapper;
using MoneyMe.API.Model;
using MoneyMe.Application.Dto;
using MoneyMe.Domain.Entities;
using System;

namespace MoneyMe.API.Mapper.Customer
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerModel, CustomerDTO>();
            CreateMap<CustomerDTO, CustomerEntity>()
                .ForMember(d => d.FirstName, o => o.MapFrom(frm => frm.FirstName.Trim()))
                .ForMember(d => d.LastName, o => o.MapFrom(frm => frm.LastName.Trim()))
                .ForMember(d => d.DateOfBirth, o => o.MapFrom(frm => Convert.ToDateTime(frm.DateOfBirth.Trim())))
                .ForMember(d => d.EmailAddress, o => o.MapFrom(frm => frm.Email.Trim()));
        }
    }
}
