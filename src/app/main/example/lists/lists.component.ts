import { Component, Injector, ViewEncapsulation, ViewChild, OnInit, Input } from '@angular/core';
import { ActivatedRoute, NavigationStart, NavigationEnd, NavigationError, NavigationCancel, Router, Event } from '@angular/router';
import { ListsServiceProxy, ListDto, CarsServiceProxy, CarDto, GetCarForViewDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditListModalComponent } from './create-or-edit-list-modal.component';
import { ViewListModalComponent } from './view-list-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';


@Component({
    templateUrl: './lists.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()],
    styleUrls: ['./create-or-edit-list-modal.component.css']
})
export class ListsComponent extends AppComponentBase implements OnInit {

    @ViewChild('createOrEditListModal', { static: true }) createOrEditListModal: CreateOrEditListModalComponent;
    @ViewChild('viewListModalComponent', { static: true }) viewListModal: ViewListModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
    value: ListDto;
    advancedFiltersAreShown = false;
    filterText = '';
    maxCarIDFilter: number;
    maxCarIDFilterEmpty: number;
    minCarIDFilter: number
    minCarIDFilterEmpty: number;
    nameFilter = '';
    detailFilter = '';
    priceFilter = '';
    car: CarDto;
    cars: GetCarForViewDto[];
    array: GetCarForViewDto;
    tmps;
    display: boolean = false;
    arrayCar: CarDto[] = [];
    showLoadingIndicator = false;
    disabled: boolean = true;
    showDialog() {
        this.display = true;
    }
    constructor(
        injector: Injector,
        private _listsServiceProxy: ListsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService,
        private _carService: CarsServiceProxy,
    ) {
        super(injector);


    }
    ngOnInit(): void {

    }
    getLists(event?: LazyLoadEvent) {
        abp.ui.setBusy(undefined, '', 1);
        setTimeout(() => {

        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        // this.primengTableHelper.showLoadingIndicator();
        // this.showLoadingIndicator=true;
        // setTimeout(() => {


        this._listsServiceProxy.getAll(
            this.filterText,
            this.maxCarIDFilter == null ? this.maxCarIDFilterEmpty : this.maxCarIDFilter,
            this.minCarIDFilter == null ? this.minCarIDFilterEmpty : this.minCarIDFilter,
            this.nameFilter,
            this.detailFilter,
            this.priceFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            // this.primengTableHelper.hideLoadingIndicator();
        });
        abp.ui.clearBusy();

        // this.showLoadingIndicator=false;
        }, 3000);
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }
    chooseList(list: ListDto): void {
        /// Loc 1;2;3 ra thanh cac element
        console.log(list.name);
        for (var i = 0, a = list.name.length; i < a; i++) {
            if (list.name[i] === ";") {
                list.name[i].substr(i, 1);
            }
            else {
                this.arrayCar = [];
                console.log(list.name[i]);
                this._carService.getCarForView(Number(list.name[i])).subscribe(data => {
                    this.car = data.car;
                    console.log(this.car);
                    this.arrayCar.push(this.car);
                    console.log(this.arrayCar);
                    
                })

            }
        }



    }
    createList(): void {
        this.createOrEditListModal.show();
    }

    deleteList(list: ListDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                //<<<---    using ()=> syntax

                if (isConfirmed) {
                    this._listsServiceProxy.delete(list.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));

                        });
                }
            }
        );



    }

    exportToExcel(): void {
        this._listsServiceProxy.getListsToExcel(
            this.filterText,
            this.maxCarIDFilter == null ? this.maxCarIDFilterEmpty : this.maxCarIDFilter,
            this.minCarIDFilter == null ? this.minCarIDFilterEmpty : this.minCarIDFilter,
            this.nameFilter,
            this.detailFilter,
            this.priceFilter,
        )
            .subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
    getData(x: any) {
        this.tmps = x;
        console.log(this.tmps);

    }
    clickData() {
        console.log(this.tmps);
        alert("OK");
    }
}
