//
//  DeepLinkViewController.m
//  sampleapp
//
//  Created by Fred Cheng on 4/1/15.
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#import "DeepLinkViewController.h"
#import "AppDelegate.h"
#import <linkedin-sdk/LISDK.h>

@interface DeepLinkViewController ()
@property (nonatomic,weak) IBOutlet UITextField *memberIdText;
@property (nonatomic,weak) IBOutlet UITextView *responseLabel;

@end

@implementation DeepLinkViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view.
    self.navigationItem.title = @"DeepLink";

}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)viewMyProfileButtonClicked:(id)sender {
    NSLog(@"viewMyProfileButton clicked");
    DeeplinkSuccessBlock success = ^(NSString *returnState) {
        NSLog(@"Success with returned state: %@",returnState);
    };
    DeeplinkErrorBlock error = ^(NSError *error, NSString *returnState) {
        NSLog(@"Error with returned state: %@", returnState);
        NSLog(@"Error %@", error);
    };
    [[LISDKDeeplinkHelper sharedInstance] viewCurrentProfileWithState:@"viewMyProfileButton" showGoToAppStoreDialog:YES success:success error:error];
}

- (IBAction)viewMemberProfileButtonClicked:(id)sender {
    NSLog(@"viewMemberProfileButton clicked");
    DeeplinkSuccessBlock success = ^(NSString *returnState) {
        NSLog(@"Success with returned state: %@",returnState);
    };
    DeeplinkErrorBlock error = ^(NSError *error, NSString *returnState) {
        NSLog(@"Error with returned state: %@", returnState);
        NSLog(@"Error %@", error);
    };
    NSString *memberId = self.memberIdText.text;
    [[LISDKDeeplinkHelper sharedInstance] viewOtherProfile:memberId withState:@"viewMemberProfileButton" showGoToAppStoreDialog:YES success:success error:error];
}

- (void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event {
    UITouch *touch = [[event allTouches] anyObject];
    if ([_memberIdText isFirstResponder] && [touch view] != _memberIdText) {
        [_memberIdText resignFirstResponder];
    }
    [super touchesBegan:touches withEvent:event];
}

@end
