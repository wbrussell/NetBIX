using NetBIX.oBIX.Client;
using NetBIX.oBIX.Client.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NetBIX.oBIX.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string Server = "<server>";
            string Username = "<username>";
            string Password = "<password>";
            using (var obixClient = new XmlObixClient(new Uri(Server)))
            {
                obixClient.WebClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue
                    (
                    "Basic",
                    Convert.ToBase64String(
                        Encoding.ASCII.GetBytes(
                            string.Format("{0}:{1}", Username, Password))));

                var connectResult = obixClient.Connect();
                if (connectResult != ObixResult.kObixClientSuccess)
                {
                    throw new Exception("Connection to server failed: " +
                                        ObixResult.Message(connectResult));
                }

                var readResult = obixClient.ReadUriXml(new Uri(Server + "histories/"));
                if (readResult.ResultSucceeded)
                {
                    var element = readResult.Result;
                    System.Console.WriteLine(element.ToString());
                }
                else
                {
                    throw new Exception("Error reading from server: " + ObixResult.Message(readResult));
                }

                System.Console.ReadKey();
            }
        }
    }
}
