using System;
using System.Messaging;

namespace Queue
{
    public class Msmq
    {
        private static MessageQueue _queue = null;
        private static readonly object LockObject = new object();

        public enum MessageType
        {
            MessageTypePlainText = 0,
            MessageTypeHelloWorld = 1
        };

        private static void Initialize()
        {
            // Open the queue.
            using (_queue = new MessageQueue(".\\MyQueue"))
            {
                // Enable the AppSpecific field in the messages.
                _queue.MessageReadPropertyFilter.AppSpecific = true;
                _queue.Formatter = new BinaryMessageFormatter();

                _queue.ReceiveCompleted += queue_ReceiveCompleted;

                _queue.BeginReceive();
                while (Console.ReadKey().Key != ConsoleKey.Q)
                {
                    // Press q to exit.
                }
            }
        }

        // Event to listen for new messages.
        private static void queue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            lock (LockObject)
            {
                if ((MessageType)e.Message.AppSpecific == MessageType.MessageTypeHelloWorld)
                {
                    HelloWorld helloWorld = (HelloWorld)e.Message.Body;
                    Console.WriteLine("Message received: " + helloWorld.GetText());
                }
                else
                {
                    string text = (string)e.Message.Body;
                    Console.WriteLine("Message received: " + text);
                }
                // Listen for the next message.
                _queue.BeginReceive();
            }
        }

        private static void SendToQueue(string message)
        {
            using (MessageQueueTransaction mqt = new MessageQueueTransaction())
            {
                mqt.Begin();
                //create message
                Message myMessage = new Message(message, new BinaryMessageFormatter());
                myMessage.Label = "MSMQ Message";
                myMessage.AppSpecific = (int)MessageType.MessageTypePlainText;
                _queue.Send(myMessage, mqt);
                mqt.Commit();
            }
        }
    }
}