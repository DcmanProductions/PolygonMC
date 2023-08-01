function CloseAlert(alert) {
    alert = $(alert);
    alert.addClass('remove');
    setTimeout(() => alert.remove(), 1000);
}