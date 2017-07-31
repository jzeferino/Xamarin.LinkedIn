using System;
using Android.Content;
using Org.Json;

using LinkedIn.Platform.Errors;
using LinkedIn.Platform.Listeners;
using LinkedIn.Platform.Utils;

namespace LinkedIn.Platform
{
    partial class APIHelper
    {
        public void GetRequest(Context context, string url, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            GetRequest(context, url, new ApiListener(onSuccess, onError));
        }

        public void DeleteRequest(Context context, string url, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            DeleteRequest(context, url, new ApiListener(onSuccess, onError));
        }

        public void PostRequest(Context context, string url, JSONObject body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            PostRequest(context, url, body, new ApiListener(onSuccess, onError));
        }

        public void PostRequest(Context context, string url, string body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            PostRequest(context, url, body, new ApiListener(onSuccess, onError));
        }

        public void PutRequest(Context context, string url, JSONObject body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            PutRequest(context, url, body, new ApiListener(onSuccess, onError));
        }

        public void PutRequest(Context context, string url, string body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            PutRequest(context, url, body, new ApiListener(onSuccess, onError));
        }

        private class ApiListener : Java.Lang.Object, IApiListener
        {
            private readonly Action<ApiResponse> _onSuccess;
            private readonly Action<LIApiError> _onError;

            public ApiListener(Action<ApiResponse> onSuccess, Action<LIApiError> onError)
            {
                _onError = onError;
                _onSuccess = onSuccess;
            }

            public void OnApiError(LIApiError error) => _onError?.Invoke(error);

            public void OnApiSuccess(ApiResponse response) => _onSuccess?.Invoke(response);
        }
    }
}
