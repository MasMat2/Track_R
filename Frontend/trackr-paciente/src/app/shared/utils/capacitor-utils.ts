import { Camera, CameraResultType } from '@capacitor/camera';
import { FilePicker, PickedFile } from '@capawesome/capacitor-file-picker';
import { Filesystem} from '@capacitor/filesystem';
export class CapacitorUtils {

  public files: PickedFile[] = [];
  url: string = '';
  imageSrc: any;
  takePicture = async (): Promise<string> => {
    try {
      const image = await Camera.getPhoto({
        quality: 90,
        allowEditing: true,
        resultType: CameraResultType.DataUrl
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

  readFilePath = async () => {
    const contents = await Filesystem.readFile({
      path: this.imageSrc
    });
    return contents;
  };

  pickFiles = async () => {
    const { files } = await FilePicker.pickFiles({
      types: ['image/png', 'image/jpeg', 'application/pdf','image/gif'],
      readData: true
    });
    this.files = files;
    console.log(this.files)
    if (this.files[0] && this.files[0].path) {
      this.url = this.files[0].path;
    }
    return this.files;

  };
}
