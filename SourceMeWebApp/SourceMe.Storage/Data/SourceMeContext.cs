

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SourceMe.Storage.Data.Entities;

namespace SourceMe.Storage.Data
{
    public class SourceMeContext:IdentityDbContext<StoreUser>
    {
        public SourceMeContext(DbContextOptions<SourceMeContext>options):base(options)
        {

        }
        
    }
}
