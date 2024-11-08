let code = ``;
let company_full_name = "Ételfutár®";
let currentLanguage = "HU";
language={ 
    "HU":[
        "Bejelentkezés",//0.
        "Regisztrálj",//1.
        "nyelv",//2.
        "kosár",//3.
        "keresés...",//4.
        "Üdvözöljük",//5.
        "2024 Ételfutár® - Minden jog fenntartva."//6.
    ], 
    "EN":[
        "Login",//0.
        "Register",//1.
        "langauge",//2.
        "cart",//3.
        "search...",//4.
        "Welcome",//5.
        "2024 Ételfutár® - All rights reserved."//6.
    ]
};
`<body>
    <main>

        <header>
            <a href="./index.html">Title</a>
        </header>
        <nav>
            <ul>
                <li id="left"><input type="text" placeholder="${language["EN"][4]}"></li>
                <li><a href="./cart/cart.html">${language["EN"][3]}</a></li>
                <li><a href="">${language["EN"][2]}</a></li>
                <li><a href="./register/register.html" id="register">${language["EN"][1]}</a></li>
                <li><a href="">${language["EN"][0]}</a></li>
            </ul>
        </nav>
        <article>
            
        </article>
        <aside>
    
        </aside>
        <footer>
            <p>${language["EN"][6]}</p>
        </footer>
    </main>
    <script src="script.js"></script>
</body>`

//Bejelentkezés
let logopen = false;
document.getElementById("login").addEventListener("click",login)
function login(){
    code = ``
    logopen=!logopen;
    regopen = false;
    cartopen = false;
    if(logopen){
        if(local_lang == "HU") 
        {
            code = `
        <div class="panel" id="leftpanel">
            <img src="img/arrow.png" onclick="login()" height="50px">
            <h1>Bejelentkezés</h1>
            <br>
            <div>
                <label for="name">Felhasználó név vagy Email Cím</label>
                <br>
                <input type="text" id="name">
            </div>
            <div>
                <label for="pass">Jelszó</label>
                <br>
                <input type="text" id="pass">
            </div>
            <br>
            <div>
                <input type="button" value="Bejelentkezés">
            </div>
        </div>
        `
        }
        else if(local_lang == "EN") 
        {
            code = `
        <div class="panel" id="leftpanel">
            <img src="img/arrow.png" onclick="login()" height="50px">
            <h1>Login</h1>
            <br>
            <div>
                <label for="name">Username or Email address</label>
                <br>
                <input type="text" id="name">
            </div>
            <div>
                <label for="pass">Password</label>
                <br>
                <input type="text" id="pass">
            </div>
            <br>
            <div>
                <input type="button" value="Login">
            </div>
        </div>
        `
        }
        
    }
    document.getElementById("panel").innerHTML=code;
}

//Regisztrálás
regopen = false;
document.getElementById("register").addEventListener("click",register)
function register(){
    code = ``
    logopen=false;;
    regopen = !regopen;
    cartopen = false;
    if(regopen){
        if(local_lang == "HU") 
        {
            code = `
            <div class="panel" id="leftpanel">
                <img src="img/arrow.png" onclick="register()" height="50px">
                <h1>Regisztráció</h1>
                <br>
                <div>
                    <label for="name">Felhasználónév</label>
                    <br>
                    <input type="text" id="name">
                </div>
                <div>
                    <label for="name">Email cím</label>
                    <br>
                    <input type="text" id="email">
                </div>
                <div>
                    <label for="pass">Jelszó</label>
                    <br>
                    <input type="text" id="pass">
                </div>
                <div>
                    <label for="pass_again">Jelszó újra</label>
                    <br>
                    <input type="text" id="pass_again">
                </div>
                <br>
                <div>
                    <input type="button" value="Regisztrálás">
                </div>
            </div>
            `
        }
        else if(local_lang == "EN") 
        {
            code = `
            <div class="panel" id="leftpanel">
                <img src="img/arrow.png" onclick="register()" height="50px">
                <h1>Register</h1>
                <br>
                <div>
                    <label for="name">Username</label>
                    <br>
                    <input type="text" id="name">
                </div>
                <div>
                    <label for="name">Email address</label>
                    <br>
                    <input type="text" id="email">
                </div>
                <div>
                    <label for="pass">Password</label>
                    <br>
                    <input type="text" id="pass">
                </div>
                <div>
                    <label for="pass_again">Password again</label>
                    <br>
                    <input type="text" id="pass_again">
                </div>
                <br>
                <div>
                    <input type="button" value="Regisztrálás">
                </div>
            </div>
            `
        }
    }
    document.getElementById("panel").innerHTML=code;
}

//Kosár
cartopen = false;
document.getElementById("cart").addEventListener("click",cart)
function cart(){
    code = ``
    logopen=false;
    regopen = false;
    cartopen = !cartopen;
    if(cartopen){
        if(local_lang == "HU") 
        {
            code = `
            <div class="panel" id="rightpanel">
                <img src="img/arrow.png" onclick="cart()" height="50px">
                <h1>Kosár</h1>
                <br>
                <div id="order">

                </div>
                <br>
                <div>
                    <input type="button" value="Rendelés">
                </div>
            </div>
            `
        }
        else if(local_lang == "EN") 
        {
            code = `
            <div class="panel" id="rightpanel">
                <img src="img/arrow.png" onclick="cart()" height="50px">
                <h1>Cart</h1>
                <br>
                <div id="order">

                </div>
                <br>
                <div>
                    <input type="button" value="Order">
                </div>
            </div>
            `
        }
    }
    document.getElementById("panel").innerHTML=code;
}

//Select Language
document.getElementById("language_select").onchange = function() {
    const language = document.getElementById("language_select").value;
    currentLanguage = language.value
}