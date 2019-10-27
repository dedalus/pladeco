/* ------------------------------------------------------------------------------

* ---------------------------------------------------------------------------- */

document.addEventListener('DOMContentLoaded', function () {

    $('.select').select2({
        placeholder: '',
        allowClear: true   // Shows an X to allow the user to clear the value.
    });

    $('.select-enum').select2();

    $('.select-multiple').select2({
        minimumResultsForSearch: Infinity
    });

    // Custom results color
    $('.select-results-color').select2({
        containerCssClass: 'bg-grey-600'
    });

    // Menu border and text color
    $('.select-border-color').select2({
        dropdownCssClass: 'border-primary',
        containerCssClass: 'border-primary text-primary-700'
    });
});
