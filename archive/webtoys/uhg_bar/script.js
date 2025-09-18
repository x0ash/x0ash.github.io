var bar = document.getElementById("barDiv");
var barContent = document.getElementById("barInner");
var waitingTime = 0.05;

var colorPicker = document.getElementById("colorPicker");
var msgColorPicker = document.getElementById("msgColorPicker");
var hexArea = document.getElementById("hexArea");
var msgHexArea = document.getElementById("msgHexArea");
var channelName = document.getElementById("channelName");
var previewText = document.getElementById("previewText");
var msgPreviewText = document.getElementById("msgPreviewText");
var barWatchingText = document.getElementById("barWatchingText");
var waitDuration = document.getElementById("waitDuration");
var barCustomInput = document.getElementById("customMessageInput");
var barCustomMessage = document.getElementById("barCustomMessage");

var finalURL = document.getElementById("finalURL");
var timesRun = 0;

var args = new URLSearchParams(window.location.search);
var timings = [2, 3, 2.5, 7, 4];
var totalTiming = 17;
var lineCount = 4;
var currentLine = 1;

var defaultTransitionDuration = barContent.style.transitionDuration;

if (args.get("name") != null) {
    barWatchingText.innerHTML = "You're watching " + args.get("name") + "!";
    channelName.value = args.get("name");
}

else {
    channelName.value = "[Name]";
}

if (args.get("color") != null) {
    if (args.get("color") == "rainbow") {
        barWatchingText.style.animation = "rainbow 1s ease";
        barWatchingText.style.animationIterationCount = "infinite";
    }
    else {
        hexArea.value = args.get("color");
        colorPicker.value = "#" + args.get("color");
        barWatchingText.style.color = "#" + args.get("color");
    }
}

if (args.get("time") != null) {
    waitingTime = parseFloat(args.get("time"));
    waitDuration.value = args.get("time");
}

if (args.get("customMsg") != null) {
    barCustomMessage.innerHTML = args.get("customMsg");
    barCustomInput.value = args.get("customMsg");
}

else {
    barCustomInput.value = "Custom Message";
}

if (args.get("msgColor") != null) {
    barCustomMessage.style.color = "#" + args.get("msgColor");
    msgColorPicker.value = "#" + args.get("msgColor");
    msgHexArea.value = args.get("msgColor");
}

updatePreview();
updateCustomMessagePreview();

if (barCustomMessage.innerHTML != "") {
    lineCount = 5;
    totalTiming = timings.reduce((x, y) => x + y, 0) + (0.5 * lineCount) + 0.5;
    bar.style.animation = "bar " + totalTiming.toString() + "s ease";
}

var barAnimation = bar.style.animation;

//barContent.style.animation = "barContent 20s ease";
bar.addEventListener("animationstart", function () {
    animLoop();
}, false);

bar.addEventListener("animationend", function () {
    bar.style.animation = "none";
    bar.style.transform = "translateY(30px)";
    timesRun += 1;
    setTimeout(function () {

        if (timesRun == (Math.round(60 / waitingTime)))         //At the minute this should be set to refresh almost every hour.
        {
            location.reload(true);      //Auto refresh (just in case I decide to push any updates while someone's stream is running. Shouldn't pose too much of an issue.)
        }

        bar.style.animation = barAnimation;
        bar.style.transform = "";
    }, waitingTime * 60000);
}, false);

function animLoop() {
    setTimeout(function () {
        if (currentLine < lineCount) {
            barContent.style.transitionDuration = defaultTransitionDuration;
            barContent.style.transform = "translateY(" + (-30 * currentLine).toString() + "px)";
            currentLine += 1;
            //console.log(-33 * currentLine);
            animLoop();
        }
        else {
            barContent.style.transitionDuration = "1.3s";
            barContent.style.transform = "translateY(0px)";
            currentLine = 1;
        }
    }, timings[currentLine - 1] * 1000);
}

function updatePreview() {
    previewText.innerHTML = "You're watching " + channelName.value + "!";
    previewText.style.color = "#" + hexArea.value;
}

function updateCustomMessagePreview() {
    msgPreviewText.innerHTML = barCustomInput.value;
    msgPreviewText.style.color = "#" + msgHexArea.value;
}

function createURL() {
    finalURL.innerHTML = window.location.href.replace(window.location.search, "") + "?name=" + channelName.value + "&color=" + hexArea.value + "&time=" + waitingTime.toString();
    if (barCustomInput.value != "Custom Message" && barCustomInput.value != "") {
        finalURL.innerHTML += "&customMsg=" + barCustomInput.value.toString() + "&msgColor=" + msgHexArea.value;
    }
}