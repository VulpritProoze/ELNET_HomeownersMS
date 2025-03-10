document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    
    if (calendarEl) {  // Ensure the calendar element exists before initializing
        var calendar = new FullCalendar.Calendar(calendarEl, {
            initialView: 'dayGridMonth',
            events: [
                { title: 'HOA Meeting', start: '2025-02-15' },
                { title: 'Maintenance Work', start: '2025-02-20' },
                { title: 'Security Drill', start: '2025-02-25' }
            ]
        });
        calendar.render();
    }
});
