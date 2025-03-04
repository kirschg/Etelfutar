import "bootstrap/dist/css/bootstrap.css";
import { useNavigate } from "react-router-dom";
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
  }, [])
  return (console.log(user),
    <div>

    </div>)
}