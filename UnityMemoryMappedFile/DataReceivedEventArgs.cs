using System;

namespace UnityMemoryMappedFile
{
    public class DataReceivedEventArgs : EventArgs
    {
        public DataReceivedEventArgs(Type commandType, string requestId, object data)
        {
            CommandType = commandType;
            RequestId = requestId;
            Data = data;
        }

        public Type CommandType { get; }
        public string RequestId { get; }
        public object Data { get; }
    }
}
