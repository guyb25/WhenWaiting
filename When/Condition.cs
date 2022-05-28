namespace WhenWaiting
{
    public class Condition
    {
        public Func<bool> ConditionFunction { get; }

        public Condition(Func<bool> condition)
        {
            ConditionFunction = condition;
        }

        public bool Verify()
        {
            return ConditionFunction();
        }
    }
}
