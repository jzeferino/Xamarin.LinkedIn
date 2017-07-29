//
//  EventsDataCenter.h
//  ICViewPager
//
//  Created by Rahul Bansal on 4/23/15.
//  Copyright (c) 2015 Ilter Cengiz. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Event.h"

@interface EventsDataCenter : NSObject

+(instancetype)sharedInstance;
-(NSArray *)events;
-(Event *)eventAtIndex:(NSUInteger)index;

@end
