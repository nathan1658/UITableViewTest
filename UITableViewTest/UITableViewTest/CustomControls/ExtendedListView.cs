using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace UITableViewTest.CustomControls
{
    public class ExtendedListView:ListView
    {
        public Action ScrollAction;
        public Action AddAction;
        public Action SubtractAction;
        public void Scroll()
        {
            ScrollAction();
        }
        public void Add()
        {
            AddAction();
        }
        public void Subtract()
        {
            SubtractAction();
        }
    }
}
