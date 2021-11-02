window.addEventListener('DOMContentLoaded', function(event){

    function validProperties(name, description, price, image){
        debugger;
        if (name === "" || description === "" || (image === "" && !imageFile) || price === 0){
            alert("Not valid values. Please fill all the fields");
            return false;
        }
        else{
            return true;
        }
    }
    async function createProduct(event)
    {
        debugger;
        let name = document.querySelector('#name_input').value;
        let description = document.querySelector('#description_input').value;
        let price = +document.querySelector('#price_input').value;
        let image = document.querySelector('#image_input').value;

        if(!validProperties(name, description, price, image)){
            return;
        }
        let url = `${baseUrl}/products/`;//`${baseUrl}/categories/`;
        let data = await fetch(url, { 
                headers: { "Content-Type": "application/json; charset=utf-8" },
                method: 'POST',
                body: JSON.stringify({
                    Name: name,
                    Description: description,
                    Price: price,
                    ImageUrl: image
                })
            });
        try {
            if(data.status === 201){
                alert('Created');
                window.location.href = `../Products/products.html?categoryId=${categoryId}`;//"http://127.0.0.1:5500/";
            }
        } catch(error){
            var errorText = await error.text();
            alert(errorText);
        }
    }

    async function createFormProduct(event)
    {
        debugger;
        let name = document.querySelector('#name_input').value;
        let description = document.querySelector('#description_input').value;
        let price = +document.querySelector('#price_input').value;
        let imageFile = document.querySelector('#form-wrapper #post-form .property-wrapper .custom-file-input').files[0];
        let imageURL = document.querySelector('#image_input').value;
        if(!validProperties(name, description, imageURL, imageFile)){
            return;
        }

        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Description', description);
        formData.append('Price', price);
        formData.append('ImageURL',imageURL);
        formData.append('Image', imageFile);

        let url = `${baseUrl}/products/Form`;//`${baseUrl}/categories/`;
        let data = await fetch(url, { 
                method: 'POST',
                body: formData
            });
        try {
            if(data.status === 201){
                alert('Created');
                window.location.href = `../Products/products.html?categoryId=${categoryId}`;//"http://127.0.0.1:5500/";
            }
        } catch(error){
            var errorText = await error.text();
            alert(errorText);
        }
    }
    
    async function fetchProduct()
    {
        const url = `${baseUrl}/products/`;//`${baseUrl}/categories/`;
        //let response = await fetch(url);
        //debugger;
                //let product = await response.json();
        let productCard = `
            <div id="form-image" style="background: url(https://images.unsplash.com/photo-1570038281437-550322a86cb8?ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&ixlib=rb-1.2.1&auto=format&fit=crop&w=334&q=80)">
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
                    <label for="price_input">Price</label>
                    <input type="text" name="name_input_price" id="price_input" class="create-input">
                </div>

                <div class="property-wrapper"> 
                    <label for="image_input">ImageURL</label>
                    <input type="text" name="name_input_image" id="image_input" class="create-input">
                </div>

                <div class="property-wrapper">
                    <label for="image_file_input">Image File</label>
                    <input type="file" name="image" id="image_file_input" class="create-input custom-file-input">
                </div>

                <div class="submit-container">
                    <button class="create-submit">Create</button>
                </div>
            </div>
        `;
        //var productContent = productCard.join('');
        document.querySelector('#form-wrapper').innerHTML = productCard;
        
        let createButton = document.querySelector('#post-form .create-submit'); /*.delete-btn[data-delete-product-id]*/ 
        createButton.addEventListener('click', createFormProduct); //createProduct
    }
    //debugger;
    var queryParams = window.location.search.split('?')[1];
    var queryParam1 = queryParams;
    var categoryId = queryParam1.split("=")[1];
    const baseUrl = `http://localhost:3030/api/categories/${categoryId}`;
    // alert(productId);
    fetchProduct();
    
});