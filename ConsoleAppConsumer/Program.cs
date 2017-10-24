using Autofac;
using Confluent.Kafka;
using N5.Confluent.Kafka;
using N5.Entities.Customer;
using N5.Entities.Customer.DataTransfer;
using N5.Entities.Customer.DataTransfer.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConsoleAppConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-- CONSUMER --");

            // autofac container
            IContainer container = CustomerContainerBuilder.Build();
            var _customerMapper = container.Resolve<CustomerMapper>();

            // customerProducer: manda al topic Customers datos de clientes procesados
            var customerProducer = new CustomProducer<string, MappedCustomerMessage>(new ObjectSerializer<MappedCustomerMessage>(), new ObjectSerializer<string>());

            // rawConsumer: recibe mensajes crudos de integracion
            var rawConsumer = new CustomConsumer<Null, CustomerMessage>(new Serializer<CustomerMessage>(), null);

            rawConsumer.Consume("customersRaw", (_, msg) => {
                var mapped = _customerMapper.Handle(msg.Value);
                customerProducer.Produce("customers", mapped.CustomerId.ToString(), mapped).Wait();
                Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {JsonConvert.SerializeObject(mapped)}");
            });

            while (true)
            {

            }
        }
    }
}
