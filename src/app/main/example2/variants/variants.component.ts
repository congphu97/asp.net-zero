import { Component, Injector, ViewEncapsulation, ViewChild, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { VariantsServiceProxy, VariantDto  } from '@shared/service-proxies/service-proxies';
import { NotifyService } from '@abp/notify/notify.service';
import { AppComponentBase } from '@shared/common/app-component-base';
import { TokenAuthServiceProxy } from '@shared/service-proxies/service-proxies';
import { CreateOrEditVariantModalComponent } from './create-or-edit-variant-modal.component';
import { ViewVariantModalComponent } from './view-variant-modal.component';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { Table } from 'primeng/components/table/table';
import { Paginator } from 'primeng/components/paginator/paginator';
import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';
import { FileDownloadService } from '@shared/utils/file-download.service';
import * as _ from 'lodash';
import * as moment from 'moment';
import { EditVariantComponent } from './edit-variant.component';
import { VariantService } from '@shared/service-proxies/variant.service';

@Component({
    templateUrl: './variants.component.html',
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class VariantsComponent extends AppComponentBase {

    @ViewChild('createOrEditVariantModal', { static: true }) createOrEditVariantModal: CreateOrEditVariantModalComponent;
    @ViewChild('viewVariantModalComponent', { static: true }) viewVariantModal: ViewVariantModalComponent;
    @ViewChild('dataTable', { static: true }) dataTable: Table;
    @ViewChild('paginator', { static: true }) paginator: Paginator;
   
    advancedFiltersAreShown = false;
    filterText = '';
    nameFilter = '';
    productIDFilter = '';
    maxPriceFilter : number;
		maxPriceFilterEmpty : number;
		minPriceFilter : number;
		minPriceFilterEmpty : number;
     
    variant:VariantDto;
    constructor(
        injector: Injector,
        private _variantsServiceProxy: VariantsServiceProxy,
        private _notifyService: NotifyService,
        private _tokenAuth: TokenAuthServiceProxy,
        private _activatedRoute: ActivatedRoute,
        private variantService:VariantService,
        private _fileDownloadService: FileDownloadService,private router:Router
    ) {
        super(injector);
    }

    getVariants(event?: LazyLoadEvent) {
        if (this.primengTableHelper.shouldResetPaging(event)) {
            this.paginator.changePage(0);
            return;
        }

        this.primengTableHelper.showLoadingIndicator();

        this._variantsServiceProxy.getAll(
            this.filterText,
            this.nameFilter,
            this.productIDFilter,
            this.maxPriceFilter == null ? this.maxPriceFilterEmpty: this.maxPriceFilter,
            this.minPriceFilter == null ? this.minPriceFilterEmpty: this.minPriceFilter,
            this.primengTableHelper.getSorting(this.dataTable),
            this.primengTableHelper.getSkipCount(this.paginator, event),
            this.primengTableHelper.getMaxResultCount(this.paginator, event)
        ).subscribe(result => {
            this.primengTableHelper.totalRecordsCount = result.totalCount;
            this.primengTableHelper.records = result.items;
            this.primengTableHelper.hideLoadingIndicator();
        });
    }

    reloadPage(): void {
        this.paginator.changePage(this.paginator.getPage());
    }

    createVariant(): void {
        this.createOrEditVariantModal.show();
    }

    deleteVariant(variant: VariantDto): void {
        this.message.confirm(
            '',
            (isConfirmed) => {
                if (isConfirmed) {
                    this._variantsServiceProxy.delete(variant.id)
                        .subscribe(() => {
                            this.reloadPage();
                            this.notify.success(this.l('SuccessfullyDeleted'));
                        });
                }
            }
        );
    }

    exportToExcel(): void {
        this._variantsServiceProxy.getVariantsToExcel(
        this.filterText,
            this.nameFilter,
            this.productIDFilter,
            this.maxPriceFilter == null ? this.maxPriceFilterEmpty: this.maxPriceFilter,
            this.minPriceFilter == null ? this.minPriceFilterEmpty: this.minPriceFilter,
        )
        .subscribe(result => {
            this._fileDownloadService.downloadTempFile(result);
         });
    }
    edit(variant:VariantDto){
        console.log(variant);
}
}
