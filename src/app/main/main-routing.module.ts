import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { OrdersComponent } from './example2/orders/orders.component';
import { VariantsComponent } from './example2/variants/variants.component';
import { ProductsComponent } from './example2/products/products.component';
import { CategoriesComponent } from './example2/categories/categories.component';
import { PersonsComponent } from './example/persons/persons.component';
import { ListsComponent } from './example/lists/lists.component';
import { CarsComponent } from './example/cars/cars.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { CreateVariantComponent } from './example2/variants/create-variant.component';
import { EditVariantComponent } from './example2/variants/edit-variant.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '',
                children: [
                    { path: 'example2/orders', component: OrdersComponent, data: { permission: 'Pages.Orders' }  },
                    { path: 'example2/variants', component: VariantsComponent, data: { permission: 'Pages.Variants' }  },
                    { path: 'example2/createVariant', component: CreateVariantComponent },
                    { path: 'editVariant/:id', component: EditVariantComponent },
                    { path: 'example2/products', component: ProductsComponent, data: { permission: 'Pages.Products' }  },
                    { path: 'example2/categories', component: CategoriesComponent, data: { permission: 'Pages.Categories' }  },
                    { path: 'example/persons', component: PersonsComponent, data: { permission: 'Pages.Persons' }  },
                    { path: 'example/lists', component: ListsComponent, data: { permission: 'Pages.Lists' }  },
                    { path: 'example/cars', component: CarsComponent, data: { permission: 'Pages.Cars' }  },
                    { path: 'dashboard', component: DashboardComponent, data: { permission: 'Pages.Tenant.Dashboard' } }
                ]
            }
        ])
    ],
    exports: [
        RouterModule
    ]
})
export class MainRoutingModule { }
