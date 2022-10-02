
const  navBtn = document.querySelector('.responsive_menu_log');
const mobileNav = document.querySelector('.mobile-nav');
navBtn.addEventListener('click', function (){
    
    mobileNav.classList.toggle('mobile-nav-active');
})
