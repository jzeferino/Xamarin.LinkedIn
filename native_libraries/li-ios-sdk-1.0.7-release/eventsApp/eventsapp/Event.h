//
//  Event.h
//  ICViewPager
//
//  Created by Rahul Bansal on 4/23/15.
//  Copyright (c) 2015 Ilter Cengiz. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Event : NSObject <NSCoding>
@property BOOL isAttending;
@property (nonatomic, readonly) NSString *eventName;
@property (nonatomic, readonly) NSString *eventLocation;
@property (nonatomic, readonly) NSString *eventImageResource;
@property (nonatomic, readonly) NSDate *eventDate;
@property (nonatomic, readonly) NSArray *eventAttendees;

+(instancetype) initWithName:(NSString *)name location:(NSString *)location date:(NSDate *)date image:(NSString *)image attendees:(NSArray *)attendees;

@end