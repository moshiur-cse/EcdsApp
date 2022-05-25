
const ViewChat = (email) => {
    $.ajax({
        type: "POST",
        url: '@Url.Action("PersonalChat")',
        data: { email: email },
        success: function (d) {
            console.log(d)

        }
    })
}


//====function to send message
const SendMsg=(url,receiver)=>{
    const data = { msg: $("#chat-msg").val(), receiver: receiver}
    console.log(data)
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (d) {
            console.log(d)
            if (d === "success") {
                ChatAppend(data.msg,'left')
            }

        }
    })
}


//====function for integrating Chats functionality for this user.
//const InsertChats=(arry,sender)=>{
//    $("#chat-msg-box").html("")
//    for (let i = 0; i < arry.length; i++) {
//        if () {
//            .append(`<div class="d-flex justify-content-start align-items-center my-4 mx-2">
//			<div class="chat-msg chat-left">Good</div>
//		</div>`)
//        }
//        else {
//    .append(`<div class="d-flex justify-content-end align-items-center my-4 mx-2">
//			<div class="chat-msg chat-right">what about you?</div>
//		</div>`)
//}
//        $("#chat-msg-box").append(``)
//    }
//}

const DisplayChat = (url,sender, receiver) => {
    const data = { receiver,sender}
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (d) {
            $("#chat-msg-box").html("")
            d.forEach(x => {
                console.log(x)
                if (x.sender == sender) {
                    ChatAppend(x.message,"right")
                }
                else{
                    ChatAppend(x.message, "left")
                }
            })
            
        }
    })
}

//====function for appending chat in the chatbox for this user.
const ChatAppend = (msg,alignment) => {
    if (alignment==='right')
        $("#chat-msg-box").append(`<div class="d-flex justify-content-end align-items-center my-2 mx-2">
        <div class="chat-msg chat-right">${msg}</div>
    </div>`)
    else if (alignment === 'left')
        $("#chat-msg-box").append(`<div class="d-flex justify-content-start align-items-center my-2 mx-2">
        <div class="chat-msg chat-left">${msg}</div>
    </div>`)
    //return
   
}