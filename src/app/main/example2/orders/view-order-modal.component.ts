import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetOrderForViewDto, OrderDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewOrderModal',
    templateUrl: './view-order-modal.component.html'
})
export class ViewOrderModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetOrderForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetOrderForViewDto();
        this.item.order = new OrderDto();
    }

    show(item: GetOrderForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
