namespace ScheduleGenerator.Tests
{
    using ScheduleGenerator.Model.Input;
    using ScheduleGenerator.Services;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class ProcessorServiceUnitTests
    {
        [Fact]
        public void GetRecipe_ReturnsRecipeIfPresentInList()
        {
            // Arrange
            var recipeList = TestHelper.TestRecipeList();
            var sut = new ProcessorService();

            // Act
            var result = sut.GetRecipe("Basil", recipeList);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetRecipe_ThrowsExceptionIfRecipeNameNotFound()
        {
            // Arrange
            var recipeList = TestHelper.TestRecipeList();
            var sut = new ProcessorService();

            // Assert
            Assert.Throws<Exception>(() => sut.GetRecipe("Cybil", recipeList));
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 0, 0)]
        [InlineData(1, 1, 1)]
        [InlineData(1, 3, 3)]
        [InlineData(2, 4, 8)]
        [InlineData(4, 10, 40)]
        public void ProcessWateringPhases_ShouldReturnTheRightNumberOfCommands(
            int numberOfPhases,
            short numberOfRepetitions,
            int expectedNumberOfCommands)
        {
            // Arrange
            var sut = new ProcessorService();
            var wateringPhases = TestHelper.TestWateringPhases(numberOfPhases, numberOfRepetitions);

            // Act
            var result = sut.ProcessWateringPhases("SomeName", 1, DateTime.Now, wateringPhases);

            // Assert
            Assert.Equal(expectedNumberOfCommands, result.Count);
        }

        [Theory]
        [InlineData(1, 1, 0, 0)]
        [InlineData(1, 1, 1, 1)]
        [InlineData(1, 1, 2, 2)]
        [InlineData(1, 2, 2, 4)]
        [InlineData(2, 3, 3, 18)]
        public void ProcessLightingPhases_ShouldReturnTheRightNumberOfCommands(
            int numberOfPhases,
            short numberOfRepetitions,
            short numberOfOperations,
            int expectedNumberOfCommands
            )
        {
            // Arrange
            var sut = new ProcessorService();
            var lightingPhases = TestHelper.TestLightingPhases(numberOfPhases, numberOfRepetitions, numberOfOperations);

            // Act
            var result = sut.ProcessLightingPhases("SomeName", 1, DateTime.Now, lightingPhases);

            // Assert
            Assert.Equal(expectedNumberOfCommands, result.Count);
        }
    }
}
