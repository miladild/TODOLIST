﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;

namespace IntegrationTest
{
    public class TestFixture : IDisposable
    {
        private readonly TestServer _server;
        public HttpClient Client { get; }

        public TestFixture()
        {
            var builder = new WebHostBuilder()
            .UseStartup<TODOLIST.Startup>()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(Path.Combine(
                    Directory.GetCurrentDirectory(),
                        "..\\..\\..\\..\\..\\src\\TODOLIST"));

                config.AddJsonFile("appsettings.json");
            });
            _server = new TestServer(builder);
            Client = _server.CreateClient();
            Client.BaseAddress = new Uri("http://localhost:8888");
        }



        public void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
