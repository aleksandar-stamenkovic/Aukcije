function showData() {
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

      let kontrole = document.querySelectorAll(".frm-cnt-profile");

      kontrole[0].value = nizPodataka[0];
      kontrole[1].value = nizPodataka[1];
      kontrole[2].value = nizPodataka[2];
      kontrole[3].value = email;
      kontrole[4].value = nizPodataka[4];
    })
  );
}

showData();

function DodajAukciju() {
  let nizKontrola = document.querySelectorAll(".frm-cnt-auction");
  let naziv = nizKontrola[0].value;
  let opis = nizKontrola[1].value;
  var cena = parseInt(nizKontrola[2].value);
  var trajanje = parseInt(nizKontrola[3].value);

  a = JSON.parse(localStorage.getItem("loged-in"));
  let email = a.email;

  console.log(trajanje);
  console.log(a.email);
  fetch("https://localhost:44371/aukcija", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      naziv: naziv,
      opis: opis,
      cena: cena,
      trajanje: trajanje,
      vlasnik: email,
    }),
  }).then((p) => {
    if (p.ok) {
      console.log("USPESNO DODATA AUKCIJA");
    }
  });
}
