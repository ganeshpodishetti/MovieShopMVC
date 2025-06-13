// JavaScript for Movie Shop

// Initialize tooltips
document.addEventListener('DOMContentLoaded', function () {
    // Any Bootstrap initializations
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    const tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    });

    // Add any other JavaScript functionality here
});
