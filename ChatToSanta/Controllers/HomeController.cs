﻿using ChatToSanta.Models;
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
        public async Task<ActionResult> Index()
        {
            var model = new IndexModel();
            using (var client = new HttpClient())
            {
                                
                var endpoint = ConfigurationManager.AppSettings["Endpoint"];
                var query = "Hello Santa";

                var endpointUri = endpoint + HttpUtility.UrlEncode(query);
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