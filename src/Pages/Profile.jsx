import "bootstrap/dist/css/bootstrap.css";
import { useNavigate, Link } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "axios";
import '../Style.css';

export const YourProfile = () => {
  const navigate = useNavigate();
  const [token, setToken] = useState(localStorage.getItem("Token"))
  const [user, setUser] = useState([]);
  useEffect(() => {
    if (token !== null) {
      axios.get("https://localhost:7106/Felhasznalok/GetFelhasznaloByTokenAsync?token=" + token, {
        headers: { "Authorization": `Bearer ${token}` }
      })
        .then(res => setUser(res.data))
        .catch(err => console.log(err))
    }
    else {
      navigate("/Login");
    }
  }, [token])
  return (console.log(user),
    <div className="App">
      <div className="Panel">
        {/*<h1 style={{ textAlign: "left" }}>{user.felhasznaloNev}</h1>
        <div>
          <ul style={{listStyle:"none"}}>
            <li>
              <h4>{user.teljesNev}</h4>
              <p style={{ color: "darkgrey" }}>{user.varos.nev + ", " + user.lakcim}</p>
            </li>
            <li>
              <p>{user.email}</p>
            </li>
          </ul>
        </div>*/}
        <h1 style={{ textAlign: "left" }}>Teszt felhasználó név</h1>
        <div>
          <ul style={{listStyle:"none"}}>
            <li>
              <h4>Teszt teljes név</h4>
              <p style={{ color: "darkgrey" }}>Teszt város + lakcím adat</p>
            </li>
            <li>
              <p>Teszt email</p>
            </li>
          </ul>
        </div>
        <Link className="button">Edit</Link>
      </div>
    </div>
  )
}