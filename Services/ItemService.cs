namespace HelloApi.Services
{
    public class ItemService : IItemService
    {
        private List<string> Items = new List<string>
        {
            "a1",
            "a2",
            "a3",
            "a4",
            "a5",
            "a6",
            "a7"
        };

        public string GetItemByName(string name)
        {
            return Items.FirstOrDefault(x => x == name) ?? string.Empty;
        }

        public IEnumerable<string> GetItems()
        {
            return Items;
        }
    }
}
