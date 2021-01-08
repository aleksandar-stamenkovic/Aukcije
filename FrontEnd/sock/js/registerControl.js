function registrujSe() {
  let ime = document.querySelector("#ime").value;
  let prezime = document.querySelector("#prezime").value;
  let email = document.querySelector("#email").value;
  let password = document.querySelector("#password").value;
  console.log(ime);
  fetch("https://localhost:44371/korisnik", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      ime: ime,
      prezime: prezime,
      email: email,
      password: password,
      id: "2",
    }),
  }).then((p) => {
    if (p.ok) {
      localStorage.clear();
      localStorage.setItem(
        "loged-in",
        JSON.stringify({ email: email, loged: true })
      );

      checkUser();

      console.log("USPESNO");
      a = JSON.parse(localStorage.getItem("loged-in"));
      console.log(a.loged);
      window.location.href = "shop.html";
    }
  });
}

function LogujSe() {
  let email = document.querySelector("#login-email").value;
  let password = document.querySelector("#login-password").value;

  fetch("https://localhost:44371/korisnik/login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      email: email,
      password: password,
    }),
  }).then((p) => {
    if (p.ok) {
      localStorage.clear();
      localStorage.setItem(
        "loged-in",
        JSON.stringify({ email: email, loged: true })
      );

      a = JSON.parse(localStorage.getItem("loged-in"));
      console.log(a.email);
      console.log("USPESNO");
      window.location.href = "shop.html";

      checkUser();
    }
  });
}

function checkUser() {
  a = JSON.parse(localStorage.getItem("loged-in"));
  let email = a.email;

  fetch("https://localhost:44371/korisnik/" + email, {
    method: "GET",
  }).then((p) =>
    p.json().then((data) => {
      let nizPodataka = [
        data["id"],
        data["ime"],
        data["prezime"],
        data["email"],
        data["password"],
      ];

      let ime = nizPodataka[1];
      let prezime = nizPodataka[2];
      console.log(ime + " " + prezime);
      localStorage.setItem(
        "loged-in-info",
        JSON.stringify({ ime: ime, prezime: prezime })
      );
      b = JSON.parse(localStorage.getItem("loged-in-info"));
      console.log(b.ime);
      console.log(b.prezime);
    })
  );
}
