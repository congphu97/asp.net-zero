import { Component, ViewChild, Injector, Output, EventEmitter, OnInit} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ProductsServiceProxy, CreateOrEditProductDto, GetProductForViewDto, ProductDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import $ from 'jquery';
declare const myTest:any;
declare var $ :any;
@Component({
    selector: 'createOrEditProductModal',
    templateUrl: './create-or-edit-product-modal.component.html',
    styleUrls:['./products.component.css']
})
export class CreateOrEditProductModalComponent extends AppComponentBase implements OnInit{

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    
    active = false;
    saving = false;
    a:GetProductForViewDto;
    // product: CreateOrEditProductDto = new CreateOrEditProductDto();
    products:GetProductForViewDto[];
    product:ProductDto = new ProductDto();
    ngOnInit(): void {
        this._productsServiceProxy.getAllProduct().subscribe(data => {
            this.products = data;
            console.log(this.products);
        })
  
        
    }

   
    constructor(
        injector: Injector,
        private _productsServiceProxy: ProductsServiceProxy
    ) {
        super(injector);
    }

    show(productId?: number): void {

        if (!productId) {
            this.product = new CreateOrEditProductDto();
            this.product.id = productId;

            this.active = true;
            this.modal.show();
        } else {
            this._productsServiceProxy.getProductForEdit(productId).subscribe(result => {
                this.product = result.product;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._productsServiceProxy.createOrEdit(this.product)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }
    validate(){
       myTest();
   }





    close(): void {

        this.active = false;
        this.modal.hide();
    }
   test(x:ProductDto){
       console.log(x);
   }
}
