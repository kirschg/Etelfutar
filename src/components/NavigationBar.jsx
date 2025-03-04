import "bootstrap/dist/css/bootstrap.css";
import { useState, useEffect } from "react";
import { NavLink, Link } from "react-router-dom";
import axios from "axios";
import '../Style.css';

export const NavBar = () => {
    const [token, setToken] = useState(localStorage.getItem("Token"))
    useEffect(() => {
        const handleStorage = () => {
            setToken(localStorage.getItem("Token"))
        }

        window.addEventListener('storage', handleStorage)
        return () => window.removeEventListener('storage', handleStorage)

    }, [])
    async function Logout() {
        axios.post("https://localhost:7106/api/Logout/" + token)
            .then(() => {
                localStorage.removeItem("Token");
                window.dispatchEvent(new Event('storage'))
            })
            .catch(err =>{
                localStorage.removeItem("Token");
                window.dispatchEvent(new Event('storage'));
                console.log(err)
            })
    }
    return (
        <>
            <div id="banner">
                <h1 style={{ width: "fit-content" }}><Link to="/" style={{ textDecoration: "none", color: "hsl(0, 0%, 100%)" }}>Ételfutár®</Link></h1>
            </div>

            <nav style={{ backgroundColor: "hsl(0, 0%, 25%)", borderBottom: "2px black solid" }}>
                <ul className="nav">
                    <li data-bs-toggle="offcanvas" data-bs-target="#offcanvasRight" aria-controls="offcanvasRight">
                        <NavLink to="/YourOrder">
                            <svg style={{ padding: 0 }} xmlns="http://www.w3.org/2000/svg" width="30" height="30" fill="currentColor" className="bi bi-basket2-fill" viewBox="0 0 16 20">
                                <path d="M5.929 1.757a.5.5 0 1 0-.858-.514L2.217 6H.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h.623l1.844 6.456A.75.75 0 0 0 3.69 15h8.622a.75.75 0 0 0 .722-.544L14.877 8h.623a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1.717L10.93 1.243a.5.5 0 1 0-.858.514L12.617 6H3.383zM4 10a1 1 0 0 1 2 0v2a1 1 0 1 1-2 0zm3 0a1 1 0 0 1 2 0v2a1 1 0 1 1-2 0zm4-1a1 1 0 0 1 1 1v2a1 1 0 1 1-2 0v-2a1 1 0 0 1 1-1" />
                            </svg>
                        </NavLink>
                    </li>
                    <li style={{ padding: 0 }}><select>
                        <option hidden>Language</option>
                        <option value="HU">HU</option>
                        <option value="EN">EN</option>
                    </select></li>
                    {
                        token !== null && (<>
                            <li><NavLink onClick={() => { Logout() }} style={{ color: "white" }}>logout</NavLink></li>
                            <li><NavLink to="/YourProfile">profile</NavLink></li>
                        </>)
                    }
                    {
                        token === null && (<>
                            <li><NavLink to="/Login">login</NavLink></li>
                            <li><NavLink to="/Register">register</NavLink></li>
                        </>)
                    }

                    <li><NavLink to="/">start</NavLink></li>
                </ul>
            </nav>
        </>)
}