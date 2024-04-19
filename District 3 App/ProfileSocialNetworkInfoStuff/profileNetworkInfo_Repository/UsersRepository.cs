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

        public UsersRepository() {  }
        public UsersRepository(List<User> usersRepositoryList) { this.usersRepositoryList = usersRepositoryList; }
        public UsersRepository(string filePath) 
        { 
            this.filePath = filePath;
            LoadUsersFromXml();
        }

        //private void LoadUsersFromXml() 
        //{
        //    usersRepositoryList = new List<User>();
        //    XDocument xDocument = XDocument.Load(filePath);
        //    var users = xDocument.Descendants("User");
        //    foreach (var user in users)
        //    {
        //        User newUser = new User
        //        {
        //            id = (Guid)user.Attribute("id"),
        //            username = (string)user.Attribute("username"),
        //            email = (string)user.Attribute("email"),
        //            password = (string)user.Attribute("password"),
        //            confirmationPassword = (string)user.Attribute("confirmationPassword")
        //        };
        //        usersRepositoryList.Add(newUser);
        //    }


        //}
        private void LoadUsersFromXml()
        {
            //usersRepositoryList = new List<User>();
            //if (File.Exists(filePath))
            //{
            //    XDocument doc = XDocument.Load(filePath);
            //    foreach (var userElement in doc.Root.Elements("User"))
            //    {
            //        User user = new User
            //        {
            //            id = Guid.Parse(userElement.Attribute("id").Value),
            //            username = userElement.Attribute("Username").Value,
            //            email = userElement.Attribute("Email").Value,
            //            password = userElement.Attribute("Password").Value,
            //            confirmationPassword = userElement.Attribute("ConfirmationPassword").Value,
            //            followingCount = (int)userElement.Attribute("Following"),
            //            followersCount = (int)userElement.Attribute("Followers")
            //        };
            //        usersRepositoryList.Add(user);
            //    }
            //}
            try
            {
                usersRepositoryList = new List<User>();

                XDocument xDocument = XDocument.Load(filePath);
                var users = xDocument.Descendants("User");

                foreach (var user in users)
                {
                    User newUser = new User
                    {
                        id = (Guid)user.Attribute("id"),
                        username = (string)user.Attribute("Username"),
                        email = (string)user.Attribute("Email"),
                        password = (string)user.Attribute("Password"),
                        confirmationPassword = (string)user.Attribute("ConfirmationPassword"),
                         followingCount = (int)user.Element("Following"),
                        followersCount = (int)user.Element("Followers")
                    };

                    usersRepositoryList.Add(newUser);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while loading users from XML: " + ex.Message);
            }

        }


        private void saveUsersToXml()
        {
            XDocument xDocument = new XDocument(
                new XElement("UserAccounts",
                    usersRepositoryList.Select(user =>
                        new XElement("User",
                            new XAttribute("id", user.id),
                            new XAttribute("Username", user.username),
                            new XAttribute("Email", user.email),
                            new XAttribute("Password", user.password),
                            new XAttribute("ConfirmationPassword", user.confirmationPassword),
                            new XElement("Following", user.followingCount),
                            new XElement("Followers", user.followersCount)
                        )
                    )
                )
            );

            xDocument.Save(filePath);
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
        public int getFollowersCount(string username)
        {
            User user = GetUserByName(username);
            if (user != null)
            {
                return user.followersCount;
            }
            else
            {
                // Handle user not found scenario
                return -1; 
            }
        }
        public int getFollowingCount(string username)
        {
            User user = GetUserByName(username);
            if (user != null)
            {
                return user.followingCount;
            }
            else
            {
                // Handle user not found scenario
                return -1; 
            }
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

        public bool UsernameExists(string username)
        {
            return usersRepositoryList.Any(user => user.username == username);
        }

        public bool EmailExists(string email)
        {
            return usersRepositoryList.Any(user => user.email == email);
        }
    }
}
