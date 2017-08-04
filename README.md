[![Build Status](https://www.bitrise.io/app/c682a090b3a7c1e7/status.svg?token=_xbX7Yn9LEMHfOFB2kMzng&branch=master)](https://www.bitrise.io/app/c682a090b3a7c1e7)

Xamarin.LinkedIn
===================

Bindings for Xamarin.Android and Xamarin.iOS of the LinkedIn mobile SDK.

<p align="center">
<img src="https://github.com/jzeferino/Xamarin.LinkedIn/blob/master/art/icon.png"/>
</p>

| Binding                                     | Sample                                            | NuGet                                     |
|---------------------------------------------|---------------------------------------------------|-------------------------------------------|
| [Xamarin.Android.LinkedIn][binding-android] | [Xamarin.Android.LinkedIn.Sample][android-sample] | [Xamarin.Android.LinkedIn][nuget-android] |
| [Xamarin.iOS.LinkedIn][binding-ios]         | [Xamarin.iOS.LinkedIn.Sample][ios-sample]         | [Xamarin.iOS.LinkedIn][nuget-ios]         |

## Run the samples

For a complete usage of the bindings, please follow the [samples][samples] and read the original documentation for [Android][docs-sdk-android] and [iOS][docs-sdk-ios].

* The Xamarin.Android.LinkedIn.Sample works out of the box, in any device that has LinkedIn installed.

* For the Xamarin.iOS.LinkedIn.Sample to work you need to [Associate your iOS app with your LinkedIn app](docs/associate-ios.md) and change in the `Info.plist` the `BundleId` to the one configured in LinkedId and `ApplicationId` (linkedIn numeric identifier).

## Integrate LinkedIn in your Android/iOS application

1. [Associate your Android app with your LinkedIn app](docs/associate-android.md)
2. [Associate your iOS app with your LinkedIn app](docs/associate-ios.md)

[docs-sdk-android]: https://developer.linkedin.com/docs/android-sdk
[docs-sdk-ios]: https://developer.linkedin.com/docs/ios-sdk
[docs-understanding]: https://developer.linkedin.com/docs/ios-sdk-auth#ux
[binding-android]: src/Xamarin.Android.LinkedIn/
[binding-ios]: src/Xamarin.iOS.LinkedIn/
[android-sample]: src/samples/Xamarin.Android.LinkedIn.Sample/
[ios-sample]: src/samples/Xamarin.iOS.LinkedIn.Sample/
[nuget-android]: https://www.nuget.org/packages/Xamarin.Android.LinkedIn/
[nuget-ios]: https://www.nuget.org/packages/Xamarin.iOS.LinkedIn/
[samples]: src/samples/

### License
[MIT Licence](LICENSE) 
