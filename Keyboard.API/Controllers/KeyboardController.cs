using Keyboard.Business.Database;
using Keyboard.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;
using Model = Keyboard.Common.Models;
using Keyboard.Common.Enums;
using Keyboard.Common.Exceptions;
using Keyboard.Business.Logic;
using Keyboard.Business.ViewModel;
using System.Web.Http.Cors;
using System.Linq;

namespace Keyboard.API.Controllers
{
    [EnableCors(origins: "http://localhost:56628", headers: "*", methods: "*")]
    public class KeyboardController : ApiController
    {
        private IKeyboards<Model.Keyboard> _keyboards;

        public KeyboardController()
        {
            _keyboards = new DatabaseKeyboards();
        }

        public KeyboardController(IKeyboards<Model.Keyboard> keyboards)
        {
            _keyboards = keyboards;
        }

        [HttpGet]
        public List<Model.PathAction> GetKeyboardPath(string searchTerm, string keyboardType)
        {
            List<Model.PathAction> pathActions = new List<Model.PathAction>();

            KeyboardType type;
            if (Enum.TryParse(keyboardType, out type))
            {
                Model.Keyboard keyboard = _keyboards.GetKeyboard(type);

                PathEngine engine = new PathEngine(searchTerm, keyboard);
                return engine.GeneratePath();
            }
            else
                throw new InvalidKeyboardTypeException($"{keyboardType} is not a valid keyboard type.");
        }
        
        [HttpGet]
        public List<KeyRowView> GetKeyboard(string keyboardType)
        {
            KeyboardType type;
            if (Enum.TryParse(keyboardType, out type))
            {
                Model.Keyboard keyboard = _keyboards.GetKeyboard(type);

                return KeyRowView.ParseViewModel(keyboard.Keys);
            }
            else
                throw new InvalidKeyboardTypeException($"{keyboardType} is not a valid keyboard type.");
        }



    }
}
