﻿using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using AutoMapper;

namespace AlkemyWallet.Core.Mapper
{
    public class EntityMapper:Profile
    {
        public EntityMapper()
        {
            CreateMap<RoleEntity, RolesDTO>().ReverseMap();
            CreateMap<FixedTermDepositEntity, FixedTermDepositDTO>().ReverseMap();
        }
    }
}
