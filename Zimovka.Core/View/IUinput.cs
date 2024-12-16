using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using View;

namespace Zimovka.View
{
    public interface IUinput
    {
        string Input {get; set;}

        int GetUserInput(List<IValidator<IUinput>> rules)
        {
          Input = Console.ReadLine();
          var isValid = false;

          if (Input == "") return -1;

          do {
            var fails = rules.Select(rule => rule.Validate(this))
                        .Where(fail => fail != null);
            
          isValid = !fails.Any();
          if (!isValid)
            Console.WriteLine(fails.First().Description);
          } while (!isValid);

          return int.Parse(Input);

        }
    }

  public class MInput : IUinput
  {
    public string Input { get; set; }

    public int? GetUserInput(List<IValidator<IUinput>> rules)
      {
        var isValid = false;

        do {
          Input = Console.ReadLine();
          if (Input == "") return null;
          var fails = rules.Select(rule => rule.Validate(this))
                      .Where(fail => fail != null);
            
        isValid = !fails.Any();
        if (!isValid)
          Console.WriteLine(fails.First().Description);
        } while (!isValid);

        return int.Parse(Input);

      }
  }

  public class DateInput : IUinput
  {
    public string Input { get; set; }

    public DateTime? GetUserInput(List<IValidator<IUinput>> rules)
      {
        var isValid = false;

        do {
          Input = Console.ReadLine();
          if (Input == "") return null;
          var fails = rules.Select(rule => rule.Validate(this))
                      .Where(fail => fail != null);
            
        isValid = !fails.Any();
        if (!isValid)
          Console.WriteLine(fails.First().Description);
        } while (!isValid);

        return DateTime.Parse(Input);

      }
  }

  public class StringInput : IUinput
  {
    public string Input { get; set; }

    public List<string>? GetUserInput(List<IValidator<IUinput>> rules)
      {
        var isValid = false;

        do {
          Input = Console.ReadLine();
          if (Input == "") return null;
          var fails = rules.Select(rule => rule.Validate(this))
                      .Where(fail => fail != null);
            
        isValid = !fails.Any();
        if (!isValid)
          Console.WriteLine(fails.First().Description);
        } while (!isValid);

        return Input.Split(' ').ToList();

      }
  }

}