using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace District_3_App.ProfileInfo_GUI
{
    /// <summary>
    /// Interaction logic for ProfileInfoDisplay.xaml
    /// </summary>
    public partial class ProfileInfoDisplay : UserControl
    {
        public ProfileInfoDisplay()
        {
            InitializeComponent();
        }

        private void EditInfoButton_Click(object sender, RoutedEventArgs e)
        {
            var newContent = new EditProfileInfo();

            ProfileInfoGrid.Children.Clear();
            ProfileInfoGrid.Children.Add(newContent);
        }

        private void TextBox_LostFocus_ChangeDescription(object sender, RoutedEventArgs e)
        {
            var editedDescription = ((TextBox)sender).Text;
        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            // do something
        }

    }
}
