export class Korisnik {

    constructor(ime,prezime,email,password) {
        this.ime=ime;
        this.prezime=prezime;
        this.email=email;
        this.password=password;
    }

    registrujSe() {
        const korisnik = new Korisnik();
        korisnik.ime = document.querySelector("#ime");
        korisnik.prezime = document.querySelector("#prezime");
        korisnik.email = document.querySelector("#email");
        korisnik.password = document.querySelector("#password");

        fetch("https://localhost:44371/Korisnik", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ "ime": korisnik.ime, "prezime": korisnik.prezime,
                    "cena": korisnik.email, "kolicina": korisnik.password, "id": 55 })
        }).then(p => {
            if (p.ok) {
                console.log("Korisnik dodat u bazu");
            }
        });
    }

    stampaj() {
        console.log("TEST");
    }
}