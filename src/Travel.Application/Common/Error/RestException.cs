using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Travel.Application.Common.Error
{
    public  class RestException:Exception
    {
        public object Errors { get; }
        public HttpStatusCode Code { get; }
        public  RestException(HttpStatusCode code, object errors = null)
            {
             Code = code;
             Errors = errors;
            }

    }
}
