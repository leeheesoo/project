using Google.Apis.AnalyticsReporting.v4;
using Google.Apis.AnalyticsReporting.v4.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace INGLife.Event.Infrastructures.GoogleAnalyticsReportingServices {
    public class GoogleServiceAccount {
        private static string serviceAccountEmail = ConfigurationManager.AppSettings["google.apis.serviceAccountEmail"] as string;
        private static string privateKey = ConfigurationManager.AppSettings["google.apis.privatekey"] as string;

        public static AnalyticsReportingService AuthenticateServiceAccountFromKey() {
            try {
                if (string.IsNullOrEmpty(privateKey))
                    throw new Exception("Key is required.");
                if (string.IsNullOrEmpty(serviceAccountEmail))
                    throw new Exception("ServiceAccountEmail is required.");

                // These are the scopes of permissions you need. It is best to request only what you need and not all of them
                string[] scopes = new string[] { AnalyticsReportingService.Scope.Analytics };             // View your Google Analytics data


                var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail) {
                    Scopes = scopes
                }.FromPrivateKey(privateKey.Replace("\\n", "\n")));


                // Create the  Analytics service.
                return new AnalyticsReportingService(new BaseClientService.Initializer() {
                    HttpClientInitializer = credential,
                    ApplicationName = "INGLife Analytics",
                });

            } catch (Exception ex) {
                Console.WriteLine("Create service account AnalyticsService failed" + ex.Message);
                throw new Exception("CreateServiceAccountAnalyticsServiceFailed", ex);
            }
        }
    }
}