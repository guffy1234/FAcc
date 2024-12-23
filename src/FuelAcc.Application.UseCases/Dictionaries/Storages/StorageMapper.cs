﻿using AutoMapper;
using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Application.UseCases.Dictionaries.Storages
{
    public class StorageMapper : Profile
    {
        public StorageMapper()
        {
            CreateMap<Storage, StorageDto>().ReverseMap();
        }
    }
}