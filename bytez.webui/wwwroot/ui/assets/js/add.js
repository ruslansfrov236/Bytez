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
const imgs = document.querySelectorAll('.img-select a');
const imgBtns = [...imgs];
let imgId = 1;

imgBtns.forEach((imgItem) => {
    imgItem.addEventListener('click', (event) => {
        event.preventDefault();
        imgId = imgItem.dataset.id;
        slideImage();
    });
});

function slideImage() {
    const displayWidth = document.querySelector('.img-showcase img:first-child').clientWidth;

    document.querySelector('.img-showcase').style.transform = `translateX(${- (imgId - 1) * displayWidth}px)`;
}

window.addEventListener('resize', slideImage);