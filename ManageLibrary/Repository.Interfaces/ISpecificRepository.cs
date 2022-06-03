using Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IAuthorSpecificRepository : IGenericDataRepository<Author>
    {
    }

    public interface IBookSpecificRepository : IGenericDataRepository<Book>
    {
    }

    public interface IPublisherSpecificRepository : IGenericDataRepository<Publisher>
    {
    }
}
