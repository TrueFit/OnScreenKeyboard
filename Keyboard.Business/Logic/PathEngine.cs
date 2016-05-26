using Model = Keyboard.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Keyboard.Common.Exceptions;

namespace Keyboard.Business.Logic
{
    public class PathEngine
    {
        public readonly string Term;

        public readonly Model.Keyboard Keyboard;
        

        private Actions _actions;

        private char _startChar;

        
        public PathEngine(string term, Model.Keyboard keyboard)
        {
            Term = term;
            Keyboard = keyboard;
            _actions = new Actions();
            _startChar = 'A';
        }

        public List<Model.PathAction> GeneratePath()
        {
            List<Model.PathAction> pathActions = new List<Model.PathAction>();

            int spaceOffset = 0;
            for (int i = 0; i < Term.Length; i++)
            {
                char c = Term[i+spaceOffset];
                if (c == ' ')
                {
                    pathActions.Add(_actions.Space);
                    i++;
                    c = Term[i];
                }

                pathActions.AddRange(ParsePathActionGroup(_startChar, c));

                _startChar = c;
            }

            return pathActions;
        }

        private List<Model.PathAction> ParsePathActionGroup(char startChar, char endChar)
        {
            List<Model.PathAction> pathActionGroup = new List<Model.PathAction>();

            Model.Key startKey = Keyboard.Keys.FirstOrDefault(k => char.ToUpperInvariant(k.Value) == char.ToUpperInvariant(startChar));
            Model.Key endKey = Keyboard.Keys.FirstOrDefault(k => char.ToUpperInvariant(k.Value) == char.ToUpperInvariant(endChar));

            if (startKey == null && startChar != ' ')
                throw new InvalidKeyException($"{startChar} is not a valid key.");
            else if (endKey == null && endChar != ' ')
                throw new InvalidKeyException($"{endChar} is not a valid key.");

            int yDifference = endKey.X - startKey.X;
            int xDifference = endKey.Y - startKey.Y;

            while (yDifference != 0)
            {
                pathActionGroup.Add(DetectAction(yDifference, _actions.Right, _actions.Left));
                yDifference = DecrementInt(yDifference);
            }

            while (xDifference != 0)
            {
                pathActionGroup.Add(DetectAction(xDifference, _actions.Down, _actions.Up));
                xDifference = DecrementInt(xDifference);
            }

            pathActionGroup.Add(_actions.Click);

            return pathActionGroup;

        }

        private Model.PathAction DetectAction(int difference, Model.PathAction positiveAction, Model.PathAction negativeAction)
        {
            // A move of -1 is left/down, +1 is right/up
            if (Math.Sign(difference) == 1)
                return positiveAction;
            else
                return negativeAction;
        }

        private int DecrementInt(int value)
        {
            // Decreasing the value by 1, regardless of sign
            return value + (Math.Sign(value) * -1);
        }

    }
}
