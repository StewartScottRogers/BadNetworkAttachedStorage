using System.Threading.Tasks;

namespace System.IO.Library {
    internal static class RetryIO {
        #region Constants

        private static readonly TimeSpan[] RetryIntervals = new TimeSpan[] {
                                                                                    TimeSpan.FromMilliseconds(1),
                                                                                    TimeSpan.FromMilliseconds(1 * 10),
                                                                                    TimeSpan.FromMilliseconds(5 * 10),
                                                                                    TimeSpan.FromMilliseconds(1 * 100),
                                                                                    TimeSpan.FromMilliseconds(3 * 100),
                                                                                    TimeSpan.FromMilliseconds(5 * 100),
                                                                                    TimeSpan.FromMilliseconds(7 * 100),
                                                                                    TimeSpan.FromMilliseconds(11 * 100),
                                                                                    TimeSpan.FromMilliseconds(13 * 100),
                                                                                    TimeSpan.FromMilliseconds(17 * 100),
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

