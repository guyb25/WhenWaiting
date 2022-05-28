using WhenWaiting.ConditionedTaskExecutors;

namespace WhenWaiting
{
    public static class WhenWaiter
    {
        public static ConditionedTaskExecutorBuilder When(Func<bool> condition)
        {
            var builtCondition = new Condition(condition);
            return new ConditionedTaskExecutorBuilder(builtCondition);
        }
    }
}