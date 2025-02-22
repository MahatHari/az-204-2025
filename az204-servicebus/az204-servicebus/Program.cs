using Azure.Messaging.ServiceBus;
// See https://aka.ms/new-console-template for more information

// connecyion string to your service bus space name
string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
Console.WriteLine($"ConnectionString: {connectionString}");

// name of your serivce bus topic 
string queueName = Environment.GetEnvironmentVariable("QueueName");


// the client that owns the connection and can be ued to create senders and receivers
ServiceBusClient client;

// the sender user to publish message to the queue
ServiceBusSender sender;


// create the clients that w be used for sending and processing messages, could have been done in one line above
client = new ServiceBusClient(connectionString);

sender = client.CreateSender(queueName);

// ceate a batch
using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

for (int i = 0; i < 3; i++)
{
    // try adding message to the batch messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}"));
    if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
    {
        Console.WriteLine("Failed to create message");
        throw new Exception($"Exception {i} has occured");
    }

}

try
{
    // use the producer client to send the batch message await sender.SendMessagesAsync(messageBatch);
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine($"A batch of {messageBatch.Count} messages has been published to queue {queueName}");
}
finally
{
    // Calling DisposeAsync on client types is required to ensure that network
    // resources and other unmanaged objects are properly cleaned up
    await sender.DisposeAsync();
    await client.DisposeAsync();

}
Console.WriteLine("Follow the directions in the exercise to review the results in the Azure portal.");
Console.WriteLine("Press any key to continue");
Console.ReadKey();



// Receiving message 
// as we had closed the connection  by client.DisposeAsync(), we have to create it again to recive the message 
client = new ServiceBusClient(connectionString);
ServiceBusProcessor messageProcessor;

// crea processor that we can use to process the message
messageProcessor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

try
{
    // add handler to the process message
    messageProcessor.ProcessMessageAsync += MessageHandlder;

    // add handler to process any error
    messageProcessor.ProcessErrorAsync += ErrorHandler;

    // start processing 
    await messageProcessor.StartProcessingAsync();

    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();

    // stop processing 
    Console.WriteLine("\nStopping the receiver...");
    await messageProcessor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");

}
finally
{
    // Calling Dispose Async on Client types is required to ensure that network
    // resources and other unmanaged objects are properly cleaned up 
    await messageProcessor.DisposeAsync();
    await client.DisposeAsync();
}

// handle received message 
async Task MessageHandlder(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();

    Console.WriteLine($"Recived : {body}");

    // Complete the message. message is deleted from the queue.
    await args.CompleteMessageAsync(args.Message);
}

Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}



