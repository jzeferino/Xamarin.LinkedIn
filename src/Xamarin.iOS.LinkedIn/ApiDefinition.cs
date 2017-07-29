using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using linkedin-sdk;

namespace Xamarin.iOS.LinkedIn
{
    // typedef void (^AuthSuccessBlock)(NSString *);
    delegate void AuthSuccessBlock(string arg0);

    // typedef void (^AuthErrorBlock)(NSError *);
    delegate void AuthErrorBlock(NSError arg0);

    // @interface LISDKSessionManager : NSObject
    [BaseType(typeof(NSObject))]
    interface LISDKSessionManager
    {
        // +(instancetype)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        LISDKSessionManager SharedInstance();

        // @property (readonly, nonatomic) LISDKSession * session;
        [Export("session")]
        LISDKSession Session { get; }

        // +(void)createSessionWithAuth:(NSArray *)permissions state:(NSString *)state showGoToAppStoreDialog:(BOOL)showDialog successBlock:(AuthSuccessBlock)successBlock errorBlock:(AuthErrorBlock)erroBlock;
        [Static]
        [Export("createSessionWithAuth:state:showGoToAppStoreDialog:successBlock:errorBlock:")]
        [Verify(StronglyTypedNSArray)]
        void CreateSessionWithAuth(NSObject[] permissions, string state, bool showDialog, AuthSuccessBlock successBlock, AuthErrorBlock erroBlock);

        // +(void)createSessionWithAccessToken:(LISDKAccessToken *)accessToken;
        [Static]
        [Export("createSessionWithAccessToken:")]
        void CreateSessionWithAccessToken(LISDKAccessToken accessToken);

        // +(void)clearSession;
        [Static]
        [Export("clearSession")]
        void ClearSession();

        // +(BOOL)hasValidSession;
        [Static]
        [Export("hasValidSession")]
        [Verify(MethodToProperty)]
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
    [BaseType(typeof(NSObject))]
    interface LISDKSession
    {
        // @property (nonatomic, strong) LISDKAccessToken * accessToken;
        [Export("accessToken", ArgumentSemantic.Strong)]
        LISDKAccessToken AccessToken { get; set; }

        // -(BOOL)isValid;
        [Export("isValid")]
        [Verify(MethodToProperty)]
        bool IsValid { get; }

        // -(NSString *)value;
        [Export("value")]
        [Verify(MethodToProperty)]
        string Value { get; }
    }

    // @interface LISDKAccessToken : NSObject
    [BaseType(typeof(NSObject))]
    interface LISDKAccessToken
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
        LISDKAccessToken LISDKAccessTokenWithValue(string value, long expiresOnMillis);

        // +(instancetype)LISDKAccessTokenWithSerializedString:(NSString *)serString;
        [Static]
        [Export("LISDKAccessTokenWithSerializedString:")]
        LISDKAccessToken LISDKAccessTokenWithSerializedString(string serString);

        // -(NSString *)serializedString;
        [Export("serializedString")]
        [Verify(MethodToProperty)]
        string SerializedString { get; }
    }

    // @interface LISDKAPIError : NSError
    [BaseType(typeof(NSError))]
    interface LISDKAPIError
    {
        // -(LISDKAPIResponse *)errorResponse;
        [Export("errorResponse")]
        [Verify(MethodToProperty)]
        LISDKAPIResponse ErrorResponse { get; }

        // +(id)errorWithApiResponse:(LISDKAPIResponse *)response;
        [Static]
        [Export("errorWithApiResponse:")]
        NSObject ErrorWithApiResponse(LISDKAPIResponse response);

        // +(id)errorWithError:(NSError *)error;
        [Static]
        [Export("errorWithError:")]
        NSObject ErrorWithError(NSError error);
    }

    // typedef void (^APISuccessBlock)(LISDKAPIResponse *);
    delegate void APISuccessBlock(LISDKAPIResponse arg0);

    // typedef void (^APIErrorBlock)(LISDKAPIResponse *, NSError *);
    delegate void APIErrorBlock(LISDKAPIResponse arg0, NSError arg1);

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
        void GetRequest(string url, Action<LISDKAPIResponse> success, Action<LISDKAPIError> error);

        // -(void)deleteRequest:(NSString *)url success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("deleteRequest:success:error:")]
        void DeleteRequest(string url, Action<LISDKAPIResponse> successCompletion, Action<LISDKAPIError> errorCompletion);

        // -(void)putRequest:(NSString *)url body:(NSData *)body success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("putRequest:body:success:error:")]
        void PutRequest(string url, NSData body, Action<LISDKAPIResponse> successCompletion, Action<LISDKAPIError> errorCompletion);

        // -(void)putRequest:(NSString *)url stringBody:(NSString *)stringBody success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("putRequest:stringBody:success:error:")]
        void PutRequest(string url, string stringBody, Action<LISDKAPIResponse> successCompletion, Action<LISDKAPIError> errorCompletion);

        // -(void)postRequest:(NSString *)url body:(NSData *)body success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("postRequest:body:success:error:")]
        void PostRequest(string url, NSData body, Action<LISDKAPIResponse> successCompletion, Action<LISDKAPIError> errorCompletion);

        // -(void)postRequest:(NSString *)url stringBody:(NSString *)stringBody success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("postRequest:stringBody:success:error:")]
        void PostRequest(string url, string stringBody, Action<LISDKAPIResponse> successCompletion, Action<LISDKAPIError> errorCompletion);

        // -(void)apiRequest:(NSString *)url method:(NSString *)method body:(NSData *)body success:(void (^)(LISDKAPIResponse *))successCompletion error:(void (^)(LISDKAPIError *))errorCompletion;
        [Export("apiRequest:method:body:success:error:")]
        void ApiRequest(string url, string method, NSData body, Action<LISDKAPIResponse> successCompletion, Action<LISDKAPIError> errorCompletion);

        // -(void)cancelCalls;
        [Export("cancelCalls")]
        void CancelCalls();
    }

    // @interface LISDKAPIResponse : NSObject
    [BaseType(typeof(NSObject))]
    interface LISDKAPIResponse
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
    [BaseType(typeof(NSObject))]
    interface LISDKCallbackHandler
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
    [BaseType(typeof(NSObject))]
    interface LISDKDeeplinkHelper
    {
        // +(instancetype)sharedInstance;
        [Static]
        [Export("sharedInstance")]
        LISDKDeeplinkHelper SharedInstance();

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