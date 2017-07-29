// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Xamarin.iOS.LinkedIn.Sample
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton _button { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel _textView { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (_button != null) {
                _button.Dispose ();
                _button = null;
            }

            if (_textView != null) {
                _textView.Dispose ();
                _textView = null;
            }
        }
    }
}