namespace HelloApi.Services
{
    public interface IItemService
    {
        IEnumerable<string> GetItems();

        string GetItemByName(string name);
    }
}
