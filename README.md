# Social-Networking-Application
In this project, we developed a social networking application implementing client and server modules. 
The Server module manages message transfers, notifications and friendship relationships among the users, and the Client module behaves as a user which adds/removes friends, accepts/rejects friendship requests, sends/receives messages and receives relevant notifications.
The server listens on a predefined port and accepts incoming client connections. There is one or more clients connected to the server at the same time. Each client knows the IP address and the listening port of the server.
Clients connect to the server on a corresponding port and identify themselves with their names. Server keeps the names of currently connected clients in order to avoid the same name to be connected more than once at a given time to the server.
On the server side, there is a predefined database of users which are presumed to be registered to the social network so we did not implement any registration process between a client and the server.
A client, whose name should be in the user database, is able to connect by providing his/her name only (no password or other type of security). 
The abovementioned introduction is for the entire project, which is completed in three steps.
