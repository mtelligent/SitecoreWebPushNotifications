var path = require("path");

module.exports = {
    entry: "./src/send-push-notification.plugin.ts",
    module: {
        rules: [
            {
                test: /\.ts$/,
                use: "ts-loader",
                exclude: path.resolve(__dirname, "node_modules")
            }
        ]
    },
    resolve: {
        extensions: [".ts", ".js"]
    },
    output: {
        path: path.resolve(__dirname, "dist"),
        filename: "sendPushNotification.plugin.js",
        library: "sendPushNotificationPlugin",
        libraryTarget: "umd"
    },
    externals: [
        "@sitecore/ma-core",
        "@angular/core",
        "@ngx-translate/core"
    ]
};