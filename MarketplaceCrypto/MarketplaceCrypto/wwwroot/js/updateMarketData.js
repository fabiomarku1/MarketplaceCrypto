var connection = new signalR.HubConnectionBuilder().withUrl("/hub/cryptos").build();


connection.on("updatemarket", (value) => {
    var tableBody = document.getElementById("cryptoData");
    

});


//invoke
function OnWindowLoad() {
    connection.send("UpdateCryptocurrencies");
}


function fulfilled() {
    console.log("Connected....");
}

function rejected() {
    console.log("USER IS NOT CONNECTED....");

}

//start
connection.start().then(fulfilled, rejected);