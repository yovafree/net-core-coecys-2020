using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using demo2.Models;
using Refit;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace demo2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Movies()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public IActionResult Movies(string title)
        {
            Movie movie = this.SearchMovie(title);

            ViewBag.movie = movie;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        #region "funciones"
        [Obsolete]
        private Movie SearchMovie(string title)
        {
            if (string.Equals(title,"x", StringComparison.InvariantCultureIgnoreCase)) return null;

            var moviesClient = CreateMoviesClient();

            var r = moviesClient.FindMovies("c9e6f58a",null,title, null, null,PlotType.full).Result;

            return r;
        }

        [Obsolete]
        private IMoviesApi CreateMoviesClient()
        {
            var moviesClient = RestService.For<IMoviesApi>("http://www.omdbapi.com",
                new RefitSettings
                {
                    JsonSerializerSettings = new JsonSerializerSettings {Converters = {new StringEnumConverter()}}
                });
            return moviesClient;
        }
        #endregion
    }
}
