import { ScreenOrientation, OrientationType } from '@capawesome/capacitor-screen-orientation';

export const lockLandscape = async () => {
    await ScreenOrientation.lock({ type: OrientationType.LANDSCAPE });
};

export const lockPortrait = async () => {
    await ScreenOrientation.lock({ type: OrientationType.PORTRAIT });
};

export const unlock = async () => {
    await ScreenOrientation.unlock();
};

export const getCurrentOrientation = async () => {
    const result = await ScreenOrientation.getCurrentOrientation();
    return result.type;
};