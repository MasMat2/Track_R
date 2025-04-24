//
//  OmronCustom.h
//  BaumaPlugin
//
//  Created by Adrián Arriaga on 28/10/24.
//

// #ifndef OmronCustom_h
// #define OmronCustom_h


// #endif /* OmronCustom_h */
// #import <UIKit/UIKit.h>
// #import <Capacitor/CAPPlugin.h>
// #import <Capacitor/CAPBridgedPlugin.h>
#import <UIKit/UIKit.h>
#import <Capacitor/CAPPlugin.h>
#import <Capacitor/CAPBridgedPlugin.h>

@class CAPPluginCall;

@interface OmronCustom : CAPPlugin <CAPBridgedPlugin>

- (void)echo:(CAPPluginCall *)call;
@end

