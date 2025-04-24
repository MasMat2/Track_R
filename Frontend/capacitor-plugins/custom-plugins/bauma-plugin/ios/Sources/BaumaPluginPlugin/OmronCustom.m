//
//  OmronCustom.m
//  BaumaPlugin
//
//  Created by Adrián Arriaga on 28/10/24.
//

#import <Foundation/Foundation.h>
#import <OmronCustom.h>

#import <Foundation/Foundation.h>
#import <objc/runtime.h>
#import <Capacitor/Capacitor.h>
#import <Capacitor/Capacitor-Swift.h>
#import <Capacitor/CAPBridgedPlugin.h>
#import <Capacitor/CAPBridgedJSTypes.h>
#import "OHQReferenceCode.h"

static void * const KVOContext = (void *)&KVOContext;


@interface OmronCustom () <OHQDeviceManagerDataSource>
// @interface OmronCustom ()

@property (nonatomic, strong) OHQDeviceManager *deviceManager;
@property (nonatomic, strong) NSUUID *deviceIdentifier;
@property (nonatomic, strong) UITextView *logTextView;
- (IBAction)startConnection:(id)sender;

@property (nonatomic, strong) CAPPluginCall *call; // Store the CAPPluginCall
@property (nonatomic, strong) NSMutableArray *readings; // Collect readings
@property (strong, nonatomic) NSMutableDictionary<NSUUID *,NSDictionary<OHQDeviceInfoKey,id> *> *deviceInfoCache;

@end


@implementation OmronCustom

- (void)echo:(CAPPluginCall *)call
{
    [call resolve:@{@"value12": @"systolic"}];
    [self initializeDeviceManager];
}

- (void)logMessage:(NSString *)message {
    // Log to console
    NSLog(@"%@", message);
}

- (void)processAndLogAllRecords {
    // Get the log records from OHQLogStore
    NSArray<NSString *> *logRecords = [[OHQLogStore sharedStore] logRecordsWithLevel:OHQLogLevelVerbose];
    
    // Iterate over each log record and add it to the log view
    for (NSString *log in logRecords) {
        [self logMessage:log];
    }
}
- (void)scanDevices:(CAPPluginCall *)call
{
    // Store the call for later resolution
    self.call = call;

    self.deviceInfoCache = [@{} mutableCopy];

    
    [self initializeDeviceManager];
    
   
//    [self tryScanDevices];
    
}

- (void)observeValueForKeyPath:(NSString *)keyPath ofObject:(id)object change:(NSDictionary *)change context:(void *)context {
    if (context != KVOContext) {
        [super observeValueForKeyPath:keyPath ofObject:object change:change context:context];
        return;
    }
    
    if ([object isEqual:[OHQDeviceManager sharedManager]] && [keyPath isEqualToString:@"state"]) {
        dispatch_async(dispatch_get_main_queue(), ^{
            if ([OHQDeviceManager sharedManager].state == OHQDeviceManagerStatePoweredOn) {
                
                [self tryScanDevices];
            }
        });
    }
}


- (void)getReadings:(CAPPluginCall *)call
{
    // Get the device identifier from the call
    NSString *deviceIdentifierString = [call.options objectForKey:@"deviceId"];
    if (!deviceIdentifierString) {
        [call unavailable:@"Device ID is required."];
        return;
    }
    
    NSUUID *deviceIdentifier = [[NSUUID alloc] initWithUUIDString:deviceIdentifierString];
    if (!deviceIdentifier) {
        [call unavailable:@"Invalid device ID."];
        return;
    }
    
    // Store the call for later resolution
    self.call = call;
    // Initialize the readings array
    self.readings = [NSMutableArray array];
    
    // Initialize the device manager if needed
    if (!self.deviceManager) {
        [self initializeDeviceManager];
    }
    
    // Set self.deviceIdentifier to the provided device ID
    self.deviceIdentifier = deviceIdentifier;
    
    // Start session with the device
    [self startSessionWithDeviceIdentifier:self.deviceIdentifier];
}

- (void)initializeDeviceManager {
    if (!self.deviceManager) {
    
        // Initialize the device manager
        [self logMessage:@"Initialize the device manager"];
        self.deviceManager = [OHQDeviceManager sharedManager];
        self.deviceManager.dataSource = self;
    }
    
    [[OHQDeviceManager sharedManager] addObserver:self forKeyPath:@"state" options:(NSKeyValueObservingOptionInitial | NSKeyValueObservingOptionNew) context:KVOContext];
}

