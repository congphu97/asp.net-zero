import { Component, ViewChild, Injector, Output, EventEmitter, OnInit} from '@angular/core';
import { ModalDirective, formatDate } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { OrdersServiceProxy, CreateOrEditOrderDto, ProductDto, GetProductForViewDto, GetCategoryForViewDto, CategoryDto, ProductsServiceProxy, CategoriesServiceProxy, VariantsServiceProxy, VariantDto, GetVariantForViewDto, OrderDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { AbpMultiTenancyService } from 'abp-ng2-module/dist/src/multi-tenancy/abp-multi-tenancy.service';


@Component({
    selector: 'createOrEditOrderModal',
    templateUrl: './create-or-edit-order-modal.component.html'
})
export class CreateOrEditOrderModalComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    order: CreateOrEditOrderDto = new CreateOrEditOrderDto();
    category:CategoryDto;
    categories:GetCategoryForViewDto[];
    product:ProductDto;
   
    products :GetProductForViewDto[];
    arrayProduct:GetProductForViewDto[];
    variant:VariantDto;
    variants:GetVariantForViewDto[];
    arrayVariant:GetVariantForViewDto[];
    
    today :number = Date.now();
    marked = false;
    theCheckbox = false;
    userNAME:string;
    ngOnInit():void{
        this.CategoryService.getAllCategory().subscribe(data => {
            this.categories = data;
            console.log(this.categories);
        })
        this.userNAME= this.appSession.user.userName;
    }
    constructor(
        injector: Injector,
        private _ordersServiceProxy: OrdersServiceProxy,private variantService : VariantsServiceProxy,private CategoryService : CategoriesServiceProxy, private ProductService:ProductsServiceProxy,
        private _abpMultiTenancyService:AbpMultiTenancyService
    ) {
        super(injector);
        this.order.status='FALSE';

    }

    show(orderId?: number): void {

        if (!orderId) {
            this.order = new CreateOrEditOrderDto();
            this.order.id = orderId;

            this.active = true;
            this.modal.show();
        } else {
        
            this._ordersServiceProxy.getOrderForEdit(orderId).subscribe(result => {
                this.order = result.order;
                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;
            this.order.productID = this.variant.productID;
            this.order.orderDate = this.today.toString();
            this.order.user = this.userNAME;
            console.log(this.order.user);
            this._ordersServiceProxy.createOrEdit(this.order)
             .pipe(finalize(() => { this.saving = false;}))
             .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));
                this.close();
                this.modalSave.emit(null);
             });
    }

    toggleVisibility(e){
    
      

        if(this.marked= e.target.checked){
            this.order.status='TRUE';
            this.order.paymentDate=this.today.toString();
        }
          else
        this.order.status='FALSE';

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
        this.order.amount=0;
        this.order.totalVAT=0;
    }
    getValueVariant(y:ProductDto){
        this.arrayVariant=[];
        this.variantService.getAllVariant().subscribe(data => {
            this.variants = data;
            this.variants.forEach(element => {
                if(y.id==Number(element.variant.productID))
                {
                    console.log(element);
                    this.arrayVariant.push(element);                                                                                                                                                                                                            
                }
                
            });
        })
        this.order.amount=0;
        this.order.totalVAT=0;

    }
    getValueOrder(x:VariantDto){
        this.order.amount=0;
        this.order.totalVAT=0;
    }
    getValueAmount(x:OrderDto){

        console.log(this.variant.id);
        this.variantService.getVariantForView(this.variant.id).subscribe(data => {
            this.variant.price = data.variant.price;
            this.order.totalVAT = this.variant.price * this.order.amount;       
        })       
    }
    
}
