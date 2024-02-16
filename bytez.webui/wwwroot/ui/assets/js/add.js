const drapdown = document.querySelector(".drapdown");
const userLink = document.querySelector(".user-link");
userLink.addEventListener("click", () => {
    drapdown.style.display = (drapdown.style.display === "none") ? "flex" : "none";
});

