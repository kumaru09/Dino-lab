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

function setDataBookingPage() {
  var d = new Date();
  var maxd = new Date();
  var t = d.toTimeString().slice(0,3);

  //set input time
  document.getElementById("inputTime").value = document.getElementById("inputTime").min;
  if ( parseInt(t) < 16 ) {
    if( parseInt(t) >= 9) {
      document.getElementById("inputTime").value = d.toTimeString().slice(0,3) + "00";
      var maxHour = 16 - parseInt(t);
      document.getElementById("inputHr").max = maxHour.toString();
    }
  }
  else {
    d.setDate(d.getDate()+1);
    maxd.setDate(maxd.getDate()+1);
  }

  //set input date
  maxd.setDate(maxd.getDate() + 6);
  document.getElementById("inputDay").min = d.toISOString().slice(0,10);
  document.getElementById("inputDay").max = maxd.toISOString().slice(0,10);
  document.getElementById("inputDay").value = document.getElementById("inputDay").min;
  
  //set table date
  var id,i;
  for(i=1; i<=7; i++){
    id = "d" + i.toString();
    document.getElementById(id).innerHTML = d.toDateString().slice(0,10);
    d.setDate(d.getDate() + 1);
  }
}

function timeStepUp() {
  document.getElementById("inputTime").stepUp(60);
  //update inputHr max
  var num = parseInt(document.getElementById("inputHr").max);
  if (num > 1) {
    num -= 1;
    document.getElementById("inputHr").max = num.toString();
    if(document.getElementById("inputHr").value > document.getElementById("inputHr").max) {
      document.getElementById("inputHr").value = document.getElementById("inputHr").max;
    }
  }
}

function timeStepDown() {
  document.getElementById("inputTime").stepDown(60);
  //update inputHr max
  var num = parseInt(document.getElementById("inputHr").max);
  if (num < 7) {
    num += 1;
    document.getElementById("inputHr").max = num.toString();
  }
}