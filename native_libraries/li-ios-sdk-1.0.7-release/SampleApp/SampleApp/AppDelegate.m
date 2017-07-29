//
//  AppDelegate.m
//  LISDKSampleApp
//
//  Created by Fred Cheng on 2/18/15.
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#import "AppDelegate.h"
#import "ViewController.h"
#import "APICallViewController.h"
#import "APIResponseViewController.h"
#import <linkedin-sdk/LISDK.h>
#import "DeepLinkViewController.h"

@interface AppDelegate ()

@property (nonatomic,strong) UINavigationController *navController;
@property (nonatomic,strong) ViewController *viewController;
@property (nonatomic,strong) APICallViewController *apiController;
@property (nonatomic,strong) APIResponseViewController *apiResponseController;
@property (nonatomic,strong) DeepLinkViewController *deepLinkController;

@end

@implementation AppDelegate


- (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {
    // Override point for customization after application launch.
    self.viewController = [[ViewController alloc] init];
    
    self.apiController = [[APICallViewController alloc] init];
    self.apiResponseController = [[APIResponseViewController alloc] init];
    self.deepLinkController = [[DeepLinkViewController alloc]init];
    self.navController = [[UINavigationController alloc] initWithRootViewController:self.viewController];

    self.window.rootViewController = self.navController;
    self.window.backgroundColor = [UIColor whiteColor];
    return YES;
}

- (void)applicationWillResignActive:(UIApplication *)application {
    // Sent when the application is about to move from active to inactive state. This can occur for certain types of temporary interruptions (such as an incoming phone call or SMS message) or when the user quits the application and it begins the transition to the background state.
    // Use this method to pause ongoing tasks, disable timers, and throttle down OpenGL ES frame rates. Games should use this method to pause the game.
}

- (void)applicationDidEnterBackground:(UIApplication *)application {
    // Use this method to release shared resources, save user data, invalidate timers, and store enough application state information to restore your application to its current state in case it is terminated later.
    // If your application supports background execution, this method is called instead of applicationWillTerminate: when the user quits.
}

- (void)applicationWillEnterForeground:(UIApplication *)application {
    // Called as part of the transition from the background to the inactive state; here you can undo many of the changes made on entering the background.
}

- (void)applicationDidBecomeActive:(UIApplication *)application {
    // Restart any tasks that were paused (or not yet started) while the application was inactive. If the application was previously in the background, optionally refresh the user interface.
}

- (void)applicationWillTerminate:(UIApplication *)application {
    // Called when the application is about to terminate. Save data if appropriate. See also applicationDidEnterBackground:.
    self.viewController = nil;
    self.apiController = nil;
    self.apiResponseController = nil;
    
}
- (BOOL)application:(UIApplication *)application openURL:(NSURL *)url sourceApplication:(NSString *)sourceApplication annotation:(id)annotation {

    NSLog(@"%s url=%@","app delegate application openURL called ", [url absoluteString]);
    if ([LISDKCallbackHandler shouldHandleUrl:url]) {
        return [LISDKCallbackHandler application:application openURL:url sourceApplication:sourceApplication annotation:annotation];
    }
    return YES;
}

- (void)showAPIPage {
    if (_navController.topViewController == _apiController) {
        return;
    }
    [self.navController pushViewController:self.apiController animated:true];
}

- (void)showAPIResponsePage:(NSString *)method status:(int)statusCode request:(NSString *)requestUrl response:(NSString *)response {
    if (_navController.topViewController == _apiResponseController) {
        return;
    }
    _apiResponseController.method = method;
    _apiResponseController.url = requestUrl;
    _apiResponseController.status = [NSString stringWithFormat:@"%d",statusCode];
    _apiResponseController.response = response;
    
    _apiResponseController.requestMethodLabel.text = method;
    _apiResponseController.requestUrlLabel.text = requestUrl;
    _apiResponseController.responseStatusLabel.text = [NSString stringWithFormat:@"%d",statusCode];
    _apiResponseController.responseTextView.text = response;
     
    [self.navController pushViewController:self.apiResponseController animated:true];
}

- (void)showDeepLinkPage {
    if (_navController.topViewController == self.deepLinkController) {
        return;
    }
    [self.navController pushViewController:self.deepLinkController animated:true];
}

@end
