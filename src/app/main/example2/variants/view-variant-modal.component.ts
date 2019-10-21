import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetVariantForViewDto, VariantDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewVariantModal',
    templateUrl: './view-variant-modal.component.html'
})
export class ViewVariantModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetVariantForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetVariantForViewDto();
        this.item.variant = new VariantDto();
    }

    show(item: GetVariantForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
