const connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/passengers")
    .withAutomaticReconnect()
    .build();

// this object will hold the.NET reference to call methods on
window.passengers = {
    dotNetRef: null,
    register(ref) { this.dotNetRef = ref; },
    unregister() { this.dotNetRef = null; } // clean component ref when dispose
};

function apply(type, data) {
    if (!window.passengers.dotNetRef) {
        console.warn("Passengers component not registered yet");
        return;
    }
    switch (type) {
        case "created":
            window.passengers.dotNetRef.invokeMethodAsync("AddPassenger", data)
                .catch(err => console.error("AddPassenger failed:", err));
            break;
        case "updated":
            window.passengers.dotNetRef.invokeMethodAsync("UpdatePassenger", data)
                .catch(err => console.error("UpdatePassenger failed:", err));
            break;
        case "deleted":
            window.passengers.dotNetRef.invokeMethodAsync("RemovePassenger", data)
                .catch(err => console.error("RemovePassenger failed:", err));
            break;
    }
}

connection.on("PassengerCreated", passenger => apply("created", passenger));
connection.on("PassengerUpdated", passenger => apply("updated", passenger));
connection.on("PassengerDeleted", id => apply("deleted", id));

connection.start()
    .then(() => console.log("Passenger hub connected"))
    .catch(err => console.error("SignalR error:", err));