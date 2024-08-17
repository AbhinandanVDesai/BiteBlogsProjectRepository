using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BiteBlogs.Models
{
    public class AuthDbContext : IdentityDbContext

    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        { }


       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            var adminRoleId = "7653eb1d-f8f2-485c-bbc9-61c678b20e13";
            var superAdminRoleId = "b34cc48b-7120-4b4f-ba71-ba80f5ba24ad";
            var userRoleId = "f721578f-5286-40d0-9df0-834ffd2a285c";

            //Seed roles(User.Admin,SuperAdmin)
            var roles = new List<IdentityRole>            
            {            

                new IdentityRole
                {
                     Name="Admin",
                     NormalizedName="ADMIN",
                     Id=adminRoleId,
                     ConcurrencyStamp=adminRoleId

                },




                new IdentityRole
                {
                    Name="SuperAdmin",
                    NormalizedName="SUPERADMIN",
                    Id=superAdminRoleId,
                    ConcurrencyStamp=superAdminRoleId

                },

                new IdentityRole
                {
                   Name="user",
                   NormalizedName="USER",
                   Id=userRoleId,
                   ConcurrencyStamp=userRoleId


                }
            };

            builder.Entity<IdentityRole>().HasData(roles);
            

            //Seed SuperAdminUser
            var superAdminId = "06bcb7f2-8ddf-4a20-a1ff-48ab63a8c246";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@biteblogs.com",
                Email = "superadmin@biteblogs.com",
                NormalizedEmail = "superadmin@biteblogs.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser,"SuperAdmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);
            

            //Add all roles to SuperAdminUser

            var superAdminRoles = new List<IdentityUserRole<string>>
            {

                new IdentityUserRole<string>
               {
                RoleId = adminRoleId,
                UserId = superAdminId
               },
                new IdentityUserRole<string>
                {
                RoleId = superAdminRoleId,
                UserId = superAdminId
                },
                new IdentityUserRole<string>
                 {
                RoleId = userRoleId,
                UserId = superAdminId
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);
        }

    }

}

//basically when we run migration with this auth-dbcontext  3 table get created
// 1   IdentityRole         2 IdentityUser          3 IdentityUserRoles
//Guid  Admin                Guid  SuperAdmin       Guid  UserId  RoleId