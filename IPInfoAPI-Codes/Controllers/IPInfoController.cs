using IPInfoAPI_Codes.DTO;
using IPInfoAPI_Codes.Repositories;
using IPInfoAPI_Codes.Services;
using IPInfoAPI_Codes.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IPInfoAPI_Codes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IPInfoController : Controller,IIPInfoController
    {
        private readonly IIPInfoService _service;

        public IPInfoController(IIPInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{ip}")]
        public async Task<IActionResult> GetIP([FromRoute] string ip)
        {
           return  Ok(await _service.GetCountry(ip));
        }

        [HttpPost]
        [Route("report")]
        public async Task<IActionResult> GetReport([FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)]List<String> ?twoLetterCodes)
        {
            Validate.twoLetterList(twoLetterCodes);
            
            return Ok(await _service.GetIpReport(twoLetterCodes));
        }


    }
}
