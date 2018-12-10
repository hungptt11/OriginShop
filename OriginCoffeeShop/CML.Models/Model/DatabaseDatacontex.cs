namespace CML.Models.Model
{
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class DatabaseDatacontex : IdentityDbContext<ApplicationUser>
    {
        public DatabaseDatacontex()
            : base("OriginShop", throwIfV1Schema: false)
        {
        }         
        
        public static DatabaseDatacontex Create()
        {
            return new DatabaseDatacontex();
        }
        public virtual DbSet<NewsCategory> NewsCategories { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductCategories> ProductCategories { get; set; }
        public virtual DbSet<UserToken> UserTokens { get; set; }
    }    
}