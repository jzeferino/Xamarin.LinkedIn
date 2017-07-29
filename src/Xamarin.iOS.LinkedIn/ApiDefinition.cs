using System;
using Foundation;
using ObjCRuntime;
using UIKit;

namespace Xamarin.iOS.LinkedIn
{
    // typedef void (^AuthSuccessBlock)(NSString *);
    delegate void AuthSuccessBlock(string arg0);

    // typedef void (^AuthErrorBlock)(NSError *);
    delegate void AuthErrorBlock(NSError arg0);

    // @interface LISDKSessionManager : NSObject
    [BaseType(typeof(NSObject), Name = "LISDKSessionManager")]
    interface SessionManager
    {
        // +(instancetype)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        SessionManager SharedInstance();

        // @property (readonly, nonatomic) LISDKSession * session;
        [Export("session")]
        Session Session { get; }

        // +(void)createSessionWithAuth:(NSArray *)permissions state:(NSString *)state showGoToAppStoreDialog:(BOOL)showDialog successBlock:(AuthSuccessBlock)successBlock errorBlock:(AuthErrorBlock)erroBlock;
        [Static]
        [Export("createSessionWithAuth:state:showGoToAppStoreDialog:successBlock:errorBlock:")]
        void CreateSessionWithAuth(NSObject[] permissions, string state, bool showDialog, AuthSuccessBlock successBlock, AuthErrorBlock erroBlock);

        // +(void)createSessionWithAccessToken:(LISDKAccessToken *)accessToken;
        [Static]
        [Export("createSessionWithAccessToken:")]
        void CreateSessionWithAccessToken(AccessToken accessToken);

        // +(void)clearSession;
        [Static]
        [Export("clearSession")]
        void ClearSession();

        // +(BOOL)hasValidSession;
        [Static]
        [Export("hasValidSession")]
        bool HasValidSession { get; }

        // +(BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;
        [Static]
        [Export("application:openURL:sourceApplication:annotation:")]
        bool Application(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation);

        // +(BOOL)shouldHandleUrl:(NSURL *)url;
        [Static]
        [Export("shouldHandleUrl:")]
        bool ShouldHandleUrl(NSUrl url);
    }

    // @interface LISDKSession : NSObject
    [BaseType(typeof(NSObject), Name = "LISDKSession")]
    interface Session
    {
        // @property (nonatomic, strong) LISDKAccessToken * accessToken;
        [Export("accessToken", ArgumentSemantic.Strong)]
        AccessToken AccessToken { get; set; }

        // -(BOOL)isValid;
        [Export("isValid")]
        bool IsValid { get; }

        // -(NSString *)value;
        [Export("value")]
        string Value { get; }
    }

    // @interface LISDKAccessToken : NSObject
    [BaseType(typeof(NSObject), Name = "LISDKAccessToken")]
    interface AccessToken
    {
        // @property (readonly, nonatomic) NSString * accessTokenValue;
        [Export("accessTokenValue")]
        string AccessTokenValue { get; }

        // @property (readonly, nonatomic) NSDate * expiration;
        [Export("expiration")]
        NSDate Expiration { get; }

        // +(instancetype)LISDKAccessTokenWithValue:(NSString *)value expiresOnMillis:(long long)expiresOnMillis;
        [Static]
        [Export("LISDKAccessTokenWithValue:expiresOnMillis:")]
        AccessToken LISDKAccessTokenWithValue(string value, long expiresOnMillis);

        // +(instancetype)LISDKAccessTokenWithSerializedString:(NSString *)serString;
        [Static]
        [Export("LISDKAccessTokenWithSerializedString:")]
        AccessToken LISDKAccessTokenWithSerializedString(string serString);

        // -(NSString *)serializedString;
        [Export("serializedString")]
        string SerializedString { get; }
    }

    // @interface LISDKAPIError : NSError
    [BaseType(typeof(NSError), Name = "LISDKAPIError")]
    interface APIError
    {
        // -(LISDKAPIResponse *)errorResponse;
        [Export("errorResponse")]
        APIResponse ErrorResponse { get; }

        // +(id)errorWithApiResponse:(LISDKAPIResponse *)response;
        [Static]
        [Export("errorWithApiResponse:")]
        NSObject ErrorWithApiResponse(APIResponse response);

        // +(id)errorWithError:(NSError *)error;
        [Static]
        [Export("errorWithError:")]
        NSObject ErrorWithError(NSError error);
    }

    // typedef void (^APISuccessBlock)(LISDKAPIResponse *);
    delegate void APISuccessBlock(APIResponse arg0);

    // typedef void (^APIErrorBlock)(LISDKAPIResponse *, NSError *);
    delegate void APIErrorBlock(APIResponse arg0, NSError arg1);

    // @interface LISDKAPIHelper : NSObject
    [BaseType(typeof(NSObject))]
    interface LISDKAPIHelper
    {
        // +(instancetype)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        LISDKAPIHelper SharedInstance();

