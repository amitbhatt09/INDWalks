using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace INDWalks.API.Data
{
    public class INDWalksAuthDbContext : IdentityDbContext
    {
        public INDWalksAuthDbContext(DbContextOptions<INDWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            var readerRoleId = "6b811a1a-d71f-4342-b54f-573aada748c1";
            var writerRoleId = "a7a6f308-eafe-44c7-93f1-b5be9970d15d";


            var roles = new List<IdentityRole>()
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId

                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleId

                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
