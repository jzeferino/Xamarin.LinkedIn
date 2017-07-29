//
//  AttendeeCell.h
//  eventsapp
//
//  Created by Rahul Bansal on 4/28/15.
//  Copyright (c) 2015 LinkedIn Corporation. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>

@interface AttendeeCell : UITableViewCell
@property (nonatomic, readonly) NSString *linkedinId;
@property (nonatomic, readonly) NSString *personName;
@property (nonatomic, readonly) NSString *personHeadline;
@property (nonatomic) UIImage *personImage;

- (instancetype)initWithStyle:(UITableViewCellStyle)style reuseIdentifier:(NSString *)reuseIdentifier linkedinId:(NSString *)linkedinId name:(NSString *)name headline:(NSString *)headline image:(UIImage *)image;

@end

