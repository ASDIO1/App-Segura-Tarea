window.addEventListener('DOMContentLoaded', function(event){

    function validProperties(name, description, price, image, imageFile){
        //debugger;
        let name_placeholder = document.querySelector('#name_input').placeholder
        let description_placeholder = document.querySelector('#description_input').placeholder
        let price_placeholder = document.querySelector('#price_input').placeholder
        let image_placeholder = document.querySelector('#image_input').placeholder
        if (name === "" || description === "" || (image === "" && !imageFile) || price === 0){
            alert("Not valid values. Please fill all the fields");
            return false;
        }
        /*else if(name === name_placeholder && description === description_placeholder && image === image_placeholder && price == price_placeholder){
            alert("Nothing changed");
            window.location.href = `../Products/products.html?categoryId=${categoryId}`;//"http://127.0.0.1:5500/";
        }*/
        
        else{
            return true;
        }
    }
    
    function editProduct()
    {
        debugger;
        let name = document.querySelector('#name_input').value;
        let description = document.querySelector('#description_input').value;
        let price = +document.querySelector('#price_input').value;
        let image = document.querySelector('#image_input').value;

        if(!validProperties(name, description, price, image)){
            return;
        }
        let url = `${baseUrl}/products/${productId}`;//`${baseUrl}/categories/${productId}`;
        fetch(url, { 
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'PUT',
            body: JSON.stringify({
                Name: name,
                Description: description,
                Price: price,
                ImageUrl: image
            })
            }).then((data)=>{
                if(data.status === 200){
                    //debugger;
                    //window.location.href = "http://127.0.0.1:5500/";
                    alert('edited');
                    window.location.href = `../Products/products.html?categoryId=${categoryId}`;//"http://127.0.0.1:5500/";
                    //window.location.reload(); //Reloads the page
                }
            }).catch((errormessage) => {
                //alert(errormessage);
            });
        
        //window.location.href = "http://127.0.0.1:5500/";
    }
    
    function editFormProduct()
    {
        //debugger;
        let name = document.querySelector('#name_input').value;
        let description = document.querySelector('#description_input').value;
        let price = +document.querySelector('#price_input').value;
        let imageUrl = document.querySelector('#image_input').value; 
        let imageFile = document.querySelector('#form-wrapper #post-form .property-wrapper .custom-file-input').files[0];
        if(!validProperties(name, description, price, imageUrl)){
            return;
        }

        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Description', description);
        formData.append('Price',price);
        formData.append('Rating',5);
        formData.append('ImageUrl',imageUrl);
        formData.append('Image', imageFile);

        let url = `${baseUrl}/products/${productId}/Form`;//`${baseUrl}/categories/${productId}`;
        let options = { 
            method: 'PUT',
            body: formData
            }
        
        fetch(url, options).then((data)=>{
                //delete options.headers['Content-Type'];
                if(data.status === 200){
                    //debugger;
                    //window.location.href = "http://127.0.0.1:5500/";
                    alert('edited');
                    window.location.href = `../Products/products.html?categoryId=${categoryId}`;//"http://127.0.0.1:5500/";
                    //window.location.reload(); //Reloads the page
                }else {
                    data.text()
                    .then((error)=>{
                        alert('edited');
                        //alert(`ARREGLAR ESTO Y QUE NO SALGA ERROR${error}`);
                        //console.log(error);
                        window.location.href = `../Products/products.html?categoryId=${categoryId}`;
                    });
                }
            });
        
        //window.location.href = "http://127.0.0.1:5500/";
    }
    
    async function fetchProduct()
    {
        //debugger;
        const url = `${baseUrl}/products/${productId}`;//`${baseUrl}/categories/${productId}`;
        let response = await fetch(url);
        //debugger;
        try{
            if(response.status == 200){
                //debugger;
                let product = await response.json();
                let backImageUrl = product.imagePath? 
                    `${baseRawUrl}/${product.imagePath}`.replace(/\\/g, "/") : product.imageUrl;
                
                let productCard = `
                    <div id="form-image" style="background: url(${backImageUrl})">
                    </div>
                    <div id="post-form">
                        <h2>Edition Info</h2>
                        <div class="property-wrapper"> 
                            <label for="name_input">Name</label>
                            <input type="text" name="name_input_name" id="name_input" placeholder="${product.name}" value="${product.name}">
                        </div>

                        <div class="property-wrapper"> 
                            <label for="description_input">Description</label>
                            <input type="text" name="name_input_description" id="description_input" placeholder="${product.description}" value="${product.description}">
                        </div>
                        
                        <div class="property-wrapper"> 
                            <label for="price_input">Price</label>
                            <input type="text" name="name_input_price" id="price_input" placeholder="${product.price}" value="${product.price}">
                        </div>
                        
                        <div class="property-wrapper"> 
                            <label for="image_input">ImageURL</label>
                            <input type="text" name="name_input_image" id="image_input" placeholder="${product.imageUrl}" value="${product.imageUrl}">
                        </div>

                        <div class="property-wrapper">
                            <label for="image_file_input">Image File</label>
                            <input type="file" name="image" id="image_file_input" class="edit-input custom-file-input" placeholder="${product.imagePath}" value="${product.imagePath}>
                        </div>

                        <div class="submit-container">
                            <button class="edit-submit">Edit</button>
                        </div>
                    </div>
                `;
                //var productContent = productCard.join('');
                document.querySelector('#form-wrapper').innerHTML = productCard;
                
                let editButton = document.querySelector('.edit-submit'); /*.delete-btn[data-delete-product-id]*/ 
                editButton.addEventListener('click', editFormProduct);//editProduct
                
            } else {
                var errorText = await response.text();
                alert(errorText);
            }
        } catch(error){
            var errorText = await error.text();
            alert(errorText);
        }
    }
    //debugger;
    var queryParams = window.location.search.split('?')[1];
    var queryParam1 = queryParams.split("&")[0];
    var queryParam2 = queryParams.split("&")[1];

    var categoryId = queryParam1.split("=")[1];
    var productId = queryParam2.split("=")[1];
    
    const baseUrl = `http://localhost:3030/api/categories/${categoryId}`;
    const baseRawUrl = 'http://localhost:3030';
    fetchProduct(productId);
    
});