using System;
using System.Net.Http;
using System.Configuration;
using NLog;

namespace UnotellyIpUpdater
{
    public class UpdateManager
    {

        private readonly IpAddressFetcher _ipAddressFetcher;
        public static IpAddressInfo AddressInfo;
        private string _userHash = ConfigurationManager.AppSettings["UserHash"];

        private const string UnoTellyUrl =
            "http://www.unotelly.com/unodns/auto_auth/hash_update/updateip.php?user_hash=";

        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public UpdateManager(IpAddressInfo info)
        {
            AddressInfo = info;
            _ipAddressFetcher = new IpAddressFetcher();
        }


        public async void CheckForUpdates()
        {
            var ipDetails = await _ipAddressFetcher.GetCurrentIpAddressDetails();

            if (AddressInfo.Ip == ipDetails.Ip)
                return;
            //IP Address has changed
            _logger.Info("Ip Address Changed - {0}", ipDetails.Ip);
            AddressInfo = ipDetails;
            UpdateIpAddressWithUnoTelly();
        }

        private void UpdateIpAddressWithUnoTelly()
        {
            var url = string.Format("{0}{1}", UnoTellyUrl, _userHash);

            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            if (!result.IsSuccessStatusCode)
            {
                _logger.Info("Update Failed");
                return;
            }
            _logger.Info("Updated Ip Address");
        }
    }
}