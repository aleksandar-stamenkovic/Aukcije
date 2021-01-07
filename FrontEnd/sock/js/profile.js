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

/*
frm-cnt-id
frm-cnt-ime
frm-cnt-prezime
frm-cnt-email
frm-cnt-pass
*/
