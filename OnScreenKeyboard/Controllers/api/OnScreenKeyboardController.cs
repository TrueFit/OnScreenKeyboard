using OnScreenKeyboard.Models;
using OnScreenKeyboard.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnScreenKeyboard.Controllers.api
{
    [RoutePrefix("api/onscreenkeyboard")]
    public class OnScreenKeyboardController : ApiController
    {
        [HttpPost]
        [Route("calculateResults")]
        public HttpResponseMessage CalculateResults([FromBody] UserInput userInput)
        {
            OnScreenKeyboardService onScreenKeyboardService = new OnScreenKeyboardService();
            List<char> results = onScreenKeyboardService.CalculateResults(userInput.KeyboardLayout, userInput.SearchTerms);

            string result = string.Join(",", results);

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(result);

            return response;
        }
    }
}
