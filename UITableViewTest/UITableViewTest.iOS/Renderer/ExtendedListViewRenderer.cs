using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using UIKit;
using UITableViewTest.CustomControls;
using UITableViewTest.iOS.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedListView), typeof(ExtendedListViewRenderer))]
namespace UITableViewTest.iOS.Renderer
{
    public class ExtendedListViewRenderer : ListViewRenderer
    {

        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        private readonly int OFFSET = 10;
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {

                var lv = e.NewElement as ExtendedListView;
                lv.AddAction = new Action(Add);
                lv.SubtractAction = new Action(Sub);
                lv.ScrollAction = new Action(Scroll);
                lv.HasUnevenRows = true;
            }
            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
            }

            if (e.OldElement != null)
            {
                UnregisterForKeyboardNotifications();
            }
        }

        void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs args)
        {
            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;
            var inset = Control.ContentInset;
            inset.Bottom = 0;
            Control.ContentInset = inset;
            Control.ScrollIndicatorInsets = Control.ContentInset;
            var offset = Control.ContentOffset;
            offset.Y += keyboardSize.Height;
            Control.SetContentOffset(offset, true);
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
            //if (Element != null)
            //{
            //    Element.Margin = new Thickness(0); //set the margins to zero when keyboard is dismissed
            //}

        }


        void UnregisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver != null)
            {
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }
        void Scroll()
        {
            var contentHeight = Control.ContentSize.Height - Control.Frame.Height;
            //var inset = Control.ContentInset;
            //inset.Bottom = (nfloat)contentHeight;
            //Control.ContentInset = inset;
            //Control.ScrollIndicatorInsets = Control.ContentInset;
            var offset = Control.ContentOffset;
            offset.Y = contentHeight;
            Control.SetContentOffset(offset, false);

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