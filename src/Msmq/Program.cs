using System;
using System.Messaging;

namespace Msmq
{

    // This class demonstrates how to set up a simple queue using msmq and pushing data into the queue
    //MSMQ has to be enabled on windows then create a private queue and replace the host name and queue name appropriately
    public class Program
    {
        private static MessageQueue _queue = null;
        private static readonly object LockObject = new object();

        public enum MessageType
        {
            MessageTypePlainText = 0,
            MessageTypeHelloWorld = 1
        };

        public static void Main(string[] args)
        {
            // Open the queue.
            using (_queue = new MessageQueue("FormatName:DIRECT=OS:[HostName]\\private$\\[QueueName]"))
            {
                // Enable the AppSpecific field in the messages.
                _queue.MessageReadPropertyFilter.AppSpecific = true;
                _queue.Formatter = new BinaryMessageFormatter();

                _queue.ReceiveCompleted += queue_ReceiveCompleted;

                _queue.BeginReceive();
                Console.Write("Queue is ready to receive");
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
                    SampleData sampleData = (SampleData)e.Message.Body;
                    Console.WriteLine("Message received: " + sampleData.GetText());
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

        public static void SendToQueue(string message)
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