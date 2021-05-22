// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var loginModal = document.getElementById("loginModal");
var loginBtn = document.getElementById("loginBtn");
var regModal = document.getElementById("regModal");
var regBtn = document.getElementById("regBtn");
var span = document.getElementsByClassName("close");
var loginLink = document.getElementById("loginLink");
var regLink = document.getElementById("regLink");

var inputTime = document.getElementById("inputTime");
var inputHr = document.getElementById("inputHr");
var inputHrMaxInt = parseInt(inputHr.max);

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


var tabItem = document.getElementsByClassName("tablist-item");
for (var i = 0; i < tabItem.length; i++) {
  tabItem[i].addEventListener("click", function() {
  var current = document.getElementsByClassName("tab-active");
  current[0].className = current[0].className.replace(" tab-active", "");
  this.className += " tab-active";
  });
}

function updateMinTime(){
  var today = new Date();
  var inputDay = document.getElementById("inputDay");
  if(inputDay == today.toISOString().slice(0,10)){
    inputTime.min = today.toTimeString().slice(0,3) + "00";
    if(inputTime.value < inputTime.min){
      inputTime.value = inputTime.min;
    }
  }
  else{
    inputTime.min = "09:00";
  }
}

function timeStepUp() {
  if(inputTime.value != inputTime.max){
    inputTime.stepUp(60);
     //update inputHr max
    if (inputHrMaxInt > 1) {
      inputHrMaxInt -= 1;
      inputHr.max = inputHrMaxInt.toString();
      if(inputHr.value > inputHr.max) {
        inputHr.value = inputHr.max;
      }
    }
  } 
}

function timeStepDown() {
  if(inputTime.value != inputTime.min){
    inputTime.stepDown(60);
     //update inputHr max
    if (inputHrMaxInt < 7) {
      inputHrMaxInt += 1;
      inputHr.max = inputHrMaxInt.toString();
    }
  } 
}