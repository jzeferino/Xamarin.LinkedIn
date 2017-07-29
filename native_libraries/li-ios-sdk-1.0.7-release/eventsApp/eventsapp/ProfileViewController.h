//
//  ProfileViewController.h
//  eventsapp
//
//  Created by Rahul Bansal on 4/28/15.
//  Copyright (c) 2015 LinkedIn Corporation. All rights reserved.
//

#import <UIKit/UIKit.h>

@interface ProfileViewController : UIViewController
@property (nonatomic, copy) NSString *linkedinId;
@property (nonatomic, copy) NSString *name;
@property (nonatomic, copy) NSString *headline;
@property (nonatomic, copy) NSString *imageUrl;
@property (weak, nonatomic) IBOutlet UILabel *nameLabel;
@property (weak, nonatomic) IBOutlet UILabel *headlineLabel;
@property (weak, nonatomic) IBOutlet UIImageView *imageView;
@end
