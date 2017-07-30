## Associate your iOS app with your LinkedIn app

See [complete docs][docs-sdk] by LinkedIn.

If you have not already done so, [create an application][create]. If you have an 
existing LinkedIn application, [configure it on the Developer website][configure].  

Go to the the “Mobile” setting page, and configure your application’s Bundle ID value 
in your LinkedIn application settings.

### Configure your Bundle ID

Associate your iOS application with your LinkedIn application by configuring your 
Bundle ID value(s) within your LinkedIn application.  Multiple Bundle ID values 
allow a collection of applications (e.g. trial vs. free versions, a suite of 
related apps, etc.) to leverage the same LinkedIn application privileges and access 
tokens.

![iOS Bundle Identifier configuration][bundle-configure]

### Determine your LinkedIn App ID value

Before you can make the necessary changes to your Info.plist file, you need to know 
what your LinkedIn application’s Application ID is.

As seen above, it can be found on the “Mobile” settings page, listed directly 
underneath the “iOS Settings” header, within the application management tool.

### Configure your application's info.plist

Locate the `Info.plist` file in your iOS project and add the following values
(note the two locations within the file where you need to substitute your LinkedIn 
Application ID value):

```xml
<key>LIAppId</key>
<string>{Your LinkedIn app ID}</string>

<key>CFBundleURLTypes</key>
<array>
    <dict>
        <key>CFBundleURLSchemes</key>
        <array>
            <string>li{Your LinkedIn app ID}</string>
        </array>
    </dict>
</array>
```
## iOS 9 Compatibility

If you are targeting iOS 9 support in your application, there are some additional 
steps required to be able to successfully build and run your app:

### Whitelisting LinkedIn Custom Schemes

In iOS 9, you will need to specifically whitelist the schemes exposed by the LinkedIn 
iOS application.  Add the following configuration to your application's `Info.plist` 
file:
```xml
<key>LSApplicationQueriesSchemes</key>
<array>
    <string>linkedin</string>
    <string>linkedin-sdk2</string>
    <string>linkedin-sdk</string>
</array>
```
### Bitcode Support

Bitcode compiling for iOS 9 is supported in version 1.0.6+ of the LinkedIn Mobile 
SDK for iOS.

### App Transport Security

As of iOS 9, Apple is enforcing the use of secure HTTPS connections between an 
app and server.  If your application is exclusively making calls over HTTPS, 
you should have no compatibility issues.  However, if your application needs to 
make requests to insecure HTTP endpoints, you must specify the insecure domain(s) 
in your app's `Info.plist` file.  

For example, the following configuration whitelists HTTP calls to *.linkedin.com 
servers:

```xml
<key>NSAppTransportSecurity</key>
<dict>
    <key>NSExceptionDomains</key>
    <dict>
        <key>linkedin.com</key>
        <dict>
            <key>NSExceptionAllowsInsecureHTTPLoads</key>
            <true/>
            <key>NSIncludesSubdomains</key>
            <true/>                
            <key>NSExceptionRequiresForwardSecrecy</key>
            <false/>
        </dict>
    </dict>
</dict>
```
## Using the SDK

The following information will explain how to use the various features that the 
SDK provides to you, including:

 - Authenticating LinkedIn members
 - Making authenticated API calls
 - Deeplinking to additional member content

Please also consider taking a look at the sample application included with the 
SDK for complete functional implementation examples of all of the features 
outlined below.

### Handle responses from the LinkedIn mobile app

Add the following method to your `AppDelegate` source code to enable the 
LinkedIn App to give control back your application in situations in situations 
where you are brought outside of the context of your application 
(e.g. deeplinking).

```c#
public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
{
    if (CallbackHandler.ShouldHandleUrl (url)) {
        CallbackHandler.OpenUrl (application, url, sourceApplication, annotation);
    }
    return true;
}
```
### Initialize a connection to LinkedIn

See [complete docs][docs-auth] by LinkedIn.

The `SessionManager` class is the heart of the Mobile SDK for iOS.  It provides 
all of the necessary functionality to create and manage the session connection 
to LinkedIn to perform additional SDK operations with.

There are two ways to initialize a LinkedIn session:

 - Without an existing access token
 - With a previously acquired access token


#### Initializing by requesting a new access token

