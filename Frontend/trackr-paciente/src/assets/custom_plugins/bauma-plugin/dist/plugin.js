var capacitorBaumaPlugin = (function (exports, core) {
    'use strict';

    const BaumaPlugin = core.registerPlugin('BaumaPlugin', {
        web: () => Promise.resolve().then(function () { return web; }).then((m) => new m.BaumaPluginWeb()),
    });
    const OmronCustom = core.registerPlugin('OmronCustom', {
        web: () => Promise.resolve().then(function () { return web; }).then((m) => new m.OmronCustomWeb()),
    });

    class BaumaPluginWeb extends core.WebPlugin {
        async echo(options) {
            console.log('ECHO', options);
            return options;
        }
        async readBauma(options) {
            console.log('READBAUMA', options);
            return options;
        }
    }
    class OmronCustomWeb extends core.WebPlugin {
        /**
         * Simulates scanning for devices by returning a list of dummy devices.
         */
        async scanDevices() {
            console.log('scanDevices called on web');
            // Return dummy data
            const devices = [
                { model: 'Dummy Device 1', identifier: 'dummy-device-1' },
                { model: 'Dummy Device 2', identifier: 'dummy-device-2' },
            ];
            return { devices };
        }
        /**
         * Simulates retrieving readings from a device by returning dummy readings.
         * @param options Contains the deviceId of the device to get readings from.
         */
        async getReadings(options) {
            console.log('getReadings called on web with deviceId:', options.deviceId);
            // Return dummy readings based on the provided deviceId
            const readings = [
                {
                    systolic: Math.floor(Math.random() * 20) + 110,
                    diastolic: Math.floor(Math.random() * 20) + 70,
                    pulseRate: Math.floor(Math.random() * 40) + 60,
                },
                {
                    systolic: Math.floor(Math.random() * 20) + 110,
                    diastolic: Math.floor(Math.random() * 20) + 70,
                    pulseRate: Math.floor(Math.random() * 40) + 60,
                },
            ];
            return { readings };
        }
        /**
         * Example echo method that returns a fixed string.
         */
        async echo() {
            console.log('echo called on web');
            return { value: 'This is a dummy echo response from the web implementation.' };
        }
    }

    var web = /*#__PURE__*/Object.freeze({
        __proto__: null,
        BaumaPluginWeb: BaumaPluginWeb,
        OmronCustomWeb: OmronCustomWeb
    });

    exports.BaumaPlugin = BaumaPlugin;
    exports.OmronCustom = OmronCustom;

    return exports;

})({}, capacitorExports);
//# sourceMappingURL=plugin.js.map
