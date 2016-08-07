$(function () {
    cvf.extensions.register(['Migration']);
    var x = $('.ibiza-table-menu tr');
    x.each(function () {
        $(this).click(function () {
            alert('x');
        });
    });
})