window.addEventListener('DOMContentLoaded', function(event){
    
    function login(event) {
        debugger;
        console.log(event.currentTarget);
        event.preventDefault();
        const url = `${baseUrl}/auth/Login`;

        if(!Boolean( document.querySelector('#username_input').value)){
            alert("Fill credentials!");
            return;
        }


        var data = {
            Email: document.querySelector('#username_input').value,
            Password: document.querySelector('#password_input').value
        }

        fetch(url, {
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'POST',
            body: JSON.stringify(data)
        }).then((response) => {
            if (response.status === 200) {
                
                response.json().then((data)=>{
                    debugger;
                    sessionStorage.setItem("jwt", data.message);
                    alert("Logued in :)");
                    window.location.href = "../Categories/categories.html";
                });
            } else {
                response.text().then((data) => {
                    alert("Wrong credentials!");
                    console.log(data);
                });
            }
        }).catch((response) => {
            alert("Fill credentials!");
            console.log(data);
        });

    }

    
    async function fetchProduct()
    {
        let productCard = `
            </div>
            <div id="post-form">
                <h2>Pastry Shop</h2>
                <h4>Made with love</h4>
                <div class="property-wrapper"> 
                    <label for="username_input">E-mail</label>
                    <input type="text" name="name_input_username" id="username_input" class="create-input">
                </div>

                <div class="property-wrapper"> 
                    <label for="password_input">Password</label>
                    <input type="text" name="name_input_password" id="password_input" class="create-input">
                </div>

                <div class="submit-container">
                    <button class="create-submit">Login</button>
                </div>
            </div>
            <div id="form-image" style="background: url(https://images.unsplash.com/photo-1458819714733-e5ab3d536722?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=333&q=80)">
        `;

        document.querySelector('#form-wrapper').innerHTML = productCard;
        
        let loginButton = document.querySelector('#post-form .create-submit');
        loginButton.addEventListener('click', login);
    }
    //debugger;
    const baseUrl = 'http://localhost:3030/api';
    // alert(productId);
    fetchProduct();
    
});