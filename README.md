# Dotnet C# code for RabbitMQ tutorials

Here you can find the C# code examples for [RabbitMQ
tutorials](https://www.rabbitmq.com/getstarted.html).

To successfully use the examples you will need a running RabbitMQ server.

## Requirements

### Requirements on Windows

* [dotnet core](https://www.microsoft.com/net/core)

We're using the command line (start->run cmd.exe) to
compile and run -p the code. Alternatively you could [use Visual Studio](https://github.com/rabbitmq/rabbitmq-tutorials/tree/master/dotnet-visual-studio) and [NuGet package](https://www.nuget.org/packages/RabbitMQ.Client/), but this set of tutorials assumes
the command line.

### Requirements on Linux

* [dotnet core](https://www.microsoft.com/net/core)

## Code

Each command is best run in a separate console/terminal instance run from the root
of the tutorial directory.

First run the `recompile.sh` script. This will run `dotnet restore` and build
each project which is required before they can be run. Alternatively or if you are
on windows cd into each project and run `dotnet restore` manually.

#### [Tutorial one: "Hello World!"](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html)

```bash    
  dotnet run -p Receive/Receive.csproj
  dotnet run -p Send/Send.csproj
```

#### [Tutorial two: Work Queues](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)

```bash
  dotnet run -p Worker/Worker.csproj
  dotnet run -p NewTask/NewTask.csproj
```

#### [Tutorial three: Publish/Subscribe](https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html)

```bash
  dotnet run -p ReceiveLogs/ReceiveLogs.csproj
  dotnet run -p EmitLog/EmitLog.csproj
```

#### [Tutorial four: Routing](https://www.rabbitmq.com/tutorials/tutorial-four-dotnet.html)

```bash
  dotnet run -p ReceiveLogsDirect/ReceiveLogsDirect.csproj info
  dotnet run -p EmitLogDirect/EmitLogDirect.csproj
```

#### [Tutorial five: Topics](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html)

```bash
  dotnet run -p ReceiveLogsTopic/ReceiveLogsTopic.csproj anonymous.info
  dotnet run -p EmitLogTopic/EmitLogTopic.csproj
```

#### [Tutorial six: RPC](https://www.rabbitmq.com/tutorials/tutorial-six-dotnet.html)

```bash
  dotnet run -p RPCServer/RPCServer.csproj
  dotnet run -p RPCClient/RPCClient.csproj
```

#### [Tutorial seven: Publisher Confirms](https://www.rabbitmq.com/tutorials/tutorial-seven-dotnet.html)

```bash
  dotnet run -p PublisherConfirms/PublisherConfirms.csproj
```

## Docker

First you gonna build the Docker image.

```bash
docker build -t rabbitmq-dotnet:0.0.1 .
```

Then start a local server

```bash
docker network create rabbitmq-net
docker run --network rabbitmq-net -d -p 5627:5672 -p 15672:15672 --hostname rabbitmq --name rabbitmq-server rabbitmq:3-management
```
#### [Tutorial one: "Hello World!"](https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html)

```bash
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p Receive/Receive.csproj
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p Send/Send.csproj
```

#### [Tutorial two: Work Queues](https://www.rabbitmq.com/tutorials/tutorial-two-dotnet.html)

```bash
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq rabbitmq-dotnet:0.0.1 dotnet run -p Worker/Worker.csproj
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p NewTask/NewTask.csproj
```

#### [Tutorial three: Publish/Subscribe](https://www.rabbitmq.com/tutorials/tutorial-three-dotnet.html)

```bash
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p ReceiveLogs/ReceiveLogs.csproj
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p EmitLog/EmitLog.csproj
```

#### [Tutorial four: Routing](https://www.rabbitmq.com/tutorials/tutorial-four-dotnet.html)

```bash
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p ReceiveLogsDirect/ReceiveLogsDirect.csproj info
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p EmitLogDirect/EmitLogDirect.csproj
```

#### [Tutorial five: Topics](https://www.rabbitmq.com/tutorials/tutorial-five-dotnet.html)

```bash
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p ReceiveLogsTopic/ReceiveLogsTopic.csproj anonymous.info
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p EmitLogTopic/EmitLogTopic.csproj
```

#### [Tutorial six: RPC](https://www.rabbitmq.com/tutorials/tutorial-six-dotnet.html)

```
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p RPCServer/RPCServer.csproj
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p RPCClient/RPCClient.csproj
```

#### [Tutorial seven: Publisher Confirms](https://www.rabbitmq.com/tutorials/tutorial-seven-dotnet.html)

```bash
  docker run --network rabbitmq-net -e RABBITMQ_HOST=rabbitmq -d rabbitmq-dotnet:0.0.1 dotnet run -p PublisherConfirms/PublisherConfirms.csproj
```