If you do not have a previously established access token for the current user to 
initialize a session with, a new one can be requested and returned to your 
application through a SDK call.

Since this call temporarily takes the user out of your application and into the 
official LinkedIn app to complete the authorization, there are various possible 
user experiences that could occur.  See [Understanding the mobile authentication 
user experience][docs-understanding] for a breakdown of what the user might 
encounter during this part of the authentication process.

Access tokens returned from this call are stored as part of the application’s 
key chain. Subsequent calls to this method will check for the existence of a 
previously requested and stored access token, and use it if found.  If no 
access token is found, or the access token has expired, the user will be 
directed to the official LinkedIn application to acquire a new one.

```c#
public static void CreateSession (
    string[] permissions, 
    string state, 
    bool showDialog, 
    AuthSuccessBlock successBlock, 
    AuthErrorBlock errorBlock);
```

This variant takes four arguments:

 - `permissions` - An array of LinkedIn member permission objects that your 
   application is requesting.  Possible values are defined in `Permission`.  
 - `state` - A value that can be used to maintain state between the request and the 
   callback.  
 - `showDialog` - A boolean that determines whether the user will be 
   directly taken to the App Store or whether they will be presented with a 
   dialog box and given the choice to go to the App Store, if the official 
   LinkedIn application cannot be found installed on the device.  
 - `successBlock` - A callback method to execute upon successful completion of the 
   authorization process. The state value provided in the original call is passed 
   back as a parameter to this callback.  
 - `errorBlock` - A callback method to execute if there are problems during the 
   authorization process.  

Below you will find an example of what the connection initialization portion of 
a typical authentication process might look like:

```c#
SessionManager.CreateSession (
    new []{ Permission.BasicProfile, Permission.EmailAddress },
    null,
    true,
    returnState => {
        Console.WriteLine ("Sign in was successful.");
        var session = SessionManager.SharedInstance.Session;
    },
    error => {
        Console.WriteLine ("Sign in failed.");
    }
);
```
#### Initializing with an existing access token

If you have an access token for the current user that you have serialized from 
a previous interaction with your application, you can use it to create a new 
LinkedIn `Session` rather than requesting a brand new one.  Serialize your access 
token (using `AccessToken.SerializedString`) and provide it as an argument to the 
following method:
```c#
public static void CreateSession (AccessToken accessToken);
```	
This variant takes the following argument:

 - `accessToken` - A valid LinkedIn access token to use when making calls to 
   LinkedIn through the SDK.

#### Mobile vs. server-side access tokens

It is important to note that access tokens that are acquired via the Mobile SDK 
are only usable with the Mobile SDK, and cannot be used to make server-side REST 
API calls.  Similarly, access tokens that you already have stored from your users 
that authenticated using a server-side REST API call will not work with the 
Mobile SDK.

Presently, there is no mechanism available to exchange them.  If you require 
tokens that can be used in both the mobile and server-side environment, you will 
need to implement a traditional OAuth 2.0 solution within your iOS environment to 
acquire tokens that can be leveraged in both situations.


### Retrieve basic profile data

See [complete docs][docs-signin] by LinkedIn.

Once your mobile user has authenticated and you have successfully established a 
LinkedInConnection on their behalf, use the following call to retrieve some 
basic profile details for the member:

```c#
if (SessionManager.HasValidSession) {
    ApiHelper.SharedInstance.GetRequest (
        "https://api.linkedin.com/v1/people/~",
        response => {
            // do something with response
        },
        apiError => {
            // do something with error
        });
}
```

With your user authenticated, and some basic member profile data returned to you 
from the REST API call, you have successfully completed the Sign In with 
LinkedIn workflow using the Mobile SDK for iOS and your application can now 
step in to continue the user experience.


[create]: https://www.linkedin.com/developer/apps/new
[configure]: https://www.linkedin.com/developer/apps
[bundle-configure]: https://content.linkedin.com/content/dam/developer/global/en_US/site/img/ios-bundle-ids.png

[docs-signin]: https://developer.linkedin.com/docs/signin-with-linkedin
[docs-sdk]: https://developer.linkedin.com/docs/ios-sdk
[docs-auth]: https://developer.linkedin.com/docs/ios-sdk-auth
[docs-understanding]: https://developer.linkedin.com/docs/ios-sdk-auth#ux