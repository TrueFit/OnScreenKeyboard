using System;
using System.Collections.Generic;
using System.Linq;
using OnScreenKeyboard.Enums;

namespace OnScreenKeyboard.Utils
{
    public interface IConvertNavigationListsToStrings
    {
        string Convert(List<Navigation> list);
    }

    public class NavigationListToStringConverter : IConvertNavigationListsToStrings
    {
        public string Convert(List<Navigation> list)
        {
            return String.Join(",", list.Select(x => x.ToString()));
        }
    }
}
