import { Component, ViewChild, Injector, Output, EventEmitter, OnInit} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { VariantsServiceProxy, CreateOrEditVariantDto, CategoriesServiceProxy, ProductsServiceProxy, GetCategoryForViewDto, CategoryDto, ProductDto, GetProductForViewDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditVariantModal',
    templateUrl: './create-or-edit-variant-modal.component.html'
})
export class CreateOrEditVariantModalComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    category:CategoryDto;
    categories:GetCategoryForViewDto[];
    product:ProductDto;
    variant: CreateOrEditVariantDto = new CreateOrEditVariantDto();
    products :GetProductForViewDto[];
    arrayProduct:GetProductForViewDto[];
    ngOnInit(): void {
       this.CategoryService.getAllCategory().subscribe(data => {
           this.categories = data;
           console.log(this.categories);
       })
        
    }

    constructor(
        injector: Injector,
        private _variantsServiceProxy: VariantsServiceProxy,private CategoryService : CategoriesServiceProxy, private ProductService:ProductsServiceProxy
    ) {
        super(injector);
    }

    show(variantId?: number): void {

        if (!variantId) {
            this.variant = new CreateOrEditVariantDto();
            this.variant.id = variantId;

            this.active = true;
            this.modal.show();
        } else {
            this._variantsServiceProxy.getVariantForEdit(variantId).subscribe(result => {
                this.variant = result.variant;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._variantsServiceProxy.createOrEdit(this.variant)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }







    close(): void {

        this.active = false;
        this.modal.hide();
    }
    getValueCategory(x:CategoryDto){
        this.arrayProduct=[];
        this.ProductService.getAllProduct().subscribe(data => {
            this.products = data;
            this.products.forEach(element => {
                if(x.id==Number(element.product.categoryID))
                {
                    console.log(element);
                    this.arrayProduct.push(element);                                                                                                                                                                                                            
                }
                
            });
        })
    }
}
