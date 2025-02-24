import "bootstrap/dist/css/bootstrap.css";
import { BrowserRouter, Routes, Route} from "react-router-dom"
import { NavBar } from "./components/NavigationBar";
import { Home } from './Pages/Home'
import { Cities } from './Pages/Cities'
import { Restaurants } from './Pages/Restaurants'
import { Foods } from './Pages/Foods';
import { Order } from './Pages/Order';
import { Register } from './Pages/Register';
import { Login } from './Pages/Login';
import { YourProfile } from "./Pages/Profile";
import './Style.css';

export const App = () => {
  return (
    <BrowserRouter>
        <header>
          <NavBar />
        </header>
        <Routes>
          <Route path="/">
            <Route index element={<Home />} />
            <Route path="Cities" element={<Cities />} />
            <Route path=":City/Restaurants" element={<Restaurants />} />
            <Route path=":City/:RestaurantId/Selection" element={<Foods />} />
            <Route path="YourOrder" element={<Order />} />
            <Route path="Register" element={<Register />} />
            <Route path="Login" element={<Login />} />
            <Route path="YourProfile" element={<YourProfile />} />
            <Route path="*" element={<Home />} />
          </Route>
        </Routes>
        <footer id="footer">
          <p >2024-2025 Ételfutár® - Minden jog fenntartva.</p>
        </footer>
    </BrowserRouter>
  );
}
