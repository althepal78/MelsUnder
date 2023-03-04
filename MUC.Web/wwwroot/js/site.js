

document.addEventListener("click", e => {
    // for the dropdown menu on the navbar

    try {
        const dropdownLinks = document.querySelector("[data-dropdown-links]");

        if (e.target.matches("[data-dropdown-button]")) {
            dropdownLinks.classList.toggle("is-visible");
            console.log("did it work")
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

    window.addEventListener('DOMContentLoaded', () => {
        document.getElementById("date").valueAsDate = new Date();
    });
