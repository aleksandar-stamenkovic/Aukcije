function dodajBidera(id, email, ime, prezime, cena) {
  var c = cena;
  fetch(
    "https://localhost:44371/aukcija/" +
      id +
      "/" +
      email +
      "/" +
      ime +
      "/" +
      prezime +
      "/" +
      c,
    {
      method: "GET",
    }
  ).then((p) => {
    if (p.ok) {
      console.log("Uspesna izmena zahteva");
    } else {
      console.log("greska prilikom izmene ili prilikom post fetcha");
    }
  });
}

function dodajBideraMaster() {
  a = JSON.parse(localStorage.getItem("loged-in"));
  b = JSON.parse(localStorage.getItem("loged-in-info"));

  let cena = document.querySelector(".price-bid-form").value;

  console.log(b.ime);
  console.log(b.prezime);
  console.log(a.email);
  console.log(cena);

  let data = sessionStorage.getItem("auctionId");

  console.log(data);
  dodajBidera(data, a.email, b.ime, b.prezime, cena);
}

function povecajCenu() {
  let data = sessionStorage.getItem("auctionId");
  console.log(data);
  const cena = document.querySelector(".price-bid-form").value;
  fetch("https://localhost:44371/aukcija/" + data + "/" + cena, {
    method: "PUT",
  }).then((p) => {
    if (p.ok) {
      console.log("Uspesan zahteva");
      dodajBideraMaster();
      postaviAukciju();
      procitajBidere();
    } else {
      console.log("greska prilikom get zahteva");
    }
  });
  //ovo lokalno ne bi trebalo
  console.log("proso promenu");
}

function procitajBidere() {
  let data = sessionStorage.getItem("auctionId");
  console.log(data);

  fetch("https://localhost:44371/aukcija/svibideri/" + data, {
    method: "GET",
  }).then((p) =>
    p.json().then((data) => {
      let nizPodataka = data;

      const myNode = document.querySelector("#table-body-data");
      while (myNode.lastElementChild) {
        myNode.removeChild(myNode.lastElementChild);
      }
      nizPodataka.forEach((el) => {
        postaviTabeluBidera(el);
        console.log(el);
      });
    })
  );
}

procitajBidere();

function postaviTabeluBidera(el) {
  var str = el;
  var res = str.split(" ");

  var tester = $(
    "<tr>" +
      "<td>" +
      res[0] +
      "</td>" +
      "<td>" +
      res[1] +
      "</td>" +
      "<td>" +
      res[2] +
      "</td>" +
      '<td class="table-price">' +
      res[3] +
      "</td>" +
      "</tr>"
  );
  $("#table-body-data").append(tester);
  console.log("generator proso");
}

function postaviAukciju() {
  let data = sessionStorage.getItem("auctionId");
  console.log(data);
  fetch("https://localhost:44371/aukcija/" + data, {
    method: "GET",
  }).then((p) =>
    p.json().then((data) => {
      let nizPodataka = [
        data["id"],
        data["naziv"],
        data["opis"],
        data["cena"],
        data["trajanje"],
      ];

      let id = nizPodataka[0];
      let naziv = nizPodataka[1];
      let opis = nizPodataka[2];
      let cena = nizPodataka[3];
      let trajanje = nizPodataka[4];

      document.querySelector(".auction-timer").innerHTML = trajanje + "min";
      document.querySelector(".last-price-value").innerHTML = cena;
      document.querySelector(".product-description").innerHTML = opis;
      document.querySelector(".auction-title").innerHTML = naziv;

      document.querySelector(".rounded").src = "https://localhost:44371/imageupload/"+ id +".jpg";
    })
  );
}

postaviAukciju();
//dodajBideraMaster();
//dodajBidera("1", "pera.pera@gmail.com", "Petar", "Petrovic", 5);
