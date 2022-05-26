
//====set the location of bottom send button based on whole container height
//let heightCont = $("#chat-msg-box").height()
//console.log(heightCont / 3)
//$(".msg-send-cont").css("bottom", heightCont / 3 + "px")



//====show or hide chat panel based on chat icon click
//const window = require("../../libs/inputmask/inputmask/global/window")

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



//====go to default mode showing only user message when the esc button is pressed

$(document).keyup(function (e) {
    if (e.key === "Escape") { // escape key maps to keycode `27`
        TakeToInitialState()

        //=====now the menu is on default state show message "Click to view full chat."
        $(".chat-info-box").text("Click to view full chat.")
    }
    
});

const TakeToInitialState = () => {
    $("#chat-msg-box").html("")
    if (window.sessionStorage.getItem("defaultUrl") != undefined) {
        let url = window.sessionStorage.getItem("defaultUrl")
        DefaultUserMessages(url)
    }

}

//=====Load Available chats for the user
const DefaultUserMessages = (url) => {
    //$("#chat-msg-box").html("")
    window.sessionStorage.setItem("defaultUrl",url)
    $.ajax({
        type: "GET",
        url: url,
        success: function (d) {
            console.log(d)
            $("#chat-msg-box").html("")
            //===set sessionStorage so that we can know where to send message
            window.sessionStorage.setItem("receiver",d[0].email)
            d.forEach(x => {
                MessageSenderInfo(x)
            })
        }
    })
}

//===Render User Message Component
const MessageSenderInfo = (d) => {
    let content = `<div class="pointer row px-2" onclick="DisplayChatOnClick('${d.urlToGetIndvMsg}','${d.email}')">
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
const SendMsg = (url) => {
    let receiver
    if (window.sessionStorage.getItem("receiver") != undefined)
        receiver = window.sessionStorage.getItem("receiver")
    else {
        $("#chat-msg").val("")
        return
    }
    const data = { msg: $("#chat-msg").val(), receiver: receiver}
    console.log(data)
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        success: function (d) {
            console.log(d)
            if (d === "success") {
                ChatAppend(data.msg, 'left')
                $("#chat-msg").val("")

                //===set send box section at right place
                let heightCont = 40 * (($("#chat-msg-box div").length / 2) - 2)
                console.log(heightCont)
                $(".msg-send-cont").css("bottom", "-" + heightCont + "px")
            }

        }
    })
}




const DisplayChatOnClick = (url, sender) => {

    //====set the location of bottom send button based on whole container height
    let heightCont = ($(".chat-box").height()/3)-20
    console.log(heightCont)
    $(".msg-send-cont").css("bottom", "-" + heightCont+ "px")


    //====show chat sending box
    $(".msg-send-cont").removeClass("d-none")
    $(".msg-send-cont").addClass("d-block")

    //=====now chat is displayed and set message to "press ESC to to go default section"
    $(".chat-info-box").text("Press ESC to to go default section.")

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
                if (x.receiver == sender) {
                    ChatAppend(x.message, "left")
                }
                else {
                    ChatAppend(x.message, "right")
                }
            })

            //====set the location of bottom send button based on whole container height
            let heightCont = 36*(($("#chat-msg-box div").length/2)-1)
            console.log(heightCont)
            $(".msg-send-cont").css("bottom", "-" + heightCont + "px")

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

}