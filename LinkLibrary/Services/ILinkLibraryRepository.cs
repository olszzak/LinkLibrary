using System.Collections.Generic;
using LinkLibrary.Entities;

namespace LinkLibrary.Services
{
    public interface ILinkLibraryRepository
    {
        IEnumerable<Link> GetLinks(int userId);
        Link GetLink(int userId, int linkId);
        void AddLink(int userId, Link link);
        User GetUser(int userId);
        bool Save();
        void DeleteLink(int linkId);
      //  string GetUserByName(string name);
    }
}