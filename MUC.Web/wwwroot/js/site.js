// JavaScript Goes here
document.addEventListener("click", e => {
    // for the dropdown menu on the navbar
    try {
        const dropdownLinks = document.querySelector("[data-dropdown-links]");
        const logoutBtn = document.querySelector("[data-logout]");
        const dropdownBtn = document.querySelector('[data-dropdownbutton]')

        if (e.target.matches("[data-dropdown-button]")) {
            dropdownLinks.classList.toggle('md:hidden');
        }

        if (!e.target.matches("[data-dropdown-button]") && !dropdownLinks.classList.contains("md:hidden")) {
            dropdownLinks.classList.add('md:hidden');
        }
    } catch (error) {
        console.log(error + ":  **** is the freaking error")
    }

    try {
        // this is for the hamburger menu
        const hamBtn = document.querySelector('[data-hamburger-button]');
        const mobileNav = document.querySelector('[data-mobileNav]');

        if (e.target === hamBtn) {
            console.log('we here')
            mobileNav.classList.toggle("hidden");
            mobileNav.classList.toggle("flex");
            if (hamBtn.textContent.trim() === "☰") {

                hamBtn.textContent = "🗙"
            } else {
                hamBtn.textContent = "☰"
            }
        } else if (mobileNav.classList.contains("flex")) {
            mobileNav.classList.add("hidden");
            mobileNav.classList.remove("flex");
            hamBtn.textContent = "☰"
        }
    } catch (err) {
        console.error(err)
    }    

})

/// just for the date

try {
    // Get today's date in YYYY-MM-DD format
    const today = new Date().toISOString().substr(0, 10);

    // Set the value of the input to today's date
    document.getElementById("inputDate").value = today;

} catch (error) {
    // Code to handle the exception
    console.error("An error occurred: " + error);
}

