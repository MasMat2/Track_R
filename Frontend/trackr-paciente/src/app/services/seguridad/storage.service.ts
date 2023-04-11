import { Injectable } from "@angular/core";
import { Storage } from '@ionic/storage-angular';

@Injectable({
    providedIn: 'root'
})
export class StorageService {

    private _storage : Storage | null = null;
    public  token    : string = "";

    constructor( private storage: Storage ){
        this.cargarStorage(); 
    }

    async cargarStorage() {
        let storageData = await this.storage.create();
        this._storage = storageData;
        this.token = await this._storage.get('token-cliente');
    }

    public set(key: string, value:any) {
        this._storage?.set(key, value);
    }

    public remove(key:string) {
        return this._storage?.remove(key);
    }

}