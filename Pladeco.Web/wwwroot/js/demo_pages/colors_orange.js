/* ------------------------------------------------------------------------------
*
*  # Orange palette colors
*
*  Demo JS code for colors_orange.html page
*
* ---------------------------------------------------------------------------- */

document.addEventListener('DOMContentLoaded', function() {


    // Selects
    // ------------------------------

    // Basic select2
    $('.select').select2({
        minimumResultsForSearch: Infinity,
        containerCssClass: 'bg-green-700-600'
    });


    // Select2 ultiselect item color
    $('.select-item-color').select2({
        containerCssClass: 'bg-green-700-600'
    });


    // Select2 dropdown menu color
    $('.select-menu-color').select2({
        containerCssClass: 'bg-green-700-600',
        dropdownCssClass: 'bg-green-700-600'
    });


    // Multiselect
    $('.multiselect').multiselect({
        buttonClass: 'btn bg-green-700-600',
        nonSelectedText: 'Select your state'
    });


    // SelectBoxIt
    $(".selectbox").selectBoxIt({
        autoWidth: false,
        theme: "bootstrap"
    });


    // Bootstrap select
    $.fn.selectpicker.defaults = {
        iconBase: '',
        tickIcon: 'icon-checkmark-circle'
    }
    $('.bootstrap-select').selectpicker();



    // Notifications
    // ------------------------------

    // jGrowl
    $('.growl-launch').on('click', function () {
        $.jGrowl('I am a well highlighted orange notice..', { theme: 'bg-green-700', header: 'Well highlighted' });
    });


    // PNotify
    $('.pnotify-launch').on('click', function () {
        new PNotify({
            title: 'Info Notice',
            text: 'Check me out! I\'m a notice.',
            icon: 'icon-info22',
            delay: 5000,
            addclass: 'bg-green-700'
        });
    });



    // Form components
    // ------------------------------

    // Switchery toggle
    var switchery = document.querySelector('.switch');
    var init = new Switchery(switchery, {color: '#FB8C00'});


    // Checkboxes and radios
    $(".styled").uniform({
        wrapperClass: "border-orange text-orange-600"
    });


    // File input
    $(".file-styled").uniform({
        fileButtonClass: 'action btn bg-green-700'
    });



    // Popups
    // ------------------------------

    // Tooltip
    $('[data-popup=tooltip-custom]').tooltip({
        template: '<div class="tooltip"><div class="bg-green-700-600"><div class="tooltip-arrow"></div><div class="tooltip-inner"></div></div></div>'
    });


    // Popover title
    $('[data-popup=popover-custom]').popover({
        template: '<div class="popover border-orange-600"><div class="arrow"></div><h3 class="popover-title bg-green-700-600"></h3><div class="popover-content"></div></div>'
    });


    // Popover background color
    $('[data-popup=popover-solid]').popover({
        template: '<div class="popover bg-green-700-600"><div class="arrow"></div><h3 class="popover-title"></h3><div class="popover-content"></div></div>'
    });

});
