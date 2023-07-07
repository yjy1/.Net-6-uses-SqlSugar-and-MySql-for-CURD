using AutoMapper;
using Net_6_使用_SqlSugar.Entities;
using Net_6_使用_SqlSugar.ViewModels;
using SqlSugar;

namespace Net_6_使用_SqlSugar.Repositories;

/// <summary>
/// 地址仓储
/// </summary>
public class AddressRepository : BaseRepository<Address, AddressView>
{
    public AddressRepository(IMapper mapper, SqlSugarScope sqlSugar) : base(sqlSugar)
    {

    }
}
