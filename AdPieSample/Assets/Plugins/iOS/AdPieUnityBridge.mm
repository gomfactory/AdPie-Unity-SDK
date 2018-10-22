//
//  AdPieUnityBridge.mm
//
//  Created by KimYongSun on 2018. 8. 17..
//  Copyright © 2018년 GomFactory. All rights reserved.
//

#import <AdPieSDK/AdPieSDK.h>
#import "UnityAppController.h"

#define kPOSITION_TOP 0
#define kPOSITION_BOTTOM 1
#define kPOSITION_TOP_LEFT 2
#define kPOSITION_TOP_RIGHT 3
#define kPOSITION_BOTTOM_LEFT 4
#define kPOSITION_BOTTOM_RIGHT 5
#define kPOSITION_CENTER 6

@interface AdPieUnityController : NSObject <APAdViewDelegate, APInterstitialDelegate>

@property (nonatomic) int position;
@property (nonatomic) APAdView *adView;
@property (nonatomic) APInterstitial *interstitial;

@end

@implementation AdPieUnityController

+ (AdPieUnityController *)sharedInstance {
    static AdPieUnityController * instance = nil;
    static dispatch_once_t onceToken;
    dispatch_once(&onceToken, ^{ instance = [[AdPieUnityController alloc] init]; });
    return instance;
}

- (void)adViewDidLoadAd:(APAdView *)view {
    UnitySendMessage("AdViewEventHandler", "AdLoaded", "");
}

- (void)adViewDidFailToLoadAd:(APAdView *)view withError:(NSError *)error {
    NSString *errorCodeString = [NSString stringWithFormat:@"%d", [error code]];
    const char *errorCodeChar = [errorCodeString UTF8String];
    UnitySendMessage("AdViewEventHandler", "AdFailedToLoad", errorCodeChar);
}

- (void)adViewWillLeaveApplication:(APAdView *)view {
    UnitySendMessage("AdViewEventHandler", "AdClicked", "");
}

- (void)interstitialDidLoadAd:(APInterstitial *)interstitial {
    UnitySendMessage("InterstitialEventHandler", "AdLoaded", "");
}

- (void)interstitialDidFailToLoadAd:(APInterstitial *)interstitial withError:(NSError *)error {
    NSString *errorCodeString = [NSString stringWithFormat:@"%d", [error code]];
    const char *errorCodeChar = [errorCodeString UTF8String];
    UnitySendMessage("InterstitialEventHandler", "AdFailedToLoad", errorCodeChar);
}

- (void)interstitialWillPresentScreen:(APInterstitial *)interstitial {
    UnitySendMessage("InterstitialEventHandler", "AdShown", "");
}

- (void)interstitialWillDismissScreen:(APInterstitial *)interstitial {
}

- (void)interstitialDidDismissScreen:(APInterstitial *)interstitial {
    UnitySendMessage("InterstitialEventHandler", "AdDismissed", "");
}

- (void)interstitialWillLeaveApplication:(APInterstitial *)interstitial {
    UnitySendMessage("InterstitialEventHandler", "AdClicked", "");
}

@end

#if __cplusplus
extern "C" {
#endif
    void adpie_sdk_init(char* mid);
    void adpie_sdk_createBanner(char* sid, int position);
    void adpie_sdk_createBannerWithSize(char* sid, int position, int width, int height);
    void adpie_sdk_loadBanner();
    void adpie_sdk_destroyBanner();
    void adpie_sdk_updateBanner();
    void adpie_sdk_createInterstitial(char* sid);
    void adpie_sdk_loadInterstitial();
    bool adpie_sdk_isLoadedInterstitial();
    void adpie_sdk_showInterstitial();
    
    CGFloat getScreenWidth();
    CGFloat getScreenHeight();
    
#if __cplusplus
}
#endif

