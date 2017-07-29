//
//  AppDelegate.h
//  LISDKSampleApp
//
//  Created by Fred Cheng on 2/18/15.
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#import <UIKit/UIKit.h>

@class ViewController;
@class APICallViewController;
@class APIResponseViewController;

@interface AppDelegate : UIResponder <UIApplicationDelegate>

@property (strong, nonatomic) UIWindow *window;

-(void) showAPIPage;
-(void) showAPIResponsePage:(NSString *)method status:(int)statusCode request:(NSString *)requestUrl response:(NSString *)response;
- (void)showDeepLinkPage;
@end

