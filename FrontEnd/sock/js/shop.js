function LoadAukcije() {
  fetch("https://localhost:44371/aukcija/sveaukcije", {
    method: "GET",
  }).then((p) =>
    p.json().then((data) => {
      data.forEach((element) => {
        let nizPodataka = [
          element["id"],
          element["naziv"],
          element["opis"],
          element["cena"],
          element["trajanje"],
        ];

        let id = nizPodataka[0];
        let naziv = nizPodataka[1];
        let opis = nizPodataka[2];
        let cena = nizPodataka[3];
        let trajanje = nizPodataka[4];

        console.log(id);
        generisiAukciju(id, id + ".png", naziv, trajanje, opis);
      });
    })
  );
}

LoadAukcije();

function generisiAukciju(id, imgSrc, naziv, trajanje, opis) {
  var tester = $(
    '<div class="shopItem col-xl-4 col-lg-4 col-md-6 col-sm-12 " onclick="predji(this)" >' +
      '<label class="myId" hidden>' +
      id +
      "</label>" +
      '<div class="sport_product">' +
      '<figure><img class="product-image" src=https://localhost:44371/imageupload/' +
      id +
      '.jpg alt="img"/ ></figure>' +
      '<p class="price-text-last-mod"> Preostalo: <strong class="price_text">' +
      trajanje +
      "</strong> min</p>" +
      '<h6 class="product-name">' +
      opis +
      "</h6>" +
      '<h4 class="product-name">' +
      naziv +
      "</h4>" +
      "</div>" +
      "</div>"
  );
  $("#shop-container").append(tester);
  console.log("generator proso");
}

function predji(host) {
  sessionStorage.clear();
  let id = host.querySelector(".myId").innerHTML;
  console.log(id);
  sessionStorage.setItem("auctionId", id);
  let data = sessionStorage.getItem("auctionId");

  console.log(data);
  window.location.href = "auction.html";
}