extern "C" {
    void adpie_sdk_debug_enabled() {
        [[AdPieSDK sharedInstance] logging];
    }
    
    void adpie_sdk_init(char* mid) {
        [[AdPieSDK sharedInstance] initWithMediaId:[NSString stringWithUTF8String:mid]];
    }
    
    void adpie_sdk_createBanner(char* sid, int position) {
        adpie_sdk_createBannerWithSize(sid, position, 320, 50);
    }
    
    void adpie_sdk_createBannerWithSize(char* sid, int position, int width, int height) {
        adpie_sdk_destroyBanner();
        
        [AdPieUnityController sharedInstance].adView = [[APAdView alloc] init];
        [AdPieUnityController sharedInstance].adView.slotId = [NSString stringWithUTF8String:sid];
        [AdPieUnityController sharedInstance].adView.frame = CGRectMake(0, 0, width, height);
        
        [AdPieUnityController sharedInstance].position = position;
        adpie_sdk_updateBanner();
        
        UnityAppController* unityAppController = GetAppController();
        UIView* view = unityAppController.rootViewController.view;
        [view addSubview:[AdPieUnityController sharedInstance].adView];
    }
    
    void adpie_sdk_loadBanner() {
        UnityAppController* unityAppController = GetAppController();
        [AdPieUnityController sharedInstance].adView.rootViewController = unityAppController.rootViewController;
        [AdPieUnityController sharedInstance].adView.delegate = [AdPieUnityController sharedInstance];
        [[AdPieUnityController sharedInstance].adView load];
    }
    
    void adpie_sdk_destroyBanner() {
        if([AdPieUnityController sharedInstance].adView) {
            [[AdPieUnityController sharedInstance].adView removeFromSuperview];
            [AdPieUnityController sharedInstance].adView = nil;
        }
    }
    
    void adpie_sdk_updateBanner() {
        if([AdPieUnityController sharedInstance].adView){
            CGRect frame = [AdPieUnityController sharedInstance].adView.frame;
            
            CGSize bannerSize = CGSizeMake(frame.size.width, frame.size.height);
            CGFloat originX = 0;
            CGFloat originY = 0;
            
            switch ([AdPieUnityController sharedInstance].position) {
                case kPOSITION_TOP:
                    originX = (getScreenWidth() - bannerSize.width) / 2;
                    originY = 0;
                    break;
                case kPOSITION_BOTTOM:
                    originX = (getScreenWidth() - bannerSize.width) / 2;
                    originY = getScreenHeight() - bannerSize.height;
                    break;
                case kPOSITION_TOP_LEFT:
                    originX = 0;
                    originY = 0;
                    break;
                case kPOSITION_TOP_RIGHT:
                    originX = getScreenWidth() - bannerSize.width;
                    originY = 0;
                    break;
                case kPOSITION_BOTTOM_LEFT:
                    originX = 0;
                    originY = getScreenHeight() - bannerSize.height;
                    break;
                case kPOSITION_BOTTOM_RIGHT:
                    originX = getScreenWidth() - bannerSize.width;
                    originY = getScreenHeight() - bannerSize.height;
                    break;
                case kPOSITION_CENTER:
                    originX = (getScreenWidth() - bannerSize.width) / 2;
                    originY = (getScreenHeight() - bannerSize.height) / 2;
                    break;
            }
            
            [AdPieUnityController sharedInstance].adView.frame =
            CGRectMake(originX, originY, bannerSize.width, bannerSize.height);
        }
    }
    
    void adpie_sdk_createInterstitial(char* sid) {
        [AdPieUnityController sharedInstance].interstitial = [[APInterstitial alloc] initWithSlotId:[NSString stringWithUTF8String:sid]];
    }
    
    void adpie_sdk_loadInterstitial() {
        [AdPieUnityController sharedInstance].interstitial.delegate = [AdPieUnityController sharedInstance];
        [[AdPieUnityController sharedInstance].interstitial load];
    }
    
    bool adpie_sdk_isLoadedInterstitial() {
        return [[AdPieUnityController sharedInstance].interstitial isReady];
    }
    
    void adpie_sdk_showInterstitial() {
        UnityAppController* unityAppController = GetAppController();
        [[AdPieUnityController sharedInstance].interstitial presentFromRootViewController:unityAppController.rootViewController];
    }
    
    
    CGFloat getScreenWidth()
    {
        CGRect screenBounds = [[UIScreen mainScreen] applicationFrame];
        
        UIInterfaceOrientation orientation = [[UIApplication sharedApplication] statusBarOrientation];
        
        CGFloat width = screenBounds.size.width;
        
        if ((UIInterfaceOrientationIsLandscape(orientation) && screenBounds.size.height > screenBounds.size.width)
            || (UIInterfaceOrientationIsPortrait(orientation) && screenBounds.size.width > screenBounds.size.height))
        {
            width = screenBounds.size.height;
        }
        
        return width;
    }
    
    CGFloat getScreenHeight()
    {
        CGRect screenBounds = [[UIScreen mainScreen] applicationFrame];
        
        UIInterfaceOrientation orientation = [[UIApplication sharedApplication] statusBarOrientation];
        
        CGFloat height = screenBounds.size.height;
        
        if ((UIInterfaceOrientationIsLandscape(orientation) && screenBounds.size.height > screenBounds.size.width)
            || (UIInterfaceOrientationIsPortrait(orientation) && screenBounds.size.width > screenBounds.size.height))
        {
            height = screenBounds.size.width;
        }
        
        return height;
    }
    
}

