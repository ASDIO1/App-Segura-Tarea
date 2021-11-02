window.addEventListener('DOMContentLoaded', function(event){

    function validProperties(name, description, image, imageFile){
        //debugger;
        let name_placeholder = document.querySelector('#name_input').placeholder
        let description_placeholder = document.querySelector('#description_input').placeholder
        let image_placeholder = document.querySelector('#image_input').placeholder
        if (name === "" || description === "" || (image === "" && !imageFile)){
            alert("Not valid values. Please fill all the fields");
            return false;
        }
        else if(name === name_placeholder && description === description_placeholder && image === image_placeholder){
            alert("Nothing changed");
            window.location.href = "categories.html";//"http://127.0.0.1:5500/";
        }
        
        else{
            return true;
        }
    }
    
    function editCategory(){
        debugger;
        let name = document.querySelector('#name_input').value;
        let description = document.querySelector('#description_input').value;
        let image = document.querySelector('#image_input').value;

        if(!validProperties(name, description, image)){
            return;
        }
        let url = `${baseUrl}/categories/${categoryId}`;//`${baseUrl}/categories/${productId}`;
        fetch(url, { 
            headers: { "Content-Type": "application/json; charset=utf-8" },
            method: 'PUT',
            body: JSON.stringify({
                Name: name,
                Description: description,
                ImageUrl: image
            })
            }).then((data)=>{
                if(data.status === 200){
                    //debugger;
                    //window.location.href = "http://127.0.0.1:5500/";
                    alert('edited');
                    window.location.href = "categories.html";//"http://127.0.0.1:5500/";
                    //window.location.reload(); //Reloads the page
                }
            }).catch((errormessage) => {
                alert(errormessage);
            });
        
        //window.location.href = "http://127.0.0.1:5500/";
    }

    function editFormCategory()
    {
        //debugger;
        let name = document.querySelector('#name_input').value;
        let description = document.querySelector('#description_input').value;
        let imageFile = document.querySelector('#form-wrapper #post-form .property-wrapper .custom-file-input').files[0];
        let imageUrl = document.querySelector('#image_input').value;
        /*if(!validProperties(name, description, imageUrl, imageFile)){
            return;
        }*/
        
        const formData = new FormData();
        formData.append('Name', name);
        formData.append('Description', description);
        formData.append('ImageURL',imageUrl);
        formData.append('Image', imageFile);
        
        let url = `${baseUrl}/categories/${categoryId}/Form`;//`${baseUrl}/categories/${productId}`;
        fetch(url, { 
            method: 'PUT',
            body: formData
            }).then((data)=>{
                if(data.status === 200){
                    //debugger;
                    //window.location.href = "http://127.0.0.1:5500/";
                    alert('edited');
                    window.location.href = "categories.html";//"http://127.0.0.1:5500/";
                    //window.location.reload(); //Reloads the page
                } else {
                    data.text()
                    .then((error)=>{
                        alert('edited');
                        //alert(error);
                        window.location.href = "categories.html";
                    });
                }
            });/*.catch((errormessage) => {
                alert(errormessage);
            });*/
        
        //window.location.href = "http://127.0.0.1:5500/";
    }
    
    async function fetchCategory()
    {
        //debugger;
        const url = `${baseUrl}/categories/${categoryId}`;//`${baseUrl}/categories/${productId}`;
        let jwtoken = sessionStorage.getItem("jwt");
        let response = await fetch(url, {
            headers: { 
                "Content-Type": "application/json; charset=utf-8",
                "Authorization": `Bearer ${jwtoken}`  
            },
            method: 'GET'
        });
        try{
            if(response.status == 200){
                let category = await response.json();
                //debugger;
                let backImageUrl = category.imagePath? 
                    `${baseRawUrl}/${category.imagePath}`.replace(/\\/g, "/") : category.imageUrl;
                let categoryCard = `
                    <div id="form-image" style="background: url(${backImageUrl})">
                    </div>
                    <div id="post-form">
                        <h2>Edition Info</h2>
                        <div class="property-wrapper"> 
                            <label for="name_input">Name</label>
                            <input type="text" name="name_input_name" id="name_input" placeholder="${category.name}" value="${category.name}">
                        </div>

                        <div class="property-wrapper"> 
                            <label for="description_input">Description</label>
                            <input type="text" name="name_input_description" id="description_input" placeholder="${category.description}" value="${category.description}">
                        </div>
                        
                        <div class="property-wrapper"> 
                            <label for="image_input">ImageURL</label>
                            <input type="text" name="name_input_image" id="image_input" placeholder="${category.imageUrl}" value="${category.imageUrl}">
                        </div>

                        <div class="property-wrapper">
                            <label for="image_file_input">Image File</label>
                            <input type="file" name="image" id="image_file_input" class="edit-input custom-file-input" placeholder="${category.imagePath}" value="${category.imagePath}>
                        </div>

                        <div class="submit-container">
                            <button class="edit-submit">Edit</button>
                        </div>
                    </div>
                `;
                //debugger;
                //var productContent = productCard.join('');
                document.querySelector('#form-wrapper').innerHTML = categoryCard;
                
                let editButton = document.querySelector('.edit-submit'); /*.delete-btn[data-delete-product-id]*/ 
                editButton.addEventListener('click', editFormCategory);//editCategory
                
            } else {
                var errorText = await response.text();
                //alert(errorText);
            }
        } catch(error){
            var errorText = await error.text;
            alert(errorText);
        }
    }

    const baseRawUrl = 'http://localhost:3030';
    const baseUrl = 'http://localhost:3030/api';
    var queryParams = window.location.search.split('?');
    var categoryId= queryParams[1].split('=')[1];
    // alert(categoryId);
    fetchCategory(categoryId);
    
});