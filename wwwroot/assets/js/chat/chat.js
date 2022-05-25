

//====show or hide chat panel based on chat icon click

$(".chat-circle").on("click", () => {
    if ($(".chat-box").hasClass("d-none")) {
        $(".chat-box").removeClass("d-none")
        $(".chat-box").addClass("d-block")
    }
    else {
        $(".chat-box").removeClass("d-block")
        $(".chat-box").addClass("d-none")
    }
})

$(".cross-btn").on("click", () => {
    $(".chat-box").removeClass("d-block")
    $(".chat-box").addClass("d-none")
})


//=====Load Available chats for the user
const DefaultUserMessages = (url) => {
    //$("#chat-msg-box").html("")
    $.ajax({
        type: "GET",
        url: url,
        success: function (d) {
            console.log(d)
            $("#chat-msg-box").html("")
            d.forEach(x => {
                MessageSenderInfo(x)
            })
        }
    })
}

//===Render User Message Component
const MessageSenderInfo = (d) => {
    let content = `<div class="row px-2" onclick="DisplayChatOnClick('${d.urlToGetIndvMsg}','${d.email}')">
				<div class="d-flex justify-content-start align-items-center">
					<img class="chat-usr-img me-4" src="${d.imageUrl}">
					<div class="">
						<div class="chat-usr-name">${d.name}</div>
						<span>${d.email}</span>
					</div>
				</div>
			</div>`
    $("#chat-msg-box").append(content)
}


//const ViewChat = (email) => {
//    $.ajax({
//        type: "POST",
//        url: '@Url.Action("PersonalChat")',
//        data: { email: email },
//        success: function (d) {
//            console.log(d)

//        }
//    })
//}


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




const DisplayChatOnClick = (url, sender) => {
    const data = { sender }
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (d) {
            $("#chat-msg-box").html("")
            console.log(d)
            d.forEach(x => {
                console.log(x)
                if (x.sender == sender) {
                    ChatAppend(x.message, "right")
                }
                else {
                    ChatAppend(x.message, "left")
                }
            })

        }
    })
}

//=====Display the chat based on url,sender & receiver 
//======built for the testing purpose

//const DisplayChat = (url,sender, receiver) => {
//    const data = { receiver,sender}
//    $.ajax({
//        type: "POST",
//        url: url,
//        data: data,
//        success: function (d) {
//            $("#chat-msg-box").html("")
//            d.forEach(x => {
//                console.log(x)
//                if (x.sender == sender) {
//                    ChatAppend(x.message,"right")
//                }
//                else{
//                    ChatAppend(x.message, "left")
//                }
//            })
            
//        }
//    })
//}

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