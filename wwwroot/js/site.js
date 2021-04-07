// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var loginModal = document.getElementById("loginModal");
var loginBtn = document.getElementById("loginBtn");
var regModal = document.getElementById("regModal");
var regBtn = document.getElementById("regBtn");
var span = document.getElementsByClassName("close");
var loginLink = document.getElementById("loginLink");
var regLink = document.getElementById("regLink")

loginBtn.onclick = () => {
  loginModal.style.display = "block";
};
regBtn.onclick = () => {
  regModal.style.display = "block";
};
span.onclick = () => {
  loginModal.style.display = "none";
  regModal.style.display = "none";
};
window.onclick = (event) => {
  if (event.target == loginModal) loginModal.style.display = "none";
  if (event.target == regModal) regModal.style.display = "none";
};

loginLink.onclick = () => {
  regModal.style.display = "none";
  loginModal.style.display = "block";
}

regLink.onclick = () => {
  loginModal.style.display = "none";
  regModal.style.display = "block";
}