import { Component, OnInit, ViewChild, Injector ,EventEmitter, Output} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetOrderForViewDto, OrderDto, OrdersServiceProxy } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'confirm',
    templateUrl: './confirm.component.html'
})
export class ConfirmComponent extends AppComponentBase implements OnInit  {
    @ViewChild('confirm', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
    
    constructor(injector: Injector,private orderService:OrdersServiceProxy
    ) {
        super(injector);
        this.item = new GetOrderForViewDto();
        this.item.order = new OrderDto();
    }

    active = false;
    saving = false;
    item: GetOrderForViewDto;
    order:OrderDto;
    ngOnInit(): void { }

    show(item: GetOrderForViewDto): void {
          this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
    payment(){
        console.log(this.item);
        this.item.order.status='TRUE';
        this.item.order.paymentDate = Date.now().toString();
        this.orderService.createOrEdit(this.item.order)
        .subscribe(() => {
         
           this.notify.info(this.l('SavedSuccessfully'));
           this.close();
           this.modalSave.emit(null);
        });
    }
}
