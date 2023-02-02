using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnScreenKeyboard.Application.Business.Keyboard.Requests;
using OnScreenKeyboard.Common.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnScreenKeyboard.Controllers
{
    [Route("api/[controller]")]
    public class KeyboardController : ApiBaseController
    {
        [HttpPost]
        public async Task<IActionResult> GetKeyboardOutput(IFormFile file)
        {
            string contents = await file.ReadAsStringAsync();
            var req = new GetDirectionFromInputCommand
            {
                FileContents = contents
            };
            var res = await Mediator.Send(req);
            return Ok(res);
        }
    }
}

