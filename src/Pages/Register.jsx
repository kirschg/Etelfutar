import "bootstrap/dist/css/bootstrap.css";
import { useState, useEffect } from "react";
import { useNavigate, Link } from "react-router-dom";
import axios from "axios";
import '../Style.css';

export const Register = () => {
  const navigate = useNavigate();
  const [cities, setCities] = useState([]);
  useEffect(() => {
    axios.get("https://localhost:7106/Varosok/GetVarosokAsync")
    .then((res) => {
      setCities(res.data);
    })
    .catch((err) => {
      console.log(err);
    });
  }, []);
  function GenerateSalt(){
    let salt = "";
    let characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    for (let i = 0; i < 60; i++) {
      salt += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return salt;
  }
  async function register(formData){
    var sha256 = require('js-sha256');
    let password = formData.get("password");
    let salt = GenerateSalt();
    let user = {
      Email: formData.get("email"),
      FelhasznaloNev: formData.get("userName"),
      TeljesNev: formData.get("fullName"),
      Hash: sha256(password + salt),
      Salt: salt,
      VarosId: formData.get("city"),
      Lakcim: formData.get("streetNumber")
    }
    
    if(password === formData.get("passwordAgain"))
    {
      axios.post("https://localhost:7106/api/Registry", user)
      .then((res) => {
        alert(res.data);
        navigate("/Login");
      })
      .catch((err) => {
        alert("Sikertelen regisztráció!");
        console.log(err);
      });
      console.log(user);
    }
    else{
      alert("Jelszavak nem eggyeznek!");
    }
  } 
  return (
    <div className="App">
        <div className="SignPanel">
            <h1>Register</h1>
            <form className="Form" action={register}>
                <div className="mb-3">
                    <label htmlFor="emailInput" className="form-label">Email address</label>
                    <input name="email" type="text" className="form-control" id="emailInput" aria-describedby="emailHelp"/>
                </div>
                <div className="mb-3">
                    <label htmlFor="userNameInput" className="form-label">Username</label>
                    <input name="userName" type="text" className="form-control" id="userNameInput" aria-describedby="emailHelp"/>
                </div>
                <div className="mb-3">
                    <label htmlFor="fullNameInput" className="form-label">Full name</label>
                    <input name="fullName" type="text" className="form-control" id="fullNameInput" aria-describedby="emailHelp"/>
                </div>
                <div className="mb-3">
                    <label htmlFor="passwordInput" className="form-label">Password</label>
                    <input name="password" type="password" className="form-control" id="passwordInput" />
                </div>
                <div className="mb-3">
                    <label htmlFor="passwordAgainInput" className="form-label">Password again</label>
                    <input name="passwordAgain" type="password" className="form-control" id="passwordAgainInput" />
                </div>
                <div className="mb-3">
                    <label htmlFor="cityInput" className="form-label">City</label>
                    <select name="city" className="form-select" id="cityInput">
                        <option value="0" hidden>City</option>
                        {cities.map((city) => (
                            <option key={city.id} value={city.id}>{city.nev}</option>
                        ))}
                    </select>
                </div>
                <div className="mb-3">
                    <label htmlFor="streetNumberInput" className="form-label">Street number</label>
                    <input name="streetNumber" type="text" className="form-control" id="streetNumberInput" aria-describedby="emailHelp"/>
                </div>
                <label style={{textAlign:"center"}}>Already have an account? <Link to="/Login">Sign in</Link>!</label>
                <button type="submit" className="btn">Register</button>
            </form>
        </div>
    </div>
  );
}