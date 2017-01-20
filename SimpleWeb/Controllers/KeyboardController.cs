using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Owin;
using SimpleWeb.Models;
using Simple.Library.Misc;
using Simple.Library.Keyboard;
using Simple.Library.SearchService;

namespace SimpleWeb.Controllers
{
    public class KeyboardController : ApiController
    {

        public KeyboardController()
        {
        }

        public KeyboardLayoutModel GetKeyboard(string type)
        {
            KeyboardLayoutModel layout = new KeyboardLayoutModel();
            IKeyboard keyboard;
            if (string.IsNullOrEmpty(type))
            {
                return null;
            }
            if(type == "TrueFit")
            {
                keyboard = new TrueFitKeyboard();
                layout.NumberOfColumns = keyboard.ReturnKeyboardColumnCount();
                layout.NumberOfRows = keyboard.ReturnKeyboardRowCount();
                layout.KeyboardCharacters = keyboard.ReturnWebKeyboard();
                layout.Information = keyboard.ReturnKeyboardType();
            }            
            return layout;            
        }

        public KeyboardSearchPathModel GetSearchPath(string type, string term, string delimeter)
        {
            KeyboardSearchPathModel search = new KeyboardSearchPathModel();
            IKeyboard keyboard;
            if (string.IsNullOrEmpty(type))
            {
                return null;
            }
            else
            {
                keyboard = new TrueFitKeyboard();
                term = System.Uri.UnescapeDataString(term);
                delimeter = System.Uri.UnescapeDataString(delimeter);
                SearchService searcher = new SearchService(keyboard, true);
                search.SearchPath = searcher.ReturnSearchPath(term, delimeter);
            }
            return search;
        }
    }
}