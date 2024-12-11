import { registerPlugin } from '@capacitor/core';

import type { BaumaPluginPlugin, OmronCustomPlugin } from './definitions';

const BaumaPlugin = registerPlugin<BaumaPluginPlugin>('BaumaPlugin', {
  web: () => import('./web').then((m) => new m.BaumaPluginWeb()),
});

const OmronCustom = registerPlugin<OmronCustomPlugin>('OmronCustom', {
  web: () => import('./web').then((m) => new m.OmronCustomWeb()),
});

export * from './definitions';
export { BaumaPlugin };
export { OmronCustom };
