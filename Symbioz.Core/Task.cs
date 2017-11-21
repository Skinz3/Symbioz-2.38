using System;
using System.Threading.Tasks;

namespace Stump.Core.Threading
{
    public static class TaskCompletionSourceExtensions
    {
        public static void SetFromTask<TResult>(this TaskCompletionSource<TResult> resultSetter, Task task)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    resultSetter.SetResult((task is Task<TResult>) ? ((Task<TResult>)task).Result : default(TResult));
                    break;

                case TaskStatus.Canceled:
                    resultSetter.SetCanceled();
                    break;

                case TaskStatus.Faulted:
                    resultSetter.SetException(task.Exception.InnerExceptions);
                    break;

                default:
                    throw new InvalidOperationException("The task was not completed.");
            }
        }

      

        public static bool TrySetFromTask<TResult>(this TaskCompletionSource<TResult> resultSetter, Task task)
        {
            bool result;
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    result = resultSetter.TrySetResult((task is Task<TResult>) ? ((Task<TResult>)task).Result : default(TResult));
                    break;

                case TaskStatus.Canceled:
                    result = resultSetter.TrySetCanceled();
                    break;

                case TaskStatus.Faulted:
                    result = resultSetter.TrySetException(task.Exception.InnerExceptions);
                    break;

                default:
                    throw new InvalidOperationException("The task was not completed.");
            }
            return result;
        }

        public static bool TrySetFromTask<TResult>(this TaskCompletionSource<TResult> resultSetter, Task<TResult> task)
        {
            return resultSetter.TrySetFromTask(task);
        }
    }
}