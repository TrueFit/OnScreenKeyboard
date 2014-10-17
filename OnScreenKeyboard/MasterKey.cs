using System.Collections.Generic;
using System.Linq;

namespace OnScreenKeyboard
{
    public class MasterKey
    {
        private byte _valueCounter = 1;
        public MasterKey()
        {
            Keyboard=new List<Key>();
            CreateRows(1, Enumerable.ToList("A,B,C,D,E,F".Split(',')));
            CreateRows(2, Enumerable.ToList("G,H,I,J,K,L".Split(',')));
            CreateRows(3, Enumerable.ToList("M,N,O,P,Q,R".Split(',')));
            CreateRows(4, Enumerable.ToList("S,T,U,V,W,X".Split(',')));
            CreateRows(5, Enumerable.ToList("Y,Z,1,2,3,4".Split(',')));
            CreateRows(6, Enumerable.ToList("5,6,7,8,9,0".Split(',')));
        }

        public List<Key> Keyboard { get; private set; }

        private void CreateRows(byte row, IEnumerable<string> characters)
        {
            foreach (var character in characters)
            {
                Keyboard.Add(new Key(row, character, _valueCounter));
                _valueCounter++;
            }
        }

        public int GetColumnPosition(Key key)
        {
            var returnValue = 1;
            var rows = Keyboard.Where(x => x.Row == key.Row).ToList();

            for(var i=0;i<=rows.Count()-1;i++)
            {
                if (rows[i].Value == key.Value)
                {
                    returnValue = i + 1;
                }
            }

            return returnValue;
        }
    }
}
