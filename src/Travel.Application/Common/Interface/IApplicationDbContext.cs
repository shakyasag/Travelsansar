using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Travel.Application.Interface
{
    public interface IApplicationDbContext
    {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
