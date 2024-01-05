import { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'trackr.app',
  appName: 'trackr-paciente',
  webDir: 'www',
  bundledWebRuntime: false,
  plugins: {
    CapacitorHttp: {
      enabled: true,
    },
  },
};

export default config;
