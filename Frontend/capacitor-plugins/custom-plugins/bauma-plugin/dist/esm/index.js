import { registerPlugin } from '@capacitor/core';
const OmronCustom = registerPlugin('OmronCustom', {
    web: () => import('./web').then((m) => new m.OmronCustomWeb()),
});
export * from './definitions';
export { OmronCustom };
//# sourceMappingURL=index.js.map