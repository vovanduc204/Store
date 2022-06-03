using DataAccessLayer.DbContext;
using Domain.Models.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class AuthorRepository : GenericDataRepository<Author>, IAuthorSpecificRepository
    {
        public AuthorRepository(ApplicationDbContext context) : base(context) { }
    }

    public class BookRepository : GenericDataRepository<Book>, IBookSpecificRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }
    }

    public class PublisherRepository : GenericDataRepository<Publisher>, IPublisherSpecificRepository
    {
        public PublisherRepository(ApplicationDbContext context) : base(context) { }
    }
}
