function ViewTicketStatus() {
                   debugger
                           $.ajax({
                               url: 'https://localhost:44370/BuildingManager/GetTicketStatus' ,
                               type: 'GET',
                               success: OnSuccess
               });
        }

function OnSuccess(response) {

    var emailList = response;
    list = document.getElementById("StatusList");


    emailList.forEach((item) => {
        li = document.createElement("li");
        li.innerText = item;
        list.appendChild(li);
    })
}