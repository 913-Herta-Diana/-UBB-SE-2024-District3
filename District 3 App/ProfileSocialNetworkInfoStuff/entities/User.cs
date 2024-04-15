﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace District_3_App.ProfileSocialNetworkInfoStuff.entities
{
    public class User : IComparable<User> //MODIFY UML DIAGRAM USER CLASS
    {
        public Guid id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public DateTime registrationDate { get; set; }

        public User() { }
        public User(Guid id, string username, string password, string email, DateTime registrationDate)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.email = email;
            this.registrationDate = registrationDate;
        }

        public string registrationDateToString()
        {
            return this.registrationDate.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public int CompareTo(User other)
        {
            return this.username.CompareTo(other.username);
        }
    }
}