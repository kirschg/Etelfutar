import "bootstrap/dist/css/bootstrap.css";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "axios";
import '../Style.css';

export const Order = () => {
  const navigate = useNavigate();
  const [order, setOrder] = useState([]);
  const [token, setToken] = useState(localStorage.getItem("Token"))
  useEffect(() => {
    if (token !== null) {
      axios.get("")
        .then(res => setOrder(res.data))
        .catch(err => console.log(err))
    }
    else {
      navigate("/Login");
    }
  }, [token])
  return (
    <div className="App" style={{ paddingTop: 0, paddingBottom: "10px" }}>
      <div id="Order">
        <ol class="list-group list-group-numbered list-group-flush">
          {
            /*order.map(o => (
              <li class="list-group-item d-flex justify-content-between align-items-start" style={{ backgroundColor: "hsl(0, 0%, 25%)", color: "hsl(0, 0%, 100%)" }}>
                <div class="ms-2 me-auto">
                  <div class="fw-bold">{//Food name
                    o.etelek.nev
                  }</div>
                  {//subtext
                    o.etelek.ar
                  }
                </div>
                <span class="badge bg-primary rounded-pill"><img src={o.etelek.indexKep} alt={o.etelek.nev} /></span>
              </li>
            ))*/
          }
          <li class="list-group-item d-flex justify-content-between align-items-start" style={{ backgroundColor: "hsl(0, 0%, 25%)", color: "hsl(0, 0%, 100%)" }}>
            <div class="ms-2 me-auto">
              <div class="fw-bold">{//Food name
                "asd"
              }</div>
              {//subtext
                "asd"
              }
            </div>
            <span class="badge bg-primary rounded-pill"><img src={/*image source*/ "asd"} alt={/*Food name*/ "asd"} /></span>
          </li>
        </ol>
        <h5 style={{margin:"auto", width:"fit-content", marginBottom:"10px"}}><input type="button" value="Confirm order" className="button" style={{margin:"auto"}} /></h5>
      </div>
    </div>
  );
}