import { registerPlugin } from '@capacitor/core';

import type { OmronCustomPlugin } from './definitions';

const OmronCustom = registerPlugin<OmronCustomPlugin>('OmronCustom', {
  web: () => import('./web').then((m) => new m.OmronCustomWeb()),
});

export * from './definitions';
export { OmronCustom };
