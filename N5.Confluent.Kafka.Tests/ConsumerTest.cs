using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace N5.Confluent.Kafka.Tests
{
    public class ConsumerTest
    {
        [Fact]
        public void ShouldConsumeTest()
        {            
            var task = new TaskFactory();
            task.StartNew(() => { 
                var consumer = new CustomConsumer<Null, string>(new StringDeserializer(Encoding.UTF8), null);
                consumer.Consume("nuevoTopico", (_, x) => { });
            });

            var producer = new CustomProducer<Null, string>(new StringSerializer(Encoding.UTF8), null);

            producer.Produce("nuevoTopico", null, "testo10").Wait();
            producer.Produce("nuevoTopico", null, "testo12").Wait();
            Thread.Sleep(18000);
        }
    }
}