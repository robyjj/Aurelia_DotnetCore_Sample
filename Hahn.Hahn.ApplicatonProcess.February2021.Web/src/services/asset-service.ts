import {inject} from "aurelia-framework";
import {HttpClient, json} from "aurelia-fetch-client";
import {IAsset} from "../asset-manager/IAsset";
import {Config} from "../common/config";
import { IAssetService } from "./iasset-service";
import { Console } from "console";

const fetch = !self.fetch ? System.import("isomorphic-fetch") : Promise.resolve(self.fetch);

@inject(HttpClient)
export class AssetService implements IAssetService {
private assets : IAsset[] ;
isRequesting = false;
countries;

    constructor(private httpClient: HttpClient) {
        this.init();
    }
    async init() {
        // ensure fetch is polyfilled before we create the http client
        await fetch;
        this.httpClient.configure( (config: any) => {
        config
       .useStandardConfiguration()
       .withBaseUrl(Config.baseApiUrl);
        });
    }

    getAll() {      
        this.isRequesting = true;
        return new  Promise( async (resolve: any) => {
          console.log("fetching");
            const http = this.httpClient;
            const response = await http.fetch("http://localhost:5005/Assets/");
            this.assets = await response.json();
            let results = this.assets;
            resolve(results);
            this.isRequesting = false;
        });
    }

    getCountries(){
      return new  Promise( async (resolve: any) => {
        console.log("fetching");
          const http = this.httpClient;
          const response = await http.fetch("https://restcountries.eu/rest/v2/all");
          this.countries = await response.json();
          let results = this.countries;
          resolve(results);
          this.isRequesting = false;
      });
  }
    save(asset){
      this.isRequesting = true;
       return new  Promise( async (resolve: any) => {
              const http = this.httpClient;
              console.log("saving");
              const response = await http.fetch("http://localhost:5005/Assets/", {
                method: "post",
                body: json(asset)
              });
              let result = await response.json();
              resolve(result);
              console.log(result);
              this.isRequesting = false;
          });
      }
      
  delete(id: number) {
    this.isRequesting = true;
    return new  Promise( async (resolve: any) => {
            const http = this.httpClient;
            const response = await http.fetch(`http://localhost:5005/Assets/${id}`, {
              method: "delete",
              body: json(id)
            });
            let result = await response.json();
            resolve(result);
            console.log(result);
            this.isRequesting = false;
        });
  }

    search(keyword: string) {
    this.isRequesting = true;
    return new  Promise( async (resolve: any) => {
            const http = this.httpClient;
            const response = await http.fetch(`http://localhost:5005/Assets/Filter?assetName=${keyword}`);
            this.assets = await response.json();
            let results = this.assets;
            resolve(results);
            this.isRequesting = false;
        });
  }

  get(id: number) {
    this.isRequesting = true;
    return new  Promise( async (resolve: any) => {
            const http = this.httpClient;
            const response = await http.fetch(`http://localhost:5005/Assets/${id}`);
            let foundAsset = await response.json();
            let result = foundAsset;
            resolve(result);
            console.log(result);
            this.isRequesting = false;
        });
  }


  // create() {
  //       this.isRequesting = true;
  //       return new  Promise( async (resolve: any) => {
  //               const http = this.httpClient;
  //               let newAsset = {
  //                 id: 0,
  //                 assetName:"",
  //                 department:"",
  //                 countryOfDepartment:"",
  //                 eMailAdressOfDepartment:"",
  //                 purchaseDate:new Date(),
  //                 broken:false
  //               };
  //               const response = await http.fetch("assets", {
  //                 method: "post",
  //                 body: json(newAsset)
  //               });
  //               let justCreatedAsset = <IAsset> (await response.json());
  //               resolve(justCreatedAsset);
  //               console.log(justCreatedAsset);
  //               this.isRequesting = false;
  //           });
  //     }
    
}