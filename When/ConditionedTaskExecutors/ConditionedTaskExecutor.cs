using WhenWaiting.Constants.ConditionedTaskExecutor;

namespace WhenWaiting.ConditionedTaskExecutors
{
    public class ConditionedTaskExecutor
    {
        private readonly Condition _condition;

        public TaskState State { get; private set; }

        public int PollingIntervalMs { get; internal set; }
        public int TimeoutMs { get; internal set; }
        public bool ApplyTimeout { get; internal set; }

        public event EventHandler<FailedTaskEventArgs>? OnFail;

        public ConditionedTaskExecutor(Condition condition)
        {
            _condition = condition;

            State = TaskState.Initializing;
            TimeoutMs = ConditionedTaskExecutorConstants.DefaultTimeoutMs;
            ApplyTimeout = ConditionedTaskExecutorConstants.DefaultApplyTimeout;
            PollingIntervalMs = ConditionedTaskExecutorConstants.DefaultPollingIntervalMs;
        }

        public ConditionedTaskExecutor AsyncExecute(Action action)
        {
            var execution = Task.Run(() =>
            {
                Execute(action);
            });

            return this;
        }

        public ConditionedTaskExecutor Execute(Action action)
        {
            State = TaskState.Waiting;
            WaitForCondition();

            if (State == TaskState.Timeout)
            {
                return this;
            }

            try
            {
                State = TaskState.Running;
                action();
                State = TaskState.Finished;
            }

            catch (Exception ex)
            {
                State = TaskState.Failed;
                OnFail?.Invoke(this, new FailedTaskEventArgs(ex));
            }

            return this;
        }

        private void WaitForCondition()
        {
            int waitedTime = 0;

            while (!_condition.Verify())
            {
                Task.Delay(PollingIntervalMs).Wait();
                waitedTime += PollingIntervalMs;

                if (ApplyTimeout && waitedTime > TimeoutMs)
                {
                    State = TaskState.Timeout;
                }
            }
        }
    }
}
