using AutoMapper;
using Net_6_使用_SqlSugar.Dtos;
using Net_6_使用_SqlSugar.Entities;
using Net_6_使用_SqlSugar.ViewModels;

namespace Net_6_使用_SqlSugar.AutoMappers;

/// <summary>
/// 自动映射
/// </summary>
public class _MapConfig : Profile
{
    public _MapConfig()
    {
        CreateMap<AddressDto, Address>();
        CreateMap<Address, AddressView>();
    }
}
