using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UIKit;

namespace Xamarin.iOS.LinkedIn.Sample
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            _button.TouchUpInside += SignIn;
        }

        private void SignIn(object sender, EventArgs e)
        {
            SessionManager.CreateSessionWithAuth(
                new[] { Permission.BasicProfile, Permission.EmailAddress },
                "state",
                true,
                returnState => GetProfilePhoto(),
                error => InvokeOnMainThread(() => 
                {
                    _textView.Text = $"Sign in failed: {error.Description}";
                })
            );
        }

        private void GetProfilePhoto()
        {
            var apiRequestUrl = "https://api.linkedin.com/v1/people/~/picture-urls::(original)?format=json";
            ApiHelper.SharedInstance.GetRequest(apiRequestUrl,
                response => InvokeOnMainThread(() =>
                {
                    var data = JsonConvert.DeserializeObject<ApiResponseData>(response.Data);
                    if (data != null && data.Values.Any())
                    {
                        var PhotoUrl = new Uri(data.Values.First(), UriKind.Absolute);
                        _textView.Text = $"Photo url {PhotoUrl}";
                    }
                    else
                    {
                        _textView.Text = "Can't retrieve photo.";
                    }
                }),
                apiError => InvokeOnMainThread(() => 
                {
                    _textView.Text = apiError.ToString();
                })
           );
        }

        private class ApiResponseData
        {
            [JsonProperty(PropertyName = "values")]
            public List<string> Values { get; set; }
        }

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);
            _button.TouchUpInside -= SignIn;
        }
    }
}
