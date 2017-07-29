//
//  APIResponseViewController.h
//  LISDKSampleApp
//
//  Created by Fred Cheng on 3/6/15.
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface APIResponseViewController : UIViewController

@property (nonatomic,weak) IBOutlet UILabel *requestMethodLabel;
@property (nonatomic,weak) IBOutlet UILabel *requestUrlLabel;
@property (nonatomic,weak) IBOutlet UILabel *responseStatusLabel;
@property (nonatomic,weak) IBOutlet UITextView *responseTextView;

@property (nonatomic,strong) NSString *method;
@property (nonatomic,strong) NSString *url;
@property (nonatomic,strong) NSString *response;
@property (nonatomic,strong) NSString *status;
@end
