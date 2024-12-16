using System;
using System.ComponentModel;
using Zimovka.View;

namespace View;


public class ValidatorFail
{
    public ValidatorFail(string desc) {Description = desc;}
    public string Description { get; set;}
}

public interface IValidator<T>
{
    ValidatorFail Validate(T input);
}

public class NumberRangeValidator : IValidator<IUinput>
{
  private int _max;
  public NumberRangeValidator(int Limit)
  {
    _max = Limit;
  }
    public ValidatorFail Validate(IUinput Uinput)
    {   
        if (! int.TryParse(Uinput.Input, out int id)) 
            return new ValidatorFail("Ошибка ввода. Введите значение меню.");
        int input = int.Parse(Uinput.Input);
        if ( input < 0 ^ input > _max)
            return new ValidatorFail("Ошибка ввода. Введите допустимое значение меню.");
        return null;   
    }
}

public class EmptyValidator : IValidator<IUinput>
{
    public ValidatorFail Validate(IUinput Uinput)
    {
        if (string.IsNullOrWhiteSpace(Uinput.Input))
            return new ValidatorFail("Ошибка ввода. Введите значение.");
        return null;
    }
}

public class DateValidator : IValidator<IUinput>
{
    public ValidatorFail Validate(IUinput Uinput)
    {
        if (! DateTime.TryParse(Uinput.Input, out DateTime res))
            return new ValidatorFail("Ошибка ввода. Введите значение.");
        return null;
    }
}