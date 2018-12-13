import { ValidatorFn, FormGroup, AbstractControl } from '@angular/forms';

/**
* @method 
* @param cntName the control name in the fromGroup
* @param min minimum chars the control must contain
* @param max maximum chars the control can contain
* @param pattern the pattern of the string 
* @return array of validators
*/
export function stringValidatorArr(cntName: string, min?: number, max?: number, pattern?: RegExp): Array<ValidatorFn> {
  return [
      f => !f.value ? { 'val': `${cntName} is required` } : null,
      f => f.value && max && f.value.length > max ? { 'val': `'${cntName}' is max ${max} chars` } : null,
      f => f.value && min && f.value.length < min ? { 'val': `'${cntName}' is min ${min} chars` } : null,
      f => f.value && pattern && !f.value.match(pattern) ? { 'val': `'${cntName}' format is not correct` } : null
  ];
}

/**
* @method 
* @return array of validators
*/
export function passValidator(): Array<ValidatorFn> {
  return [null]
}

/**
* @method 
* @param cntName the control name in the fromGroup
* @param min min value the control must contain
* @param max max value the control can contain
* @return array of validators
*/
export function numberValidatorArr(cntName: string, min?: number, max?: number): Array<ValidatorFn> {
  return [
    f => f.value == undefined ? { 'val': `${cntName} is required` } : null,
    f => f.value && max != undefined && max != null && f.value > max ? { 'val': `${cntName} can be maximum ${max}` } : null,
    f => f.value && min != undefined && min != null && f.value < min ? { 'val': `${cntName} must be minimum ${min}` } : null,
  ];
}

export function confirmPasswordValidator(fromGroup: FormGroup): Array<ValidatorFn> {
  return [
    f => !f.value ? { 'val': '\'confirmPassword\'  is required' } : null,
    f => f.value && fromGroup.get('password').value != f.value ? { 'val': '\'confirmPassword\' doesn\'t not match to \'password\'' } : null
  ];
}

export function DateValidator(fromControlToCompare: any): Array<ValidatorFn> {
  let ctrlName: string;
  let dateToCompare: Date;

  if (fromControlToCompare == null) {
    ctrlName = 'start date';
    let date: Date = new Date();
    dateToCompare = new Date(date.getFullYear(), date.getMonth(), date.getDate());
  }
  else {
    ctrlName = 'end date';
    let date: Date = fromControlToCompare.value;
    if (!date)
      date = new Date();
    dateToCompare = new Date(date.getFullYear(), date.getMonth(), date.getDate());
  }
  return [
    f => !f.value ? { 'val': `'${ctrlName}'  is required` } : null,
    f => f.value && f.value < dateToCompare ? { 'val': `'${ctrlName}' can't be before ${dateToCompare.toDateString()}` } : null
  ];
  
}