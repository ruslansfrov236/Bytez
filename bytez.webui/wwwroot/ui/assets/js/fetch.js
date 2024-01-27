const drapdown = document.querySelector(".drapdown");
const userLink = document.querySelector(".user-link");

const DrapdownContent = () => {
   
    const computedStyle = getComputedStyle(drapdown);

    if (computedStyle.display === "none") {
        drapdown.style.display = "flex";
    } else {
        drapdown.style.display = "none";
    }
};

userLink.addEventListener("click", DrapdownContent);
