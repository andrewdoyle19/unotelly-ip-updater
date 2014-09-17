using System;
using System.Timers;
using System.Web;
using NLog;

namespace UnotellyIpUpdater
{
    public class IpAddressUpdaterService
    {
        private readonly UpdateManager _manager;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        private const int _interval = 60000*30; //30 mins

        public IpAddressUpdaterService()
        {
            try
            {
                var fetcher = new IpAddressFetcher();
                var currentIp = fetcher.GetCurrentIpAddressDetails().Result;
                _manager = new UpdateManager(currentIp);
                _logger.Info("Ip Address Service Started - Current IP {0}", currentIp.Ip);
            }
            catch (AggregateException exception)
            {
                var message = exception.InnerExceptions.Count > 0 ? exception.InnerException.Message : exception.Message;
                _logger.Error(message);
            }
        }

        public void Start()
        {
            var timer = new Timer {Interval = _interval};
            timer.Elapsed += OnTimer;
            timer.Start();
        }


        public void Stop()
        {
            _logger.Info("Ip Address Service Stopped");
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            try
            {
                _logger.Debug("Checking For Updates");
                _manager.CheckForUpdates();
            }
            catch (HttpException exception)
            {
                _logger.Error(exception.Message);
            }
            catch (Exception exception)
            {
                Stop();
                _logger.Error(exception.Message);
            }
        }
    }
}