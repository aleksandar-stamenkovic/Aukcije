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
      // data je server response u kojoj se nalazi id novokreirane aukcije
      p.json().then(data => {
        uploadujSliku(data);
      })
      console.log("USPESNO DODATA AUKCIJA");
    }
  });
}

function uploadujSliku(id) {
	var file_data = $('#file').prop('files')[0];
	var form_data = new FormData();
	form_data.append('files', file_data); //<-- OVO MORA OVAKO (nece bez 'files')
	$.ajax({
		url: 'https://localhost:44371/ImageUpload/' + id, // point to server-side controller method
		dataType: 'text', // what to expect back from the server
		cache: false,
		contentType: false,
		processData: false,
		data: form_data,
		type: 'post',
		success: function(response) {
      //$('#msg').html(response); // display success response from the server
      console.log("Uploadovana slika");
		},
		error: function(response) {
      //$('#msg').html(response); // display error response from the server
      console.log("Greska Uploadovana slika");
		}
	});
}