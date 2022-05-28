namespace WhenWaiting.ConditionedTaskExecutors
{
    public class FailedTaskEventArgs : EventArgs
    {
        public Exception Exception { get; }

        public FailedTaskEventArgs(Exception exception)
        {
            Exception = exception;
        }
    }
}
