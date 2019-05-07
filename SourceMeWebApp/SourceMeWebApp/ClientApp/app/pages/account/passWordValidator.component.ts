import { FormGroup } from '@angular/forms';

export class PassWordValidator {
    static validate(passwordFormGroup: FormGroup) {
        let password = passwordFormGroup.controls.password.value;
        let repeatPassword = passwordFormGroup.controls.confirmPassword.value;

        if (repeatPassword.length <= 0) {
            return null;
        }

        if (repeatPassword !== password) {
            return {
                doesMatchPassword: true
            };
        }

        return null;

    }
}