namespace ScheduleGenerator.Tests
{
    using ScheduleGenerator.Services;
    using ScheduleGenerator.Model;
    using System.Collections.Generic;
    using Xunit;
    using Newtonsoft.Json;
    using System;

    public class ConverterServiceUnitTests
    {
        private readonly ConverterService sut;

        public ConverterServiceUnitTests()
        {
            sut = new ConverterService();
        }

        [Fact]
        public void GetRecipies_ReturnsListOfRecipesIfRawDataCorrectlyFormatted()
        {
            // Arrange
            var validRawData = JsonConvert.SerializeObject(
                new
                {
                    recipes = new List<Recipe>()
                    {
                        new Recipe("SomeRecipeName")
                    }
                }
            );

            // Act
            var result = sut.GetRecipies(validRawData);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public void GetRecipies_ThrowsExceptionIfRawDataIncorrectlyFormatted()
        {
            // Arrange
            var invalidRawData = "SomeInvalidJson";

            // Assert
            Assert.Throws<Exception>(() => sut.GetRecipies(invalidRawData));
        }
    }
}
