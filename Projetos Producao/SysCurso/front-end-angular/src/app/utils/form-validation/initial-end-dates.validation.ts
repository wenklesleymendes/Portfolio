import { FormGroup } from '@angular/forms';

/** Custom form validation */
export const CompareInitialEndDate = (field1Control: string, field2Control: string): any => {
  return (formGroup: FormGroup) => {
    // Controls
    const value1 = formGroup.controls[field1Control].value
    const value2 = formGroup.controls[field2Control].value
    
    // Dates
    const date1 = new Date(value1);
    const date2 = new Date(value2);
    
    // Compare dates
    if ((value1 && value2) && date1 > date2) {
      formGroup.controls[field2Control].setErrors({ InitialDateBigThenEndDate: true }); // Set error 
    }
  };
}