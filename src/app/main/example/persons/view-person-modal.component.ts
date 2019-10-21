import { Component, ViewChild, Injector, Output, EventEmitter } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { GetPersonForViewDto, PersonDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'viewPersonModal',
    templateUrl: './view-person-modal.component.html'
})
export class ViewPersonModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    item: GetPersonForViewDto;


    constructor(
        injector: Injector
    ) {
        super(injector);
        this.item = new GetPersonForViewDto();
        this.item.person = new PersonDto();
    }

    show(item: GetPersonForViewDto): void {
        this.item = item;
        this.active = true;
        this.modal.show();
    }

    close(): void {
        this.active = false;
        this.modal.hide();
    }
}
