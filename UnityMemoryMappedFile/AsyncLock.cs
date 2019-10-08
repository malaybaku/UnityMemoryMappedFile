using System;
using System.Threading;
using System.Threading.Tasks;

namespace UnityMemoryMappedFile
{
    public sealed class AsyncLock
    {
        public AsyncLock()
        {
            m_releaser = Task.FromResult((IDisposable)new Releaser(this));
        }
        private readonly SemaphoreSlim m_semaphore = new SemaphoreSlim(1, 1);
        private readonly Task<IDisposable> m_releaser;

        public Task<IDisposable> LockAsync()
        {
            var wait = m_semaphore.WaitAsync();
            return wait.IsCompleted ?
            m_releaser :
            wait.ContinueWith(
            (_, state) => (IDisposable)state,
            m_releaser.Result,
            CancellationToken.None,
            TaskContinuationOptions.ExecuteSynchronously,
            TaskScheduler.Default
            );
        }

        private sealed class Releaser : IDisposable
        {
            internal Releaser(AsyncLock toRelease) 
            {
                m_toRelease = toRelease; 
            }

            private readonly AsyncLock m_toRelease;

            public void Dispose()
            {
                m_toRelease.m_semaphore.Release(); 
            }
        }
    }
}
