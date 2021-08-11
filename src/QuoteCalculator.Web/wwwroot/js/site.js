// helper namespace

var QuoteCalculator = {

    // ** namespace pattern
    namespace: function (name) {

        // ** single var pattern
        var parts = name.split(".");
        var ns = this;

        // ** iterator pattern
        for (var i = 0, len = parts.length; i < len; i++) {
            // ** || idiom
            ns[parts[i]] = ns[parts[i]] || {};
            ns = ns[parts[i]];
        }

        return ns;
    }
};

// Quote Page
QuoteCalculator.namespace("Quote").Page = (function () {
    var init = function () {
        $("#calcQuote").on("click", function (event) {
            $('form').attr('action', '/').submit();
        });
    };

    return {
        init: init,
    };
})();

// Apply Loan Page
QuoteCalculator.namespace("Apply").Page = (function () {
    var init = function () {
        $("#quoteComputation").addClass('d-none');
        $("#viewComputation").text('View Computation');

        $("#viewComputation").on("click", function (event) {
            if ($("#quoteComputation").hasClass('d-none')) {
                $("#quoteComputation").removeClass('d-none');
                $(this).text('Hide Computation');
            }
            else {
                $("#quoteComputation").addClass('d-none');
                $(this).text('View Computation');
            }
        });

        $("#applyLoan").on("click", function (event) {
            $('form').attr('action', '/apply').submit();
        });

        $("#editDetails").on("click", function (event) {
            $('form').attr('action', '/editquote').submit();
        });
    };

    return {
        init: init,
    };
})();