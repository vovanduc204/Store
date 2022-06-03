using Library.Core.Entity;
using System.Collections.Generic;


namespace Domain.Models.Entities
{
    public class Publisher : Entity
    {
        public Publisher(string name)
        {
            Name = name;
            Books = new List<Book>();
        }

        public string Name { get; private set; }
        public virtual IEnumerable<Book> Books { get; private set; }
    }
}