using RajaFath.Models;

namespace RajaFath.Interfaces.Repositories
{
    public interface ISiteSettingRepository
    {
        Task<SiteSetting> GetSiteSettingsAsync();
        Task<bool> HasSiteSetting();
        Task<bool> AddSiteSettingAsync(SiteSetting setting);
        Task<bool> UpdateSiteSettingAsync(SiteSetting setting);
        Task<bool> DeleteSiteSettingAsync();
        Task<bool> SaveAsync();
    }
}
