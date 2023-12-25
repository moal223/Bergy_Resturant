let adminBtn = document.getElementById("admin");
let adminAuth = document.querySelector(".admin-auth");

adminBtn.addEventListener("click", ()=> {
    adminAuth.classList.toggle("active");
});