var displayDate = function (date) {
    if (!date) return '';
    var time = moment.utc(date);
    var displayFormat = 'ddd - Do MMM YYYY hh:mm:ss A';
    return time.local().format(displayFormat);
}