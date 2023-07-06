
using Task_Management_System.DataContexts.Models;

namespace Task_Management_System.Handlers.Interfaces
{
    public interface IServiceBusHandler
    {
        public void PublishTaskUpdateIntoTheQueue(TaskUpdateRequest serializedData);
    }
}
