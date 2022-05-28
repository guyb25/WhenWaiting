using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using WhenWaiting;
using WhenWaiting.ConditionedTaskExecutors;

namespace WhenTest
{
    [TestClass]
    public class FailedTaskTests
    {
        [TestMethod]
        public void ShouldntExecuteUntilCondition()
        {
            // Arrange
            bool executionCondition = false;
            bool success = false;
            string expectedExceptionMessage = "This is an exception";

            var taskExecutor = WhenWaiter
                .When(() => executionCondition)
                .WithOnFailEvent((taskExecutor, args) => success = args.Exception.Message.Equals(expectedExceptionMessage))
                .AsyncExecute(() => throw new Exception(expectedExceptionMessage));

            // Act
            executionCondition = true;
            Task.Delay(1000).Wait();

            // Assert
            taskExecutor.State.Should().Be(TaskState.Failed);
            success.Should().BeTrue();
        }
    }
}