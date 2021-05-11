import {inject} from 'aurelia-framework';
import {HttpClient} from 'aurelia-fetch-client';
import {AssetService} from "../services/asset-service";
import * as toastr from "toastr";
import { IAsset } from './IAsset';
import { ValidationControllerFactory, ValidationRules,ValidationController } from 'aurelia-validation';
import { runInThisContext } from 'vm';
//import {BootstrapFormRenderer} from 'aurelia-form-renderer-bootstrap';
@inject(HttpClient,AssetService,ValidationControllerFactory)
export default class Asset {
    assets;    
    asset: IAsset;
    controller :ValidationController;
    //TODO : pass this using IAsset
    //id = 0;
    assetName ='';
    department=0;
    countryOfDepartment='';
    eMailAdressOfDepartment='';
    purchaseDate= new Date();
    broken = false;  
    searchText='';

    isRequesting = false;  
    
    departments = [
        { id: 1, name: 'HQ' },
        { id: 2, name: 'Store1' },
        { id: 3, name: 'Store2' },
        { id: 4, name: 'Store3' },
        { id: 5, name: 'MaintenanceStation' }
      ];
    
    //populated from countries API
    countries =[];

    constructor(private httpClient:HttpClient        
        ,private assetService:AssetService
        ,controllerFactory){
            assetService = new AssetService(httpClient);
            this.asset =  new IAsset();
            this.controller = controllerFactory.createForCurrentScope();
           // this.controller.addRenderer(new BootstrapFormRenderer());

            ValidationRules.customRule(
                'dateRule',
                (value, obj) =>{
                    let date2 = new Date();
                    if(Math.abs(date2.getTime() - value.getTime()) > 365){
                        return false;
                    }
                    return true;
                 },
                `\${$displayName} must not be older than a year.` 
                );
                
            ValidationRules
            .ensure('assetName').required()
            .minLength(5)           
            .ensure('eMailAdressOfDepartment').required()
            .email()
            .ensure('purchaseDate').required()
            //.satisfiesRule('dateRule')
            .on(this);
            
    } 
    get canSave() {
        return this.assetName && this.department && this.countryOfDepartment && this.eMailAdressOfDepartment && this.purchaseDate
    }

    created() {
        this.getAllAssets();
        this.getCountries();
    }
    save(){
        this.controller.validate();        
        console.log("Saving" + this.assetName);
        //TODO Convert to model
        this.asset.assetName = this.assetName;
        this.asset.department = this.department;
        this.asset.countryOfDepartment = this.countryOfDepartment;
        this.asset.eMailAdressOfDepartment = this.eMailAdressOfDepartment;
        this.asset.purchaseDate = this.purchaseDate;
        this.asset.broken = this.broken;
        this.assetService.save(this.asset).then(asset=>{
            this.asset = <IAsset>asset;
            toastr.success(`Asset - ${this.asset.assetName} saved successfully`);
            this.getAllAssets();
        });
      
    }
    delete(assetId){
        this.assetService.delete(assetId).then(asset=>{  
            this.asset = <IAsset>asset;       
            toastr.success(`Asset "${this.asset.assetName}" is deleted successfully`);
            this.getAllAssets();
        });
    }

    getAllAssets(){
        this.assetService.getAll().then((assets : IAsset[])=>{
            this.assets = assets;
            toastr.success("Asset List is loaded successfully");
        });
    }
    search(){
        this.assetService.search(this.searchText).then((assets : IAsset[])=>{
            this.assets = assets;
            toastr.success("Asset List is loaded successfully");
        });
    }

     getCountries(){
        this.assetService.getCountries().then((countriesData)=>{
            this.populate(countriesData);            
        });
     }

     populate(countriesData) {
         for(let i=0;i<countriesData.length;i++){
             this.countries.push(countriesData[i].name);
         }
     }
}