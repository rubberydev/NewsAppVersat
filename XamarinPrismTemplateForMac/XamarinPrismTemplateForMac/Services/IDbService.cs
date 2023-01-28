using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinPrismTemplateForMac.DbModels;

namespace XamarinPrismTemplateForMac.Services
{
	public interface IDbService
	{
        Task<List<RSSNewsDbModel>> GetNewsAsync();
        Task<int> InsertNewsAsync(RSSNewsDbModel item);
        Task<int> UpdateNewsAsync(RSSNewsDbModel item);
        Task<int> DeleteNewsAsync(RSSNewsDbModel item);
    }
}

