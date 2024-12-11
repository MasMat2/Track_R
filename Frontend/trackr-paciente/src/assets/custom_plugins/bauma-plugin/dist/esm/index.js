import { registerPlugin } from '@capacitor/core';
const BaumaPlugin = registerPlugin('BaumaPlugin', {
    web: () => import('./web').then((m) => new m.BaumaPluginWeb()),
});
const OmronCustom = registerPlugin('OmronCustom', {
    web: () => import('./web').then((m) => new m.OmronCustomWeb()),
});
export * from './definitions';
export { BaumaPlugin };
export { OmronCustom };
//# sourceMappingURL=index.js.map