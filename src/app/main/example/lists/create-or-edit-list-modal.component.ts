import { Component, ViewChild, Injector, Output, EventEmitter, OnInit, Input } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap';
import { finalize } from 'rxjs/operators';
import { ListsServiceProxy, CreateOrEditListDto, CarsServiceProxy, GetCarForViewDto, CarDto } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';
import { Element } from '@angular/compiler/src/render3/r3_ast';


@Component({
    selector: 'createOrEditListModal',
    templateUrl: './create-or-edit-list-modal.component.html',
    styleUrls: ['./create-or-edit-list-modal.component.css']
})
export class CreateOrEditListModalComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;


    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    @Output() buttonClicked = new EventEmitter<CarDto>();

    @Input() array: CarDto[];

    showLoadingIndicator = false;
    active = false;
    saving = false;
    dropdownList = [];
    selectedItems = [];
    dropdownSettings = {};
    list: CreateOrEditListDto = new CreateOrEditListDto();
    cars: GetCarForViewDto[];
    car: CarDto[] = [];
    tmps;
    constructor(
        injector: Injector,
        private _listsServiceProxy: ListsServiceProxy,
        private _carService: CarsServiceProxy
    ) {
        super(injector);
    }
    ngOnInit() {
        this._carService.getAllCar().subscribe(data => {
            this.cars = data;
            this.cars.forEach(element => {
                this.car.push(element.car);

            });
            console.log(this.car);
        });
        this.dropdownSettings = {
            singleSelection: false,
            text: "Select Cars",
            selectAllText: 'Select All',
            unSelectAllText: 'UnSelect All',
            enableSearchFilter: true,
            classes: "myclass custom-class",
            labelKey: 'name',
            primaryKey: 'id',

        };
    }
    onItemSelect(item: any) {
        console.log(item);
        console.log(this.array);
        var temps = this.array.map(i => {
            return i.id;
        })
        this.tmps = temps.join(";");
        console.log(this.tmps);

        this.buttonClicked.emit(this.tmps);
    }
    
    OnItemDeSelect(item: any) {
        console.log(item);
        console.log(this.array);

    }
    onSelectAll(items: any) {
        console.log(items);
    }
    onDeSelectAll(items: any) {
        console.log(items);
    }


    show(listId?: number): void {
        if (!listId) {
            this.list = new CreateOrEditListDto();
            this.list.id = listId;

            this.active = true;
            this.modal.show();
        } else {
            this._listsServiceProxy.getListForEdit(listId).subscribe(result => {
                console.log(result.list);
                this.list = result.list;


                this.active = true;
                this.modal.show();

            });
        }
    }

    save(): void {
        // this.showLoadingIndicator = true;
        setTimeout(() => {
            this.saving = true;
            this._listsServiceProxy.createOrEdit(this.list)
                .pipe(finalize(() => { this.saving = false; }))
                .subscribe(() => {
                    this.notify.info(this.l('SavedSuccessfully'));
                    this.close();
                    this.modalSave.emit(null);
                    // this.showLoadingIndicator = false;
                });
        }, 3000);





    }
    saveSelect() {
        this.list.name = this.tmps;
        this._listsServiceProxy.createOrEdit(this.list).subscribe(() => {
            this.modalSave.emit(null);
        })
    }

    close(): void {

        this.active = false;
        this.modal.hide();
    }
}
