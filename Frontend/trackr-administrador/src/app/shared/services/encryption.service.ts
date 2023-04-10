import { Injectable } from '@angular/core';
import * as crc from 'crc';
import * as bigInt from 'big-integer';

@Injectable({
  providedIn: 'root',
})
export class EncryptionService {
  private poly: string = '110101110010100001101100110111011';

  public CRC32(id: number) {

    const result: any = {
      crc: undefined,
      oLength: undefined,
      pLength: undefined,
    };

    const pLen = this.poly.length - 1;
    const idBin = id.toString(2);
    const idLen = idBin.length;
    let CRC = idBin + '0'.repeat(pLen);
    let idLenCRC = CRC.length;
    let CRCN: number = 0;

    while (idLenCRC - pLen > 0) {
      let aux = '';
      const PNS = this.poly + '0'.repeat(idLenCRC - pLen);
      for (let i = 0; i < idLenCRC; i++) {
        aux += CRC.charAt(i) !== PNS.charAt(i) ? '1' : '0';
      }
      CRCN = parseInt(aux, 2);
      CRC = CRCN.toString(2);
      idLenCRC = CRCN.toString(2).length;
    }

    result.crc = CRCN;
    result.oLength = idLen;
    result.pLength = pLen;

    return result;
  };

  private revertCRC32(
    crc: number,
    length: number,
    crcLength: number
  ) {
    let crcB = crc.toString(2).split('').reverse().join('');
    crcB += '0'.repeat(length + (crcLength - crcB.length));
    const pInv = this.poly.split('').reverse().join('');
    let aux;
    for (let i = 0; i < length; i++) {
      aux = '' + '0'.repeat(i);
      if (crcB.charAt(i) === '1') {
        for (let j = 0; j + i < pInv.length + length - 1; j++) {
          if (j >= pInv.length && j < crcLength + length) {
            if (j + i < crcLength + length) {
              aux += crcB.charAt(j + i);
            }
          } else {
            aux += crcB.charAt(j + i) !== pInv.charAt(j) ? '1' : '0';
          }
        }
        crcB = aux;
      }
    }
    crcB = crcB.slice(crcLength);
    crcB = crcB.split('').reverse().join('');

    return parseInt(crcB, 2);
  };

  private hexEncode(encode: String) {
    let hex, i;

    let result = '';
    for (i = 0; i < encode.length; i++) {
      hex = encode.charCodeAt(i).toString(16);
      result += ('000' + hex).slice(-4);
    }

    return result;
  };

  private hexDecode(decode: String) {
    let j;
    const hexes = decode.match(/.{1,4}/g) || [];
    let back = '';
    for (j = 0; j < hexes.length; j++) {
      back += String.fromCharCode(parseInt(hexes[j], 16));
    }

    return back;
  };

  private encryptId(id: Number) {
    let idHex: any =
      new Date().getDate().toString() + this.hexEncode(id.toString());
    idHex = bigInt(idHex, 10);
    idHex = idHex.toString(16);
    const crc32 = crc.crc32(idHex).toString(16);
    return idHex + '-' + crc32;
  };
  private encrypt(param: String) {
    const paramHex = this.hexEncode(param);
    const crc32 = crc.crc32(paramHex).toString(16);
    return paramHex + '-' + crc32;
  };

  private encryptTimestamp(time: number) {
    const timestamp = ((time / 1000) | 0).toString(16);
    const crc32 = crc.crc32(timestamp).toString(16);
    return timestamp + '-' + crc32;
  };

  generateURL(params: any) {
    const identifier = 'id';
    const queryParams: any = {
      t: undefined,
    };
    const p: string[] = Object.keys(params);
    
    for (const i in p) {
      if (p[i].indexOf(identifier) === 0) {
        queryParams[p[i]] = this.encryptId(params[p[i]]);
      } else {
        queryParams[p[i]] = this.encrypt(params[p[i]]);
      }
    }
    queryParams.t = this.encryptTimestamp(Date.now());
    return queryParams;
  };

  readUrlParams(params: any) {
    const identifier = 'id';
    const paramsCRC = {
      id: undefined,
      timestamp: undefined,
      error: undefined,
    };
    const error: String[] = [];
    const decryptedParams: any = {};
    const p = Object.keys(params);

    for (const i in p) {
      if (p[i] === 't') {
        const t = params[p[i]].split('-');
        const aux = crc.crc32(t[0]).toString(16);
        if (aux === t[1]) {
          decryptedParams[p[i]] = parseInt(t[0], 16) * 1000;
        } else {
          error.push(p[i]);
        }
      } else {
        const prop = params[p[i]].split('-');
        let aux : any = crc.crc32(prop[0]).toString(16);
        if (aux === prop[1]) {
          if (p[i].indexOf(identifier) === 0) {

            aux = bigInt(prop[0], 16);
            aux = aux.toString().slice(new Date().getDate().toString().length);
            decryptedParams[p[i]] = this.hexDecode(aux);
          } else {
            decryptedParams[p[i]] = this.hexDecode(prop[0]);
          }
        } else {
          error.push(p[i]);
        }
      }
    }
    decryptedParams['error'] =
      (error !== undefined && error.length > 0) || p.length === 0;
    decryptedParams['errorList'] =
      error !== undefined ? error : p.length === 0 ? ['No Params'] : [];

    return decryptedParams;
  };

  regenParams(params: any) {
    const obj: any = {
      id: undefined,
      timestamp: undefined,
      error: undefined,
    };
    const id = params.id.slice(-8);
    const idLength = parseInt(this.substract(params.id, id), 10);
    obj.id = this.revertCRC32(parseInt(id, 16), idLength, 32);
    const timestamp = params.t.slice(-8);
    const tLength = parseInt(this.substract(params.t, timestamp), 10);
    obj.timestamp = this.revertCRC32(parseInt(timestamp, 16), tLength, 32);
    return obj;
  };

  substract(o: String, r: String) {
    const oe = this.hexEncode(o);
    const re = this.hexEncode(r);

    const res = oe.replace(re, '');
    const result = this.hexDecode(res);

    return result;
  };

  constructor() {}
}
