using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Zimovka.View;
using View;

namespace Zimovka.Test.TestResults
{
    public class EmptyValidatorTest
    {
        private class MockUinput : IUinput
    {
        public string Input { get; set; }
    }

    [Theory]
    [InlineData("", "Ошибка ввода. Введите значение.")] // Empty input
    [InlineData("   ", "Ошибка ввода. Введите значение.")] // Whitespace input
    [InlineData("valid input", null)] // Valid case
    public void Validate_EmptyValidator(string input, string expectedMessage)
    {
        // Arrange
        var validator = new EmptyValidator();
        var uinput = new MockUinput { Input = input };

        // Act
        var result = validator.Validate(uinput);

        // Assert
        if (expectedMessage == null)
        {
            Assert.Null(result);
        }
        else
        {
            Assert.NotNull(result);
            Assert.Equal(expectedMessage, result.Description);
        }
    }
    }
}