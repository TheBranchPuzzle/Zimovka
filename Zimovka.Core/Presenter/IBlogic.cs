using Zimovka.Data;

namespace Zimovka.Presenter
{
  public interface IBlogic
  {

    List<RegionOutput> CurrentSearch {get; set;}
    List<RegionOutput> Favorites {get; set;}
       
    List<RegionOutput> Search(DateTime startDate, DateTime endDate, List<string> Regions) 
    {
      return CurrentSearch;
    }

    public void AddFav(int index) {}

    public void RemoveFav(int index) {}

    public List<string> Analyse() 
    {
      return CurrentSearch.OrderBy(x => x.avgPrice).Take(2).Select(x => x.Region).ToList();
    }
    
  }
}