const connection = new signalR.HubConnectionBuilder()
  .withUrl("/hubs/passengers")
  .withAutomaticReconnect()
  .build();

function reload() {

  location.reload();
}

connection.on("PassengerCreated", reload);
connection.on("PassengerUpdated", reload);
connection.on("PassengerDeleted", reload);

connection.start().catch(err => console.error("SignalR error:", err));