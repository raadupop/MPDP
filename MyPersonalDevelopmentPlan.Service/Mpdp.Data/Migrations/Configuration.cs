using Mpdp.Entities;

namespace Mpdp.Data.Migrations
{
  using System;
  using System.Data.Entity;
  using System.Data.Entity.Migrations;
  using System.Linq;

  internal sealed class Configuration : DbMigrationsConfiguration<Mpdp.Data.MpdpContext>
  {
    public Configuration()
    {
      AutomaticMigrationsEnabled = false;
    }

    protected override void Seed(Mpdp.Data.MpdpContext context)
    {
      // generate role
      context.RoleSet.AddOrUpdate(r => r.Name, GenerateRoles());

      // generate user 
      context.UserSet.AddOrUpdate(u => u.Email, new User[]
      {
        new User()
        {
          DateCreated = DateTime.Now,
          Username = "radu",
          Email = "radu@rp-webdesign.ro",
          HashedPassword ="XwAQoiq84p1RUzhAyPfaMDKVgSwnn80NCtsE8dNv3XI=",
          Salt = "mNKLRbEFCH8y1xIyTXP4qA==",
          IsLocked = false
        }
      });

      // generate user role 
      context.UserRoleSet.AddOrUpdate(new UserRole[]
      {
        new UserRole()
        {
          RoleId = 2,
          UserId = 1
        }
      });

    }

    private Role[] GenerateRoles()
    {
      Role[] roles =
        {
          new Role()
          {
            Name = "Admin"
          },
          new Role()
          {
            Name = "User"
          }
        };

      return roles;
    }
  }
}
