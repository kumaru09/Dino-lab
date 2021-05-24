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
const regRes = document.getElementById("formRes");
const regForm = document.getElementById("formContent")
const loginRes = document.getElementById("loginRes")
const loginForm = document.getElementById("loginForm")
const menu = document.getElementById("menu")
const userMenu = document.getElementById("userMenu")

loginBtn.onclick = () => {
  loginModal.style.display = "block";
  loginForm.style.display = "block"
};
regBtn.onclick = () => {
  regModal.style.display = "block";
  regForm.style.display = "Block"
};
const ModalClose = () => {
  loginModal.style.display = "none";
  regModal.style.display = "none";
};
window.onclick = (event) => {
  if (event.target == loginModal) {
    loginModal.style.display = "none";
    loginRes.style.display = "none"
  } 
  if (event.target == regModal) {
    regModal.style.display = "none";
    regRes.style.display = "none"
  } 
};

loginLink.onclick = () => {
  regModal.style.display = "none";
  loginModal.style.display = "block";
}

regLink.onclick = () => {
  loginModal.style.display = "none";
  regModal.style.display = "block";
}

async function register() {
  document.getElementById("errUsername").innerHTML = ""
  document.getElementById("errEmailReg").innerHTML = ""
  document.getElementById("errPasswordReg").innerHTML = ""
  document.getElementById("errReg").innerHTML = ""
  const input = {}
  input.Username = document.getElementById("RegUsername").value
  input.Email =  document.getElementById("RegEmail").value
  input.Password =  document.getElementById("RegPassword").value
  console.log(input)
  let config = {
    method: "POST",
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(input)
  };
  try {
    const response = await fetch("https://localhost:5001/Register", config)
    const data = await response.json()
    if ( data.errors != null )
    {
      if (data.errors.Username) document.getElementById("errUsername").innerHTML = data.errors.Username
      if (data.errors.Email) document.getElementById("errEmailReg").innerHTML = data.errors.Email
      if (data.errors.Password) document.getElementById("errPasswordReg").innerHTML = data.errors.Password
      if (!data.errors.Email && !data.errors.Password)document.getElementById("errReg").innerHTML = data.errors.toString().replace(/,/g,"<br>")
    }
    else
    {
      regForm.style.display = "none"
      regRes.style.display = "block"
      document.getElementById("errUsername").innerHTML = ""
      document.getElementById("errEmailReg").innerHTML = ""
      document.getElementById("errPasswordReg").innerHTML = ""
      document.getElementById("errReg").innerHTML = ""
    }
  } 
  catch (err) {
  }
}

async function Login() {
  document.getElementById("errEmail").innerHTML = ""
  document.getElementById("errPassword").innerHTML = ""
  document.getElementById("err").innerHTML = ""
  const input = {}
  input.Email = document.getElementById("LoginEmail").value
  input.Password = document.getElementById("LoginPassword").value
  let config = {
    method: "POST",
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(input)
  }
  try {
    const response = await fetch("https://localhost:5001/Login", config)
    const data = await response.json()
    if ( data.errors != null )
    {
      if (data.errors.Email) document.getElementById("errEmail").innerHTML = data.errors.Email
      if (data.errors.Password) document.getElementById("errPassword").innerHTML = data.errors.Password
      if (!data.errors.Email && !data.errors.Password)document.getElementById("err").innerHTML = data.errors
    }
    else
    {
      sessionStorage.setItem('user', JSON.stringify(data))
      loginRes.style.display = "block"
      loginForm.style.display = "none"
      document.getElementById("errEmail").innerHTML = ""
      document.getElementById("errPassword").innerHTML = ""
      document.getElementById("err").innerHTML = ""
      location.reload()
    }
  } 
  catch (err) {
  }
}

if(sessionStorage.getItem("user") != null) {
  menu.style.display = "none"
  userMenu.style.display = "block"
  let username = JSON.parse(atob(JSON.parse(sessionStorage.getItem('user')).token.split(".")[1]))
  userMenu.innerHTML = username.sub
}


var inputTime = document.getElementById("inputTime");
var inputHr = document.getElementById("inputHr");

function updateMinTime() {
  var today = new Date();
  var inputDay = document.getElementById("inputDay");
  if (inputDay.value == today.toISOString().slice(0, 10)) {
    inputTime.min = today.toTimeString().slice(0, 3) + "00";
    if (inputTime.value < inputTime.min) {
      inputTime.value = inputTime.min;
    }
  }
  else {
    inputTime.min = "09:00";
  }
}

function timeStepUp() {
  if (inputTime.value != inputTime.max) {
    inputTime.stepUp(60);
    //update inputHr max
    var inputHrMaxInt = parseInt(inputHr.max);
    if (inputHrMaxInt > 1) {
      inputHrMaxInt -= 1;
      inputHr.max = inputHrMaxInt.toString();
      if (inputHr.value > inputHr.max) {
        inputHr.value = inputHr.max;
      }
    }
  }
}

function timeStepDown() {
  if (inputTime.value != inputTime.min) {
    inputTime.stepDown(60);
    //update inputHr max
    var inputHrMaxInt = parseInt(inputHr.max);
    if (inputHrMaxInt < 7) {
      inputHrMaxInt += 1;
      inputHr.max = inputHrMaxInt.toString();
    }
  }
}