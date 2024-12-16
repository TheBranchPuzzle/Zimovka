using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Zimovka.View;
using View;

namespace Zimovka.Core.Data
{
    public class DateVaidatorTest
    {
        private class MockUinput : IUinput
    {
        public string Input { get; set; }
    }

    [Theory]
    [InlineData("2023-10-01", null)] // Valid date
    [InlineData("invalid date", "Ошибка ввода. Введите значение.")] // Invalid date
    [InlineData("", "Ошибка ввода. Введите значение.")] // Empty input
    public void Validate_DateValidator(string input, string expectedMessage)
    {
        // Arrange
        var validator = new DateValidator();
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