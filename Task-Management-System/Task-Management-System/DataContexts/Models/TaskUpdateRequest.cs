using ProtoBuf;

namespace Task_Management_System.DataContexts.Models
{
    [ProtoContract]
    public class TaskUpdateRequest
    {
        [ProtoMember(1)]
        public int TaskId { get; set; }

        [ProtoMember(2)]
        public TaskStatusEnum NewStatus { get; set; }

        [ProtoMember(3)]
        public string UpdatedBy { get; set; }
    }
}
