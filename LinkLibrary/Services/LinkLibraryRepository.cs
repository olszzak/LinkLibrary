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

        // TODO BP: pobieranie usera nie pasuje to repozytorium linków.
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
            // TODO BP: czemu akurat tutaj to ustawiasz a nie tam gdzie tą metodę wywołujesz?
            link.UserId = userId;
            _linkContext.Links.Add(link);

        }

        public bool Save()
        {
            return (_linkContext.SaveChanges() >= 0);
        }

        public Link GetLink(int userId, int linkId)
        {
            // TODO BP: A) linkId już jest PK, podając linkId nie potrzebujesz nic więcej aby wyciągnąć konkretnego linka
            //     B) jeśli przeprowadzasz autoryzację i okazuje się że dany Link nie należy do aktualnego Usera to powinieneś wyrzucić błąd 403 Forbidden.
            //        w tym przypadku nie jesteś w stanie tego zrobić bo nie wiesz czy link nie istnieje czy po prostu należy do innego usera.
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
