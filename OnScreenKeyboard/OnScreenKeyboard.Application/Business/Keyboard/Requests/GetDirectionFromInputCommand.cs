using System;
using MediatR;
using OnScreenKeyboard.Application.Services.Keyboard;

namespace OnScreenKeyboard.Application.Business.Keyboard.Requests
{
    public record GetDirectionFromInputRequest : IRequest<IList<string>>
    {
        public string FileContents { get; init; }
    }

    public class GetDirectionFromInputRequestHandler : IRequestHandler<GetDirectionFromInputRequest, IList<string>>
    {
        private readonly IKeyboard _keyboard;
        public GetDirectionFromInputRequestHandler(IKeyboard keyboard)
        {
            _keyboard = keyboard;
        }

        public async Task<IList<string>> Handle(GetDirectionFromInputRequest request, CancellationToken cancellationToken)
        {
            var filesLines = request.FileContents.Split('\r', '\n');
            List<string> res = new();
            foreach (var line in filesLines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                int currentY = 0;
                int currentX = 0;
                List<string> currentLineOutput = new List<string>();

                foreach (var character in line.ToUpper().ToCharArray())
                {
                    if (character == ' ')
                    {
                        currentLineOutput.Add("S");
                        continue;
                    }

                    var row = GetLetterRow(character);

                    var column = GetLetterColumn(row, character);

                    var horizontalNum = row - currentX;

                    string horizontalDirection = horizontalNum > 0 ? "D" : "U";

                    currentLineOutput.AddRange(Enumerable.Repeat(horizontalDirection, Math.Abs(horizontalNum)));

                    currentX += horizontalNum;

                    var verticalNum = column - currentY;

                    string verticalDirection = verticalNum > 0 ? "R" : "L";

                    currentLineOutput.AddRange(Enumerable.Repeat(verticalDirection, Math.Abs(verticalNum)));

                    currentY += verticalNum;

                    currentLineOutput.Add("#");

                }
                res.Add(string.Join(",", currentLineOutput));
            }
            return res;
        }

        private int GetLetterRow(char character)
        {
            foreach (var key in _keyboard.Keyboard.Keys)
            {
                if (_keyboard.Keyboard[key].Values.Any(x => x == character))
                {
                    return key;
                }
            }

            throw new ArgumentException($"Character {character} not found in keyboard");
        }

        private int GetLetterColumn(int row, char character)
        {
            foreach (var key in _keyboard.Keyboard[row].Keys)
            {
                if (_keyboard.Keyboard[row][key] == character)
                {
                    return key;
                }
            }

            throw new ArgumentException($"Character {character} not found in keyboard");
        }
    }
}

