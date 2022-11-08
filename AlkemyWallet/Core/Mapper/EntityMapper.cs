using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AutoMapper;

namespace AlkemyWallet.Core.Mapper
{
    public class EntityMapper:Profile
    {
        public EntityMapper()
        {
            CreateMap<RoleEntity, RolesDTO>().ForMember(r => r.IdRol, opr => opr.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<RolesDTO, RoleEntity>();
            CreateMap<FixedTermDepositEntity, FixedTermDepositDTO>().ReverseMap();
            CreateMap<AccountsEntity, AccountDto>();
            CreateMap<UserEntity, UserDTO>();
        }
    }
}
