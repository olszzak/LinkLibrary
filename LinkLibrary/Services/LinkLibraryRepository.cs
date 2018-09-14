using LinkLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LinkLibrary.Services
{
    public class LinkLibraryRepository : ILinkLibraryRepository
    {
        private LinkLibraryContext _linkContext;
        public LinkLibraryRepository(LinkLibraryContext linkContext)
        {
            _linkContext = linkContext;
        }

        public User GetUser(int userId)
        {
            return _linkContext.Users.Where(u => u.Id == userId).FirstOrDefault();
        }

        public IEnumerable<Link> GetLinks(int userId)
        {
            return _linkContext.Links.Where(l => l.UserId.Equals(userId)).ToList();
        }

        public void AddLink(int userId, Link link)
        {
            //var user = GetUser(userId);
            link.UserId = userId;
            _linkContext.Links.Add(link);
            
        }

        public bool Save()
        {
            return (_linkContext.SaveChanges() >= 0);
        }

        public Link GetLink(int userId, int linkId)
        {
            return _linkContext.Links.Where(l => l.UserId == userId && l.Id == linkId).FirstOrDefault();
        }

        public void DeleteLink(int linkId)
        {
            var link = _linkContext.Links.Where(l => l.Id == linkId).First();
            _linkContext.Links.Remove(link);
        }
       /* public string GetUserByName(string name)
        {
            return _linkContext.Users.Where(u => u.UserName == name).FirstOrDefault().Id;
        }*/
    }
}
