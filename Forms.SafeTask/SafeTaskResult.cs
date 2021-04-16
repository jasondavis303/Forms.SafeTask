using System;
using System.Collections.Generic;

namespace Forms
{
    public class SafeTaskResult
    {
        internal SafeTaskResult()
        {
            Errors = new List<Exception>();
        }

        internal SafeTaskResult(IEnumerable<Exception> exceptions)
        {
            if (exceptions == null)
                exceptions = new List<Exception>();
            Errors = new List<Exception>(exceptions);
        }

        internal SafeTaskResult(Exception exception)
        {
            var exceptions = new List<Exception>();
            if (exception != null)
                exceptions.Add(exception);
            Errors = new List<Exception>(exceptions);
        }

        public bool Success => Errors.Count == 0;

        public IReadOnlyList<Exception> Errors { get; }
    }
}
