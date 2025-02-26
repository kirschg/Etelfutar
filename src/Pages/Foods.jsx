import "bootstrap/dist/css/bootstrap.css";
import { useState, useEffect } from "react";
import { useParams, Link } from "react-router-dom";
import axios from "axios";
import '../Style.css';

export const Foods = () => {
    const { RestaurantId } = useParams();
    const [foods, setFoods] = useState([]);
    useEffect(() => {
        axios.get("https://localhost:7106/Etelek/GetEtelekByEtterem?etteremId=" + RestaurantId)
            .then((res) => {
                setFoods(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }, [RestaurantId]);

    async function AddOrder(id){
        axios.post("" + id)
            .then((res) => {
                setFoods(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }
    
    return (
        <div className="App">
            <div className=" d-flex justify-content-evenly flex-wrap">
                {foods.map((food) => (
                    <h3 className="food selector" key={food.id}>
                        <Link onClick={()=>{AddOrder(food.id)}} style={{ backgroundImage: `url(${food.indexkep})` }}>
                            <div>{food.nev}</div>
                        </Link>
                    </h3>
                ))}
            </div>
        </div>
    );
}