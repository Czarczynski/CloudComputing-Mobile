(function () {
    let txtMessage = $("#txtMessage");
    let btnSend = $("#btnSend");
    let listMessages = $("#listMessages");
    let userName = $("#userName").val();
    const connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();

    $(btnSend).on("click", () => {
        let userMessage = $(txtMessage).val();

        connection.invoke("SendMessage", {
            username: userName,
            message: userMessage
        }).catch((err) => {
            alert("Cannot send message");
            console.log({...err});
        });
        
        $(txtMessage).val("");

    });
    connection.start().then(() => {
        console.log("Connected to the signalrHub");
        $(btnSend).removeAttr("disabled");
    });
    
    connection.on("ReceiveMessage",function(obj){
        $(listMessages).prepend(`
                <li>
                    [${obj.timeStampString}]
                    <span class="font-weight-bold">user: ${obj.username}</span>
                    <span> | message: ${obj.message}</span>
                </li>`)
    })
})();