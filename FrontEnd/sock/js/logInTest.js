console.log("usoooo");

function ispitajDaLiJeLogovan() {
  a = JSON.parse(localStorage.getItem("loged-in"));
  console.log(a.email + " " + a.loged);
  if (a.loged == false) {
    window.location.href = "register.html";
  }
}

ispitajDaLiJeLogovan();

function IzlogujSe() {
  localStorage.clear();
  localStorage.setItem("loged-in", JSON.stringify({ loged: false }));
  window.location.href = "index.html";
}
