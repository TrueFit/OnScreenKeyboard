using System;
using FluentValidation;

namespace OnScreenKeyboard.Application.Business.Keyboard.Requests
{
    //Depending on use case I could do regex checks here if needed, but with a dynamic keyboard object
    //it's probably better off doing it in the mediatr request
    //This will fire off as soon as the request object is initialized in the controller
    public class GetDirectionFromInputRequestValidator : AbstractValidator<GetDirectionFromInputRequest>
    {
        public GetDirectionFromInputRequestValidator()
        {
            RuleFor(x => x.FileContents)
                .NotEmpty().WithMessage("File contents are required");

        }
    }
}

