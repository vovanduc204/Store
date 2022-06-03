using Domain.Model.Helpers.Extensions;

namespace Domain.Model
{
    public class RoleCategory
    {
        private RoleCategory()
        { }
        public RoleCategory(string name)
        {
            Name = name.TrimAndSingleSpace();
        }
        //For Fake Data
        public RoleCategory(int id, string name)
        {
            Id = id;
            Name = name.TrimAndSingleSpace();
        }
        public int Id { get; private set; }
        public string Name { get; private set; }
        public void Modify(string name)
        {
            Name = name ?? string.Empty;
        }
    }
}
