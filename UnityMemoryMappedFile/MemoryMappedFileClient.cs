namespace UnityMemoryMappedFile
{
    public class MemoryMappedFileClient : MemoryMappedFileBase
    {
        public void Start(string pipeName)
        {
            StartInternal(pipeName, false);
        }
    }
}
