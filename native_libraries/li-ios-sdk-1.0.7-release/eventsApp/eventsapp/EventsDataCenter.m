//
//  EventsDataCenter.m
//  ICViewPager
//
//  Created by Rahul Bansal on 4/23/15.
//  Copyright (c) 2015 Ilter Cengiz. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "EventsDataCenter.h"
#import "Event.h"
#import "Person.h"

@interface EventsDataCenter ()
@property (nonatomic, strong) NSArray *events;
@end

@implementation EventsDataCenter

+(instancetype) sharedInstance {
    static EventsDataCenter *instance;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{
        instance = [[EventsDataCenter alloc] initWithEvents];
    });
    return instance;
}

-(id)initWithEvents {
    self = [super init];
    if (self) {
        NSArray *eventNames = [[NSArray alloc] initWithObjects:@"CONFERENCE 1", @"CONFERENCE 2", @"CONFERENCE 3", @"CONFERENCE 4", nil];
        NSArray *eventLocations = [[NSArray alloc] initWithObjects:@"Davos, Switzerland", @"Bogota, Colombia", @"San Francisco, CA", @"Las Vegas, NV", nil];
        NSString *eventImageName = @"conference";

        //event dates
        NSMutableArray *eventDates = [[NSMutableArray alloc] init];
        NSDate *today = [NSDate date];
        [eventDates addObject:today];
        for (int i = 1; i <= 3 ; i++) {
            NSDate *futureDate = [[NSCalendar currentCalendar] dateByAddingUnit:NSCalendarUnitDay
                                                                          value:i
                                                                         toDate:today
                                                                        options:0];
            [eventDates addObject:futureDate];
        }

        //persons
        Person *ann = [Person initWithFirstName:@"Ann" lastName:@"Lapin" linkedinId:@"PHeQr0r91D" headline:@"Engineering Manager at Company X"];
        Person *john = [Person initWithFirstName:@"John" lastName:@"Smith" linkedinId:@"BfwQMHuidm" headline:@"Senior Product Manager at Company Y"];
        Person *jane = [Person initWithFirstName:@"Jane" lastName:@"Chang" linkedinId:@"kpc_glrab4" headline:@"Software Engineer at Company Z"];
        Person *norman = [Person initWithFirstName:@"Norman" lastName:@"Miller" linkedinId:@"ueZCXc4nYx" headline:@"Web Developer at Company M"];
        NSMutableArray *persons = [[NSMutableArray alloc] initWithObjects:ann,john,jane,norman,nil];

        for (int i = 1; i <= 3 ; i++) {
            NSDate *futureDate = [[NSCalendar currentCalendar] dateByAddingUnit:NSCalendarUnitDay
                                                                          value:i
                                                                         toDate:today
                                                                        options:0];
            [eventDates addObject:futureDate];
        }
        
        NSMutableArray *events = [[NSMutableArray alloc] init];
        for (int i = 0; i < 4; i++) {
            NSString *eventName = [eventNames objectAtIndex:i];
            NSString *eventLocation = [eventLocations objectAtIndex:i];
            NSDate *eventDate = [eventDates objectAtIndex:i];
            
            NSMutableArray *attendees = [[NSMutableArray alloc] init];
            for (int j = 0; j <= i; j++) {
                [attendees addObject:[persons objectAtIndex:j]];
            }
            
            Event *event = [Event initWithName:eventName location:eventLocation date:eventDate image:eventImageName attendees:attendees];
            [events addObject:event];
        }
        self.events = events;
    }
    return self;
}

-(NSArray *)events {
    return _events;
}

-(Event *)eventAtIndex:(NSUInteger)index {
    return [_events objectAtIndex:index];
}

@end
