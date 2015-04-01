$(document).ready(function () {
    $('#keyboard-input-button').click(function (event) {
        event.preventDefault;
        var input = $('#keyboard-input').val();
        var result = getPath(input);
    });
});

function getPath(input){
    
}





//Helper Methods

function getColumn(c) {
    var result = 0;
    switch (c) {
        case 'a':
        case 'g':
        case 'm':
        case 's':
        case 'y':
        case '5':
        result = 1;
        break;
        case 'b':
        case 'h':
        case 'n':
        case 't':
        case 'z':
        case '6':
        result = 2;
        break;
        case 'c':
        case 'i':
        case 'o':
        case 'u':
        case '1':
        case '7':
        result = 3;
        break;
        case 'd':
        case 'j':
        case 'p':
        case 'v':
        case '2':
        case '8':
        result = 4;
        break;
        case 'e':
        case 'k':
        case 'q':
        case 'w':
        case '3':
        case '9':
        result = 5;
        break;
        case 'f':
        case 'l':
        case 'r':
        case 'x':
        case '4':
        case '0':
        result = 6;
        break;
        default:
            result = 0; //for a space
        }
        return result;
    }

      function getRow(c) {
        var result = 0;
        switch (c) {
            case 'a':
            case 'b':
            case 'c':
            case 'd':
            case 'e':
            case 'f':
            result = 1;
            break;
            case 'g':
            case 'h':
            case 'i':
            case 'j':
            case 'k':
            case 'l':
            result = 2;
            break;
            case 'm':
            case 'n':
            case 'o':
            case 'p':
            case 'q':
            case 'r':
            result = 3;
            break;
            case 's':
            case 't':
            case 'u':
            case 'v':
            case 'w':
            case 'x':
            result = 4;
            break;
            case 'y':
            case 'z':
            case '1':
            case '2':
            case '3':
            case '4':
            result = 5;
            break;
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case '0':
            result = 6;
            break;
            default:
            result = 0; //for a space
        }
        return result;
    }