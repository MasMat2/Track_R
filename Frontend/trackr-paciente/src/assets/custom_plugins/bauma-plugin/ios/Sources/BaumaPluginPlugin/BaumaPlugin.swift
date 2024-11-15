import Foundation
import Capacitor

@objc public class BaumaPlugin: CAPPlugin {
    @objc public func echo(_ value: String) -> String {
        print(value)
        return value
    }
    
    
    @objc public func readBauma() -> String {
        return "systolic, diasistolic"
    }
}
