using OnScreenKeyboard.Models;
using OnScreenKeyboard.Services;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace OnScreenKeyboard.Controllers.api
{
    [RoutePrefix("api/onscreenkeyboard")]
    public class OnScreenKeyboardController : ApiController
    {
        // GET: api/OnScreenKeyboard
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //// GET: api/OnScreenKeyboard/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/OnScreenKeyboard
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/OnScreenKeyboard/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/OnScreenKeyboard/5
        //public void Delete(int id)
        //{
        //}

        [HttpPost]
        [Route("calculateResults")]
        public HttpResponseMessage CalculateResults([FromBody] UserInput userInput)
        {
            OnScreenKeyboardService onScreenKeyboardService = new OnScreenKeyboardService();
            List<char> results = onScreenKeyboardService.CalculateResults(userInput.Alphabet, userInput.SearchTerms);

            // TODO make this more elegant
            string result = "";
            for (int i = 0; i < results.Count; i++)
            {
                result += results[i];

                if (i != results.Count - 1)
                {
                    result += ",";
                }
            }

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(result);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return response;
        }
    }
}
