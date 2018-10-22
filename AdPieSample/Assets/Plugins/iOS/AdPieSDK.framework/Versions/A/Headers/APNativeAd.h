//
//  APNativeAd.h
//  AdPieSDK
//
//  Created by sunny on 2016. 8. 30..
//  Copyright © 2016년 GomFactory. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "APNativeAdData.h"
#import "APNativeAdView.h"

@protocol APNativeDelegate;

@interface APNativeAd : NSObject

@property(weak, nonatomic) id<APNativeDelegate> delegate;

@property(copy, nonatomic) NSString *slotId;

@property(nonatomic, readonly) APNativeAdData *nativeAdData;

- (id)initWithSlotId:(NSString *)slotId;

- (void)load;

- (void)registerViewForInteraction:(APNativeAdView *)nativeAdView;

@end

@protocol APNativeDelegate <NSObject>

@required

// 네이티브 성공
- (void)nativeDidLoadAd:(APNativeAd *)nativeAd;

// 네이티브 실패
- (void)nativeDidFailToLoadAd:(APNativeAd *)nativeAd withError:(NSError *)error;

@optional

// 네이티브 클릭 알림
- (void)nativeWillLeaveApplication:(APNativeAd *)nativeAd;

@end
