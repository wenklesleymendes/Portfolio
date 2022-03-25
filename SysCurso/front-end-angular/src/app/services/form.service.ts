import { Injectable } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl, FormArray } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormService {

  /**
   * @description Touch all fields
   * @param {FormGroup} formGroup 
   */
  validateAllFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) control.markAsTouched({ onlySelf: true })
      else if (control instanceof FormArray) control.markAllAsTouched()
      else if (control instanceof FormGroup) this.validateAllFields(control)
    });
  }

  /**
   * @description Touch all fields
   * @param {FormGroup} formGroup 
   */
  untuchAllFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsUntouched({ onlySelf: true });
        control.markAsPristine({ onlySelf: true });
        control.markAsUntouched();
        control.updateValueAndValidity({onlySelf: true, emitEvent: true});
      }
      else if (control instanceof FormGroup) this.untuchAllFields(control)
    });
  }

  /**
   * @description Disable all fields
   * @param {FormGroup} formGroup 
   */
  disableAllFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.setValidators([]);
        control.updateValueAndValidity();
        if (field === 'id' ) control.setValue(0);
        else if (field === 'isActive' ) control.setValue(false);
        else control.setValue(null);
        control.disable({emitEvent: false});
      }
      else if (control instanceof FormGroup) this.disableAllFields(control)
    });
  }

  /**
   * @description Disable field
   * @param {FormControl} control 
   */
  disableField(control: FormControl | AbstractControl, value: any = null) {
    control.setValidators([]);
    control.updateValueAndValidity();
    control.setValue(value);
    control.disable({emitEvent: false});
  }

  /**
   * @description Enable all fields
   * @param {FormGroup} formGroup 
   */
  enableAllFields(formGroup: FormGroup, required: boolean = true) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        if (required) control.setValidators([Validators.required]);
        control.updateValueAndValidity();
        control.enable();
      }
      else if (control instanceof FormGroup) this.enableAllFields(control)
    });
  }

  /**
   * @description Enable field
   * @param {FormControl} control 
   */
  enableField(control: FormControl | AbstractControl) {
    control.setValidators([Validators.required]);
    control.updateValueAndValidity();
    control.enable();
  }

  /**
   * @description Mandatory all fields
   * @param {FormGroup} formGroup 
   */
  notMandatoryAllFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.setValidators([]);
        control.updateValueAndValidity();
      }
      else if (control instanceof FormGroup) this.enableAllFields(control)
    });
  }

  /**
   * @description Mandatory all fields
   * @param {FormGroup} formGroup 
   */
  mandatoryAllFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.setValidators([Validators.required]);
        control.updateValueAndValidity();
      }
      else if (control instanceof FormGroup) this.enableAllFields(control)
    });
  }

  /**
   * @description Mandatory fields
   * @param {FormControl} control 
   */
  notMandatoryFields(control: FormControl | AbstractControl, validators: any = []) {
    control.setValidators(validators);
    control.updateValueAndValidity();
  }

  /**
   * @description Mandatory fields
   * @param {FormControl} control 
   */
  mandatoryFields(control: FormControl | AbstractControl, validators: any = []) {
    control.setValidators([Validators.required, ...validators]);
    control.updateValueAndValidity();
  }

}
