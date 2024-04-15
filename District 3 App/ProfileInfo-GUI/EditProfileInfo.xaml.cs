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
    /// Interaction logic for EditProfileInfo.xaml
    /// </summary>
    public partial class EditProfileInfo : UserControl
    {
        public EditProfileInfo()
        {
            InitializeComponent();
        }

        private void DateOfBirth(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = sender as DatePicker;
            DateTime? selectedDate = datePicker.SelectedDate;
        }

        private void SaveChanges_Button(object sender, RoutedEventArgs e)
        {
            /// save data to Profile info page
        }

        private void Cancel_Button(object sender, RoutedEventArgs e)
        {
            var newContent = new ProfileInfoDisplay();

            EditProfileGrid.Children.Clear();
            EditProfileGrid.Children.Add(newContent);
        }
    }
}
