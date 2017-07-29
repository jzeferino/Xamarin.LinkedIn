//
//  APICallViewController.m
//  LISDKSampleApp
//
//  Created by Fred Cheng on 2/23/15.
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#import "APICallViewController.h"
#import "AppDelegate.h"
#import <linkedin-sdk/LISDK.h>

@interface APICallViewController ()

@property (nonatomic,weak) IBOutlet UITextField *resourceTextField;
@property (nonatomic,weak) IBOutlet UITextView *bodyTextField;
@property (nonatomic,weak) IBOutlet UISegmentedControl *methodTypeControl;

-(IBAction)indexChanged:(UISegmentedControl *)sender;

@end

@implementation APICallViewController


- (instancetype)initWithNibName:(NSString *)nibNameOrNil bundle:(NSBundle *)nibBundleOrNil {
    self = [super initWithNibName:nibNameOrNil bundle:nibBundleOrNil];
    if (self) {
        self.tabBarItem.title = @"API Calls";
    }
    return self;
}

- (void)viewDidLoad {
    [super viewDidLoad];
    self.navigationItem.title = @"API Call";
    self.bodyTextField.layer.borderWidth = 1.0f;
    self.bodyTextField.layer.borderColor = [[UIColor grayColor] CGColor];
    self.methodTypeControl.selectedSegmentIndex = 0;
}

- (NSString *)getMethod {
    int method =  [_methodTypeControl selectedSegmentIndex];
    switch (method) {
        case 0:
            return @"GET";
        case 1:
            return @"PUT";
        case 2:
            return @"POST";
        case 3:
            return @"DELETE";
        default:
            return @"Error";
    }
}

- (IBAction)execute:(id)sender {
    NSLog(@"execute called with %@ body %@ and %d", _resourceTextField.text, _bodyTextField.text, (int)[_methodTypeControl selectedSegmentIndex] );
    // save state when request was made
    NSString *method = [self getMethod];
    [[LISDKAPIHelper sharedInstance] apiRequest:_resourceTextField.text
                                         method:method
                                           body:[_bodyTextField.text dataUsingEncoding:NSUTF8StringEncoding]
                                        success:^(LISDKAPIResponse *response) {
                                            NSLog(@"success called %@", response.data);
                                            AppDelegate *myAppDelegate = (AppDelegate *)[[UIApplication sharedApplication] delegate];
                                            dispatch_sync(dispatch_get_main_queue(), ^{
                                                [myAppDelegate showAPIResponsePage:method
                                                                            status:response.statusCode
                                                                           request:_resourceTextField.text
                                                                          response:response.data];
                                            });
                                        }
                                          error:^(LISDKAPIError *apiError) {
                                              NSLog(@"error called %@", apiError.description);
                                               AppDelegate *myAppDelegate = (AppDelegate *) [[UIApplication sharedApplication] delegate];
                                              dispatch_sync(dispatch_get_main_queue(), ^{
                                                  LISDKAPIResponse *response = [apiError errorResponse];
                                                  NSString *errorText;
                                                  if (response) {
                                                      errorText = response.data;
                                                  }
                                                  else {
                                                      errorText = apiError.description;
                                                  }
                                              [myAppDelegate showAPIResponsePage:[self getMethod]
                                                                          status:[apiError errorResponse].statusCode
                                                                         request:_resourceTextField.text
                                                                        response:errorText];
                                              });
                                          }];
}

- (void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event {
    
    UITouch *touch = [[event allTouches] anyObject];
    if ([_resourceTextField isFirstResponder] && [touch view] != _resourceTextField) {
        [_resourceTextField resignFirstResponder];
    }
    if ([_bodyTextField isFirstResponder] && [touch view] != _bodyTextField) {
        [_bodyTextField resignFirstResponder];
    }
    [super touchesBegan:touches withEvent:event];
}

-(IBAction)indexChanged:(UISegmentedControl *)sender {

}
@end
