using System;
us

U
namespace Msmq.Sen

{
    public static class Transmit
    {
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
