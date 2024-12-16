using Zimovka.Data;

namespace Zimovka.Model
{
  public interface IStorage
  {
    public List<RegionItem> Search(DateTime startDate, DateTime endDate, string Region) 
    {
      return new List<RegionItem>();
    }

  }
}