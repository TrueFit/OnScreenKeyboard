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
            List<string> directions = new();
            foreach (var line in filesLines)
            {
                if (string.IsNullOrEmpty(line))
                    continue;

                var direction = _keyboard.GetDirectionFromInput(line);
                directions.Add(direction);
            }
            return directions;
        }


    }
}

