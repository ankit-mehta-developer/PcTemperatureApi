﻿using log4net;
using Microsoft.Owin.Hosting;
using System;
using System.ServiceProcess;

namespace CpuTemperature
{
    public partial class CpuTempReaderService : ServiceBase
    {
        private IDisposable _server = null;
        private static readonly ILog Log = LogManager.GetLogger(typeof(CpuTempReaderService));

        public CpuTempReaderService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            StartService(args);
        }

        protected override void OnStop()
        {
            StopService();
        }

        public void StartService(string[] args)
        {
            var address = string.Format("http://*:{0}/", System.Configuration.ConfigurationManager.AppSettings["ListeningPort"]);
            _server = WebApp.Start<Startup>(address);
        }

        public void StopService()
        {
            Log.Info("Stop requested");
            if (_server != null)
                _server.Dispose();
        }
    }
}
