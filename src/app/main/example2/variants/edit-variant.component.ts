import { Component, OnInit, Injector, Input, ViewChild } from '@angular/core';
import { VariantsServiceProxy, CreateOrEditVariantDto, VariantDto } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';
import { AppComponentBase } from '@shared/common/app-component-base';
import { ModalDirective } from 'ngx-bootstrap';
import { VariantService } from '@shared/service-proxies/variant.service';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'edit-variant',
    templateUrl: './edit-variant.component.html'

})
export class EditVariantComponent extends AppComponentBase implements OnInit {
    @ViewChild('createOrEditModal', { static: true }) modal: ModalDirective;
  
    constructor(
        injector: Injector,
        private _variantsServiceProxy: VariantsServiceProxy,
        private activeRoute: ActivatedRoute,
        private variantService:VariantService
    ) { 
        super(injector);
    }
    
    active = false;
    saving = false;
    variant: CreateOrEditVariantDto = new CreateOrEditVariantDto();
    @Input() dataEdit:VariantDto;
    ngOnInit(): void {
        const id = this.activeRoute.snapshot.params['id'];
        this._variantsServiceProxy.getVariantForView(id).subscribe((data) => {
          this.variant = data.variant;
        });
     }
    save(): void {
        this.saving = true;

        
        this._variantsServiceProxy.createOrEdit(this.variant)
         .pipe(finalize(() => { this.saving = false;}))
         .subscribe(() => {
            this.notify.info(this.l('SavedSuccessfully'));
            
       
         });
}



}
