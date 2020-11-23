using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ProjectFacebook.ApplicationLayer.Logger
{
    public class SystemLogger:ILogger
    {
        IWebHostEnvironment _hostEnvironment;

        public SystemLogger(IWebHostEnvironment webHostEnvironment) => this._hostEnvironment = webHostEnvironment;


        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            using (StreamWriter streamWriter = new StreamWriter($"{_hostEnvironment.ContentRootPath}/log.txt", true))
            {
                await streamWriter.WriteLineAsync($"Log Level: {logLevel.ToString()} | Event Id: {eventId.Id} | Event Name: {eventId.Name} | Formatter: {formatter(state, exception)}");
                streamWriter.Close();
                await streamWriter.DisposeAsync();
            }
        }
    }
}
