using District_3_App.ProfileSocialNetworkInfoStuff.entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace District_3_App.ProfileSocialNetworkInfoStuff.profileNetworkInfo_Repository
{
    public class UsersRepository
    {
        public List<User> usersRepositoryList { get; set; }

        private string filePath;

        public UsersRepository() { }
        public UsersRepository(List<User> usersRepositoryList) { this.usersRepositoryList = usersRepositoryList; }
        public UsersRepository(string filePath) 
        { 
            this.filePath = filePath;
            LoadUsersFromXml();
        }

        private void LoadUsersFromXml() 
        {
            usersRepositoryList = new List<User>();
            if (File.Exists(filePath))
            {
                XDocument doc = XDocument.Load(filePath);
                foreach (var userElement in doc.Root.Elements("User"))
                {
                    User user = new User
                    {
                        id = Guid.Parse(userElement.Attribute("id").Value),
                        username = userElement.Attribute("Username").Value,
                        email = userElement.Attribute("Email").Value,
                        password = userElement.Attribute("Password").Value,
                        confirmationPassword = userElement.Attribute("ConfirmationPassword").Value
                    };
                    usersRepositoryList.Add(user);
                }
            }
        }

        private void saveUsersToXml()
        {

            XDocument doc = new XDocument();
            XElement root = new XElement("UsersAccounts");
            foreach (var user in usersRepositoryList)
            {
                XElement userElement = new XElement("User",
                    new XAttribute("id", user.id),
                    new XAttribute("Username", user.username),
                    new XAttribute("Email", user.email),
                    new XAttribute("Password", user.password),
                    new XAttribute("ConfirmationPassword", user.confirmationPassword));
                root.Add(userElement);
            }
            doc.Add(root);
            doc.Save(filePath);
        }

        public User GetUserByName(string username)
        {
            foreach (var user in usersRepositoryList)
            {
                if (user.username == username) return user;
            }

            return null;
        }

        public List<User> GetAllUsers() { return usersRepositoryList; }

        public void AddUser(User user)
        {
            usersRepositoryList.Add(user);
            saveUsersToXml();
        }

        public bool UpdatePassword(string email, string newPassword)
        {
            try
            {
                XElement root = XElement.Load(filePath);
                IEnumerable<XElement> users = from user in root.Elements("User")
                                              where (string)user.Attribute("Email") == email
                                              select user;

                if (users.Any())
                {
                    foreach (XElement user in users)
                    {
                        user.SetAttributeValue("Password", newPassword);
                    }

                    root.Save(filePath);
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating password: " + ex.Message);
                return false;
            }
        }
        public User GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            return usersRepositoryList.FirstOrDefault(user => user.username == usernameOrEmail || user.email == usernameOrEmail);
        }


    }
}
