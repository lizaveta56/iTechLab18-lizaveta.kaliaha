﻿using Microsoft.AspNetCore.Mvc.Filters;
using server_task4.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server_task4.Filters
{
    public class LogExceptionAttribute : ExceptionFilterAttribute
    {
        private readonly ILog4NetService _log4net;
        public LogExceptionAttribute(ILog4NetService log4NetService)
        {
            this._log4net = log4NetService;
        }

        public override void OnException(ExceptionContext context)
        {
            _log4net.LogException(context.Exception);
        }
    }
}