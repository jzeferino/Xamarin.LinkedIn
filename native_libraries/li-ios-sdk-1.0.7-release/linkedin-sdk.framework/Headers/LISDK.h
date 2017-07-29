//
//  LISDK.h
//
//  Copyright (c) 2015 linkedin. All rights reserved.
//

#ifndef LISDK_h
#define LISDK_h

#import <Foundation/Foundation.h>

/**
 file for application to include to interact with LinkedIn SDK for ios.
 
 A typical usage flow might be:
 
 1. use LISDKSessionManager to initialize a linkedin session if it is not already valid.  
 This will ask the user to authorize the application to use his/her linkedin data.
  
 if (! [LISDKSessionManager hasValidSession] ) {
   [LISDKSessionManager createSessionWithAuthorize:[NSArray arrayWithObjects:LISDK_BASIC_PROFILE_PERMISSION, LISDK_EMAILADDRESS_PERMISSION, nil]
                                             state:@"some state"
                            showGoToAppStoreDialog:NO
                                      successBlock:^(NSString *returnState) {
                                      }
                                        errorBlock:^(NSError *error) {
                                        }];
 }
 
 2. use LISDKAPIHelper or LISDKDeeplinkHelper to make API calls or to perform deep link operations
 
 */

#include "LISDKSessionManager.h"
#include "LISDKSession.h"
#include "LISDKAccessToken.h"
#include "LISDKAPIError.h"
#include "LISDKAPIHelper.h"
#include "LISDKAPIResponse.h"
#include "LISDKCallbackHandler.h"
#include "LISDKDeeplinkHelper.h"
#include "LISDKErrorCode.h"
#include "LISDKPermission.h"

#endif
