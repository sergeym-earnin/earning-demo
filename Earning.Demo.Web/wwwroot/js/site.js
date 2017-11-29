$(document).ready(function () {
    var watcher = function () {
        var content = ('#content');
        $.ajax({
            type: 'GET',
            url: $(content).data('url'),
            dataType: "json",
            async: false,
            success: function (payload) {
                if (payload != null)
                {
                  $(content).empty();
                  $(content).append('Data: ' + payload);
                }
            }
        }).always(function() {
            setTimeout(watcher, 3000);
        });;
    };

    watcher();

});
