window.addEventListener('DOMContentLoaded', function(event){

    var expanded = false;

    function showCheckboxes() {
        var checkboxes = document.getElementById("checkboxes");
        if (!expanded) {
            checkboxes.style.display = "flex";
            expanded = true;
        } else {
            checkboxes.style.display = "none";
            expanded = false;
        }
    }

    function validProperties(name, description, image, imageFile){
        //debugger;
        if (name === "" || description === "" || (image === "" && !imageFile)){
            alert("Not valid values. Please fill all the fields");
            return false;
        }
        else{
            return true;
        }
    }

    async function attachProductToCombo(url, comboId, productId){
        //debugger;
        fetch(url, { 
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'POST',
            body: JSON.stringify({
                ComboId: comboId,
                ProductId: productId
            })
        }).then(response => {
            if(response.status === 201){
                alert('Created');
                window.location.href = "combos.html";
            } else {
                response.text()
                .then((error)=>{
                    alert(error);
                });
            }
        });
    }

    function addProductToCombo(comboId){
        debugger;
        let cont = 0;
        let productCbox = document.querySelectorAll('#post-form #checkboxes label');
        productCbox.forEach(prodCb => {
                let product = prodCb.querySelector("input");
                if (product.checked){
                    debugger;
                    //selectedProducts.push(product);
                    let url = `${baseUrl}/combos/ProductsCombos`;
                    let productId = product.id.split('-')[0];
                    attachProductToCombo(url, +comboId, +productId);
                    cont = cont +1;
                    //alert('Created');
                    //window.location.href = "combos.html";
                }
                
        });
        if(cont === 0){
            alert("Select a product!!");
        }
    }
    
    async function PostFormCombo(event)
    {
        //debugger;
        event.preventDefault();
        let url = `${baseUrl}/combos/Form`;
        
        let name = document.querySelector('#name_input').value;
        let description = document.querySelector('#description_input').value;
        let imageFile = document.querySelector('#form-wrapper #post-form .property-wrapper .custom-file-input').files[0];
        let imageURL = document.querySelector('#image_input').value;
        if(!validProperties(name, description, imageURL, imageFile)){
            return;
        }
        
        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Description', description);
        formData.append('ImageURL',imageURL);
        formData.append('Image', imageFile);
        //debugger;

        fetch(url, {
            method: 'POST',
            body: formData
        }).then(response => {
            if(response.status === 201){
                response.json().then(data => {
                    //debugger;
                    comboId = data.id;
                    addProductToCombo(comboId);
                });
            } else {
                response.text()
                .then((error)=>{
                    alert(error);
                });
            }
        });

    }

    async function fetchCategory()
    {
        let productCard = `
            <div id="form-image" style="background: url(https://images.unsplash.com/photo-1560963614-153d4432f741?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=334&q=80)">
            </div>
            <div id="post-form">
                <h2>Creation Form</h2>
                <div class="property-wrapper"> 
                    <label for="name_input">Name</label>
                    <input type="text" name="name_input_name" id="name_input" class="create-input">
                </div>

                <div class="property-wrapper"> 
                    <label for="description_input">Description</label>
                    <input type="text" name="name_input_description" id="description_input" class="create-input">
                </div>

                <div class="property-wrapper"> 
                    <label for="image_input">Image URL</label>
                    <input type="text" name="name_input_image" id="image_input" class="create-input">
                </div>

                <div class="property-wrapper">
                    <label for="image_file_input">Image File</label>
                    <input type="file" name="image" id="image_file_input" class="create-input custom-file-input">
                </div>

                <div class="property-wrapper product-selection">

                </div>

                <div class="submit-container">
                    <button class="create-submit">Create</button>
                </div>
            </div>
        `;
        
        document.querySelector('#form-wrapper').innerHTML = productCard;
        
        
        let productsSelection = `
        <form>
            <div class="multiselect">
            <div class="selectBox">
                <select>
                <option>Select products</option>
                </select>
                <div class="overSelect"></div>
            </div>
            <div id="checkboxes">

            </div>
            </div>
        </form>`;

        document.querySelector('#post-form .property-wrapper.product-selection').innerHTML = productsSelection;

        
        const url = `${baseUrl}/categories/1/products/all`;
        let response = await fetch(url);
        if(response.status = 200){
            let data = await response.json();
            let productOptions = data.map(product => { 
                return `
                <label for="${product.id}cbox">
                <input type="checkbox" id="${product.id}-cbox" />${product.name}</label>`
            });

            document.querySelector('#post-form #checkboxes').innerHTML = productOptions;

        }else {
            var errorText = await response.text();
            alert(errorText);
        }
        
        let createButton = document.querySelector('#post-form .create-submit');
        createButton.addEventListener('click', PostFormCombo);

        let selectDropdown = document.querySelector('#post-form .selectBox');
        selectDropdown.addEventListener('click', showCheckboxes)
    }

    var comboId;
    //debugger;
    const baseUrl = 'http://localhost:3030/api';
    // alert(productId);
    fetchCategory();
    
});

// Apple pie with milky coffe
// Delicious combo
