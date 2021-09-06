using BlablacarApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Baucenter.ru
{
    class Program
    {
        static void Main(string[] args)
        {
            var code = "416001653";
            var proxy = new WebProxy("127.0.0.1:8888");
            var cookieContainer = new CookieContainer();

            var postRequest = new PostRequest("https://baucenter.ru/");
            postRequest.Data = $"ajax_call=y&INPUT_ID=title-search-input&q={code}&l=2";
            postRequest.Accept = "*/*";
            postRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36";
            postRequest.ContentType = "application/x-www-form-urlencoded";
            postRequest.Referer = "https://baucenter.ru/";
            postRequest.Host = "baucenter.ru";
            postRequest.Proxy = proxy;

            postRequest.Headers.Add("Bx-ajax", "true");
            //postRequest.Headers.Add("Origin", "https://baucenter.ru");
            //postRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"92\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"92\"");
            //postRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            //postRequest.Headers.Add("Sec-Fetch-Dest", "empty");
            //postRequest.Headers.Add("Sec-Fetch-Mode", "cors");
            //postRequest.Headers.Add("Sec-Fetch-Site", "same-origin");

            postRequest.Run(cookieContainer);

            var strStart = postRequest.Response.IndexOf("search-result-group search-result-product");
            strStart = postRequest.Response.IndexOf("<a href=", strStart) + 9;

            var strEnd = postRequest.Response.IndexOf("\"", strStart);
            var getPath = postRequest.Response.Substring(strStart, strEnd - strStart);

            Console.WriteLine($"getPath={getPath}");

            var getRequest = new GetRequest($"https://baucenter.ru{getPath}");
            getRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
            getRequest.Useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36";
            getRequest.Referer = "https://baucenter.ru/";
            //getRequest.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"92\", \" Not A;Brand\";v=\"99\", \"Google Chrome\";v=\"92\"");
            //getRequest.Headers.Add("sec-ch-ua-mobile", "?0");
            //getRequest.Headers.Add("Sec-Fetch-Dest", "document");
            //getRequest.Headers.Add("Sec-Fetch-Mode", "navigate");
            //getRequest.Headers.Add("Sec-Fetch-Site", "same-origin");
            //getRequest.Headers.Add("Upgrade-Insecure-Requests", "1");
            getRequest.Host = "baucenter.ru";
            getRequest.Proxy = proxy;
            getRequest.Run(cookieContainer);

            var card = new Card();
            card.Parse(getRequest.Response);

            Console.WriteLine($"title={card.Title}");
            Console.WriteLine($"price={card.Price}");

            Console.ReadKey();
        }
    }
}
