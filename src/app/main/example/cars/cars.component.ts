import { Component, Injector, ViewEncapsulation, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CarsServiceProxy, CarDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditCarModalComponent } from './create-or-edit-car-modal.component';
import { ViewCarModalComponent } from './view-car-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';

@Component({
    templateUrl: './cars.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls:['./cars.component.css']
})
export class CarsComponent extends AppComponentBase {

    @ViewChild('createOrEditCarModal', { static: true }) createOrEditCarModal: CreateOrEditCarModalComponent;
    @ViewChild('viewCarModalComponent', { static: true }) viewCarModal: ViewCarModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    nameFilter = '';
    detailFilter = '';
    priceFilter = '';

    showLoadingIndicator: boolean = false;


    constructor(
        injector: Injector,
        private _carsServiceProxy: CarsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getCars(event?: LazyLoadEvent) {
       
        
        setTimeout(() => { 


            if (this.primengTableHelper.shouldResetPaging(event)) {
                // this.p
            this.paginator.changePage(0);
   
            
            return;
        }

            this._carsServiceProxy.getAll(
                this.filterText,
                this.nameFilter,
                this.detailFilter,
                this.priceFilter,
                this.primengTableHelper.getSorting(this.dataTable),
                this.primengTableHelper.getSkipCount(this.paginator, event),
                this.primengTableHelper.getMaxResultCount(this.paginator, event)
            ).subscribe(result => {
                this.primengTableHelper.totalRecordsCount = result.totalCount;
                this.primengTableHelper.records = result.items;
                this.showLoadingIndicator = false;
            });
 
        }, 3000);
      
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createCar(): void {
        this.createOrEditCarModal.show();
    }

    deleteCar(car: CarDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._carsServiceProxy.delete(car.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._carsServiceProxy.getCarsToExcel(
        this.filterText,
            this.nameFilter,
            this.detailFilter,
            this.priceFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
}
