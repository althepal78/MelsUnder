
// hamburger menu 
const hamburger = document.querySelector(".hamburger");
const navMenu = document.querySelector(".nav-menu");
hamburger.addEventListener('click', () => {
    hamburger.classList.toggle("active");
    navMenu.classList.toggle("active");
})

document.addEventListener("click", e => {
    // for the dropdown menu on the navbar

    try {
        const dropdownLinks = document.querySelector("[data-dropdown-links]");
        const logoutBtn = document.querySelector("[data-logout]");

        if (e.target.matches("[data-dropdown-button]")) {
            dropdownLinks.classList.toggle("is-visible");
            logoutBtn.classList.toggle('d-none');
            
        }

        if (!e.target.matches("[data-dropdown-button]") && dropdownLinks.classList.contains("is-visible")) {
            dropdownLinks.classList.remove("is-visible");
        }
    }catch (error) {
        console.log(error + ":  **** is the freaking error")
    }
})


  // this is for the cards section
try {
    document.querySelector("#cards").onmousemove = e => {
        for (const card of document.querySelectorAll(".card")) {
            const rect = card.getBoundingClientRect(),
                x = e.clientX - rect.left,
                y = e.clientY - rect.top;

            card.style.setProperty("--mouse-x", `${x}px`);
            card.style.setProperty("--mouse-y", `${y}px`);
        }
    }
} catch (err) {
    console.log("Card beauty Error: " + err);
}

  /// just for the date
try {
    // Get today's date in YYYY-MM-DD format
    const today = new Date().toISOString().substr(0, 10);

    // Set the value of the input to today's date
    document.getElementById("inputDate").value = today;

} catch (error) {
    // Code to handle the exception
    console.error("An error occurred:" + error);
}

// for the hamburger menu
