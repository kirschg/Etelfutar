import "bootstrap/dist/css/bootstrap.css";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "axios";
import '../Style.css';

export const Order = () => {
  const navigate = useNavigate();
  const [order, setOrder] = useState([]);
  const [token] = useState(localStorage.getItem("Token"))
  useEffect(() => {
    if (token !== null) {
      axios.get("https://localhost:7106/Rendeles/GetByToken?token=" + token,
        { headers: { "Authorization": `Bearer ${token}` } }
      )
        .then(res =>{console.log(res.data);
          setOrder(res.data)})
        .catch(err => console.log(err))
    }
    else {
      navigate("/Login");
    }
  }, [navigate, token]);
  return (
    <div className="App" style={{ paddingTop: 0, paddingBottom: "10px" }}>
      <div id="Order">
        <ol className="list-group list-group-numbered list-group-flush">
          {
            order.rendeles.map(o => (
              <li className="list-group-item d-flex justify-content-between align-items-start" style={{ backgroundColor: "hsl(0, 0%, 25%)", color: "hsl(0, 0%, 100%)" }}>
                <div className="ms-2 me-auto">
                  <div className="fw-bold">{//Food name
                    o.nev
                  }</div>
                  {//subtext
                    o.ar
                  }
                </div>
                <span className="badge bg-primary rounded-pill"><img src={o.indexKep} alt={o.nev} /></span>
              </li>
            ))
          }
          <li className="list-group-item d-flex justify-content-between align-items-start" style={{ backgroundColor: "hsl(0, 0%, 25%)", color: "hsl(0, 0%, 100%)" }}>
            <div className="ms-2 me-auto">
              <div className="fw-bold">{//Food name
                "asd"
              }</div>
              {//subtext
                "asd"
              }
            </div>
            <span className="badge bg-primary rounded-pill"><img src={/*image source*/ "asd"} alt={/*Food name*/ "asd"} /></span>
          </li>
        </ol>
        <h5 style={{ margin: "auto", width: "fit-content", marginBottom: "10px" }}><input type="button" value="Confirm order" className="button" style={{ margin: "auto" }} /></h5>
      </div>
    </div>
  );
}