using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Application.Interface
{
    public interface IUserAccessor
    {
        Guid UserId { get; }
        string UserName { get; }
    }
}
