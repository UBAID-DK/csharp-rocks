using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WizKids.API.Model;
using WizKids.API.Service;

namespace WizKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WizKidsController : ControllerBase
    {
        public string dbPaths;
        private readonly IWizKidSearchService _wizKidSearchService;
        public WizKidsController(IWizKidSearchService wizKidSearchService)
        {
            _wizKidSearchService = wizKidSearchService;
        }

        [HttpGet("GetApi")]
        public IActionResult GetApiData(string text)
        {
            try
            {
                //var fetchApiData = JsonConvert.SerializeObject(_wizKidSearchService.FetchAPIData(text)).OrderByDescending(x=>x);
                var fetchApiData = _wizKidSearchService.FetchAPIData(text);
                return Ok(fetchApiData);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest, "API connectivity Problem");
            }
        }
        [HttpGet("GetDB")]
        public IActionResult GetDBdata(string text)
        {
            try
            {
                //var fetchDbData = JsonConvert.SerializeObject(_wizKidSearchService.FetchDBData(text)).OrderByDescending(x => x);
                var fetchDbData = _wizKidSearchService.FetchDBData(text);
                return Ok(fetchDbData);
            }
            catch(Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,"Data Connectivity Error");
            }
        }
        [HttpGet("GetFull")]
        public IActionResult GetFullData(string text)
        {
            try
            { 
                // under development...

           var fetchDbData = _wizKidSearchService.FetchDBData(text);
           var fetchApiData = _wizKidSearchService.FetchAPIData(text);

        
                return Ok(fetchDbData);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status400BadRequest);
            }
        }
        
    }
}