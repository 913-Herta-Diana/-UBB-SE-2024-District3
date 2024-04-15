using District_3_App.ExtraInfo;
using District_3_App.HighlightsFE;
using District_3_App.LogIn;
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
        public MainWindow()
        {
            InitializeComponent();
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
            //Popup2.IsOpen = false;
            //ExtraInfoControl.Content = new ExtraInfo.ExtraInfo();
            //ines
            //mainGrid.Children.Clear();
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
        }
    }
}