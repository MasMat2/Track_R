import { Injectable } from '@angular/core';
import { ScreenOrientation, OrientationType } from '@capawesome/capacitor-screen-orientation';


@Injectable({
  providedIn: 'root'
})
export class ScreenOrientationService {

  constructor() { }

  async lockLandscape() {
    try {
      await ScreenOrientation.lock({ type: OrientationType.LANDSCAPE });
    } catch (error) {
      console.error('Error Screen orientation LANDSCAPE:', error);
    }
  }

  async lockPortrait() {
    try {
      await ScreenOrientation.lock({ type: OrientationType.PORTRAIT });
    } catch (error) {
      console.error('Error Screen orientation PORTRAIT:', error);
    }
  }

  async unlock() {
    try {
      await ScreenOrientation.unlock();
    } catch (error) {
      console.error('Error al bloquear la orientación:', error);
    }
  }

  async getCurrentOrientation() {
    try {
      const result = await ScreenOrientation.getCurrentOrientation();
      return result.type;
    } catch (error) {
      console.error('Error al obtener la orientación actual:', error);
      return null;
    }
  }


}
