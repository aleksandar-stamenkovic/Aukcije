

function registrujSe()
{
    let ime = document.querySelector("#ime").value;
    let prezime = document.querySelector("#prezime").value;
    let email = document.querySelector("#email").value;
    let password = document.querySelector("#password").value;
    console.log(ime);
    fetch("https://localhost:44371/korisnik", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ "ime": ime, "prezime": prezime,
                "email": email, "password": password, "id": "1" })
    }).then(p => {
        if (p.ok) {
            console.log("USPESNO");
        }
    });

}