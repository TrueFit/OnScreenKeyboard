app.service('animationService', function () {

    this.animationCount = 0;

    this.baseDelay = 200;

    this.animationTime = 500;

    this.queueAnimation = function (selector,animation) {
        
        var delay = this.baseDelay * this.animationCount;
        this[animation](selector, delay);
        this.animationCount++;

    };

    this.pulseBorder = function (selector, delay) {
        setTimeout(function () {

            $(selector)
            .animate({
                borderColor: "#158cba"
            }, this.animationTime)
            .animate({
                borderColor: "#e2e2e2"
            }, this.animationTime);

        }, delay);

    }

    this.pulseBackground = function (selector, delay) {
        setTimeout(function () {

            $(selector)
            .animate({
                backgroundColor: "#76BEF7",
                borderColor: "#3F9FEC"
            }, this.animationTime)
            .animate({
                backgroundColor: "#eeeeee",
                borderColor: "#e2e2e2"
            }, this.animationTime);

        }, delay - 100);
    }


});