

window.addEventListener('DOMContentLoaded', function(event){

    function goToEditProduct(){
        let productId = this.dataset.editProductId;
        //alert(productId);
        window.location.href = `./editProduct.html?categoryId=${categoryId}&productId=${productId}`;
    }

    function goToCreateProduct(){
        window.location.href = `./createProduct.html?categoryId=${categoryId}`;
    }

    function DeleteProduct(event){
        //debugger;
        let productId = this.dataset.deleteProductId;
        //debugger;
        let url = `${baseUrl}/products/${productId}`;//`${baseUrl}/categories/${productId}`;
        fetch(url, { 
        method: 'DELETE' 
        }).then((data)=>{
            if(data.status === 200){
                alert('deleted');
                window.location.reload(); //Reloads the page
            }
        }); 
    }

    function cardsGeneralFilter() {
        debugger;
//return productCards.filter(card => card.includes(`<h2>C`));
        //filter = "name_a-z";
        let productCards = document.querySelectorAll('.card');
        let filter = document.querySelector('.filter-container .general-filter-wrapper #general-filter').value;
        if(filter === "name_a-z"){
            /*let x = productCards[0].innerHTML;
            productCards.sort((a,b) => {
                return a.innerHTML.toLowerCase().localeCompare(b.innerHTML.toLowerCase());
            });//card => card.innerHTML);
            let orderCards = cardNames.sort();*/

            /*let cardNames = productCards.map(card => {
                return card.substring(card.substring(card.indexOf(`<div class="card-name"><h2>`), +1))
            });*/
            return productCards.filter(card => card.includes(`<h2>C`));
        }
        else if(filter === "name_a-z"){
            return productCards;
        }
        else if(filter === "name_z-a"){
            return productCards;
        }
        else if(filter === "price_low-high"){
            return productCards;
        }
        else if(filter === "price_high-low"){
            return productCards;
        }
        else if(filter === "none"){
            return productCards;
        }
        else{
            return productCards;
        }
        
    }

    function cardsSpecificFilter(){
        debugger;
        let filter = document.querySelector('.filter-container .specific-filter-wrapper #specific-filter').value;
        let selector = (filter==="name")? '.card-name' : (filter==="description")? '.card-description' : 'none';
        let val = document.querySelector('#specific-filter-input').value;//"cake";
        val = val.toUpperCase();
        let cards = document.querySelectorAll('.card');
    
        if(selector != "none"){
            cards.forEach(card => {
                debugger;
                let name = card.querySelector(`.card-properties ${selector}`).innerText.toUpperCase();
                if(!name.includes(val))
                    card.style.display = "none";
                else{
                    card.style.display = "block";
                }
            });
        }
        
        //window.location.reload();

    }
    
    async function fetchProducts()
    {
        //debugger;
        const url = `${baseUrl}/products`;//`${baseUrl}/categories`;
        let response = await fetch(url);
        //debugger;
        try{
            if(response.status == 200){
                let data = await response.json();
                let productCards = data.map(product => { 
                //debugger;
                let backImageUrl = product.imagePath? 
                `${baseRawUrl}/${product.imagePath}`.replace(/\\/g, "/") : product.imageUrl;    
                return `
                <div class="card" style="background: url(${backImageUrl})">
                    <div class="card-top">
                        <div class="card-properties"> 
                            <div class="card-name"> 
                                <h2>${product.name}</h2> <!--max35 chars-->
                            </div>
                            <div class="card-description"> 
                                <p>${product.description}</p> <!--max270 chars-->
                            </div>
                            <div class="card-price"> 
                                <p><span class="price"></span>${product.price}<span20 class="coin">Bs</span20></p>
                            </div>
                        </div>
                    </div>
                
                    <div class="card-bottom">
                        <div class="btn-wrapper">
                            <div class="edit-container" data-edit-product-id="${product.id}">
                                <button class="btn edit-btn">edit</button>
                                <div class="fill-btn"> </div>
                            </div>
                            <div class="delete-container" data-delete-product-id="${product.id}">
                                <button class="btn delete-btn">delete</button>
                                <div class="fill-btn"> </div>
                            </div>
                        </div>
                    </div>
                    
                    <div class="card-overlay"></div>
                </div>`});
                //debugger;
                /*let filteredCards = cardsFilter(productCards);//.querySelector('.card-properties .card-name h2').innerText == "CHOCOLATE CAKE");
                var productsContent = filteredCards.join('');
                document.getElementById('cards-container').innerHTML = productsContent;*/
                var productsContent = productCards.join('');
                document.getElementById('cards-container').innerHTML = productsContent;
                
                let delButtons = document.querySelectorAll('#cards-container .card .card-bottom .btn-wrapper .delete-container'); /*.delete-btn[data-delete-product-id]*/ 
                for (let button of delButtons) {
                    button.addEventListener('click', DeleteProduct);
                }

                let editButtons = document.querySelectorAll('#cards-container .card .card-bottom .btn-wrapper .edit-container'); /*.delete-btn[data-delete-product-id]*/ 
                for (let button of editButtons) {
                    button.addEventListener('click', goToEditProduct);
                }

                let createButton = document.querySelector('#navigation-container .create-container');//('.navigation .create-container');
                createButton.addEventListener('click', goToCreateProduct)

                let specific_filter_btn = document.querySelector('#specific-filter-btn');
                specific_filter_btn.addEventListener('click', cardsSpecificFilter)

                /*let general_filter_btn = document.querySelector('#general-filter-btn');
                general_filter_btn.addEventListener('click', cardsGeneralFilter)*/
                
            } else {
                var errorText = await response.text();
                alert(errorText);
            }
        } catch(error){
            var errorText = await error.text;
            // alert(errorText);
        }
    }

    baseRawUrl = 'http://localhost:3030';
    var queryParams = window.location.search.split('?');
    var categoryId = queryParams[1].split('=')[1];
    const baseUrl = `http://localhost:3030/api/categories/${categoryId}`;
    //document.getElementById('fetch-btn').addEventListener('click', fetchTeams);
    fetchProducts();
    
});

//https://www.freecodecamp.org/news/a-practical-es6-guide-on-how-to-perform-http-requests-using-the-fetch-api-594c3d91a547/