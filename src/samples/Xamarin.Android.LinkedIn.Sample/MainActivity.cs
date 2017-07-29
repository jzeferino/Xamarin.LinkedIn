using Android.App;
using Android.Widget;
using Android.OS;
using Com.Linkedin.Platform;
using Com.Linkedin.Platform.Errors;
using Com.Linkedin.Platform.Listeners;
using Android.Content;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Com.Linkedin.Platform.Utils;

namespace Xamarin.Android.LinkedIn.Sample
{
    [Activity(Label = "Xamarin.Android.LinkedIn.Sample", MainLauncher = true)]
    public class MainActivity : Activity, IAuthListener, IApiListener
    {
        public LISessionManager _linkedInInstance;
        private TextView _textview;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);

            _linkedInInstance = LISessionManager.GetInstance(this);

            FindViewById<Button>(Resource.Id.button).Click += (sender, e) => Authenticate();
            _textview = FindViewById<TextView>(Resource.Id.textView);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            _linkedInInstance.OnActivityResult(this, requestCode, (int)resultCode, data);
        }

        public void Authenticate() => _linkedInInstance.Init(this, Scope.Build(Scope.RBasicprofile), this, true);

        public void OnAuthError(LIAuthError p0) => _textview.Text = p0.ToString();

        public void OnAuthSuccess() => GetUserPhotoUrl();

        public void GetUserPhotoUrl()
        {
            var apiRequestUrl = "https://api.linkedin.com/v1/people/~/picture-urls::(original)?format=json";
            APIHelper.GetInstance(this).GetRequest(this, apiRequestUrl, this);
        }

        public void OnApiError(LIApiError p0) => _textview.Text = p0.ToString();

        public void OnApiSuccess(ApiResponse apiResponse)
        {
            var data = JsonConvert.DeserializeObject<ApiResponseData>(apiResponse.ResponseDataAsString);
            if (data != null && data.Values.Any())
            {
                var PhotoUrl = new Uri(data.Values.First(), UriKind.Absolute);
                _textview.Text = $"Photo url {PhotoUrl}";
            }
            else
            {
                _textview.Text = "Can't retrieve photo.";
            }
        }

        private class ApiResponseData
        {
            [JsonProperty(PropertyName = "values")]
            public List<string> Values { get; set; }
        }
    }
}

