import { FormGroup, FormControl, Validators } from "@angular/forms";
import { MatSnackBar } from '@angular/material/snack-bar';
import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from 'src/app/security/auth.service';
import { CompareFields } from 'src/app/utils/form-validation/compare-fields.validation';
import { CelMask } from '../../utils/mask/mask';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  form: FormGroup = new FormGroup({
    email: new FormControl(null, [Validators.required, Validators.email]),
    password: new FormControl(null, [Validators.required]),
    confirmPassword: new FormControl(null, [Validators.required]),
    userName: new FormControl(null, [Validators.required]),
    birthdate: new FormControl(null, [Validators.required]),
    phone: new FormControl(null, [Validators.required]),
    unidade: new FormControl(null, [Validators.required])
  },
    CompareFields('password', 'confirmPassword')
  );

  // Percentage of password strength
  pwdStrength: number = 0;
  // Hide password
  pwdhide: boolean = true;
  // Hide password
  confirmPwdhide: boolean = true;
  // Cel Mask
  celMask = CelMask;

  unidadeUsuario : any[];

  constructor(
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar
  ) { }

  ngOnInit() {
    // //debugger
    window.localStorage.clear();
    // On form changes will check password strength
    this.form.get('password').valueChanges.subscribe(val => this.checkPasswordStrength(val));
  }

  forgotPassword(): void {
    this.snackBar.open('Verifique seu e-mail', 'Ok', { duration: 5000 ,panelClass: 'snackbar-success' });
  }

  /**Defines the password strength */
  checkPasswordStrength(pwd: string): void {
    let strengthPassed= 0;
    let numberOfRules = 0;

    // If pwd is null change to empty string
    if (!pwd || pwd == null || pwd == undefined) pwd = '';

    // All Rules
    let rule = {
      hasNumber:    pwd.match(/\d/)       ? true : false,
      hasLowerCase: pwd.match(/[a-z]/)    ? true : false,
      hasUpperCase: pwd.match(/[A-Z]/)    ? true : false,
      isBigLength:  pwd.match(/^.{10,}$/) ? true : false,
      specialChar:  pwd.match(/[^\w\*]/)  ? true : false
    }

    // Run through all rules
    for(let item in rule) {
      // Counts all validated rules
      if (rule[item]) strengthPassed++;
      // Count the number os rules
      numberOfRules++;
    }

    // Calculates the percentage of password strength
    this.pwdStrength = strengthPassed * (100 / numberOfRules)
  }

  login() {
    // //debugger
    const {userName, password} = this.form.value;
    this.authService.login(userName, password)
    .subscribe(val => {
        const isAluno = val?.user?.isAluno;
        const IsMenuAtendimento = val?.user?.isMenuAtendimento;
        this.unidadeUsuario = val?.user?.unidade;
        const route = IsMenuAtendimento ? 'menu-atendimento/m-a-home' :  isAluno ? 'initial' : 'ticket/initial'
        this.router.navigate([route]);
      })
    // this.authService.authenticate(userName, password)
    //   .subscribe(val => {
    //     console.log(val)
    //     this.router.navigate(['']);
    //   })
  }

  cadastrar() {
    this.authService.cadastrar(this.form.value)
    .subscribe(val => {
      // console.log(val)
        const isAluno = this.authService.isAluno();
        const route = isAluno ? 'initial' : 'ticket/initial'
        this.router.navigate([route]);
      })
  }
}
