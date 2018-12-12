using System;
using System.Collections;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Seldat.MDS.Connector
{
    public class Base
    {
        private static HttpClient _instance;
        internal static HttpClient Instance
        {
            get
            {
                if (_instance == null)
                {
                    Initialize();
                }

                return _instance;
            }
        }

        private static void Initialize()
        {
            _instance = new HttpClient(new RetryHandler(new HttpClientHandler()));
            _instance.BaseAddress =ConfigMembers.URL;
            _instance.DefaultRequestHeaders.Accept.Clear();
            _instance.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _instance.DefaultRequestHeaders.Add("accountId", ConfigMembers.ApplicationDomainId);
        }

        public static void AddAuthorizationHeader(string token)
        {
            Instance.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void Logout()
        {
            Instance.DefaultRequestHeaders.Authorization = null;
        }

        private static void OnBeforeSend()
        {
            if (Instance.DefaultRequestHeaders.Authorization == null)
                LoginManager.Login();
        }

        private static void OnComplete(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
        }

        internal static HttpResponseMessage Get(string url, params object[] parameters)
        {
            url = string.Format(url, parameters);

            OnBeforeSend();

            HttpResponseMessage response = Instance.GetAsync(url).Result;

            OnComplete(response);

            return response;
        }

        internal static HttpResponseMessage Post(string url, StringContent content, params object[] parameters)
        {
            url = string.Format(url, parameters);

            OnBeforeSend();

            HttpResponseMessage response = Instance.PostAsync(url, content).Result;

            OnComplete(response);

            return response;
        }

        internal static HttpResponseMessage PostNonAuthorize(string url, StringContent content, params object[] parameters)
        {
            url = string.Format(url, parameters);

            HttpResponseMessage response = Instance.PostAsync(url, content).Result;

            response.EnsureSuccessStatusCode();

            return response;
        }

        internal static HttpResponseMessage Put(string url, StringContent content, params object[] parameters)
        {
            url = string.Format(url, parameters);

            OnBeforeSend();

            HttpResponseMessage response = Instance.PutAsync(url, content).Result;

            OnComplete(response);

            return response;
        }

        internal static HttpResponseMessage Delete(string url, params object[] parameters)
        {
            url = string.Format(url, parameters);

            OnBeforeSend();

            HttpResponseMessage response = Instance.DeleteAsync(url).Result;

            OnComplete(response);

            return response;
        }
    }
}
