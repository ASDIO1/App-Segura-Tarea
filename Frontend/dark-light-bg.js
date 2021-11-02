window.addEventListener('DOMContentLoaded', function(event){
    //debugger;
    const sec = document.querySelector('.sec');
    const toggle = document.querySelector('.sec .toggle-colors');

    toggle.onclick = function(){
        //debugger;
        sec.classList.toggle('dark');
    }
});