using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Logger
{
    public class LoggerProvider : ILoggerProvider
    {
        IWebHostEnvironment _webHostEnvironment;

        public LoggerProvider(IWebHostEnvironment webHostEnvironment) => this._webHostEnvironment = webHostEnvironment;

        public ILogger CreateLogger(string categoryName) => new SystemLogger(_webHostEnvironment);

        public void Dispose() => throw new NotImplementedException();
    }
}
