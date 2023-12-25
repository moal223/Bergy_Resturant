// Sticky Header
let header = document.querySelector(".header");
window.addEventListener("scroll", function () {
  if (this.window.scrollY > 70) {
    header.classList.add("sticky");
  } else {
    header.classList.remove("sticky");
  }
});

// Search
let search = document.querySelector(".search-icon");
let searchBox = document.querySelector(".search-box");
let closeSearch = document.querySelector(".close-search");
let overlay = document.querySelector(".overlay");
search.addEventListener("click", function () {
  searchBox.classList.add("active");
  overlay.classList.add("active");
});

closeSearch.addEventListener("click", function () {
  searchBox.classList.remove("active");
  overlay.classList.remove("active");
});

//Menu
let links = document.querySelector(".links");
let menuIcon = document.querySelector(".menu-icon");
let closeMenu = document.querySelector(".close-menu");
menuIcon.addEventListener("click", function () {
  links.classList.add("show");
});

closeMenu.addEventListener("click", function () {
  links.classList.remove("show");
});

// Chevron Up
let up = document.querySelector(".up");
window.addEventListener("scroll", function () {
  if (this.window.scrollY > 100) {
    up.classList.add("show");
  } else {
    up.classList.remove("show");
  }
});

up.addEventListener("click", function () {
  window.scrollTo({
    top: 0,
    left: 0,
    behavior: "smooth",
  });
});

// Add to Favorite Click
let heartIcon = document.querySelectorAll(".heart-icon");
heartIcon.forEach(function (e) {
  e.addEventListener("click", function (e) {
    e.target.parentNode.querySelector("i").classList.toggle("fas");
  });
});

// Filter Menu
let allListItems = document.querySelectorAll(".list-items");
let allDishes = document.querySelectorAll(".menu .dish");

// Add click Lister event to each filter item in list
allListItems.forEach((btn) => {
  btn.addEventListener("click", removeActive);
  btn.addEventListener("click", filterDishes);
});

// remove active class and active class to the cliked item
function removeActive() {
  document.querySelector(".list .active").classList.remove("active");
  this.classList.add("active");
}

// Filter all dishes 
function filterDishes(item) {

  // remove all dishes  
  allDishes.forEach((dish) => {
    dish.style.display = "none";
  });
  // appear only the dishes related to the item in the list
  document.querySelectorAll(this.dataset.filter).forEach((dish) => {
    dish.style.display = "block";
  });
}


// Swiper js 
let swiperCards = new Swiper('.swiper', {
  loop: true,
  spaceBetween: 32,
  spaceBetween: 30,
  centeredSlides: true,
  autoplay: {
    delay: 2500,
    disableOnInteraction: false,
  },

  pagination: {
    el: '.swiper-pagination',
    clickable: true,
  },

  breakpoints: {
    767: {
      slidesPerView: 3,
    }
  },
  navigation: {
    nextEl: '.swiper-button-next',
    prevEl: '.swiper-button-prev',
  },
});

// Events Number Counter 
let value = document.querySelectorAll(".num");
let interval = 6000;
value.forEach(function (value) {
    let start = 0;
    // Begin
    let end = value.getAttribute("data-val"); // End = dat-val in Html
    let duration = Math.floor(interval / end); // Increment , to be the Same Increment
    let counter = setInterval(function () { // Code
        start += 1;
        value.textContent = start;
        if (start == end) {
            clearInterval(counter);
        }
    }, duration)
});


// Add Active Class On Scroll to Navbar
let sections = document.querySelectorAll("section");

window.addEventListener("scroll" , function(){
    let scrollPosition = document.documentElement.scrollTop;

    sections.forEach(section => {
        if (scrollPosition > section.offsetTop &&  scrollPosition < section.offsetTop + section.offsetHeight){
           let currentId = section.attributes.id.value;
            removeAllActives();
            addActiveClass(currentId);
        }
    });
});

let removeAllActives = function(){
    // Remove active class from all Lis
    document.querySelectorAll("li a").forEach(ele => {
        ele.classList.remove("active");
    });
};

let addActiveClass = function(id){
    // Choose a to add active class to it.
    let select = `li a[href = "#${id}"]`; 
    document.querySelector(select).classList.add("active");
    
};

