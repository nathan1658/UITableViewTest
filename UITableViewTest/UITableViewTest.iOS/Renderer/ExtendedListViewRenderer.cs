using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UITableViewTest.CustomControls;
using UITableViewTest.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer (typeof(ExtendedListView),typeof(ExtendedListViewRenderer))]
namespace UITableViewTest.iOS.Renderer
{
    public class ExtendedListViewRenderer : ListViewRenderer
    {
        private readonly int OFFSET = 10;
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement!=null)
            {
                var lv = e.NewElement as ExtendedListView;
                lv.AddAction = new Action(Add);
                lv.SubtractAction = new Action(Sub);
            }
        }

        void Add()
        {
            var inset = Control.ContentInset;
            inset.Bottom += OFFSET;
            Control.ContentInset = inset;
            Control.ScrollIndicatorInsets = Control.ContentInset;
            var offset = Control.ContentOffset;
            offset.Y += (nfloat)OFFSET;
            Control.ContentOffset = offset;
        }

        void Sub()
        {
            var inset = Control.ContentInset;
            inset.Bottom -= OFFSET;
            Control.ContentInset = inset;
            Control.ScrollIndicatorInsets = Control.ContentInset;
            var offset = Control.ContentOffset;
            offset.Y -= (nfloat)OFFSET;
            Control.ContentOffset = offset;
        }


    }
}