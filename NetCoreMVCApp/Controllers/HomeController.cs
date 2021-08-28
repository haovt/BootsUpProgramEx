using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Logging;
using NetCoreMVCApp.Exceptions;
using NetCoreMVCApp.Models;
using NetCoreMVCApp.Services;
using System;
using System.Diagnostics;
using System.Linq;

namespace NetCoreMVCApp.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        private readonly IConfigurationRoot _config;


        public HomeController(ILogger<HomeController> logger, IHomeService homeService,
            IConfiguration config)
        {
            _logger = logger;
            _homeService = homeService;
            //_config = (IConfigurationRoot) config;
        }

        [HttpGet]
        [Route("")]
        [Route("Index")]
        public IActionResult Index()
        {
            //var jsonconfig = _config.Providers.FirstOrDefault(x => x.GetType().Name == nameof(JsonConfigurationProvider));
            //jsonconfig.TryGet("UserInfor:Name", out string name);
            //jsonconfig.TryGet("UserInfor:Visa", out string visa);

            //Console.WriteLine($"Config from json: {name}, {visa}");

            //var envConfig = _config.Providers.FirstOrDefault(x => x.GetType().Name == nameof(EnvironmentVariablesConfigurationProvider));
            //envConfig.TryGet("COMPUTERNAME", out string computername);
            //Console.WriteLine($"Config from env: {computername}");

            return View();
        }

        [HttpPost]
        [Route("Verify")]
        public IActionResult Verify([FromForm] VerifyModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
            {
                throw new UsernameException();
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                throw new EmailException();
            }

            model.Description = _homeService.GetHomeData();
            return View("Privacy", model);
        }

        [HttpPost]
        [Route("Home/Privacy")]
        public IActionResult Privacy(VerifyModel model)
        {
            return View();
        }

        [HttpGet]
        [Route("Home/PackageData/{id}")]
        public IActionResult GetPackageData(long id)
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
