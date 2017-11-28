$(document).ready(function () {
    var watcher = function () {
        var content = ('#content');
        $.ajax({
            type: 'GET',
            url: $(content).data('url') + '/api/application/getdata',
            dataType: "json",
            async: false,
            success: function (data) {
                if (data != null)
                {
                  $(content).empty();
                  $(content).append('Data: ' + data);
                }
            }
        }).always(function() {
            setTimeout(watcher, 3000);
        });;
    };

    watcher();

});
