import { FormGroup } from '@angular/forms';

/** Custom form validation */
export const CompareInitialEndHour = (field1Control: string, field2Control: string): any => {
  return (formGroup: FormGroup) => {
    // Controls
    let hour1 = formGroup.controls[field1Control].value;
    let hour2 = formGroup.controls[field2Control].value;
    
    hour1 = hour1 * 1;
    hour2 = hour2 * 1;
    
    // Compare dates
    if ((hour1 && hour2) && hour1 > hour2) {
      formGroup.controls[field2Control].setErrors({ InitialHourBigThenEndHour: true }); // Set error 
    }
  };
}