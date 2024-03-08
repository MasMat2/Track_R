import { Injectable } from '@angular/core';
import { utils, writeFile } from 'xlsx';

@Injectable()
export class GridGeneralService {
  public lstGridPageSize = new Array();
  constructor() {
    this.lstGridPageSize[0] = { id: 0, cant: 5 };
    this.lstGridPageSize[1] = { id: 1, cant: 10 };
    this.lstGridPageSize[2] = { id: 2, cant: 25 };
    this.lstGridPageSize[3] = { id: 3, cant: 50 };
    this.lstGridPageSize[4] = { id: 4, cant: 100 };
  }

  cargarListaPaginado() {
    return this.lstGridPageSize;
  }

  getDefault() {
    return this.lstGridPageSize[1].id;
  }

  download(params: any, content: any, extension: any, headers: any, headerSizes: any) {
    // crea un sheet de excel con los headers
    const worksheet = utils.json_to_sheet([headers], { skipHeader: true });

    // agrega los datos a la sheet
    utils.sheet_add_json(worksheet, content, {
      skipHeader: true,
      origin: 'A2'
    });

    // agrega el ancho de las columnas de acuerdo al tamano del header
    worksheet['!cols'] = headerSizes;

    // Crea un book de excel y le anexa la sheet
    const workbook = utils.book_new();
    utils.book_append_sheet(workbook, worksheet, 'Sheet1');

    // asigna el nombre del archivo y descaga
    const fileNamePresent = params && params.fileName && params.fileName.length !== 0;

    const hoy = new Date();
    const fileName =
      hoy.getFullYear() +
      ('0' + (hoy.getMonth() + 1)).slice(-2) +
      ('0' + hoy.getDate()).slice(-2) +
      '_' +
      params.fileName +
      '.' +
      extension;

    writeFile(workbook, fileName);
  }

  private b64toBlob(b64Data: any, contentType: any) {
    const sliceSize = 512;
    const byteCharacters = atob(b64Data);
    const byteArrays = [];
    for (let offset = 0; offset < byteCharacters.length; offset += sliceSize) {
      const slice = byteCharacters.slice(offset, offset + sliceSize);
      const byteNumbers = new Array(slice.length);
      for (let i = 0; i < slice.length; i++) {
        byteNumbers[i] = slice.charCodeAt(i);
      }
      const byteArray = new Uint8Array(byteNumbers);
      byteArrays.push(byteArray);
    }
    const blob = new Blob(byteArrays, { type: contentType });
    return blob;
  }

  // obtiene los headers que se pondran en el excel
  getHeadersOfChildren(children: any) {
    const headers: any = {};
    for (let i = 0; i < children.length; i++) {
      if (children[i].field !== '' && !children[i].hide) {
        headers[children[i].field] = children[i].headerName;
      }
    }

    return headers;
  }

  // obtiene los headers que se pondran en el excel
  getHeadersOfChildrenWithHidden(children: any) {
    let headers: any = {};
    for (let i = 0; i < children.length; i++) {
      if (children[i].field !== '') {
        headers[children[i].field] = children[i].headerName;
      }
    }
    return headers;
  }

  /**
   * obtiene las funciones de valueGetter que se aplicaran
   * a las columnas para desplegar la información con un formato adecuado
   */
  getFormats(children: any) {
    const formats: any = {};
    for (let i = 0; i < children.length; i++) {
      if (children[i].field !== '' && !children[i].hide) {
        if (children[i].valueGetter !== undefined) {
          formats[children[i].field] = children[i].valueGetter;
        } else if (children[i].cellRenderer !== undefined) {
          formats[children[i].field] = children[i].cellRenderer;
        } else if (children[i].valueFormatter !== undefined) {
          formats[children[i].field] = children[i].valueFormatter;
        } else {
          formats[children[i].field] = undefined;
        }
      }
    }

    return formats;
  }

  /**
   * obtiene las funciones de valueGetter que se aplicaran
   * a las columnas (incluyendo las que se marcaron com hide: true) para desplegar la información con un formato adecuado
   */
  getFormatsWithHidden(children: any) {
    let formats: any = {};
    for (let i = 0; i < children.length; i++) {
      if (children[i].field != '') {
        if (children[i].valueGetter != undefined) {
          formats[children[i].field] = children[i].valueGetter;
        } else if (children[i].cellRenderer != undefined) {
          formats[children[i].field] = children[i].cellRenderer;
        } else {
          formats[children[i].field] = undefined;
        }
      }
    }

    return formats;
  }

  /**
   * obtiene el ancho de las columnas de acuerdo al tamaño de los headers
   */
  getHeaderSizes(headers: any) {
    const headerSizes = [];
    for (const prop in headers) {
      const size = { wch: headers[prop].length };
      headerSizes.push(size);
    }
    return headerSizes;
  }

  /**
   * obtención de informacion del rowsData y aplicacion de formatos
   * que se aplican en los grids.
   */
  getContentFileFromData(formats: any, rowsData: any) {
    const content: any = [];

    // recorre los rows de el data que se muestra en el grid
    for (let i = 0; i < rowsData.length; i++) {
      const contentItem: { [key: string]: any } = {};
      // recorre los formatos que se aplican a las columnas
      // para que se vean como en el grid
      // si no tiene un formato (valueGetter, cellRenderer) solo pone el valor
      for (const itemFormat in formats) {
        if (formats[itemFormat] !== undefined) {
          if (formats[itemFormat].name === 'valueGetter') {
            // aplica el formato al dato
            rowsData[i].node = { id: i };
            contentItem[itemFormat] = formats[itemFormat](rowsData[i]);
          } else {
            // aplica el formato al dato
            const valueData = {
              value: this.getValue(itemFormat, rowsData[i].data)
            };

            if (typeof formats[itemFormat] === 'function') {
              contentItem[itemFormat] = formats[itemFormat](valueData);
            }
          }
        } else {
          // escribe el valor
          contentItem[itemFormat] = this.getValue(itemFormat, rowsData[i].data);
        }
      }
      // agrega el objeto a la lista de data
      content.push(contentItem);
    }

    return content;
  }

  /**
   * Navega entre las propiedades de un objeto para obtener el valor
   */
  getValue(field: string, itemData: any) {
    const fieldNavigation = field.split('.');
    let value = itemData;

    // itera entre la "profundidad" de un objeto para obtener el value
    fieldNavigation.forEach((key) => {
      value = value[key] !== null ? value[key] : '';
    });
    return value;
  }
}
