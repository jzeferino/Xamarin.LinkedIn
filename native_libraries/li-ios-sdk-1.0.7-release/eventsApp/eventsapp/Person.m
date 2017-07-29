//
//  Person.m
//  ICViewPager
//
//  Created by Rahul Bansal on 4/23/15.
//  Copyright (c) 2015 Ilter Cengiz. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "Person.h"

@implementation Person

+(instancetype) initWithFirstName:(NSString *)firstName lastName:(NSString *)lastName linkedinId:(NSString *)linkedinId headline:(NSString *)headline {
    return [[Person alloc] initWithFirstName:firstName lastName:lastName linkedinId:linkedinId headline:headline];
}

-(id) initWithFirstName:(NSString *)firstName lastName:(NSString *)lastName linkedinId:(NSString *)linkedinId headline:(NSString *)headline {
    self = [super init];
    if (self) {
        self.firstName = firstName;
        self.lastName = lastName;
        self.linkedinId = linkedinId;
        self.headline = headline;
    }
    return self;
}

- (void)encodeWithCoder:(NSCoder *)coder {
    [coder encodeObject:_firstName forKey:@"firstName"];
    [coder encodeObject:_lastName forKey:@"lastName"];
    [coder encodeObject:_linkedinId forKey:@"linkedinId"];
    [coder encodeObject:_headline forKey:@"headline"];
}

- (id)initWithCoder:(NSCoder *)coder {
    self = [super init];
    if (self) {
        self.firstName = [coder decodeObjectForKey:@"firstName"];
        self.lastName = [coder decodeObjectForKey:@"lastName"];
        self.linkedinId = [coder decodeObjectForKey:@"linkedinId"];
        self.headline = [coder decodeObjectForKey:@"headline"];
    }
    return self;
}

@end