//
//  APIResponseViewController.m
//  LISDKSampleApp
//
//  Created by Fred Cheng on 3/6/15.
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#import "APIResponseViewController.h"

@interface APIResponseViewController ()

@end

@implementation APIResponseViewController

- (void)viewDidLoad {
    [super viewDidLoad];
    self.navigationItem.title = @"API Response";
    // Do any additional setup after loading the view from its nib.
    self.responseTextView.layer.borderWidth = 1.0f;
    self.responseTextView.layer.borderColor = [[UIColor grayColor] CGColor];
    self.requestMethodLabel.text = self.method;
    self.requestUrlLabel.text = self.url;
    self.responseStatusLabel.text = self.status;
    self.responseTextView.text = self.response;
}

- (void)didReceiveMemoryWarning {
    [super didReceiveMemoryWarning];
    // Dispose of any resources that can be recreated.
}

- (void)touchesBegan:(NSSet *)touches withEvent:(UIEvent *)event {
    
    UITouch *touch = [[event allTouches] anyObject];
    if ([_responseTextView isFirstResponder] && [touch view] != _responseTextView) {
        [_responseTextView resignFirstResponder];
    }
    [super touchesBegan:touches withEvent:event];
}


@end
