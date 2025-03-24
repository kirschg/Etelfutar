import "bootstrap/dist/css/bootstrap.css";
import { useState, useEffect } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import axios from "axios";
import '../Style.css';

export const Foods = () => {
    const navigate = useNavigate();
    const { RestaurantId } = useParams();
    const [foods, setFoods] = useState([]);
    const [token] = useState(localStorage.getItem("Token"))
    useEffect(() => {

        axios.get("https://localhost:7106/Etelek/GetEtelekByEtterem?etteremId=" + RestaurantId)
            .then((res) => {
                setFoods(res.data);
                console.log(res)
            })
            .catch((err) => {
                console.log(err);
            });
    }, [RestaurantId]);

    async function AddOrder(id) {
        axios.get("https://localhost:7106/Felhasznalok/GetFelhasznaloByTokenAsync?token=" + token,
            { headers: { "Authorization": `Bearer ${token}` } }
        )
            .then(response => {
                axios.post(`https://localhost:7106/Rendeltetel/PostRendeltetelAsync?etelId=${id}&felhasznaloId=${response.data.id}`, "",
                    { headers: { "Authorization": `Bearer ${token}` } }
                )
                    .then((res) => {
                        console.log(res)
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            })
            .catch(error => {
                console.log(error);
                navigate("/Login");
            })

    }
    function Checker(data){
        return data.etteremId === parseInt(RestaurantId)
    }
    function Check(data){
        var found = data.learazas.find(Checker);
        if (found === undefined) {
            return (<p style={{ position: "absolute", right: 20, bottom: -5 }}>{data.ar} FT</p>)
        }
        else{
            return (<p style={{ position: "absolute", right: 20, bottom: -5 }}>
                {Math.round(data.ar * (100-found.learazas)/100)} FT <span style={{ textDecoration:"line-through" }}>{data.ar} FT</span>
                </p>)
        }
    }

    return (
        <div className="App">
            <div className=" d-flex justify-content-evenly flex-wrap">
                {foods.map((food) => (
                    <h3 className="food selector" key={food.id}>
                        <Link onClick={() => { AddOrder(food.id) }} style={{ backgroundImage: `url(${food.indexkep})` }}>
                            <div>{food.nev}</div>
                            {Check(food)}
                        </Link>
                    </h3>
                ))}
            </div>
        </div>
    );
}