using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace N5.Confluent.Kafka.Tests
{
    public class ProducerTest
    {
        [Fact]
        public async Task ShouldProduceMessasgeAsync()
        {
            var producer = new CustomProducer<Null, string>(new StringSerializer(Encoding.UTF8), null);
            await producer.Produce("test",null, "Hello world!").ContinueWith(x => Assert.True(x.IsCompletedSuccessfully));
        }
    }
}
