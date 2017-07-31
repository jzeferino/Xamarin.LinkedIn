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
            var listener = new ApiListener(onSuccess, onError);
            GetRequest(context, url, listener);
        }

        public void DeleteRequest(Context context, string url, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            var listener = new ApiListener(onSuccess, onError);
            DeleteRequest(context, url, listener);
        }

        public void PostRequest(Context context, string url, JSONObject body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            var listener = new ApiListener(onSuccess, onError);
            PostRequest(context, url, body, listener);
        }

        public void PostRequest(Context context, string url, string body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            var listener = new ApiListener(onSuccess, onError);
            PostRequest(context, url, body, listener);
        }

        public void PutRequest(Context context, string url, JSONObject body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            var listener = new ApiListener(onSuccess, onError);
            PutRequest(context, url, body, listener);
        }

        public void PutRequest(Context context, string url, string body, Action<ApiResponse> onSuccess, Action<LIApiError> onError)
        {
            var listener = new ApiListener(onSuccess, onError);
            PutRequest(context, url, body, listener);
        }

        private class ApiListener : Java.Lang.Object, IApiListener
        {
            readonly Action<ApiResponse> onSuccess;
            readonly Action<LIApiError> onError;

            public ApiListener(Action<ApiResponse> onSuccess, Action<LIApiError> onError)
            {
                this.onError = onError;
                this.onSuccess = onSuccess;
            }

            public void OnApiError(LIApiError error)
            {
                onError?.Invoke(error);
            }

            public void OnApiSuccess(ApiResponse response)
            {
                onSuccess?.Invoke(response);
            }
        }
    }
}
