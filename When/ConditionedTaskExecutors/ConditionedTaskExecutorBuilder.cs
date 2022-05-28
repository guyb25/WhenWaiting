namespace WhenWaiting.ConditionedTaskExecutors
{
    public class ConditionedTaskExecutorBuilder
    {
        public ConditionedTaskExecutor TaskExecutor { get; }

        public ConditionedTaskExecutorBuilder(Condition condition)
        {
            TaskExecutor = new ConditionedTaskExecutor(condition);
        }

        public ConditionedTaskExecutorBuilder WithTimeout(int timeoutMs)
        {
            TaskExecutor.ApplyTimeout = true;
            TaskExecutor.TimeoutMs = timeoutMs;
            return this;
        }

        public ConditionedTaskExecutorBuilder WithoutTimeout()
        {
            TaskExecutor.ApplyTimeout = false;
            return this;
        }

        public ConditionedTaskExecutorBuilder WithPollingInterval(int pollingIntervalMs)
        {
            TaskExecutor.PollingIntervalMs = pollingIntervalMs;
            return this;
        }

        public ConditionedTaskExecutorBuilder WithOnFailEvent(EventHandler<FailedTaskEventArgs> eventHandler)
        {
            TaskExecutor.OnFail += eventHandler;
            return this;
        }

        public ConditionedTaskExecutor AsyncExecute(Action action)
        {
            return TaskExecutor.AsyncExecute(action);
        }

        public ConditionedTaskExecutor Execute(Action action)
        {
            return TaskExecutor.Execute(action);
        }
    }
}
