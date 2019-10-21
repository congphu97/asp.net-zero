import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { OrdersComponent } from './example2/orders/orders.component';
import { ViewOrderModalComponent } from './example2/orders/view-order-modal.component';
import { CreateOrEditOrderModalComponent } from './example2/orders/create-or-edit-order-modal.component';

import { VariantsComponent } from './example2/variants/variants.component';
import { ViewVariantModalComponent } from './example2/variants/view-variant-modal.component';
import { CreateOrEditVariantModalComponent } from './example2/variants/create-or-edit-variant-modal.component';

import { ProductsComponent } from './example2/products/products.component';
import { ViewProductModalComponent } from './example2/products/view-product-modal.component';
import { CreateOrEditProductModalComponent } from './example2/products/create-or-edit-product-modal.component';

import { CategoriesComponent } from './example2/categories/categories.component';
import { ViewCategoryModalComponent } from './example2/categories/view-category-modal.component';
import { CreateOrEditCategoryModalComponent } from './example2/categories/create-or-edit-category-modal.component';

import { PersonsComponent } from './example/persons/persons.component';
import { ViewPersonModalComponent } from './example/persons/view-person-modal.component';
import { CreateOrEditPersonModalComponent } from './example/persons/create-or-edit-person-modal.component';

import { CarsComponent } from './example/cars/cars.component';
import { ViewCarModalComponent } from './example/cars/view-car-modal.component';
import { CreateOrEditCarModalComponent } from './example/cars/create-or-edit-car-modal.component';

import {ButtonModule} from 'primeng/button';
import { ListsComponent } from './example/lists/lists.component';
import { ViewListModalComponent } from './example/lists/view-list-modal.component';
import { CreateOrEditListModalComponent } from './example/lists/create-or-edit-list-modal.component';


import { AutoCompleteModule } from 'primeng/primeng';
import { PaginatorModule } from 'primeng/primeng';
import { EditorModule } from 'primeng/primeng';
import { InputMaskModule } from 'primeng/primeng';import { FileUploadModule } from 'primeng/primeng';
import { TableModule } from 'primeng/table';

import { UtilsModule } from '@shared/utils/utils.module';
import { CountoModule } from 'angular2-counto';
import { ModalModule, TabsModule, TooltipModule, BsDropdownModule, PopoverModule } from 'ngx-bootstrap';
import { DashboardComponent } from './dashboard/dashboard.component';
import { MainRoutingModule } from './main-routing.module';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { BsDatepickerModule, BsDatepickerConfig, BsDaterangepickerConfig, BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxBootstrapDatePickerConfigService } from 'assets/ngx-bootstrap/ngx-bootstrap-datepicker-config.service';


NgxBootstrapDatePickerConfigService.registerNgxBootstrapDatePickerLocales();
import {DialogModule} from 'primeng/dialog';
import {SplitButtonModule} from 'primeng/splitbutton';
import { NgMultiSelectDropDownModule } from 'ng-multiselect-dropdown';

import { AngularMultiSelectModule } from 'angular2-multiselect-dropdown';

import { SpinnerComponent } from './example/cars/spinner.component';
import { CreateVariantComponent } from './example2/variants/create-variant.component';
import { EditVariantComponent } from './example2/variants/edit-variant.component';

 import $ from 'jquery';
import { ConfirmComponent } from './example2/orders/confirm.component';
 
@NgModule({
    imports: [
		FileUploadModule,
		AutoCompleteModule,
		PaginatorModule,
		EditorModule,
		InputMaskModule,		TableModule,
        DialogModule,
        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        NgxChartsModule,
        BsDatepickerModule.forRoot(),
        BsDropdownModule.forRoot(),
        PopoverModule.forRoot(),
        SplitButtonModule,
        NgMultiSelectDropDownModule.forRoot(),
        AngularMultiSelectModule,
        ButtonModule,
        FormsModule,
        ReactiveFormsModule,
        DialogModule
    ],
    declarations: [
		OrdersComponent,
		ViewOrderModalComponent,		CreateOrEditOrderModalComponent,
		VariantsComponent,
		ViewVariantModalComponent,		CreateOrEditVariantModalComponent,
		ProductsComponent,
		ViewProductModalComponent,		CreateOrEditProductModalComponent,
		CategoriesComponent,
		ViewCategoryModalComponent,		CreateOrEditCategoryModalComponent,
		PersonsComponent,
		ViewPersonModalComponent,		CreateOrEditPersonModalComponent,
		CarsComponent,
		ViewCarModalComponent,		CreateOrEditCarModalComponent,
		CarsComponent,
		ViewCarModalComponent,		CreateOrEditCarModalComponent,
		CarsComponent,
		ViewCarModalComponent,		CreateOrEditCarModalComponent,
		ListsComponent,
		ViewListModalComponent,		CreateOrEditListModalComponent,
		CarsComponent,
		ViewCarModalComponent,		CreateOrEditCarModalComponent,
        DashboardComponent, SpinnerComponent,
        CreateVariantComponent, EditVariantComponent,ConfirmComponent
      

    ],
    providers: [
        { provide: BsDatepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerConfig },
        { provide: BsDaterangepickerConfig, useFactory: NgxBootstrapDatePickerConfigService.getDaterangepickerConfig },
        { provide: BsLocaleService, useFactory: NgxBootstrapDatePickerConfigService.getDatepickerLocale }
    ]
})
export class MainModule { }
