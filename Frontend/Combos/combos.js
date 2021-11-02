
window.addEventListener('DOMContentLoaded', function(event){

    function goToProducts(){
        //debugger;
        let categoryId = this.dataset.cardCategoryId;
        window.location.href = `../Products/products.html?categoryId=${categoryId}`;
    }

    function goToCreateCombo(){
        window.location.href = `createCombo.html`;
    }
    
    async function fetchCombos()
    {
        const url = `${baseUrl}/combos`;
        let response = await fetch(url, { 
            method: 'GET',
            "Authorization": `Bearer ${sessionStorage.getItem("jwt")}`
            });
        try{
            if(response.status == 200){
                let data = await response.json();
                let comboCards = data.map(combo => { 
                    //debugger;
                    let backImageUrl = combo.imagePath? 
                    `${baseRawUrl}/${combo.imagePath}`.replace(/\\/g, "/") : combo.imageUrl;
                    return `
                    <div class="card" style="background: url(${backImageUrl})">
                        <div class="card-top" >
                            <div class="card-properties"> 
                                <div class="card-name"> 
                                    <h2>${combo.name}</h2> <!--max35 chars-->
                                </div>
                                <div class="card-description"> 
                                    <p>${combo.description}</p> <!--max270 chars-->
                                </div>
                                <div class="card-price"> 
                                <p><span class="price"></span>${combo.price}<span20 class="coin">Bs</span20></p>
                            </div>
                            </div>
                        </div>
                    
                        <div class="card-bottom">
                            <div class="btn-wrapper">
                                <div class="edit-container" data-edit-combo-id="${combo.id}">
                                    <button class="btn edit-btn">edit</button>
                                    <div class="fill-btn"> </div>
                                </div>
                                <div class="delete-container" data-delete-combo-id="${combo.id}">
                                    <button class="btn delete-btn">delete</button>
                                    <div class="fill-btn"> </div>
                                </div>
                            </div>
                        </div>
                        
                        <div class="card-overlay" data-card-combo-id="${combo.id}"></div>
                    </div>`});
                
                var combosContent = comboCards.join('');
                document.getElementById('cards-container').innerHTML = combosContent;

                let createButton = document.querySelector('#navigation-container .create-container');//('.navigation .create-container');
                createButton.addEventListener('click', goToCreateCombo)
                
                let productCard = document.querySelectorAll('#cards-container .card .card-overlay');//.card-top');
                for (let card of productCard) {
                    card.addEventListener('click', goToProducts);
                }
                
                /*let specific_filter_btn = document.querySelector('#specific-filter-btn');
                specific_filter_btn.addEventListener('click', cardsSpecificFilter)*/
                
            } else {
                var errorText = await response.text();
                alert(errorText);
            }
        } catch(error){
            var errorText = await error.text;
            alert(errorText);
        }
    }

    // VALIDATING JWT TOKEN FOR ACCESS 
    /*if(!Boolean(sessionStorage.getItem("jwt"))){
        window.location.href = "combos.html";
    }*/

    const baseRawUrl = 'http://localhost:3030';
    const baseUrl = 'http://localhost:3030/api';
    fetchCombos();
});

//https://www.freecodecamp.org/news/a-practical-es6-guide-on-how-to-perform-http-requests-using-the-fetch-api-594c3d91a547/