using Confluent.Kafka;
using N5.Confluent.Kafka;
using N5.Entities.Customer.DataTransfer.DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConsoleAppProducer
{
    class Program
    {
        private const int defaultElapsedTime = 50;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Por favor ingresar argumentos: fileName [timeElapse]");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("-- PRODUCER --");
            Console.ReadLine();
            string fileName = (string)args[0];
            int timeElapse = args.Length == 2 ? Convert.ToInt32(args[1]) : defaultElapsedTime;

            var messages = LoadMessages(fileName);

            var producer = new CustomProducer<Null, CustomerMessage>(new Serializer<CustomerMessage>(), null);

            while (messages.Any())
            {
                var message = messages.Pop();
                producer.Produce("customersRaw", null, message).Wait();
                Console.WriteLine($"salio {JsonConvert.SerializeObject(message)}");
                //Thread.Sleep(timeElapse);
            }

            while (true) { }
        }

        public static Stack<CustomerMessage> LoadMessages(string fileName)
        {
            var messages = LoadJson<CustomerMessage>(fileName);

            foreach (var msg in messages)
            {
                msg.Data = msg.Data.Where(d => d.Value != null).ToDictionary(d => d.Key, d => d.Value);
            }

            return messages;
        }

        public static Stack<T> LoadJson<T>(string jsonFileName)
        {
            using (StreamReader r = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), jsonFileName)))
            {
                string json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Stack<T>>(json);
            }
        }
    }
}
