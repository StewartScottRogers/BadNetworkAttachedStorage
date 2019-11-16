using System.Threading.Tasks;

namespace System.IO.Library {
    internal static class RetryIOActions {
        #region Constants

        private static readonly TimeSpan[] RetryIntervals = new TimeSpan[] {
                                                                                    TimeSpan.FromMilliseconds(1 * 1000),
                                                                                    TimeSpan.FromMilliseconds(3 * 1000),
                                                                                    TimeSpan.FromMilliseconds(3 * 1000),
                                                                                    TimeSpan.FromMilliseconds(3 * 1000),
                                                                                    TimeSpan.FromMilliseconds(3 * 1000),
                                                                                    TimeSpan.FromMilliseconds(3 * 1000),
                                                                                    TimeSpan.FromMilliseconds(3 * 1000),
                                                                            };
        #endregion

        #region Public Methods
        [System.Diagnostics.DebuggerStepThrough]
        public static void Retry(Action action) {
            Retry(action, RetryIntervals);
        }

        [System.Diagnostics.DebuggerStepThrough]
        public static T Retry<T>(Func<T> argAction) {
            return Retry(argAction, RetryIntervals);
        }
        #endregion

        #region Private    
        [System.Diagnostics.DebuggerStepThrough]
        private static void Retry(Action action, params TimeSpan[] retryIntervals) {
            Retry<Object>(() => { action(); return null; }, retryIntervals);
        }

        [System.Diagnostics.DebuggerStepThrough]
        private static T Retry<T>(Func<T> argAction, params TimeSpan[] argRetryIntervals) {
            var exceptionResult = new Exception();

            try {
                return argAction();
            } catch (Exception exception) {
                exceptionResult = exception;
            }

            foreach (var argRetryInterval in argRetryIntervals)
                try {
                    Task.Delay(argRetryInterval).Wait();
                    return argAction();
                } catch (Exception exception) {
                    exceptionResult = exception;
                }

            throw exceptionResult;
        }
        #endregion
    }
}

