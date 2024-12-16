using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using View;
using Zimovka.Data;
using Zimovka.Presenter;

namespace Zimovka.View
{

  
  public class UCli
  {
    private IBlogic _bLogic;
    private ISaveManager _saveManager;

    public UCli(IBlogic b, ISaveManager s)
    {
      _bLogic = b;
      _saveManager = s;
    }  

    public async Task Start()
    {
      await _saveManager.Load();
      Info();
      await Menu();
    }

    

    private async Task Menu()
    {
      
      while (true) 
      {
        UserMenu.DisplayMenu();
        var choice = UserMenu.MenuInput();

        var choiceIndex = (int)choice-1;

        if (choiceIndex >= 0 && choiceIndex < Enum.GetValues(typeof(MenuEnum)).Length)
          switch (choiceIndex)
          {
            case (int)MenuEnum.userSearch:
              Console.Clear();

              Search();
              break;

            case (int)MenuEnum.addFav:
              Console.Clear();

              AddFav();
              break;

            case (int)MenuEnum.userFav:
              Console.Clear();

              OpenFav();
              break;

            case (int)MenuEnum.userExit:

              if (UserMenu.MenuExit()) {
                await _saveManager.Save();
                return;
              } 
 
              Console.Clear();
              continue;

            default:
              Console.WriteLine(
                "Неверный выбор. Повторите ввод."
              );
              break;
          }
          else
            Console.WriteLine("Неверный выбор. Повторите ввод.");
 
        Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
        //Console.ReadKey();
        Console.Clear(); 
      }

    }

    private void Info()
    {
      Console.WriteLine("Как пользоваться программой текст текст текст");
    }

    private void Search()
    {
      Console.WriteLine("Введите начальную дату [мм/дд/гггг]:");
      var input1 = new DateInput();
      var startDate = input1.GetUserInput( new List<IValidator<IUinput>> { new DateValidator() } );

      if (startDate == null) return;

      Console.WriteLine();
      Console.WriteLine("Введите конечную дату [мм/дд/гггг]:");

      var input2 = new DateInput();
      var endDate = input2.GetUserInput( new List<IValidator<IUinput>> { new DateValidator() } );

      if (endDate == null) return;
          
      Console.WriteLine();    
      Console.WriteLine("Поиск c " + startDate + " по " +  endDate);
      Console.WriteLine();

      Console.WriteLine("Введите название Регионов, через пробел:");

      var input3 = new StringInput();
      var regions = input3.GetUserInput( new List<IValidator<IUinput>> { } );

      if (regions == null) return;

      Console.Clear();
                    
      var output = _bLogic.Search(startDate.Value, endDate.Value, regions);

      if (output.Any()) 
      {
        Console.WriteLine("Регионы по запросу:\n");
        foreach (var item in output)
          Console.WriteLine(item + "\n");

        Console.WriteLine("Наиболее рекоммендованные регионы:");
        foreach (var item in _bLogic.Analyse())
          Console.WriteLine("- " + item + "\n");
      }
      else  
        Console.WriteLine("Ничего не найдено");      

    }

    private void AddFav()
    {
      while (true)
      {

        var Index = 1;
        foreach (var item in _bLogic.CurrentSearch)
        {
          Console.WriteLine($"[{Index++}] {item}\n"); 
        }

        Console.WriteLine("Введите номер позиции, чтобы добавть в избранное:");

        var input = new MInput();
        int? index = input.GetUserInput( new List<IValidator<IUinput>> { new NumberRangeValidator(Index) } );

        if (index == null) break;

        _bLogic.AddFav(index.Value);
        Console.Clear();
        Console.WriteLine("Регион успешно добавлен в избранное");

      }
    }

    private void OpenFav()
    {
      if (_bLogic.Favorites != null)
      {
        var Index = 1;
        foreach (var item in _bLogic.Favorites)
        {
          Console.WriteLine($"[{Index++}] {item}\n"); 
        }
        
        //добваить удаление из избраноонго
        while (true)
        {
          Console.WriteLine("Введите номер позиции, чтобы удалить из избранное:");
          var input = new MInput();
          int? index = input.GetUserInput( new List<IValidator<IUinput>> { new NumberRangeValidator(Index) } );

          if (index == null) break;

          _bLogic.RemoveFav(index.Value);
        }


      }
      else
        Console.WriteLine("Нет избранных рагионов");

    }

  }
}