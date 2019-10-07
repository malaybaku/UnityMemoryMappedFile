namespace UnityMemoryMappedFile
{
    public class MemoryMappedFileServer : MemoryMappedFileBase
    {
        public void Start(string pipeName)
        {
            StartInternal(pipeName, true);
        }
    }
}
