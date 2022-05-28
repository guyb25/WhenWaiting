namespace WhenWaiting.ConditionedTaskExecutors
{
    public enum TaskState
    {
        Initializing,
        Waiting,
        Running,
        Finished,
        Failed,
        Timeout
    }
}
