using INDWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Data
{
    public class INDWalksDbContext : DbContext
    {
        public INDWalksDbContext(DbContextOptions<INDWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
               
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Region> Regions { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for difficulties
            //Easy, Medium, Hard

            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    ID = Guid.Parse("9c3a1d2e-4f5b-4c6d-8e7f-1a2b3c4d5e6f"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    ID = Guid.Parse("1a2b3c4d-5e6f-7a8b-9c0d-1e2f3a4b5c6d"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    ID = Guid.Parse("0d9c8b7a-6f5e-4d3c-2b1a-0f9e8d7c6b5a"),
                    Name = "Hard"
                }
            };

            //seed difficulties data to the Difficulties table
            modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //seed data for Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    ID = Guid.Parse("a1b2c3d4-e5f6-7a8b-9c0d-1e2f3a4b5c6d"),
                    Name = "Sikkim",
                    Code = "SK",
                    RegionImageUrl= "https://www.istockphoto.com/photos/sikkim"
                },
                new Region()
                {
                    ID = Guid.Parse("0d9c8b7a-6f5e-4d3c-2b1a-0f9e8d7c6b5a"),
                    Name = "Himanchal Pradesh",
                    Code = "HP",
                    RegionImageUrl="https://www.istockphoto.com/photos/himanchal-pradesh"
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
