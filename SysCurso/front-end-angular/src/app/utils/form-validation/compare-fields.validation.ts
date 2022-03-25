import { FormGroup } from '@angular/forms';

/** Custom form validation */
export const CompareFields = (field1Control: string, field2Control: string): any => {
  return (formGroup: FormGroup) => {
    // Controls
    const field1 = formGroup.controls[field1Control];
    const field2 = formGroup.controls[field2Control];
    
    // Compare fields
    if (field1.value !== field2.value) {
      field2.setErrors({ differentField: true }); // Set error 
    }
  };
}