        // -(void)getRequest:(NSString *)url success:(void (^)(LISDKAPIResponse *))success error:(void (^)(LISDKAPIError *))error;
        [Export("getRequest:success:error:")]
        void GetRequest(string url, Action<APIResponse> success, Action<APIError> error);

        // -(void)deleteRequest:(NSString *)url success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("deleteRequest:success:error:")]
        void DeleteRequest(string url, Action<APIResponse> successCompletion, Action<APIError> errorCompletion);

        // -(void)putRequest:(NSString *)url body:(NSData *)body success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("putRequest:body:success:error:")]
        void PutRequest(string url, NSData body, Action<APIResponse> successCompletion, Action<APIError> errorCompletion);

        // -(void)putRequest:(NSString *)url stringBody:(NSString *)stringBody success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("putRequest:stringBody:success:error:")]
        void PutRequest(string url, string stringBody, Action<APIResponse> successCompletion, Action<APIError> errorCompletion);

        // -(void)postRequest:(NSString *)url body:(NSData *)body success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("postRequest:body:success:error:")]
        void PostRequest(string url, NSData body, Action<APIResponse> successCompletion, Action<APIError> errorCompletion);

        // -(void)postRequest:(NSString *)url stringBody:(NSString *)stringBody success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("postRequest:stringBody:success:error:")]
        void PostRequest(string url, string stringBody, Action<APIResponse> successCompletion, Action<APIError> errorCompletion);

        // -(void)apiRequest:(NSString *)url method:(NSString *)method body:(NSData *)body success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("apiRequest:method:body:success:error:")]
        void ApiRequest(string url, string method, NSData body, Action<APIResponse> successCompletion, Action<APIError> errorCompletion);

        // -(void)cancelCalls;
        [Export("cancelCalls")]
        void CancelCalls();
    }

    // @interface LISDKAPIResponse : NSObject
    [BaseType(typeof(NSObject), Name = "LISDKAPIResponse")]
    interface APIResponse
    {
        // @property (readonly, nonatomic) NSString * data;
        [Export("data")]
        string Data { get; }

        // @property (readonly, nonatomic) int statusCode;
        [Export("statusCode")]
        int StatusCode { get; }

        // @property (readonly, nonatomic) NSDictionary * headers;
        [Export("headers")]
        NSDictionary Headers { get; }

        // -(id)initWithData:(NSString *)data headers:(NSDictionary *)headers statusCode:(int)statusCode;
        [Export("initWithData:headers:statusCode:")]
        IntPtr Constructor(string data, NSDictionary headers, int statusCode);
    }

    // @interface LISDKCallbackHandler : NSObject
    [BaseType(typeof(NSObject), Name = "LISDKCallbackHandler")]
    interface CallbackHandler
    {
        // +(BOOL)shouldHandleUrl:(NSURL *)url;
        [Static]
        [Export("shouldHandleUrl:")]
        bool ShouldHandleUrl(NSUrl url);

        // +(BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;
        [Static]
        [Export("application:openURL:sourceApplication:annotation:")]
        bool Application(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation);
    }

    // typedef void (^DeeplinkSuccessBlock)(NSString *);
    delegate void DeeplinkSuccessBlock(string arg0);

    // typedef void (^DeeplinkErrorBlock)(NSError *, NSString *);
    delegate void DeeplinkErrorBlock(NSError arg0, string arg1);

    // @interface LISDKDeeplinkHelper : NSObject
    [BaseType(typeof(NSObject), Name = "LISDKDeeplinkHelper")]
    interface DeeplinkHelper
    {
        // +(instancetype)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        DeeplinkHelper SharedInstance();

        // -(void)viewCurrentProfileWithState:(NSString *)state showGoToAppStoreDialog:(BOOL)showDialog success:(DeeplinkSuccessBlock)success error:(DeeplinkErrorBlock)error;
        [Export("viewCurrentProfileWithState:showGoToAppStoreDialog:success:error:")]
        void ViewCurrentProfileWithState(string state, bool showDialog, DeeplinkSuccessBlock success, DeeplinkErrorBlock error);

        // -(void)viewOtherProfile:(NSString *)memberId withState:(NSString *)state showGoToAppStoreDialog:(BOOL)showDialog success:(DeeplinkSuccessBlock)success error:(DeeplinkErrorBlock)error;
        [Export("viewOtherProfile:withState:showGoToAppStoreDialog:success:error:")]
        void ViewOtherProfile(string memberId, string state, bool showDialog, DeeplinkSuccessBlock success, DeeplinkErrorBlock error);

        // +(BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation;
        [Static]
        [Export("application:openURL:sourceApplication:annotation:")]
        bool Application(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation);

        // +(BOOL)shouldHandleUrl:(NSURL *)url;
        [Static]
        [Export("shouldHandleUrl:")]
        bool ShouldHandleUrl(NSUrl url);
    }
}