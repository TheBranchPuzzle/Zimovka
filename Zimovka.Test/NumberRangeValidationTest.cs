using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using View;
using Xunit;
using Zimovka.View;

namespace Zimovka.Test
{
    public class NumberRangeValidationTest
    {
        private class MockUinput : IUinput
    {
        public string Input { get; set; }
    }

    [Theory]
    [InlineData("10", null)] // Valid case
    [InlineData("20", null)] // Valid case
    [InlineData("-1", "Ошибка ввода. Введите допустимое значение меню.")] // Below range
    [InlineData("21", "Ошибка ввода. Введите допустимое значение меню.")] // Above range
    [InlineData("abc", "Ошибка ввода. Введите значение меню.")] // Invalid input
    public void Validate_NumberRangeValidator(string input, string expectedMessage)
    {
        // Arrange
        var validator = new NumberRangeValidator(20);
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