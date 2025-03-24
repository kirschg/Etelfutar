import "bootstrap/dist/css/bootstrap.css";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "axios";
import '../Style.css';

export const Order = () => {
  const navigate = useNavigate();
  const [order, setOrder] = useState([]);
  const [userId, setUserId] = useState(0);
  const [token] = useState(localStorage.getItem("Token"))
  const [price, setPrice] = useState(0);
  useEffect(() => {
    if (token !== null) {
      getOrder()
    }
    else {
      navigate("/Login");
    }
  }, [token]);

  function getOrder() {
    axios.get("https://localhost:7106/Rendeles/GetByToken?token=" + token,
      { headers: { "Authorization": `Bearer ${token}` } }
    )
      .then(res => {
        axios.get("https://localhost:7106/Felhasznalok/GetFelhasznaloByTokenAsync?token=" + token,
          { headers: { "Authorization": `Bearer ${token}` } }
        )
        .then(response=>{
          setUserId(response.data.id)
          setOrder(res.data.rendeles)
          setPrice(res.data.osszar)
        })
      })
      .catch(err => {
        console.log(err);
      })
  }

  function DeleteOrder(id) {
    axios.delete(`https://localhost:7106/Rendeltetel/DeleteRendeltetelAsync?etelId=${id}&felhasznaloId=${userId}`,
      { headers: { "Authorization": `Bearer ${token}` } })
      .then(res => {
        console.log(res);
        getOrder();
      })
      .catch(err => console.log(err))
  }
  return (
    <div className="App" style={{ paddingTop: 0, paddingBottom: "10px" }}>
      <div id="Order">
        <ol className="list-group list-group-numbered list-group-flush">
          {order.map(o => (
            <li
              className="list-group-item d-flex justify-content-between align-items-start"
              style={{
                backgroundColor: "hsl(0, 0%, 25%)", color: "hsl(0, 0%, 100%)"
              }}
              key={o.id}
            >
              <div className="ms-2 me-auto">
                <div className="fw-bold">{//Food name
                  o.nev
                }</div>
                {//subtext
                  o.ar
                }
                Ft
              </div>
              <span className="badge button" style={{ margin: "3px" }} onClick={() => { DeleteOrder(o.id) }}>
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-x-lg" viewBox="0 0 16 16">
                  <path d="M2.146 2.854a.5.5 0 1 1 .708-.708L8 7.293l5.146-5.147a.5.5 0 0 1 .708.708L8.707 8l5.147 5.146a.5.5 0 0 1-.708.708L8 8.707l-5.146 5.147a.5.5 0 0 1-.708-.708L7.293 8z" />
                </svg>
              </span>
            </li>
          ))
          }
        </ol>
        <h5 style={{ margin: "auto", width: "fit-content", marginBottom: "10px" }}>
          <input type="button" value="Confirm order" className="button" style={{ margin: "auto" }} />
        </h5>
      </div>
    </div>
  );
}