document.addEventListener("DOMContentLoaded", function () {
    const sideNav_toggleBtn = document.getElementById("sideNav_toggleBtn");
    const sideNavBar = document.getElementById("sidenavBar");
    const overlay = document.getElementById("overlay");
    const navLinks = document.querySelectorAll(".navItem"); // Select all <a> links
    let isSideNavOpen = false; // Track sidebar state

    // Toggle sidebar and overlay when clicking the button
    if (sideNav_toggleBtn && sideNavBar && overlay) {
        sideNav_toggleBtn.addEventListener("click", function (event) {
            isSideNavOpen = !isSideNavOpen; // Toggle state
            sideNavBar.classList.toggle("show", isSideNavOpen); // Update class
            overlay.classList.toggle("active", isSideNavOpen); // Toggle overlay
            event.stopPropagation();
        });
    }

    // Ensure clicking inside the sidebar doesn't close it
    sideNavBar.addEventListener("click", function (event) {
        event.stopPropagation();
    });

    // Handle navigation click events
    navLinks.forEach(link => {
        link.addEventListener("click", function (event) {

            // Remove 'active' class from all nav items
            navLinks.forEach(nav => nav.classList.remove("active"));
            this.classList.add("active"); // Add active class to the clicked link
            console.log("Clicked:", this, "Has class active:", this.classList.contains("active"));

            // Load the new page dynamically (Without full reload)
            const targetPage = this.getAttribute("href");
            loadContent(targetPage);

            // Close the sidebar and overlay after navigation
            sideNavBar.classList.remove("show");
            overlay.classList.remove("active");
            isSideNavOpen = false; // Update state
        });
    });

    // Close sidebar and overlay when clicking outside
    document.addEventListener("click", function (event) {
        if (!sideNavBar.contains(event.target) && !sideNav_toggleBtn.contains(event.target)) {
            sideNavBar.classList.remove("show");
            overlay.classList.remove("active");
            isSideNavOpen = false; // Update state
        }
    });

    // Close sidebar and overlay when clicking on the overlay
    overlay.addEventListener("click", function () {
        sideNavBar.classList.remove("show");
        overlay.classList.remove("active");
        isSideNavOpen = false; // Update state
    });

    // Function to load content dynamically (without full reload)
    function loadContent(url) {
        fetch(url)
            .then(response => response.text())
            .then(data => {
                const tempDiv = document.createElement("div");
                tempDiv.innerHTML = data;

                // Ensure we're only replacing the main content, not the sidebar
                const newContent = tempDiv.querySelector("#mainContent");
                if (newContent) {
                    document.getElementById("mainContent").innerHTML = newContent.innerHTML;
                }

                history.pushState(null, "", url); // Update URL without refreshing
            })
            .catch(error => console.error("Error loading page:", error));
    }
});