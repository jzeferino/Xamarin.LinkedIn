//
//  ViewController.m
//  LISDKSampleApp
//
//  Created by Fred Cheng on 2/18/15.
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#import "ViewController.h"
#import "AppDelegate.h"
#import <linkedin-sdk/LISDK.h>

@interface ViewController ()

@property (nonatomic,weak) IBOutlet UILabel *titleLabel;
@property (nonatomic,weak) IBOutlet UITextView *responseLabel;
@property (nonatomic,weak) IBOutlet UIButton *apiButton;
@property (nonatomic,weak) IBOutlet UIButton *deepLinkButton;
@property (nonatomic,weak) IBOutlet UIButton *clearSessionButton;
@property (nonatomic,strong) NSError *lastError;

@end

@implementation ViewController



- (instancetype)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
    self = [super initWithNibName:@"MainView"  bundle:nil];
    if (self) {
        self.tabBarItem.title = @"Session";
        self.lastError = nil;
    }
    return self;
}

- (void)viewDidLoad {
    [super viewDidLoad];
    // Do any additional setup after loading the view, typically from a nib.
    self.responseLabel.layer.borderWidth = 1.0f;
    self.responseLabel.layer.borderColor = [[UIColor grayColor] CGColor];
    [self updateControlsWithResponseLabel:YES];
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (IBAction)sync:(id)sender {
    NSLog(@"%s","sync pressed2");
    [LISDKSessionManager createSessionWithAuth:[NSArray arrayWithObjects:LISDK_BASIC_PROFILE_PERMISSION, LISDK_EMAILADDRESS_PERMISSION, nil]
                                     state:@"some state"
                                     showGoToAppStoreDialog:YES
                                               successBlock:^(NSString *returnState) {
                                                   
                                                   NSLog(@"%s","success called!");
                                                   LISDKSession *session = [[LISDKSessionManager sharedInstance] session];
                                                   NSLog(@"value=%@ isvalid=%@",[session value],[session isValid] ? @"YES" : @"NO");
                                                   NSMutableString *text = [[NSMutableString alloc] initWithString:[session.accessToken description]];
                                                   [text appendString:[NSString stringWithFormat:@",state=\"%@\"",returnState]];
                                                   NSLog(@"Response label text %@",text);
                                                   _responseLabel.text = text;
                                                   self.lastError = nil;
                                                   // retain cycle here?
                                                   [self updateControlsWithResponseLabel:NO];
                                                   
                                               }
                                                 errorBlock:^(NSError *error) {
                                                     NSLog(@"%s %@","error called! ", [error description]);
                                                     self.lastError = error;
                                                     //  _responseLabel.text = [error description];
                                                     [self updateControlsWithResponseLabel:YES];
                                                 }
     ];
    NSLog(@"%s","sync pressed3");
}

- (IBAction)clearSession:(id)sender {
    NSLog(@"%s","clear pressed");
    [LISDKSessionManager clearSession];
    _responseLabel.text = nil;
    [self updateControlsWithResponseLabel:YES];
}

- (IBAction)apiCall:(id)sender {
    NSLog(@"%s","api pressed");
    AppDelegate *myAppDelegate = (AppDelegate *)[[UIApplication sharedApplication] delegate];
    [myAppDelegate showAPIPage];
    
}

- (IBAction)deepLink:(id)sender {
    NSLog(@"%s","deeplink pressed");
    AppDelegate *myAppDelegate = (AppDelegate *)[[UIApplication sharedApplication] delegate];
    [myAppDelegate showDeepLinkPage];
}


- (void)updateControlsWithResponseLabel:(BOOL)updateResponseLabel {
    if ([LISDKSessionManager hasValidSession]) {
        if (updateResponseLabel) {
            _responseLabel.text = [[[LISDKSessionManager sharedInstance] session].accessToken description];
        }
        _apiButton.enabled = YES;
        _deepLinkButton.enabled = YES;
        _clearSessionButton.enabled = YES;
    }
    else {
        if (self.lastError != nil) {
            _responseLabel.text = self.lastError.description;
        }
        else {
            _responseLabel.text = nil;
        }
        _apiButton.enabled = NO;
        _deepLinkButton.enabled = NO;
        _clearSessionButton.enabled = NO;
    }
}

@end
