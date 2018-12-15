using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;




namespace UITableViewTest
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = new MainPageViewModel();

            var list = ExtendedListView.ItemsSource as ObservableCollection<int>;
            ExtendedListView.ScrollTo(list.Last(), ScrollToPosition.MakeVisible, false);

        }

        private void AddBtn_Clicked(object sender, EventArgs e)
        {
            ExtendedListView.Add();
        }

        private void SubBtn_Clicked(object sender, EventArgs e)
        {
            ExtendedListView.Subtract();
        }
        bool moved = false;
        private void MoveBtn_Clicked(object sender, EventArgs e)
        {
            var y = -250;
            if (moved)
            {
                y *= 0;
            }
            ButtonGrid.TranslateTo(0, y, length: 250, easing: Easing.CubicInOut);
            moved = !moved;

        }

        private void ScrollToBottom_Clicked(object sender, EventArgs e)
        {
            ExtendedListView.Scroll();
        }
    }

    public class MainPageViewModel
    {
        public ObservableCollection<int> ListViewSource { get; set; }
        public string HTMLString { get; set; }


        public MainPageViewModel()
        {
            var list = new ObservableCollection<int>();
            for (int i = 0; i < 100; i++)
            {
                list.Add(i);
            }
            ListViewSource = list;
            HTMLString = @"Test http://hkgolden.com
hkgolden.com abcde fdfsdf
dsad www.facebook.com";
            //HTMLString = @"http://hkgolden.com";
        }
    }
}
