using AutoMapper;
using CadastroCliente.Model;
using CadastroCliente.Web.Models;

namespace CadastroCliente.Web.Map
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<LoginModel, LoginModelDTO>();
            CreateMap<LoginModelDTO, LoginModel>();
        }
    }

}
