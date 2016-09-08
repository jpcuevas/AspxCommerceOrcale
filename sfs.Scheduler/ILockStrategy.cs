using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SageFrame.Scheduler
{

    public interface ILockStrategy : IDisposable
    {
        bool ThreadCanRead { get; }
        bool ThreadCanWrite { get; }
        bool SupportsConcurrentReads { get; }
        ILock GetReadLock();
        ILock GetReadLock(TimeSpan timeout);
        ILock GetWriteLock();
        ILock GetWriteLock(TimeSpan timeout);
    }

}