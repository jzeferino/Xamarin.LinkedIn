using System;
using System.Threading.Tasks;
using Android.App;

using LinkedIn.Platform.Errors;
using LinkedIn.Platform.Listeners;
using LinkedIn.Platform.Utils;

namespace LinkedIn.Platform
{
    partial class LISessionManager
    {
        public void Init(Activity activity, Scope scope, bool showGoToAppStoreDialog, Action onSuccess, Action<LIAuthError> onError)
        {
            var listener = new AuthListener(onSuccess, onError);
            Init(activity, scope, listener, showGoToAppStoreDialog);
        }

        private class AuthListener : Java.Lang.Object, IAuthListener
        {
            readonly Action onSuccess;
            readonly Action<LIAuthError> onError;

            public AuthListener(Action onSuccess, Action<LIAuthError> onError)
            {
                this.onError = onError;
                this.onSuccess = onSuccess;
            }

            public void OnAuthError(LIAuthError error)
            {
                onError?.Invoke(error);
            }

            public void OnAuthSuccess()
            {
                onSuccess?.Invoke();
            }
        }
    }
}
