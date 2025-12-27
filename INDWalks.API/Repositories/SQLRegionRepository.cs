using INDWalks.API.Data;
using INDWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Repositories
{
    public class SQLRegionRepository :  IRegionRepository
    {
        private readonly INDWalksDbContext dbcontext;
        public SQLRegionRepository(INDWalksDbContext dbcontext)
        {
            this.dbcontext = dbcontext;
        }
        public async Task<List<Region>>GetAllAsync()
        {
            return await dbcontext.Regions.ToListAsync();
        }

        public async Task<Region?>GetByIdAsync(Guid id)
        {
            return await dbcontext.Regions.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task<Region>CreateAsync(Region region)
        {
            await dbcontext.Regions.AddAsync(region);
            await dbcontext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?>UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.ID == id);
            if (existingRegion == null)
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            await dbcontext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?>DeleteAsync(Guid id)
        {
            var existingRegion = await dbcontext.Regions.FirstOrDefaultAsync(x => x.ID == id);
            if (existingRegion == null)
            {
                return null;
            }
            dbcontext.Regions.Remove(existingRegion);
            await dbcontext.SaveChangesAsync();
            return existingRegion;
        }
    }
}
    