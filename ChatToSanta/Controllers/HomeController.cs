using ChatToSanta.Models;
using ChatToSanta.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ChatToSanta.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new IndexModel
            {
                Answer = "Ho ho ho!"
            });
        }
        
        private string DinosaurOfMaliciousIntent()
        {
                            __.--'~~~~~`--.
         ..       __.    .-~               ~-.
         ((\     /   `}.~                     `.
          \\\  .{     }               /     \   \
      (\   \\~~       }              |       }   \
       \`.-~ -@~     }  ,-,.         |       )    \
       (___     ) _}  (    :        |    / /      `._
        `----._-~.     _\ \ |_       \   / /-.__     `._
               ~~----~~  \ \| ~~--~~~(  + /     ~-._    ~-._
                         /  /         \  \          ~--.,___~_-_.
                      __/  /          _\  )
                    .<___.'         .<___/  
        }

        [HttpPost]
        public async Task<ActionResult> Index(IndexModel model)
        {            
            using (var client = new HttpClient())
            {

                var endpoint = ConfigurationManager.AppSettings["Endpoint"];                

                var endpointUri = endpoint + HttpUtility.UrlEncode(model.Message);
                var response = await client.GetAsync(endpointUri);

                var strResponseContent = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<Response>(strResponseContent);

                model = new IndexModel();
                model.Answer = GetResponseFromIntent(responseObj.TopScoringIntent.IntentString);
            }

            return View(model);
        }

        private string GetResponseFromIntent(string intentString)
        {
            switch(intentString.ToLower())
            {
                case "sayhello":
                    return "Hello!";

                case "listreindeer":
                    return "Well... I know I have Rudolph, are there others?";

                case "doyoulike":
                    return "I do, a lot!";

                case "dothereindeerlike":
                    return "They do, very much!";

                case "askforgift":
                    return "I'm sure the elves can make you one of those!";

                case "wheredoyoulive":
                    return "I live at the north pole!";

                default:
                    return "Ho ho ho!";
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a demo application.";

            return View();
        }
    }
}
