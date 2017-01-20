var keyboardConfig;

$(function () {
    searchConfig = [];
    initializeKeyboard();
    keyboardConfig = [];
    keyboardConfig["Type"] = "0";
    keyboardConfig["Delimeter"] = "";
    keyboardConfig["Term"] = "";
    keyboardConfig["Path"] = "";
    $("#btnGeneratePath").click(function () {
        generatePath();
    });
});
function initializeKeyboard() {
    $("#ddlKeyboardImplementation").change(function () {
        var type = $(this).val();
        if (type != keyboardConfig["Type"]) {
            keyboardConfig["Type"] = type;
            if (keyboardConfig["Type"] == "0") {
                $("#keyboardContainer").empty();
            }
            else {
                $.ajax({
                    type: "GET",
                    url: "/api/Keyboard/GetKeyboard?type=" + keyboardConfig["Type"],
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (data) {
                        buildKeyboard(data);
                    }
                });
            }
        }
    });
}
function buildKeyboard(data) {
    $("#keyboardInfo").text(data.information);
    var keyboardHTML = "";
    for (var y = 0; y < data.numberOfRows; y++) {
        keyboardHTML += "<div class='keyboardRow'>";
        for (var x = 0; x < data.numberOfColumns; x++) {
            keyboardHTML += "<div class='keyboardKey' id='key_" + x.toString() + "_" + y.toString() + "'>" + returnKeyAt(data.keyboardCharacters, x, y) + "</div>";
        }
        keyboardHTML += "</div>";
    }
    $("#keyboardContainer").html(keyboardHTML);
}
function returnKeyAt(characters, x, y) {
    for (var z = 0; z < characters.length; z++) {
        if (characters[z].location.xCoord == x && characters[z].location.yCoord == y) {
            return characters[z].character;
        }
    }
}
function generatePath() {
    if (keyboardConfig["Type"] == "0") {
        alert("Please select a keyboard implementation.");
        return;
    }
    var term = $("#txtSearchTerm").val();
    if (term.length < 1) {
        alert("Please enter a search term.");
        return;
    }
    keyboardConfig["Term"] = term;
    var delimeter = $("#txtDelimeter").val();
    if (delimeter.length < 1) {
        delimeter = ",";
    }
    keyboardConfig["Delimeter"] = delimeter;
    $.ajax({
        type: "GET",
        url: "/api/Keyboard/GetSearchPath?type=" + keyboardConfig["Type"] + "&term=" + encodeURIComponent(keyboardConfig["Term"]) + "&delimeter=" + encodeURIComponent(keyboardConfig["Delimeter"]),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#pathInfo").text(data.searchPath);
            keyboardConfig["Path"] = data.searchPath;
            visualizePath();
        }
    });
}
function visualizePath(path) {
    var xPos = 0;
    var yPos = 0;
    var pathOnly = keyboardConfig["Path"].split(keyboardConfig["Delimeter"]);

    var animationSequence = [];
    animationSequence.push({ e: $("#key_0_0"), p: { opacity: 0 }, o: { duration: 500 } });
    animationSequence.push({ e: $("#key_0_0"), p: { opacity: 1 }, o: { duration: 500 } });
    for (var x = 0; x < pathOnly.length; x++) {
        var direction = pathOnly[x];
        if (direction == "#") {
            var keyID = "#key_" + xPos.toString() + "_" + yPos.toString();
            animationSequence.push({ e: $(keyID), p: { backgroundColor: "#ff6a00" }, o: { duration: 500 } });
            animationSequence.push({ e: $(keyID), p: { rotateZ: "360deg" }, o: { duration: 500 } });
            animationSequence.push({ e: $(keyID), p: { backgroundColor: "#ffffff" }, o: { duration: 500 } });
        }
        else {
            if (direction == "U") {
                yPos = yPos - 1;
            }
            else if (direction == "R") {
                xPos = xPos + 1;
            }
            else if (direction == "D") {
                yPos = yPos + 1;
            }
            else if (direction == "L") {
                xPos = xPos - 1;
            }
            var keyID = "#key_" + xPos.toString() + "_" + yPos.toString();
            animationSequence.push({ e: $(keyID), p: { opacity: 0 }, o: { duration: 500 } });
            animationSequence.push({ e: $(keyID), p: { opacity: 1 }, o: { duration: 500 } });
        }
    }
    $.Velocity.RunSequence(animationSequence);
}