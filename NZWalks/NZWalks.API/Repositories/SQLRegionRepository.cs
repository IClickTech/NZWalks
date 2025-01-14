using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLRegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }       

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {
            return await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> AddAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);

            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(r => r.Id == id);

            if(region == null)
            {
                return null;
            }

            dbContext.Regions.Remove(region);

            await dbContext.SaveChangesAsync();

            return region;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var regionExist = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(regionExist == null)
            {
                return null;
            }

            regionExist.Code = region.Code;
            regionExist.Name = region.Name;
            regionExist.Area = region.Area;
            regionExist.Long = region.Long                ;
            regionExist.Lat = region.Lat;
            regionExist.Population = region.Population;

            await dbContext.SaveChangesAsync();

            return regionExist;

        }
    }
}
