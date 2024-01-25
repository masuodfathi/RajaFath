using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Interfaces.Repositories;
using RajaFath.Models;
using SQLitePCL;

namespace RajaFath.Repository
{
    public class SiteSettingRepository : ISiteSettingRepository
    {
        private readonly ApplicationDbContext _context;

        public SiteSettingRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteSiteSettingAsync()
        {
            if(_context.siteSettings.Any())
            {
                _context.siteSettings.RemoveRange(_context.siteSettings);
                return await SaveAsync();
            }
            return false;
        }

        public async Task<SiteSetting> GetSiteSettingsAsync()
        {
            return await _context.siteSettings.SingleOrDefaultAsync();
        }

        public async Task<bool> HasSiteSetting()
        {
            return await _context.siteSettings.AnyAsync();
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var save = await _context.SaveChangesAsync();
                if (save > 0)
                {
                    return true;
                }
                throw new DbUpdateException("Error updating the database.");

            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error updating the database.", ex);
            }
        }

        public async Task<bool> AddSiteSettingAsync(SiteSetting setting)
        {
            _context.siteSettings.Add(setting);
            return await SaveAsync();
        }

        public async Task<bool> UpdateSiteSettingAsync(SiteSetting setting)
        {
            _context.siteSettings.Update(setting);
            return await SaveAsync();
        }
    }
}
