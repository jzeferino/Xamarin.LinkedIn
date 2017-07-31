using System;
using Android.App;

using LinkedIn.Platform.Errors;
using LinkedIn.Platform.Listeners;
using LinkedIn.Platform.Utils;

namespace LinkedIn.Platform
{
    partial class DeepLinkHelper
    {
        public void OpenCurrentProfile(Activity activity, Action onSuccess, Action<LIDeepLinkError> onError)
        {
            var listener = new DeepLinkListener(onSuccess, onError);
            OpenCurrentProfile(activity, listener);
        }

        public void OpenOtherProfile(Activity activity, string memberId, Action onSuccess, Action<LIDeepLinkError> onError)
        {
            var listener = new DeepLinkListener(onSuccess, onError);
            OpenOtherProfile(activity, memberId, listener);
        }

        private class DeepLinkListener : Java.Lang.Object, IDeepLinkListener
        {
            readonly Action onSuccess;
            readonly Action<LIDeepLinkError> onError;

            public DeepLinkListener(Action onSuccess, Action<LIDeepLinkError> onError)
            {
                this.onError = onError;
                this.onSuccess = onSuccess;
            }

            public void OnDeepLinkError(LIDeepLinkError error)
            {
                onError?.Invoke(error);
            }

            public void OnDeepLinkSuccess()
            {
                onSuccess?.Invoke();
            }
        }
    }
}
