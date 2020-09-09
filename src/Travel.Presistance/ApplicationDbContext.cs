
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Travel.Application.Interface;
using Travel.Entities;
using Travel.Entities.Common;
using Travel.Entities.Entities;

namespace Travel.Presistance
{
    public class ApplicationDbContext:IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>, IApplicationDbContext
    {
        private readonly IDateTime _datetime;
        private readonly IUserAccessor _user;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
             //IOptions<OperationalStoreOptions> operationalStoreOptions,
            IDateTime datetime,IUserAccessor user)
            : base(options)
        {
            _datetime = datetime;
            _user = user;


        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _user.UserId;
                        entry.Entity.CreatedTs = _datetime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedBy = _user.UserId;
                        entry.Entity.LastModifiedTs = _datetime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            
            base.OnModelCreating(builder);
        }
    }
}
