// swift-tools-version: 5.9
import PackageDescription

let package = Package(
    name: "BaumaPlugin",
    platforms: [.iOS(.v13)],
    products: [
        .library(
            name: "BaumaPlugin",
            targets: ["BaumaPluginPlugin"])
    ],
    dependencies: [
        .package(url: "https://github.com/ionic-team/capacitor-swift-pm.git", branch: "main")
    ],
    targets: [
        .target(
            name: "BaumaPluginPlugin",
            dependencies: [
                .product(name: "Capacitor", package: "capacitor-swift-pm"),
                .product(name: "Cordova", package: "capacitor-swift-pm")
            ],
            path: "ios/Sources/BaumaPluginPlugin"),
        .testTarget(
            name: "BaumaPluginPluginTests",
            dependencies: ["BaumaPluginPlugin"],
            path: "ios/Tests/BaumaPluginPluginTests")
    ]
)