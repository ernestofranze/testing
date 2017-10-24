using Autofac;
using MergeConsumer.Services;
using N5.Confluent.Kafka;
using N5.Entities.Customer.DataTransfer.DTO;
using System;
using System.Collections.Generic;

namespace MergeConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-- MERGE --");

            // consumer: recibe mensajes mapeados
            // TODO: Autofac!
            var consumer = new CustomConsumer<string, MappedCustomerMessage>(new ObjectSerializer<MappedCustomerMessage>(), new ObjectSerializer<string>());

            IContainer container = Startup.BuildContainer();
            var mergeService = container.Resolve<IMergeService>();

            consumer.Consume("customers", (_, msg) => {
                mergeService.Save(msg.Key, msg.Value);
            });

            while (true)
            {

            }
        }
    }
}
