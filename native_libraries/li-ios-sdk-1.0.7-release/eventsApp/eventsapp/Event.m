//
//  Event.m
//  ICViewPager
//
//  Created by Rahul Bansal on 4/23/15.
//  Copyright (c) 2015 Ilter Cengiz. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Event.h"

@interface Event ()
@property (nonatomic, readwrite) NSString *eventName;
@property (nonatomic, readwrite) NSString *eventLocation;
@property (nonatomic, readwrite) NSString *eventImageResource;
@property (nonatomic, readwrite) NSDate *eventDate;
@property (nonatomic, readwrite) NSArray *eventAttendees;
@end

@implementation Event

+(instancetype) initWithName:(NSString *)name location:(NSString *)location date:(NSDate *)date image:(NSString *)image attendees:(NSArray *)attendees {
    return [[Event alloc] initWithName:name location:location date:date image:image attendees:attendees];
}

-(id) initWithName:(NSString *)name location:(NSString *)location date:(NSDate *)date image:(NSString *)image attendees:attendees {
    self = [super init];
    if (self) {
        self.eventName = name;
        self.eventLocation = location;
        self.eventDate = date;
        self.eventImageResource = image;
        self.eventAttendees = attendees;
    }
    return self;
}

- (void)encodeWithCoder:(NSCoder *)coder {
    [coder encodeObject:_eventName forKey:@"eventName"];
    [coder encodeObject:_eventLocation forKey:@"eventLocation"];
    [coder encodeObject:_eventDate forKey:@"eventDate"];
    [coder encodeObject:_eventImageResource forKey:@"eventImageResource"];
    [coder encodeObject:_eventAttendees forKey:@"eventAttendees"];
    [coder encodeBool:_isAttending forKey:@"isAttending"];
}

- (id)initWithCoder:(NSCoder *)coder {
    self = [super init];
    if (self) {
        self.eventName = [coder decodeObjectForKey:@"eventName"];
        self.eventLocation = [coder decodeObjectForKey:@"eventLocation"];
        self.eventDate = [coder decodeObjectForKey:@"eventDate"];
        self.eventImageResource = [coder decodeObjectForKey:@"eventImageResource"];
        self.eventAttendees = [coder decodeObjectForKey:@"eventAttendees"];
        self.isAttending = [coder decodeBoolForKey:@"isAttending"];
    }
    return self;
}

@end