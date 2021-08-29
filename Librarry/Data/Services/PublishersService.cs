using Librarry.Data.Models;
using Librarry.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Librarry.Data.Services
{
    public class PublishersService
    {
        private readonly AppDbContext _context;

        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public List<Publisher> GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            List<Publisher> publishers = publishers = _context.Publishers.ToList();
            if (!string.IsNullOrEmpty(searchString))
            {
                publishers = publishers.Where(p => p.Name.Contains(searchString)).ToList();
            }
            //////
            if (sortBy == "as")
            {
                publishers = publishers.OrderBy(p => p.Name).ToList();
            }
            else if (sortBy == "desc")
            {
                publishers = publishers.OrderByDescending(p => p.Name).ToList();
            }
            //////
            if (pageNumber == 0)
            {
                publishers = publishers.Take(6).ToList();
            }
            else
            {
                publishers = publishers.Skip(6 * (pageNumber - 1)).Take(6).ToList();
            }
            //////
            return publishers;
        }

        /*
        public List<Publisher> GetAllPublishers()
        {
            var allPublishers = _context.Publishers.ToList();
            return allPublishers;
        }
         */


        public Publisher AddPublisher(PublisherVM publisher)
        {
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _context.Publishers.Add(_publisher);
            _context.SaveChanges();

            return _publisher;
        }

        public Publisher GetPublisherById(int id) => _context.Publishers.FirstOrDefault(n => n.Id == id);

        public PublisherWithBooksAndAuthotsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId)
                .Select(n => new PublisherWithBooksAndAuthotsVM()
                {
                    Name = n.Name,
                    BookAuthors = n.Books.Select(n => new BookAuthorVM()
                    {
                        BookName = n.Title,
                        BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();
            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if(_publisher != null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} not found");
            }
        }

    }
}
