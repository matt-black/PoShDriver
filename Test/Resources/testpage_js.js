function alertBox() {
  alert("I am an alert box!")
}

function confirmBox() {
  var x;
  if (confirm("Press a button!") == true) {
    x = "You pressed OK!";
  } else {
    x = "You pressed Cancel!";
  }
  document.getElementById("alert_textbox").value = x;
}

function promptBox() {
  var person = prompt("Please enter your name", "Harry Potter");

  if (person != null) {
    document.getElementById("alert_textbox").value = person;
  }
}

function getUserAgent() {
  return navigator.userAgent
}