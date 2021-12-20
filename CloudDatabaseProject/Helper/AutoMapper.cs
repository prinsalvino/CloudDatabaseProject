using AutoMapper;
using Domain;
using CloudDatabaseProject.DTO;
using System.Collections.Generic;

namespace CloudDatabaseProject.Helper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<ImageDTO, Image>();
            CreateMap<ProductDTO, Product>();
            CreateMap<OrderItemDTO, OrderItem>();
            CreateMap<OrderDTO, Order>();
            CreateMap<UpdateOrderDTO, Order>();
            CreateMap<OrderDescription, OrderDescriptionDTO>();
            CreateMap<Order, OrderReturnDTO>();
            CreateMap<OrderItem, OrderItemReturnDTO>();
            CreateMap<User, UserReturnDTO>();
        }
    }
}
