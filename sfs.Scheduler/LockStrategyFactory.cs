using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SageFrame.Scheduler
{
    internal class LockStrategyFactory
    {
        public static ILockStrategy Create(LockingType strategy)
        {
            switch (strategy)
            {
                case LockingType.ReaderWriter:

                    return new LockStrategy();
                case LockingType.Exclusive:

                    return new ForcefulLockStrategy();
                default:

                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}