using System;
using System.Threading.Tasks;

namespace Forms
{
    public static class SafeTask
    {
        public static async Task<SafeTaskResult> RunAsync(Action action)
        {
            try
            {
                await Task.Run(() => action()).ConfigureAwait(false);
                return new SafeTaskResult();
            }
            catch (AggregateException ex)
            {
                return new SafeTaskResult(ex.InnerExceptions);
            }
            catch (Exception ex)
            {
                return new SafeTaskResult(ex);
            }

        }

        public static async Task<SafeTaskResult> RunAsync(Func<Task> action)
        {
            try
            {
                await action().ConfigureAwait(false);
                return new SafeTaskResult();
            }
            catch (AggregateException ex)
            {
                return new SafeTaskResult(ex.InnerExceptions);
            }
            catch (Exception ex)
            {
                return new SafeTaskResult(ex);
            }

        }

        public static async Task<SafeTaskResultObject<T>> RunAsync<T>(Func<Task<T>> funcion)
        {
            try
            {
                T result = await funcion().ConfigureAwait(false);
                return new SafeTaskResultObject<T>(result);
            }
            catch (AggregateException ex)
            {
                return new SafeTaskResultObject<T>(ex.InnerExceptions);
            }
            catch (Exception ex)
            {
                return new SafeTaskResultObject<T>(ex);
            }

        }

        public static async Task<SafeTaskResultObject<T>> RunAsync<T>(Func<T> function)
        {
            try
            {
                T result = await Task<T>.Run(() => { return function(); }).ConfigureAwait(false);
                return new SafeTaskResultObject<T>(result);
            }
            catch (AggregateException ex)
            {
                return new SafeTaskResultObject<T>(ex.InnerExceptions);
            }
            catch (Exception ex)
            {
                return new SafeTaskResultObject<T>(ex);
            }
        }

        public static async Task<SafeTaskResult> RunAsync(Task task)
        {
            try
            {
                if (task.Status == TaskStatus.Created)
                    task.Start();

                if (!task.IsCompleted)
                    await task.ConfigureAwait(false);

                return new SafeTaskResult();
            }
            catch (AggregateException ex)
            {
                return new SafeTaskResult(ex.InnerExceptions);
            }
            catch (Exception ex)
            {
                return new SafeTaskResult(ex);
            }
        }

        public static async Task<SafeTaskResultObject<T>> RunAsync<T>(Task<T> task)
        {
            try
            {

                if (task.Status == TaskStatus.Created)
                    task.Start();

                if (!task.IsCompleted)
                    await task.ConfigureAwait(false);

                return new SafeTaskResultObject<T>(task.Result);
            }
            catch (AggregateException ex)
            {
                return new SafeTaskResultObject<T>(ex.InnerExceptions);
            }
            catch (Exception ex)
            {
                return new SafeTaskResultObject<T>(ex);
            }
        }
    }
}
