import Foundation
import Capacitor

/**
 * Please read the Capacitor iOS Plugin Development Guide
 * here: https://capacitorjs.com/docs/plugins/ios
 */
@objc(BaumaPluginPlugin)
public class BaumaPluginPlugin: CAPPlugin, CAPBridgedPlugin {
    public let identifier = "BaumaPluginPlugin"
    public let jsName = "BaumaPlugin"
    public let pluginMethods: [CAPPluginMethod] = [
        CAPPluginMethod(name: "echo", returnType: CAPPluginReturnPromise),
        CAPPluginMethod(name: "readBauma", returnType: CAPPluginReturnPromise)
    ]
    private let implementation = BaumaPlugin()

    @objc func echo(_ call: CAPPluginCall) {
        let value = call.getString("value") ?? ""
        call.resolve([
            "value": implementation.echo(value)
        ])
    }
    
    @objc func readBauma(_ call: CAPPluginCall) {
        let value = call.getString("value") ?? ""
        call.resolve([
            "value": implementation.readBauma()
        ])
    }
}
