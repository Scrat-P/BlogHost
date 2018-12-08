const commentHubConnection = new signalR.HubConnectionBuilder().withUrl("/comment")
    .configureLogging(signalR.LogLevel.Information).build();

commentHubConnection.start();