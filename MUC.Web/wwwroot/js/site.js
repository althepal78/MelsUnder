document.addEventListener("click", e => {
   
    const dropdownLinks = document.querySelector("[data-dropdown-links]");   

    if (e.target.matches("[data-dropdown-button]")) {
        dropdownLinks.classList.toggle("is-visible");
        console.log("did it work")
    }

    if (!e.target.matches("[data-dropdown-button]") && dropdownLinks.classList.contains("is-visible")) {
        dropdownLinks.classList.remove("is-visible");
    }
   
})

