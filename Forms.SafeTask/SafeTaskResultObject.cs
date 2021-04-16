using System;
using System.Collections.Generic;

namespace Forms
{
    public class SafeTaskResultObject<T> : SafeTaskResult
    {
        internal SafeTaskResultObject(T result) : base()
        {
            Result = result;
        }

        internal SafeTaskResultObject(IEnumerable<Exception> exceptions) : base(exceptions) { }

        internal SafeTaskResultObject(Exception exception) : base(exception) { }

        public T Result { get; }
    }
}
