/*function test() {
  a = JSON.parse(localStorage.getItem("test"));

  console.log(a.email + "   " + a.loged);
}

test();

function test2() {
  localStorage.setItem(
    "test",
    JSON.stringify({ email: "mika@mikic.com", loged: true })
  );
  console.log("test abc");
}

test2();
*/

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
