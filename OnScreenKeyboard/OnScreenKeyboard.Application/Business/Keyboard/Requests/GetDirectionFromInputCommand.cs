using System;
using MediatR;
using OnScreenKeyboard.Application.Services.Keyboard;

namespace OnScreenKeyboard.Application.Business.Keyboard.Requests
{
    public record GetDirectionFromInputCommand : IRequest<IList<string>>
    {
        public string FileContents { get; init; }
    }

    public class GetDirectionFromInputCommandHandler : IRequestHandler<GetDirectionFromInputCommand, IList<string>>
    {
        private readonly IKeyboardService _keyboardService;
        public GetDirectionFromInputCommandHandler(IKeyboardService keyboardService)
        {
            _keyboardService = keyboardService;
        }

        public async Task<IList<string>> Handle(GetDirectionFromInputCommand request, CancellationToken cancellationToken)
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

                    if (row > currentX)
                    {
                        while (currentX < row)
                        {
                            currentLineOutput.Add("D");
                            currentX++;
                        }
                    }
                    else
                    {
                        while (currentX > row)
                        {
                            currentLineOutput.Add("U");
                            currentX--;
                        }
                    }

                    if (column > currentY)
                    {
                        while (currentY < column)
                        {
                            currentLineOutput.Add("R");
                            currentY++;
                        }
                    }
                    else
                    {
                        while (currentY > column)
                        {
                            currentLineOutput.Add("L");
                            currentY--;
                        }
                    }

                    currentLineOutput.Add("#");

                }
                res.Add(string.Join(",", currentLineOutput));
            }
            return res;
        }

        private int GetLetterRow(char character)
        {
            foreach (var key in _keyboardService.Keyboard.Keys)
            {
                if (_keyboardService.Keyboard[key].Values.Any(x => x == character))
                {
                    return key;
                }
            }

            throw new ArgumentException($"Character {character} not found in keyboard");
        }

        private int GetLetterColumn(int row, char character)
        {
            foreach (var key in _keyboardService.Keyboard[row].Keys)
            {
                if (_keyboardService.Keyboard[row][key] == character)
                {
                    return key;
                }
            }

            throw new ArgumentException($"Character {character} not found in keyboard");
        }
    }
}

