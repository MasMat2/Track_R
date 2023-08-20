import { Camera, CameraResultType } from '@capacitor/camera';
import { FilePicker, PickedFile } from '@capawesome/capacitor-file-picker';
export class CapacitorUtils {
  //public files: PickedFile[] = [];
  imageSrc: string | undefined;

  takePicture = async (): Promise<string> => {
    try {
      const image = await Camera.getPhoto({
        quality: 90,
        allowEditing: true,
        resultType: CameraResultType.DataUrl,
      });
      this.imageSrc = image.dataUrl;
      if (this.imageSrc) {
        return this.imageSrc;
      } else {
        throw new Error('Error');
      }
    } catch (error) {
      console.error('Error al tomar la foto:', error);
      throw error;
    }
  };

  pickFiles = async () => {
    const { files } = await FilePicker.pickFiles({
      types: ['image/png', 'image/jpeg', 'application/pdf', 'image/gif'],
      readData: true,
    });
    return files;
  };
}
