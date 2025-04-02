import "bootstrap/dist/css/bootstrap.css";
import React from "react";
import { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import axios from "axios";
import '../Style.css';

export const Cities = () => {
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
    return (
        <div className="App">
            <div className=" d-flex justify-content-evenly flex-wrap">
                {cities.map((city) => (
                    <h3 className="city selector" role="status" key={city.id}>
                        <Link to={{ pathname: "/" + city.nev + "/Restaurants", state: { city: city.nev } }} style={{ backgroundImage: `url("${city.indexKep}")` }}>
                            <div>{city.nev}</div>
                        </Link>
                    </h3>
                ))}
            </div>
        </div>
    );
}