ko.bindingHandlers.date = {
    update: function (element, valueAccessor, allBindings, viewModel, bindingContext) {
        var value = valueAccessor();
        var formatString = allBindings.get('formatString') || 'MM/DD/YYYY';
        if (value() == null) {
            $(element).text('');
            return;
        }
        var date = moment(value());
        if (formatString == null) {
            $(element).text(date.format('MM/DD/YYYY'));
        }
        else {
            $(element).text(date.format(formatString));
        }
    }
};