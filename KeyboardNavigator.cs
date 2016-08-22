using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace OnScreenKeyboard
{
    public interface IKeyboardNavigator
    {
        string PathTo(string input);
        void Reset();
    }

    public class KeyboardNavigator : IKeyboardNavigator
    {
        // Typically the logger and other dependencies would be injected.
        // This has been omitted for expediency.
        static ILogger _logger = new Logger();
        private readonly IKeyboardModel _keyboard;

        private Point _cursor;   // current cursor position

        public KeyboardNavigator(IKeyboardModel keyboard)
        {
            _keyboard = keyboard;
        }

        public string PathTo(string input)
        {
            //_logger.Info(string.Format("Getting path for {0}", input));
            Reset();

            var path = new PathBuilder();
            foreach (var c in input)
            {
                if (c == ' ')
                {
                    path.Append('S');
                }
                else
                {
                    Point pos = new Point();
                    if (!_keyboard.TryFind(c, out pos))
                        throw new ArgumentException(string.Format("Invalid keyboard character - {0}", c));

                    // Assumption is that keyboard navigation does not wrap around. For example, from
                    // the right-most edge, going right again does not end up on the left-most edge.
                    //_logger.Info(string.Format("Current position: {0}, {1}, Next position: ",
                    //    _cursor.X, _cursor.Y, pos.X, pos.Y));

                    if (_cursor.Y < pos.Y)
                    {
                        for (int y = _cursor.Y; y < pos.Y; y++)
                            path.Append('D');
                    }
                    else
                    {
                        for (int y = pos.Y; y < _cursor.Y; y++)
                            path.Append('U');
                    }

                    if (_cursor.X < pos.X)
                    {
                        for (int x = _cursor.X; x < pos.X; x++)
                            path.Append('R');
                    }
                    else
                    {
                        for (int x = pos.X; x < _cursor.X; x++)
                            path.Append('L');
                    }
                    path.Append('#');
                    _cursor = pos;
                }
            }

            return path.ToString();
        }

        public void Reset()
        {
            if (!_keyboard.TryFind('A', out _cursor))
                throw new Exception("Cannot determine keyboard start position");
        }
    }

    class PathBuilder
    {
        private StringBuilder _path;

        public PathBuilder()
        {
            _path = new StringBuilder();
        }

        public void Append(char step)
        {
            if (_path.Length > 0)
                _path.Append(',');
            _path.Append(step);
        }

        public string ToString()
        {
            return _path.ToString();
        }
    }
}
