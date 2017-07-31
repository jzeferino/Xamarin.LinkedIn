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
            OpenCurrentProfile(activity, new DeepLinkListener(onSuccess, onError));
        }

        public void OpenOtherProfile(Activity activity, string memberId, Action onSuccess, Action<LIDeepLinkError> onError)
        {
            OpenOtherProfile(activity, memberId, new DeepLinkListener(onSuccess, onError));
        }

        private class DeepLinkListener : Java.Lang.Object, IDeepLinkListener
        {
            private readonly Action _onSuccess;
            private readonly Action<LIDeepLinkError> _onError;

            public DeepLinkListener(Action onSuccess, Action<LIDeepLinkError> onError)
            {
                _onError = onError;
                _onSuccess = onSuccess;
            }

            public void OnDeepLinkError(LIDeepLinkError error) => _onError?.Invoke(error);

            public void OnDeepLinkSuccess() => _onSuccess?.Invoke();
        }
    }
}
