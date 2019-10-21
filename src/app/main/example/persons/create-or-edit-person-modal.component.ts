import { Component, ViewChild, Injector, Output, EventEmitter} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { PersonsServiceProxy, CreateOrEditPersonDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditPersonModal',
    templateUrl: './create-or-edit-person-modal.component.html'
})
export class CreateOrEditPersonModalComponent extends AppComponentBase {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;

    person: CreateOrEditPersonDto = new CreateOrEditPersonDto();



    constructor(
        injector: Injector,
        private _personsServiceProxy: PersonsServiceProxy
    ) {
        super(injector);
    }

    show(personId?: number): void {

        if (!personId) {
            this.person = new CreateOrEditPersonDto();
            this.person.id = personId;

            this.active = true;
            this.modal.show();
        } else {
            this._personsServiceProxy.getPersonForEdit(personId).subscribe(result => {
                this.person = result.person;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._personsServiceProxy.createOrEdit(this.person)
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
}
