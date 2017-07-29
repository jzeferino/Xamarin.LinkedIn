//
//  AttendeeCell.m
//  eventsapp
//
//  Created by Rahul Bansal on 4/28/15.
//  Copyright (c) 2015 LinkedIn Corporation. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "AttendeeCell.h"

@interface AttendeeCell ()
@property (nonatomic, readwrite) NSString *linkedinId;
@property (nonatomic, readwrite) NSString *personName;
@property (nonatomic, readwrite) NSString *personHeadline;
@end

@implementation AttendeeCell

- (instancetype)initWithStyle:(UITableViewCellStyle)style reuseIdentifier:(NSString *)reuseIdentifier linkedinId:(NSString *)linkedinId name:(NSString *)name headline:(NSString *)headline image:(UIImage *)image {
    self = [self initWithStyle:style reuseIdentifier:reuseIdentifier];
    self.linkedinId = linkedinId;
    self.personName = name;
    self.personHeadline = headline;
    self.personImage = image;
    return self;
}

@end