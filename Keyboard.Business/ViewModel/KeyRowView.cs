using Keyboard.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keyboard.Business.ViewModel
{
    public class KeyRowView
    {
        public List<Key> Keys { get; set; }

        public static List<KeyRowView> ParseViewModel(ICollection<Key> keys)
        {
            List<KeyRowView> keyboardView = new List<KeyRowView>();

            List<int> rowIndexes = keys.Select(k => k.X).Distinct().ToList();

            foreach (int row in rowIndexes)
            {
                KeyRowView keyRow = new KeyRowView() { Keys = keys.Where(k => k.Y == row).ToList() };
                keyboardView.Add(keyRow);
            }

            return keyboardView;
        }
    }

}
