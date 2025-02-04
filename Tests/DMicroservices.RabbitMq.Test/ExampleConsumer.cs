﻿using System;
using System.Threading;
using DMicroservices.RabbitMq.Base;
using DMicroservices.RabbitMq.Consumer;
using RabbitMQ.Client.Events;

namespace DMicroservices.RabbitMq.Test  
{
    [ListenQueue(typeof(Test), "QueueName")]
    class ExampleConsumer : BasicConsumer<ExampleModel>
    {
        public override bool AutoAck => false;
        public override ushort PrefectCount { get => 10; }

        public override bool Durable => false;
        public override bool AutoDelete => true;

        public override Action<ExampleModel, BasicDeliverEventArgs> DataReceivedAction => DataReceived;

        private void DataReceived(ExampleModel model, BasicDeliverEventArgs e)
        {
            Console.WriteLine(model.Message);

            Thread.Sleep(1000);
            //Send Ack.
            BasicAck(e.DeliveryTag, false);

        }

    }

    public class Test
    {
        public static string QueueName => "test";
    }
}
