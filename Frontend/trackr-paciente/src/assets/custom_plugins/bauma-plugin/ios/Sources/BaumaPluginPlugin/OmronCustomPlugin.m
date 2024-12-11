//
//  OmronCustomPlugin.m
//  BaumaPlugin
//
//  Created by Adrián Arriaga on 28/10/24.
//

#import <Foundation/Foundation.h>
#import <Capacitor/Capacitor.h>

// Define the plugin using the CAP_PLUGIN Macro, and
// each method the plugin supports using the CAP_PLUGIN_METHOD macro.
CAP_PLUGIN(OmronCustom, "OmronCustom",
           CAP_PLUGIN_METHOD(scanDevices, CAPPluginReturnPromise);
           CAP_PLUGIN_METHOD(getReadings, CAPPluginReturnPromise);
)
