// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function setHoverReactForListItem(root = $(document)) {
    root.find('.list-group-item-action').hover(function () {
        $(this).addClass('active');
    }, function () {
        $(this).removeClass('active');
    });
}
$(document).ready(setHoverReactForListItem());
