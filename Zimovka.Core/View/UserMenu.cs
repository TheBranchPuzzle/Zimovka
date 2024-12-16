using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Zimovka.View
{
    public class UserMenu
    {
      public static MenuEnum MenuInput()
      {
        var input = Console.ReadLine();
          return Enum.TryParse(input, out MenuEnum choice)
            ? choice
            : MenuEnum.Unknown; 
      }
      
      public static string GetEnumDescription(Enum value)
      {
        var field = value.GetType().GetField(value.ToString());
        
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(
            field, typeof(DescriptionAttribute)
            );
        return attribute == null ? value.ToString() : attribute.Description;
      }

      public static void DisplayMenu()
      {
        Console.WriteLine("Выбирите опцию меню:\n");
        var menuItemNumber = 1;
        foreach (MenuEnum choice in Enum.GetValues(typeof(MenuEnum)))
            if (choice != MenuEnum.Unknown)
            {
                var description = GetEnumDescription(choice);
                Console.WriteLine($"[{menuItemNumber}]:    {description}");
                menuItemNumber++;
            }
    
        Console.Write("\nВведите опцию меню: ");
      }
      
      public static bool MenuExit() 
      {
        Console.Write(
        "Вы точно хотите закрыть приложение? (Y/N): "
        );
        var confirmation = Console.ReadLine().ToUpper()[0];
        Console.WriteLine();
        if (confirmation == 'Y')
        {
          Console.WriteLine("Выход из приложения...");
                
          return true; 
        }
        return false;
      }

    }
  }