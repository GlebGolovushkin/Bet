// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.IO;
using System.Net;
using CsQuery;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;

namespace ConsoleApp8
{
    class Program
    {
        static double minProcToSend = 0.6;
        static double minKoefToSend = 1.4;

        static void Main(string[] args)
        {
            InvokeRequestResponseService().Wait();
            Console.ReadKey();
        }

        static async Task InvokeRequestResponseService()
        {
            StreamReader reader = new StreamReader("input.txt");
            string ATP = reader.ReadLine();
            string Location = reader.ReadLine();
            string Tournament = reader.ReadLine();
            string Series = reader.ReadLine();
            string Court = reader.ReadLine();
            string Surface = reader.ReadLine();
            string Round = reader.ReadLine();
            string Best = reader.ReadLine();
            string Player1 = reader.ReadLine();
            string Rank1 = reader.ReadLine();
            string Pts1 = reader.ReadLine();
            string Player2 = reader.ReadLine();
            string Rank2 = reader.ReadLine();
            string Pts2 = reader.ReadLine();
            string Avg1 = reader.ReadLine();
            string Avg2 = reader.ReadLine();
            double result1 = -1;
            double result2 = -1;
            Console.WriteLine("Parse data?");
            if (!(Console.ReadLine().Contains("y")))
            {
                int winner1 = -1;
                int winner2 = -2;
                using (var client = new HttpClient())
                {
                    var scoreRequest = new
                    {
                        Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "ATP", ATP
                                            },
                                            {
                                                "Location", Location
                                            },
                                            {
                                                "Tournament", Tournament
                                            },
                                            {
                                                "Series", Series
                                            },
                                            {
                                                "Court", Court
                                            },
                                            {
                                                "Surface", Surface
                                            },
                                            {
                                                "Round", Round
                                            },
                                            {
                                                "Best of", Best
                                            },
                                            {
                                                "Player 1", Player1
                                            },
                                            {
                                                "Player 2", Player2
                                            },
                                            {
                                                "Rank 1", Rank1
                                            },
                                            {
                                                "Rank 2", Rank2
                                            },
                                            {
                                                "Pts 1", Pts1
                                            },
                                            {
                                                "Pts 2", Pts2
                                            },
                                            {
                                                "Avg 1", Avg1
                                            },
                                            {
                                                "Avg 2", Avg2
                                            },
                                }
                            }
                        },
                    },
                        GlobalParameters = new Dictionary<string, string>() {
                            {
                                "Excel sheet or embedded table name", ""
                            },
                    }
                    };

                    const string apiKey = "H2yfTuYQLBC/8P7Gnj6MZkerzKVjL4fPXPgugIUlBKBTboPZIRo9ddPDaSHMcaRUqmZs2rnRey1pkbhYbq9GuQ=="; // Replace this with the API key for the web service
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/d074ca9c106e4e2f8ff67d91b2b11e2e/services/83d38c3043834843a8aa48eccef71ad8/execute?api-version=2.0&format=swagger");

                    // WARNING: The 'await' statement below can result in a deadlock
                    // if you are calling this code from the UI thread of an ASP.Net application.
                    // One way to address this would be to call ConfigureAwait(false)
                    // so that the execution does not attempt to resume on the original context.
                    // For instance, replace code such as:
                    //      result = await DoSomeTask()
                    // with the following:
                    //      result = await DoSomeTask().ConfigureAwait(false)

                    HttpResponseMessage response = await client.PostAsync("", new StringContent(JsonConvert.SerializeObject(scoreRequest), Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();
                        result1 = Convert.ToDouble(GetValue(result, "Scored Probabilities"));
                        winner1 = Convert.ToInt32(GetValue(result, "Scored Labels"));
                        Console.WriteLine(winner1);
                        Console.WriteLine(result1);
                    }
                    else
                    {
                        Console.WriteLine("Bad request");
                    }

                    var scoreRequest2 = new
                    {
                        Inputs = new Dictionary<string, List<Dictionary<string, string>>>() {
                        {
                            "input1",
                            new List<Dictionary<string, string>>(){new Dictionary<string, string>(){
                                            {
                                                "ATP", ATP
                                            },
                                            {
                                                "Location", Location
                                            },
                                            {
                                                "Tournament", Tournament
                                            },
                                            {
                                                "Series", Series
                                            },
                                            {
                                                "Court", Court
                                            },
                                            {
                                                "Surface", Surface
                                            },
                                            {
                                                "Round", Round
                                            },
                                            {
                                                "Best of", Best
                                            },
                                            {
                                                "Player 1", Player2
                                            },
                                            {
                                                "Player 2", Player1
                                            },
                                            {
                                                "Rank 1", Rank2
                                            },
                                            {
                                                "Rank 2", Rank1
                                            },
                                            {
                                                "Pts 1", Pts2
                                            },
                                            {
                                                "Pts 2", Pts1
                                            },
                                            {
                                                "Avg 1", Avg2
                                            },
                                            {
                                                "Avg 2", Avg1
                                            },
                                }
                            }
                        },
                    },
                        GlobalParameters = new Dictionary<string, string>() {
                            {
                                "Excel sheet or embedded table name", ""
                            },
                    }
                    };

                    HttpResponseMessage response2 = await client.PostAsync("", new StringContent(JsonConvert.SerializeObject(scoreRequest2), Encoding.UTF8, "application/json"));

                    if (response2.IsSuccessStatusCode)
                    {
                        string result = await response2.Content.ReadAsStringAsync();
                        result2 = Convert.ToDouble(GetValue(result, "Scored Probabilities"));
                        winner2 = Convert.ToInt32(GetValue(result, "Scored Labels"));
                        Console.WriteLine(winner2);
                        Console.WriteLine(result2);
                    }
                    else
                    {
                        Console.WriteLine("Bad request");
                    }

                    if (result1 < 0.5)
                    {
                        result1 = 1 - result1;
                    }

                    if (result2 < 0.5)
                    {
                        result2 = 1 - result2;
                    }

                    double resProb = (result1 + result2) / 2;

                    Console.WriteLine(resProb);

                    if (resProb > minProcToSend && winner1 != winner2 && ((winner1 == 1 && Convert.ToDouble(Avg1) > minKoefToSend) || (winner1 == 2 && Convert.ToDouble(Avg2) > minKoefToSend)))
                    {
                        VkApi vkApi = new VkApi();
                        int appId = 6619481;
                        string email = "shadaw_2010@mail.ru";
                        string password = "Shadaw433";
                        vkApi.Authorize(new VkParams
                        {
                            ApplicationId = (ulong)appId,
                            Login = email,
                            Password = password,
                            Settings = Settings.All
                        });
                        System.Threading.Thread.Sleep(500);
                        if (winner1 == 1)
                        {
                            vkApi.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                            {
                                Message = $"{Player1} will play with {Player2} and first will win with {resProb} probability, koef is {Avg1}",

                                UserId = 101585745
                            });

                            System.Threading.Thread.Sleep(500);

                            vkApi.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                            {
                                Message = $"{Player1} will play with {Player2} and first will win with {resProb} probability, koef is {Avg1}",

                                UserId = 60520272
                            });
                        }
                        else
                        {
                            vkApi.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                            {
                                Message = $"{Player1} will play with {Player2} and second will win with {resProb} probability, koef is {Avg2}",

                                UserId = 101585745
                            });

                            System.Threading.Thread.Sleep(500);

                            vkApi.Messages.Send(new VkNet.Model.RequestParams.MessagesSendParams
                            {
                                Message = $"{Player1} will play with {Player2} and second will win with {resProb} probability, koef is {Avg2}",

                                UserId = 60520272
                            });
                        }
                    }

                }
            }
            else
            {
                string rt;

                WebRequest request = WebRequest.Create("https://www.atpworldtour.com/en/rankings/singles/?rankDate=2018-6-25&countryCode=all&rankRange=0-10000&ajax=true");

                WebResponse response3 = request.GetResponse();

                Stream dataStream = response3.GetResponseStream();

                StreamReader reader2 = new StreamReader(dataStream);

                rt = reader2.ReadToEnd();

                CQ cq = CQ.Create(rt);
                var trs = cq.Select(".main-content .table-rankings-wrapper .mega-table tbody tr");
                string rank1 = "";
                string player1 = "";
                string points1 = "";
                bool found = false;
                foreach (CsQuery.Implementation.DomElement tr in trs)
                {
                    foreach (var node in tr.ChildNodes)
                    {
                        if (node.NextElementSibling?.ClassName != null)
                        {
                            if (node.NextElementSibling.ClassName == "rank-cell")
                            {
                                string tmp = node.NextElementSibling.FirstChild.NodeValue.ToString().Remove(0, 6);
                                rank1 = "";
                                for (int i = 0; i < tmp.Length; i++)
                                {
                                    if (!Char.IsDigit(tmp[i]))
                                    {
                                        break;
                                    }

                                    rank1 += tmp[i];
                                }
                            }
                            if (node.NextElementSibling.ClassName == "player-cell")
                            {
                                player1 = node.NextElementSibling.ChildNodes[1].ChildNodes[0].NodeValue;
                                if ((player1.Contains(Player1.Remove(Player1.IndexOf(" "), Player1.Length - Player1.IndexOf(" "))) && player1[0] == Player1[Player1.Length - 2]) || (player1.Contains(Player2.Remove(Player2.IndexOf(" "), Player2.Length - Player2.IndexOf(" "))) && player1[0] == Player2[Player2.Length - 2]))
                                {
                                    found = true;
                                }
                            }
                            if (node.NextElementSibling.ClassName == "points-cell")
                            {
                                points1 = node.NextElementSibling.ChildNodes[1].ChildNodes[0].NodeValue;
                                if (found)
                                {
                                    if (points1[1] == ',')
                                    {
                                        points1 = points1.Remove(1, 1);
                                    }
                                    Console.WriteLine(player1);
                                    Console.WriteLine(rank1);
                                    Console.WriteLine(points1);
                                    found = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        public static string GetValue(string result, string key)
        {
            string res = "";
            for (int i = result.IndexOf(key) + key.Length + 3; i < result.Length; i++)
            {
                if (result[i] != '\"')
                {
                    res += result[i];
                }
                else break;
            }
            return res;
        }

        public class VkParams : IApiAuthParams
        {
            public ulong ApplicationId { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
            public Settings Settings { get; set; }
            public Func<string> TwoFactorAuthorization { get; set; }
            public string AccessToken { get; set; }
            public int TokenExpireTime { get; set; }
            public long UserId { get; set; }
            public long? CaptchaSid { get; set; }
            public string CaptchaKey { get; set; }
            public string Host { get; set; }
            public int? Port { get; set; }
            public string ProxyLogin { get; set; }
            public string ProxyPassword { get; set; }
        }
    }
}