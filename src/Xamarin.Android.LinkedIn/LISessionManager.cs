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
            Init(activity, scope, new AuthListener(onSuccess, onError), showGoToAppStoreDialog);
        }

        private class AuthListener : Java.Lang.Object, IAuthListener
        {
            private readonly Action _onSuccess;
            private readonly Action<LIAuthError> _onError;

            public AuthListener(Action onSuccess, Action<LIAuthError> onError)
            {
                _onError = onError;
                _onSuccess = onSuccess;
            }

            public void OnAuthError(LIAuthError error) => _onError?.Invoke(error);

            public void OnAuthSuccess() => _onSuccess?.Invoke();
        }
    }
}
