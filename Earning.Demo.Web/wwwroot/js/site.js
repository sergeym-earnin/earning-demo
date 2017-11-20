$(document).ready(function () {
    var content = ('#Content');
    $.ajax({
        type: 'GET',
        url: $(content).data('url') + '/api/values',
        dataType: "json",
        async: false,
        success: function (data) {
            console.log(data);
            $(content).empty();
            Object.keys(data).forEach(function (item) {
                $(content).append('<li><h4>' + item + ':<\h4>' + data[item] + '</li>');
            });
        }

    });
});
