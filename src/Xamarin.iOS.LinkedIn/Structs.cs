using System;

namespace Xamarin.iOS.LinkedIn
{
    public enum LISDKErrorCode
	{
		None,
		InvalidRequest,
		NetworkUnavailable,
		UserCancelled,
		UnknownError,
		ServerError,
		LinkedinAppNotFound,
		NotAuthenticated
	}
}
