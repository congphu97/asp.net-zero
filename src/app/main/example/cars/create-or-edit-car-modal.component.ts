import { Component, ViewChild, Injector, Output, EventEmitter, OnInit} from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { CarsServiceProxy, CreateOrEditCarDto, GetCarForViewDto, CarDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';


@Component({
    selector: 'createOrEditCarModal',
    templateUrl: './create-or-edit-car-modal.component.html'
})
export class CreateOrEditCarModalComponent extends AppComponentBase implements OnInit {
    ngOnInit(): void {
  this._carsServiceProxy.getAllCar().subscribe(data =>
    {
        this.cars=data;
        console.log(this.cars);
    })
        
    }
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    active = false;
    saving = false;
    cars :GetCarForViewDto[];
    // car: CreateOrEditCarDto = new CreateOrEditCarDto();

car:CarDto;

    constructor(
        injector: Injector,
        private _carsServiceProxy: CarsServiceProxy
    ) {
        super(injector);
    }

    show(carId?: number): void {

        if (!carId) {
            this.car = new CreateOrEditCarDto();
            this.car.id = carId;

            this.active = true;
            this.modal.show();
        } else {
            this._carsServiceProxy.getCarForEdit(carId).subscribe(result => {
                this.car = result.car;


                this.active = true;
                this.modal.show();
            });
        }
    }

    save(): void {
            this.saving = true;

			
            this._carsServiceProxy.createOrEdit(this.car)
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
