using LibraryApi.Domain;
using LibraryApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Mappers
{
    public class EfBookMapper : IMapBooks
    {

        LibraryDataContext Context;

        public EfBookMapper(LibraryDataContext context)
        {
            Context = Context;
        }


        private IQueryable<Book> GetBooksInInventory()
        {
            return Context.Books.Where(b => b.InInventory);
        }

        public async Task<GetBookDetailsResponse> GetBooksById(int id)
        {
            return await GetBooksInInventory()
               .Where(b => b.Id == id)
               .Select(b => new GetBookDetailsResponse
               {
                   Id = b.Id,
                   Title = b.Title,
                   Author = b.Author,
                   Genre = b.Genre,
                   NumberOfPages = b.NumberOfPages
               }).SingleOrDefaultAsync();
        }
    }
}
