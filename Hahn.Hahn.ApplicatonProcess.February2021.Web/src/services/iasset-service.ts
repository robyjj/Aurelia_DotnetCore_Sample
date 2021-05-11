import { IAsset } from "../asset-manager/iasset";

export interface IAssetService {
    getAll();
    get(id: number);
    search(keyword: string);
    save(contact: IAsset);
    //create();
    delete(id: number);
}