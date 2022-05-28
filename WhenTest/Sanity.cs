using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using WhenWaiting;
using WhenWaiting.ConditionedTaskExecutors;

namespace WhenTest
{
    [TestClass]
    public class Sanity
    {
        [TestMethod]
        public void ShouldntExecuteUntilCondition()
        {
            // Arrange
            bool executionCondition = false;
            bool success = false;

            var taskExecutor = WhenWaiter
                .When(() => executionCondition)
                .AsyncExecute(() => success = true);

            // Act
            Task.Delay(500).Wait();

            // Assert
            success.Should().BeFalse();
            taskExecutor.State.Should().Be(TaskState.Waiting);
        }

        [TestMethod]
        public void ShouldExecuteAfterConditionIsMet()
        {
            // Arrange
            bool executionCondition = false;
            bool success = false;

            var taskExecutor = WhenWaiter
                .When(() => executionCondition)
                .AsyncExecute(() => success = true);

            // Act
            executionCondition = true;
            Task.Delay(1000).Wait();

            // Assert
            success.Should().BeTrue();
            taskExecutor.State.Should().Be(TaskState.Finished);
        }
    }
}