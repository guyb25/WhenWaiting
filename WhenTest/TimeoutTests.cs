using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WhenWaiting;
using WhenWaiting.ConditionedTaskExecutors;

namespace WhenTest
{
    [TestClass]
    public class TimeoutTests
    {
        [TestMethod]
        public void ShouldntExecuteIfTimeoutReached()
        {
            // Arrange
            bool executionCondition = false;
            bool success = false;

            var taskExecutor = WhenWaiter
                .When(() => executionCondition)
                .WithTimeout(500)
                .AsyncExecute(() => success = true);

            // Act
            Task.Delay(1000).Wait();
            executionCondition = true;
            Task.Delay(1000).Wait();

            // Assert
            success.Should().BeFalse();
            taskExecutor.State.Should().Be(TaskState.Timeout);
        }

        [TestMethod]
        public void ShouldExecuteIfTimeoutNotReached()
        {
            // Arrange
            bool executionCondition = false;
            bool success = false;

            var taskExecutor = WhenWaiter
                .When(() => executionCondition)
                .WithTimeout(5000)
                .AsyncExecute(() => success = true);

            // Act
            Task.Delay(500).Wait();
            executionCondition = true;
            Task.Delay(1000).Wait();

            // Assert
            success.Should().BeTrue();
            taskExecutor.State.Should().Be(TaskState.Finished);
        }
    }
}
