using System.Collections.Generic;
using ABapi.Models;

namespace ABapi.Data {

    public interface IWebServiceRepository {

bool SaveChanges();

        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User usr);
        void UpdateUser( User usr);
        void DeleteUser(User usr);

    }
}