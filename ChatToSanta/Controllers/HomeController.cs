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

        [HttpPost]
        public async Task<ActionResult> Index(IndexModel model)
        {
            model = new IndexModel();
            using (var client = new HttpClient())
            {

                var endpoint = ConfigurationManager.AppSettings["Endpoint"];                

                var endpointUri = endpoint + HttpUtility.UrlEncode(model.Message);
                var response = await client.GetAsync(endpointUri);

                var strResponseContent = await response.Content.ReadAsStringAsync();
                var responseObj = JsonConvert.DeserializeObject<Response>(strResponseContent);

                model.Answer = responseObj.TopScoringIntent.IntentString;
            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "This is a demo application.";

            return View();
        }
    }
}