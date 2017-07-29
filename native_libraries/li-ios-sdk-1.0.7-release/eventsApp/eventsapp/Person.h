//
//  Person.h
//  ICViewPager
//
//  Created by Rahul Bansal on 4/23/15.
//  Copyright (c) 2015 Ilter Cengiz. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface Person : NSObject <NSCoding>
@property (nonatomic, copy) NSString *linkedinId;
@property (nonatomic, copy) NSString *firstName;
@property (nonatomic, copy) NSString *lastName;
@property (nonatomic, copy) NSString *headline;

+(instancetype) initWithFirstName:(NSString *)firstName lastName:(NSString *)lastName linkedinId:(NSString *)linkedinId headline:(NSString *)headline;

@end
