﻿using District_3_App.ExtraInfo;
using District_3_App.HighlightsFE;
using District_3_App.LogIn;
using District_3_App.ProfileSocialNetworkInfoStuff.entities;
using District_3_App.ProfileSocialNetworkInfoStuff.profileNetworkInfo_Repository;
using District_3_App.Service;
using Log_In;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace District_3_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ProfileInfoSettings ProfileInfoSettings = new ProfileInfoSettings(new Guid());
        private CasualProfileService casualProfileService = new CasualProfileService(null, ProfileInfoSettings);
        private UsersRepository userRepository;
        public MainWindow()
        {
            InitializeComponent();
            generateFrame();
            userRepository = new UsersRepository("Users.xml"); 
            LoadUserProfile();
        }
        private void LoadUserProfile()
        {

            string username = "test_0";

            // Get the user from the repository
            User user = userRepository.GetUserByName(username);
            if (user != null)
            {
                // Display the username
                UsernameTextBlock.Text = $"{user.username}";

                // Display the followers count
                FollowerCountTextBlock.Text = $"{userRepository.getFollowersCount(username)}";

                // Display the following count
                FollowingTextBlock.Text = $"{userRepository.getFollowingCount(username)}";
            }
            else
            {
                // User not found
                MessageBox.Show("User not found.");
            }
        }
        private void Button_Click_Home(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl6();
        }
        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl5();
        }
        private void Button_Click_Explore(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl4();
        }
        private void Button_Click_Reels(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl3();
        }
        private void Button_Click_Messages(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl2();
        }
        private void Button_Click_Notifications(object sender, RoutedEventArgs e)
        {
            CC.Content = new UserControl1();
        }
        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            CC.Content = null;
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            Popup.IsOpen = true;
        }
        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            Popup2.IsOpen = true;
        }
        private void ExtraInfo_Click(object sender, RoutedEventArgs e)
        {
            var newContent = new ExtraInfo.ExtraInfo();
            Grid.SetColumn(newContent, 2);
            Grid.SetRow(newContent, 0);   
            Grid.SetRowSpan(newContent, 6);
            mainGrid.Children.Add(newContent);
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            var newContent = new SignUp();
            Grid.SetColumn(newContent, 2);
            Grid.SetRow(newContent, 0);
            Grid.SetRowSpan(newContent, 6);
            mainGrid.Children.Add(newContent);
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Popup2.IsOpen = false;
        }
        private void MoreAbout_Click(object sender, RoutedEventArgs e)
        {
            MoreAboutPopup.IsOpen = true;
        }

        private void onAddHighlight_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            var newContent = new SelectPostsPage();
            Grid.SetColumn(newContent, 2); // Place in the third column
            Grid.SetRow(newContent, 0);    // Place in the first row
            Grid.SetRowSpan(newContent, 4); // Span multiple rows if necessary
            mainGrid.Children.Add(newContent);
        }
        private void PostsButton_Click(object sender, RoutedEventArgs e)
        {
            // Show PostsPanel and hide ReelsPanel and TagsPanel
            PostsPanel.Visibility = Visibility.Visible;
            ReelsPanel.Visibility = Visibility.Collapsed;
            TagsPanel.Visibility = Visibility.Collapsed;
        }

        private void ReelsButton_Click(object sender, RoutedEventArgs e)
        {
            // Show ReelsPanel and hide PostsPanel and TagsPanel
            PostsPanel.Visibility = Visibility.Collapsed;
            ReelsPanel.Visibility = Visibility.Visible;
            TagsPanel.Visibility = Visibility.Collapsed;
        }

        private void TagsButton_Click(object sender, RoutedEventArgs e)
        {
            // Show TagsPanel and hide PostsPanel and ReelsPanel
            PostsPanel.Visibility = Visibility.Collapsed;
            ReelsPanel.Visibility = Visibility.Collapsed;
            TagsPanel.Visibility = Visibility.Visible;
        }
        private bool isDescriptionVisible = false;
        private void MoreDescriptionButton_Click(object sender, RoutedEventArgs e)
        {
            if (isDescriptionVisible)
            {
                // Hide the additional description
                AdditionalDescription.Visibility = Visibility.Collapsed;
                isDescriptionVisible = false;
            }
            else
            {
                // Show the additional description
                AdditionalDescription.Visibility = Visibility.Visible;
                isDescriptionVisible = true;
            }

            var links = ProfileInfoSettings.GetLinks();
            if (links != null && links.Count >= 1)
            {
                link1Text.Text = links[0];
            }
            else
            {
                link1Text.Text = string.Empty; 
            }

            if (links != null && links.Count >= 2)
            {
                link2Text.Text = links[1];
            }
            else
            {
                link2Text.Text = string.Empty; 
            }

            var dailyMotto = ProfileInfoSettings.GetDailyMotto();
            mottoText.Text = dailyMotto != null ? dailyMotto : string.Empty; 

            var hashtag = ProfileInfoSettings.GetHashtag();
            hashtagText.Text = hashtag != null ? hashtag : string.Empty; 


        }
        private void generateFrame()
        {
            int frameNumber=casualProfileService.getProfileInfoSettings().GetFrameNumber();
            if(frameNumber==1)
            {
                firstFrame.Visibility = Visibility.Visible;
                secondFrame.Visibility = Visibility.Collapsed;
                thirdFrame.Visibility = Visibility.Collapsed;
                fourthFrame.Visibility = Visibility.Collapsed;
                fifthFrame.Visibility = Visibility.Collapsed;
                romanianFrame.Visibility = Visibility.Collapsed;
            }else if (frameNumber == 2)
            {
                firstFrame.Visibility = Visibility.Collapsed;
                secondFrame.Visibility = Visibility.Visible;
                thirdFrame.Visibility = Visibility.Collapsed;
                fourthFrame.Visibility = Visibility.Collapsed;
                fifthFrame.Visibility = Visibility.Collapsed;
                romanianFrame.Visibility = Visibility.Collapsed;
            }else if(frameNumber == 3)
            {
                firstFrame.Visibility = Visibility.Collapsed;
                secondFrame.Visibility = Visibility.Collapsed;
                thirdFrame.Visibility = Visibility.Visible;
                fourthFrame.Visibility = Visibility.Collapsed;
                fifthFrame.Visibility = Visibility.Collapsed;
                romanianFrame.Visibility = Visibility.Collapsed;
            }else if(frameNumber == 4)
            {
                firstFrame.Visibility = Visibility.Collapsed;
                secondFrame.Visibility = Visibility.Collapsed;
                thirdFrame.Visibility = Visibility.Collapsed;
                fourthFrame.Visibility = Visibility.Visible;
                fifthFrame.Visibility = Visibility.Collapsed;
                romanianFrame.Visibility = Visibility.Collapsed;
            }
            else if (frameNumber == 5)
            {
                firstFrame.Visibility = Visibility.Collapsed;
                secondFrame.Visibility = Visibility.Collapsed;
                thirdFrame.Visibility = Visibility.Collapsed;
                fourthFrame.Visibility = Visibility.Collapsed;
                fifthFrame.Visibility = Visibility.Visible;
                romanianFrame.Visibility = Visibility.Collapsed;
            }else if(frameNumber == 6)
            {
                firstFrame.Visibility = Visibility.Collapsed;
                secondFrame.Visibility = Visibility.Collapsed;
                thirdFrame.Visibility = Visibility.Collapsed;
                fourthFrame.Visibility = Visibility.Collapsed;
                fifthFrame.Visibility = Visibility.Collapsed;
                romanianFrame.Visibility = Visibility.Visible;
            }
            else {
                firstFrame.Visibility = Visibility.Collapsed;
                secondFrame.Visibility = Visibility.Collapsed;
                thirdFrame.Visibility = Visibility.Collapsed;
                fourthFrame.Visibility = Visibility.Collapsed;
                fifthFrame.Visibility = Visibility.Collapsed;
                romanianFrame.Visibility = Visibility.Collapsed;
            }

        }
       
        private void MoreImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Perform your action here
            MessageBox.Show("More2 image clicked!");
        }
    }
}