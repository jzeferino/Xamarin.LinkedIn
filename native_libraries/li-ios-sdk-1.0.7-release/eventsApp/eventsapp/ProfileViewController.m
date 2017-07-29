//
//  ProfileViewController.m
//  eventsapp
//
//  Created by Rahul Bansal on 4/28/15.
//  Copyright (c) 2015 LinkedIn Corporation. All rights reserved.
//

#import "ProfileViewController.h"
#import <linkedin-sdk/LISDK.h>

@interface ProfileViewController ()

@end

@implementation ProfileViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    NSURL *url = [NSURL URLWithString:self.imageUrl];
    NSMutableURLRequest *request = [NSMutableURLRequest requestWithURL:url];
    [NSURLConnection sendAsynchronousRequest:request
                                       queue:[NSOperationQueue mainQueue]
                           completionHandler:^(NSURLResponse *response, NSData *data, NSError *error) {
                               if ( !error )
                               {
                                   self.imageView.image = [[UIImage alloc] initWithData:data];
                               }
                               else
                               {
                                   NSLog(@"Error %@",[error description]);
                                   self.imageView.image = [UIImage imageNamed:[[NSBundle mainBundle] pathForResource:@"ghost_person" ofType:@"png"]];
                               }
                           }];

    self.nameLabel.text = self.name;
    self.headlineLabel.text = self.headline;
    
    UIBarButtonItem *doneButton = [[UIBarButtonItem alloc] initWithBarButtonSystemItem:UIBarButtonSystemItemDone target:self action:@selector(doneFunc)];
    [doneButton setTitleTextAttributes:[NSDictionary dictionaryWithObjectsAndKeys:[UIColor blackColor],UITextAttributeTextColor,nil] forState:UIControlStateNormal];
    self.navigationItem.rightBarButtonItem = doneButton ;
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

-(IBAction)viewProfileOnLinkedIn:(id)sender {
    [[LISDKDeeplinkHelper sharedInstance] viewOtherProfile:self.linkedinId withState:self.linkedinId showGoToAppStoreDialog:YES success:nil error:nil];
}

-(void)doneFunc {
    [self.navigationController dismissViewControllerAnimated:YES completion:nil];
}

@end
