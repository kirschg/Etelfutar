import "bootstrap/dist/css/bootstrap.css";
import { useState, useEffect } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import axios from "axios";
import '../Style.css';

export const Foods = () => {
    const navigate = useNavigate();
    const { RestaurantId } = useParams();
    const [foods, setFoods] = useState([]);
    const [orderId, setOrderId] = useState();
    const [token, setToken] = useState(localStorage.getItem("Token"))
    useEffect(() => {

        axios.get("https://localhost:7106/Etelek/GetEtelekByEtterem?etteremId=" + RestaurantId)
            .then((res) => {
                setFoods(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }, [RestaurantId]);

    async function AddOrder(id) {
        axios.get("https://localhost:7106/Rendeles/GetByToken?token=" + token,
            { headers: { "Authorization": `Bearer ${token}` } }
          )
            .then(res => {
              setOrderId(res.data.id)
                axios.post(`https://localhost:7106/Rendeltetel/PostRendeltetelAsync?etelId=${id}&rendelesId=${orderId}`)
                    .then((res) => {
                        console.log(res)
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            })
            .catch(err =>{
                console.log(err);
                navigate("/Login");
            })

    }

    return (
        <div className="App">
            <div className=" d-flex justify-content-evenly flex-wrap">
                {foods.map((food) => (
                    <h3 className="food selector" key={food.id}>
                        <Link onClick={() => { AddOrder(food.id) }} style={{ backgroundImage: `url(${food.indexkep})` }}>
                            <div>{food.nev}</div>
                            <p style={{ position:"absolute", right:20, bottom:-5}}>{food.ar} FT</p>
                        </Link>
                    </h3>
                ))}
            </div>
        </div>
    );
}