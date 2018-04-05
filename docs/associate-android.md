## Associate your Android app with your LinkedIn app

See [complete docs][docs-sdk] by LinkedIn.

You must identify your specific Android app with LinkedIn by adding some information about it to your LinkedIn application's settings.
If you have not already done so, [create an application][create]. If you have an existing application, [select][select] it to modify its settings.  Along with the other required values, ensure that you have filled out the "Android Package Names and Hashes" field, using the following information:

There are two types of key hashes: debug (i.e. development) and release (i.e. production).

* Debug keys are required for LinkedIn to verify the authenticity of your application while it interacts with our APIs during your development cycle.  
* A release key is required because all Android applications must be signed with one before they can be uploaded to the Play store for distribution.
We strongly encourage you to read Google's official Signing Your Applications documentation, specifically the sections regarding "Signing Considerations" and "Securing Your Private Key", to ensure that you fully understand the Android application signing process.

### Generating a debug key hash value

Use the following command to generate a debug key hash value.  The location of your debug key store will differ depending on whether you are developing on a Windows or Mac platform.  If you do not have OpenSSL installed, you can download it for Windows or Mac/Unix.

* Windows 

```
keytool -exportcert -keystore %HOMEPATH%\.android\debug.keystore -alias androiddebugkey | openssl sha1 -binary | openssl base64
```

* mac/unix 

```
keytool -exportcert -keystore ~/.android/debug.keystore -alias androiddebugkey | openssl sha1 -binary | openssl base64
```
### Generating a release key hash value

To generate a release key hash value, use the following command:

```
keytool -exportcert -keystore YOUR_RELEASE_KEY_PATH -alias YOUR_RELEASE_KEY_ALIAS | openssl sha1 -binary | openssl base64
```

### Configure the values

In the Mobile->Android Settings->"Package Name & Package Hash" section of your [LinkedIn application's][linkedin-app] configuration, add your application's package and hash values:

For example:

![Android package configuration][package-config]

### Initialize a connection to LinkedIn

The `LISessionManager` class is the heart of the Mobile SDK for Android.  It provides 
all of the necessary functionality to create and manage the session connection 
to LinkedIn to perform additional SDK operations with.

```c#
_linkedInInstance = LISessionManager.GetInstance(this);

// Request the authentication.
_linkedInInstance.Init(this, Scope.Build(Scope.RBasicprofile), true,
    () =>
    {
        // Authentication succeeded.
    },
    error =>
    {
        // Error authenticating.
    });

````

## Making Authenticated REST API calls

Once your users are authenticated, you can easily make calls to LinkedIn's REST API on their behalf.  The `APIHelper` object provides helper methods for all of the necessary HTTP verbs.  For complete details on all of the available API calls that can be made, consult the developer [documentation][documentation].

### APIHelper.GetInstance(this).GetRequest

Make an HTTP `GET` request to LinkedIn's REST API using the currently authenticated user's credentials.  If successful, a LinkedIn `ApiResponse` object containing all of the relevant aspects of the server's response will be returned.

This method takes the following arguments:

* context - The Android `Context` that the API call is originating from. This value acts as a reference that will allow you to cancel this request in the future.
* `url` - The URL for the LinkedIn REST API endpoint that you wish to call.
* `ApiListener` - An implementation of `ApiListener` to handle the response from the API call.  The `OnApiSuccess()` and `OnApiError()` methods should be overridden to handle the result of the call in a manner that makes sense to your application.

```c#
APIHelper.GetInstance(this).GetRequest(this, "https://api.linkedin.com/v1/people/~", 
    apiResponse =>
    {
        // do something with response
    },
    error =>
    {
        // do something with error
    });
```

[docs-sdk]: https://developer.linkedin.com/docs/android-sdk
[create]: https://www.linkedin.com/developer/apps/new
[select]: https://www.linkedin.com/secure/developer
[linkedin-app]: https://www.linkedin.com/secure/developer
[package-config]: https://content.linkedin.com/content/dam/developer/global/en_US/site/img/package_hash_values.png
[documentation]: https://developer.linkedin.com/docs
