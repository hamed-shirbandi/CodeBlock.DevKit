window.Helpers = {
    RedirectTo: function (path) {
        window.location = path;
    },
    ShowLoading: function () {
        displayLoading('block');
    },
    HideLoading: function () {
        displayLoading('none');
    },
    DisplayLoading: function (display) {
        displayLoading(display);

    }
};

function displayLoading(display) {
    $('.spinner-loading').css('display', display);
}

(function () {
    displayLoading('none');
})();