- (void)tryScanDevices { 
    // Start scanning for devices
    [self logMessage:@"Start scanning for devices"];
    [self.deviceManager scanForDevicesWithCategory:OHQDeviceCategoryBloodPressureMonitor
                                     usingObserver:^(NSDictionary<OHQDeviceInfoKey,id> * _Nonnull deviceInfo) {
        
        dispatch_async(dispatch_get_main_queue(), ^{
            [self logMessage:[NSString stringWithFormat:@"Discovered device: %@", deviceInfo]];
            
            // Get the device identifier
            [self logMessage:@"Get the device identifier"];
            self.deviceIdentifier = deviceInfo[OHQDeviceInfoIdentifierKey];
            self.deviceInfoCache[self.deviceIdentifier] = deviceInfo;
            
            
            // Start session with the device
            // [self logMessage:@"Start session with the device"];
            // Print self.deviceIdentifier
            [self logMessage:[NSString stringWithFormat:@"self.deviceIdentifier: %@", self.deviceIdentifier]];
            //[self startSessionWithDeviceIdentifier:self.deviceIdentifier];
        });
        
    } completion:^(OHQCompletionReason reason) {
        dispatch_async(dispatch_get_main_queue(), ^{
            [self logMessage:[NSString stringWithFormat:@"Scan completed with reason: %@", OHQCompletionReasonDescription(reason)]];
            
            switch (reason) {
                case OHQCompletionReasonPoweredOff: {
                    // [[OHQDeviceManager sharedManager] stopScan];
                    // [self tryScanDevices];
                    break;
                }
                case OHQCompletionReasonCanceled: {
                    // Resolve the call with the list of devices
                    if (self.call) {
                        if (self.deviceInfoCache.count > 0) {
                            // [self.call resolve:@{@"devices": self.deviceInfoCache}];
                            NSMutableArray *devices = [@[] mutableCopy];
                            for (NSDictionary *info in self.deviceInfoCache.allValues) {
                                [devices addObject:@{
                                    @"identifier": [info[OHQDeviceInfoIdentifierKey] UUIDString],
                                    @"model": info[OHQDeviceInfoModelNameKey]
                                }];
                            }
                            [self.call resolve:@{@"devices": devices}];
                        } else {
                            [self.call resolve:@{@"devices": @[]}];
                        }
                        self.call = nil;
                    }
                }
                default: {
                    break;
                }
            }
        });
    }];
    
//    [self logMessage:@"Stop scanning"];
//    [self.deviceManager stopScan];
    
    dispatch_after(dispatch_time(DISPATCH_TIME_NOW, (int64_t)(10 * NSEC_PER_SEC)), dispatch_get_main_queue(), ^{

        // Stop scanning
        [self logMessage:@"Stop scanning"];
        [self.deviceManager stopScan];
    });
    
    
//     Schedule to stop scanning after 10 seconds
//    [NSTimer scheduledTimerWithTimeInterval:10.0
//                                     target:self
//                                   selector:@selector(stopScanning)
//                                   userInfo:nil
//                                    repeats:NO];
}

- (void)stopScanning {
    [self logMessage:@"Stop scanning"];
    [self.deviceManager stopScan];
}

- (void)startSessionWithDeviceIdentifier:(NSUUID *)identifier {
    [self.deviceManager startSessionWithDevice:identifier
                              usingDataObserver:^(OHQDataType dataType, id  _Nonnull data) {
        // Handle received data
        [self logMessage:@"Handle received data"];
        [self logMessage:[NSString stringWithFormat:@"Data received: %@ - %@", OHQDataTypeDescription(dataType), data]];
        
        if (dataType == OHQDataTypeMeasurementRecords) {
            // Process measurement records
            NSArray *records = (NSArray *)data;
            [self logMessage:[NSString stringWithFormat:@"Measurement records: %@", records]];
            
            // Collect systolic and diastolic values
            for (NSDictionary *record in records) {
                NSNumber *systolic = record[OHQMeasurementRecordSystolicKey];
                NSNumber *diastolic = record[OHQMeasurementRecordDiastolicKey];
                NSNumber *pulseRate = record[OHQMeasurementRecordPulseRateKey];
                [self logMessage:[NSString stringWithFormat:@"Systolic: %@, Diastolic: %@, Pulse Rate: %@", systolic, diastolic, pulseRate]];
                
                NSMutableDictionary *reading = [NSMutableDictionary dictionary];
                if (systolic) {
                    reading[@"systolic"] = systolic;
                }
                if (diastolic) {
                    reading[@"diastolic"] = diastolic;
                }
                if (pulseRate) {
                    reading[@"pulseRate"] = pulseRate;
                }
                [self.readings addObject:reading];
            }
        }
        
    } connectionObserver:^(OHQConnectionState state) {
        // Handle connection state changes
        [self logMessage:[NSString stringWithFormat:@"Connection state changed: %@", OHQConnectionStateDescription(state)]];
        
    } completion:^(OHQCompletionReason reason) {
        // Session completed
        [self logMessage:[NSString stringWithFormat:@"Session completed with reason: %@", OHQCompletionReasonDescription(reason)]];
        
//        [self processAndLogAllRecords];
        
        // Resolve the call with the collected readings
        if (self.call) {
            if (self.readings.count > 0) {
                [self.call resolve:@{@"readings": self.readings}];
            } else {
                [self.call resolve:@{@"readings": @[]}];
            }
            self.call = nil;
        }
    } options:@{
        // Specify session options as needed
        OHQSessionOptionReadMeasurementRecordsKey: @YES,
    }];
}
@end
