import { Component, Injector, ViewEncapsulation, ViewChild, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrdersServiceProxy, OrderDto, GetOrderForViewDto } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditOrderModalComponent } from './create-or-edit-order-modal.component';
import { ViewOrderModalComponent } from './view-order-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { ConfirmComponent } from './confirm.component';

@Component({
    templateUrl: './orders.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class OrdersComponent extends AppComponentBase implements OnInit {

    @ViewChild('confirm', { static: true }) confirm: ConfirmComponent;
    @ViewChild('createOrEditOrderModal', { static: true }) createOrEditOrderModal: CreateOrEditOrderModalComponent;
    @ViewChild('viewOrderModalComponent', { static: true }) viewOrderModal: ViewOrderModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;

    advancedFiltersAreShown = false;
    filterText = '';
    productIDFilter = '';
    orderDateFilter = '';
    statusFilter = '';
    paymentDateFilter = '';
    maxAmountFilter: number;
    maxAmountFilterEmpty: number;
    minAmountFilter: number;
    minAmountFilterEmpty: number;
    maxTotalVATFilter: number;
    maxTotalVATFilterEmpty: number;
    minTotalVATFilter: number;
    minTotalVATFilterEmpty: number;
    userFilter = '';
    userNAME: string;

    display: boolean = false;

    ngOnInit(): void {
        this.userNAME = this.appSession.user.userName;
        if(this.appSession.user.userName == 'admin'){
            this.userNAME = '';
        }
        else this.userNAME = this.appSession.user.userName;
        console.log(this.userNAME);

    }

    constructor(
        injector: Injector,
        private _ordersServiceProxy: OrdersServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private _fileDownloadService: FileDownloadService
    ) {
        super(injector);
    }

    getOrders(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._ordersServiceProxy.getAllOrder(this.userNAME).subscribe(result => {
            this.primengTableHelper.records = result;

        })


    }
    showDialog() {
        this.display = true;
        console.log(1);
    }
    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createOrder(): void {
        this.createOrEditOrderModal.show();
    }

    deleteOrder(order: OrderDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._ordersServiceProxy.delete(order.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._ordersServiceProxy.getOrdersToExcel(
            this.filterText,
            this.productIDFilter,
            this.orderDateFilter,
            this.statusFilter,
            this.paymentDateFilter,
            this.maxAmountFilter == null ? this.maxAmountFilterEmpty : this.maxAmountFilter,
            this.minAmountFilter == null ? this.minAmountFilterEmpty : this.minAmountFilter,
            this.maxTotalVATFilter == null ? this.maxTotalVATFilterEmpty : this.maxTotalVATFilter,
            this.minTotalVATFilter == null ? this.minTotalVATFilterEmpty : this.minTotalVATFilter,
            this.userFilter,
        )
            .subscribe(result => {
                this._fileDownloadService.downloadTempFile(result);
            });
    }
}
