using System;
using System.Threading;

namespace Tools
{
    public class AutoLockBase : IDisposable
    {
        protected readonly ReaderWriterLockSlim readerWriterLock_;
        private bool isDisposed_;


        public AutoLockBase(ReaderWriterLockSlim readerLock) => readerWriterLock_ = readerLock;
        ~AutoLockBase() => Dispose_(false);
        

        protected virtual void Dispose_(bool isDisposing)
        {
            if (isDisposed_)
                return;

            if (isDisposing) {
                // release unmanaged resources
            }

            isDisposed_ = true;
        }
        
        
        public void Dispose()
        {
            Dispose_(true);
            GC.SuppressFinalize(this);
        }
    }


    public class AutoReadLock : AutoLockBase
    {
        public AutoReadLock(ReaderWriterLockSlim readerLock) : base(readerLock) => readerLock.EnterReadLock();


        protected override void Dispose_(bool isDisposing)
        {
            base.Dispose_(isDisposing);
            readerWriterLock_.ExitReadLock();
        }
    }
    
    
    
    public class AutoWriteLock : AutoLockBase
    {
        public AutoWriteLock(ReaderWriterLockSlim readerLock) : base(readerLock) => readerLock.EnterWriteLock();


        protected override void Dispose_(bool isDisposing)
        {
            base.Dispose_(isDisposing);
            readerWriterLock_.ExitWriteLock();
        }
    }
}