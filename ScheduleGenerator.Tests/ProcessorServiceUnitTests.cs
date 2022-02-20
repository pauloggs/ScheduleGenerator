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
    }
}
