# Steps

1. cd `li-ios-sdk-1.0.7-release/linkedin-sdk.framework/Headers/`
2. replace the contents of "LISDK.h" with

```
#ifndef LISDK_h
#define LISDK_h

#import <Foundation/Foundation.h>

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
```

3. see the available sdks `sharpie xcode -sdks`
4. `sharpie -tlm-do-not-submit bind -sdk iphoneos10.2 -scope . *.h`
5. place the `ApiDefenitions.cs` and `StrucstAndEnums.cs` inside the ios bindings project. changing the names to `ApiDefenition.cs` and `Structs.cs` 
6. place the `linkedin-sdk` into the ios binding project changing the extension to `.a`
7. right-clicking the project and choosing Add > Add Files to select the native library
8. Remove all the `LISDK` from the nametypes using `[BaseType(typeof(NSObject), Name = "LISDKSessionManager")]`