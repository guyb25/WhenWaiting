using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WhenWaiting;
using WhenWaiting.ConditionedTaskExecutors;

namespace WhenTest
{
    [TestClass]
    public class PollingIntervalTests
    {
        [TestMethod]
        public void ShouldntExecuteUntilPolled()
        {
            // Arrange
            bool executionCondition = false;
            bool success = false;

            var taskExecutor = WhenWaiter
                .When(() => executionCondition)
                .WithPollingInterval(5000)
                .AsyncExecute(() => success = true);

            // Act
            Task.Delay(500).Wait();
            executionCondition = true;
            Task.Delay(500).Wait();

            // Assert
            success.Should().BeFalse();
            taskExecutor.State.Should().Be(TaskState.Waiting);
        }

        [TestMethod]
        public void ShouldExecuteAfterPolled()
        {
            // Arrange
            bool executionCondition = false;
            bool success = false;

            var taskExecutor = WhenWaiter
                .When(() => executionCondition)
                .WithPollingInterval(1000)
                .AsyncExecute(() => success = true);

            // Act
            executionCondition = true;
            Task.Delay(1500).Wait();

            // Assert
            success.Should().BeTrue();
            taskExecutor.State.Should().Be(TaskState.Finished);
        }
    }
}
