import { Component, OnInit, Injector } from '@angular/core';
import { VariantsServiceProxy, CreateOrEditVariantDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
    selector: 'create-variant',
    templateUrl: './create-variant.component.html'

})
export class CreateVariantComponent extends AppComponentBase implements OnInit {
    constructor(
        injector: Injector,
        private _variantsServiceProxy: VariantsServiceProxy
    ) {
        super(injector);
    }

    active = false;
    saving = false;
    variant: CreateOrEditVariantDto = new CreateOrEditVariantDto();

    ngOnInit(): void { }
    save(): void {
        this.saving = true;


        this._variantsServiceProxy.createOrEdit(this.variant)
            .pipe(finalize(() => { this.saving = false; }))
            .subscribe(() => {
                this.notify.info(this.l('SavedSuccessfully'));


            });
    }
}
