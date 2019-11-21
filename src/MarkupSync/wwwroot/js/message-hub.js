const messageConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44301/message",
        {
            logger: signalR.LogLevel.Verbose
        })
    .build();

messageConnection.start().catch(err => console.error(err.toString()));
