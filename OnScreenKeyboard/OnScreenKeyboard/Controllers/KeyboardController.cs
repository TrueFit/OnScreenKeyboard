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
        //Probably should be a get, but Swagger doesn't allow for the file picker on gets. 
        [HttpPost("file")]
        [ProducesResponseType(typeof(IList<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetKeyboardOutputFromFile(IFormFile file)
        {
            string contents = await file.ReadAsStringAsync();
            var req = new GetDirectionFromInputRequest
            {
                FileContents = contents
            };
            var res = await Mediator.Send(req);
            return Ok(res);
        }

        //Mostly used for testing, but is also a valid way to recieve feedback without a file.
        //Also showcases fluent validation
        //use /r/n in swagger request for new lines
        [HttpPost()]
        [ProducesResponseType(typeof(IList<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetKeyboardOutputFromRequest(GetDirectionFromInputRequest request)
        {
            var res = await Mediator.Send(request);
            return Ok(res);
        }

    }
}

