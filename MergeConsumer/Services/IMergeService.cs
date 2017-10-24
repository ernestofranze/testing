using N5.Entities.Customer.DataTransfer.DTO;

namespace MergeConsumer.Services
{
    public interface IMergeService
    {
        void Save(string id, MappedCustomerMessage message);
    }
}
