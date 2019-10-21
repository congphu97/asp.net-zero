import { Injectable } from '@angular/core';
import { VariantDto, GetVariantForViewDto } from './service-proxies';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class VariantService {
  
    public variant:VariantDto = new VariantDto();
     public a:VariantDto = new VariantDto();
    constructor(private http: HttpClient) {

    }
    
    public get Edit():VariantDto{

    return this.variant;
    }
    public set Edit(variant:VariantDto){
       this.a= variant;
    }
}
