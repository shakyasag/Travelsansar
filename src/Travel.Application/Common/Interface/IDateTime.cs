using System;
using System.Collections.Generic;
using System.Text;

namespace Travel.Application.Interface
{
    public interface IDateTime
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}
