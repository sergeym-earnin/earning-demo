$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:50229/api/values',
        dataType: "json",
        async: false,
        success: function (data) {
            console.log(data);
            var content = ('#Content');
            $(content).empty();
            Object.keys(data).forEach(function (item) {
                $(content).append('<li><h4>' + item + ':<\h4>' + data[item] + '</li>');
            });
        }

    });
});
