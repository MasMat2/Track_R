// swift-tools-version: 5.9
import PackageDescription

let package = Package(
    name: "BaumaPlugin",
    platforms: [.iOS(.v14)],
    products: [
        .library(
            name: "BaumaPlugin",
            targets: ["OmronCustomPlugin"])
    ],
    dependencies: [
        .package(url: "https://github.com/ionic-team/capacitor-swift-pm.git", from: "7.0.0")
    ],
    targets: [
        .target(
            name: "OmronCustomPlugin",
            dependencies: [
                .product(name: "Capacitor", package: "capacitor-swift-pm"),
                .product(name: "Cordova", package: "capacitor-swift-pm")
            ],
            path: "ios/Sources/OmronCustomPlugin"),
        .testTarget(
            name: "OmronCustomPluginTests",
            dependencies: ["OmronCustomPlugin"],
            path: "ios/Tests/OmronCustomPluginTests")
    ]
)