using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using MyProject.Models;
using System.Net.Http;

namespace MyProject.Tools
{
    public class HttpHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="baseUrl"></param>
        /// <param name="josnBody"></param>
        /// <returns></returns>
        public static Result HttpPost<T>(string baseUrl,T josnBody)
        {
            Result result = new Result() {Code=0 };
            try {
                RestRequest request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddJsonBody(josnBody);
                RestClient client = new RestClient(baseUrl);
                IRestResponse restResponse = client.Execute(request);
                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.Code = 1;
                    result.JsonObj= restResponse.Content;
                    return result;
                }
                else
                {
                    result.Message = restResponse.ErrorMessage;
                    return result;
                }
            }
            catch (Exception ex) {
                result.Message = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <returns></returns>
        public static Result HttpGet(string baseUrl)
        {
            Result result = new Result() { Code = 0 };
            try
            {
                RestRequest request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                RestClient client = new RestClient(baseUrl);
                IRestResponse restResponse = client.Execute(request);
                if (restResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result.Code = 1;
                    result.JsonObj = restResponse.Content;
                    return result;
                }
                else
                {
                    result.Message = restResponse.ErrorMessage;
                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }

        public static string HttpPost(string url, string postData = null, string contentType = null, int timeOut = 30, Dictionary<string, string> headers = null)
        {
            postData = postData ?? "";
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                using (HttpContent httpContent = new StringContent(postData, Encoding.UTF8))
                {
                    if (contentType != null)
                        httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                    HttpResponseMessage response = client.PostAsync(url, httpContent).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public static string HttpGet(string url, int timeOut = 30, Dictionary<string, string> headers = null)
        {
            using (HttpClient client = new HttpClient())
            {
                if (headers != null)
                {
                    foreach (var header in headers)
                        client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
                HttpResponseMessage response = client.GetAsync(url).Result;
                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
