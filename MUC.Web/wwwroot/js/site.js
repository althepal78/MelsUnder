// for the hambuger button
const hamburgerButton = document.querySelector(".hamburger-button")
const isActive = document.querySelector(".navbar-links");

try {
    hamburgerButton.addEventListener("click", () => {
        const isOpened = hamburgerButton.getAttribute('aria-expanded');
        
        if (isOpened === 'false') {
            hamburgerButton.setAttribute('aria-expanded', 'true')
            isActive.classList.toggle('active');
        }
        else if(isOpened === "true"){
            hamburgerButton.setAttribute('aria-expanded', 'false')
            isActive.classList.remove('active');
        }
       
    })
} catch (err) {
    console.log("Hamburger Error: " + err )
}

// This is for the dropdown menu in the nav

try {
    document.querySelector("[data-special]").addEventListener('click', e => {
      
        const isDropdownButton = e.target.matches("[data-dropdown-button]");
        if (!isDropdownButton && e.target.closest("[data-dropdown]" != null)) return

        let currentDropdown;
        if (isDropdownButton) {
            currentDropdown = e.target.closest("[data-dropdown]")
            currentDropdown.classList.toggle('active');
            return;
        } 
       //console.log(currentDropdown)       
    })
}
catch (err) {
    console.log("Dropdown Error: " + err )
}

try {
    document.addEventListener('click', e => {
        
        let data_drop_down = document.querySelector("[data-dropdown]");
        if (e.target != data_drop_down && !data_drop_down.contains(e.target)) 
            data_drop_down.classList.remove('active');
    })
}
catch (err) {
    console.log("Dropdown Error: " + err)
}

// this is for the the beautiful color on the cards

try {
    document.querySelector("#cards").onmousemove = e => {
        for (const card of document.querySelectorAll(".card")) {
            const rect = card.getBoundingClientRect(),
                x = e.clientX - rect.left,
                y = e.clientY - rect.top;

            card.style.setProperty("--mouse-x", `${x}px`);
            card.style.setProperty("--mouse-y", `${y}px`);
        };
    }
} catch(err) {
    console.log("Card beauty Error: " + err);
}


// this is for the date to be today's date lets see if it works
window.addEventListener('DOMContentLoaded', (event) => {
    document.getElementById("date").valueAsDate = new Date();
});

/// this is for the animation on the buttons since I have changed them all but the ones inside the navigation section
//try {
//    (function setGlowEffectRx() {
//        const glowEffects = document.querySelectorAll(".glow-effect");

//        glowEffects.forEach((glowEffect) => {
//            const glowLines = glowEffect.querySelectorAll("rect");
//            const rx = getComputedStyle(glowEffect).borderRadius;

//            glowLines.forEach((line) => {
//                line.setAttribute("rx", rx);
//            });
//        });
//    })();
//} catch (er) {
//    console.log("The Error is: " + er)
//}
//// for the navbar
//try {
//    (function setGlowEffectRx() {
//        const glowEffects = document.querySelectorAll(".nav-glow-effect");

//        glowEffects.forEach((glowEffect) => {
//            const glowLines = glowEffect.querySelectorAll("rect");
//            const rx = getComputedStyle(glowEffect).borderRadius;

//            glowLines.forEach((line) => {
//                line.setAttribute("rx", rx);
//            });
//        });
//    })();
//} catch (er) {
//    console.log("The Error is: " + er)
//}