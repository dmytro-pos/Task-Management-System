using Task_Management_System.DataContexts;
using Task_Management_System.DataContexts.Models;
using Task_Management_System.Handlers.Interfaces;
using Task_Management_System.Helpers;

namespace Task_Management_System.Handlers
{
    public class ServiceBusHandler : IServiceBusHandler
    {
        public event Action<byte[]> TaskUpdated;
        private readonly TaskManagementInMemoryDatabase _taskManagementInMemoryDatabase;
        
        public ServiceBusHandler(TaskManagementInMemoryDatabase taskManagementInMemoryDatabase)
        {
            TaskUpdated += ConsumeTaskUpdateFromTheQueue;
            _taskManagementInMemoryDatabase = taskManagementInMemoryDatabase;
        }

        public void PublishTaskUpdateIntoTheQueue(TaskUpdateRequest taskUpdateRequest)
        {
            // Serealize data using protobuf
            byte[] serializedData = ProtobufHelper.Serialize(taskUpdateRequest);

            // In order to be able to run this locally I decided to imitate queue and use delegates,
            // instead of explicitly using Azure Service Bus or any other message broker
            TaskUpdated?.Invoke(serializedData);
        }

        private void ConsumeTaskUpdateFromTheQueue(byte[] serializedData)
        {
            // Deserealize data using protobuf
            var taskUpdateRequest = ProtobufHelper.Deserialize<TaskUpdateRequest>(serializedData);
            
            var task = _taskManagementInMemoryDatabase.Tasks.Where(t => t.Id == taskUpdateRequest.TaskId).FirstOrDefault();

            if (task == null) 
            {
                throw new Exception("Task with id: {taskUpdateRequest.TaskId} does not exist so it can't be updated");
            }
            if (taskUpdateRequest.NewStatus == task.Status)
            {
                throw new Exception("New status is same as existing status. Update operation will not be performed");
            }

            task.Status = taskUpdateRequest.NewStatus;

            _taskManagementInMemoryDatabase.SaveChanges();
        }
    }
}
