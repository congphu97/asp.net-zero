
function myTest(){
    console.log("1");
    $('body').delegate('input[type="text"]','keypress', function(evt) {
        console.log("2 ");
        // if (evt.which != 8 && evt.which != 0 && evt.which < 48 || evt.which > 57) //chi cho phep nhap so khong cho nhap chu
        if(evt.which==32 &&  evt.target.selectionStart === 0) 
         {
            evt.preventDefault();
            
         
        }
    });
    
};
