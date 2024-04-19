using District_3_App.ProfileSocialNetworkInfoStuff.entities;
using District_3_App.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace District_3_App.Statistics
{
    class StatisticsService
    {
        private int timeSpentOnApp = 0;
        Dictionary<User, int> friends = new Dictionary<User, int>();
        private Window mainWindow;
        private string filePath;



        public StatisticsService(string filePath)
        {
            this.mainWindow = Application.Current.MainWindow;


            this.filePath = generateDefaultFilePath();
            Console.WriteLine(filePath);
            if (!File.Exists(filePath))
            {
                createXml(filePath);
            }
            Load(filePath);

            var sortedSequence = friends.OrderByDescending(pair => pair.Value);
            Dictionary<User, int> sortedDictionary = sortedSequence.ToDictionary(pair => pair.Key, pair => pair.Value);

            friends = sortedDictionary;

            SaveStreaksToXML();
            readXML();


        }


        private string generateDefaultFilePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Stats.xml");

        }

        public void createXml(string filePath)
        {
            XDocument xDocument = new XDocument(new XElement("Friends"));
            xDocument.Save(filePath);
        }
        private void Load(string filePath)
        {
            Console.WriteLine("Reading from file: " + filePath);

            XDocument xDocument = XDocument.Load(filePath);
            XElement root = xDocument.Element("Friends");
            if (!root.HasElements)
            {
                throw new Exception("No friend elements found in the XML file.");
            }

            foreach (var friendElem in root.Elements("Friend"))
            {
                string username = friendElem.Element("Username")?.Value;
                int streak = int.Parse(friendElem.Element("Streak")?.Value ?? "0");

                if (!string.IsNullOrEmpty(username))
                {
                    User user = new User(Guid.NewGuid(), username, "", "", DateTime.Now.ToString()); // Fill in appropriate values for other properties
                    friends.Add(user, streak);
                }
            }
        }

        
        public void SaveStreaksToXML()
        {
            XDocument xDocument = XDocument.Load(filePath);
            XElement root = xDocument.Element("Friends");

            root.Elements("Friend").Remove();

            foreach (var kvp in friends)
            {
                XElement friendElem = new XElement("Friend",
                    new XElement("Username", kvp.Key.username),
                    new XElement("Streak", kvp.Value));
                root.Add(friendElem);
            }

            xDocument.Save(filePath);
        }


        private void readXML()
        {
            // Read from the XML file

            try
            {
                // Load the XML document from the file
                string filePath = "TimeData.xml";
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filePath);

                // Get the root element
                XmlElement root = xmlDoc.DocumentElement;

                // Get all time elements
                XmlNodeList timeNodes = root.SelectNodes("Time");

                // Iterate over each time element and accumulate their values
                foreach (XmlNode timeNode in timeNodes)
                {
                    // Parse the time value and add it to the timeSpentOnApp variable
                    int timeValue;
                    if (int.TryParse(timeNode.InnerText, out timeValue))
                    {
                        timeSpentOnApp += timeValue;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show($"Error reading time data from XML file: {ex.Message}");
            }
        }

        public List<string> GetFriendNames()
        {
            List<string> friendNames = new List<string>();
            int contor = 5;
            foreach (User user in friends.Keys)
            {
                contor--;
                friendNames.Add(user.username);
                if (contor == 0)
                    return friendNames;
            }

            return friendNames;
        }
        public List<int> GetUserStreaks()
        {
            List<int> friendStreaks = new List<int>();
            int contor = 5;
            foreach (User user in friends.Keys)
            {
                contor--;
                friendStreaks.Add(friends[user]);
                if (contor == 0)
                    return friendStreaks;
            }

            return friendStreaks;

        }

      
        public int getAverageTimeSpent()
        {
            return timeSpentOnApp;
        }


        public string ConvertSecondsToHMS()
        {
            // Calculate hours
            int hours = timeSpentOnApp / 3600;

            // Calculate remaining seconds after removing hours
            int remainingSeconds = timeSpentOnApp % 3600;

            // Calculate minutes
            int minutes = remainingSeconds / 60;

            // Calculate remaining seconds after removing minutes
            int seconds = remainingSeconds % 60;

            // Format the result as hours:minutes:seconds
            return $"{hours:D2}h{minutes:D2}min";
        }

    }
}
