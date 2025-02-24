import "bootstrap/dist/css/bootstrap.css";
import { useNavigate } from "react-router-dom";
import { useState, useEffect } from "react";
import axios from "axios";
import '../Style.css';

export const YourProfile = () => {
    const navigate = useNavigate();
    const [token, setToken] = useState(localStorage.getItem("Token"))
    const [user, setUser] = useState([]);
    useEffect(()=>{
        if(token!==null){

        }
        else{
          navigate("/Login");
        }
    },[])
    return (<div>
        
    </div>)
}