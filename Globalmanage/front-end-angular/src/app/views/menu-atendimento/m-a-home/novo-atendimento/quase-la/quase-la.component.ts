import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { FormService } from 'src/app/services/form.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { AtendimentoService } from 'src/app/services/Atendimento/Atendimento.service';
import { MatAccordion } from '@angular/material/expansion';
import { delay, map, startWith } from 'rxjs/operators';
import { AuthService } from 'src/app/security/auth.service';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-quase-la',
  templateUrl: './quase-la.component.html',
  styleUrls: ['./quase-la.component.scss']
})
export class QuaseLaComponent implements OnInit {
  [x: string]: any;
  @ViewChild(MatAccordion, { static: false }) accordion!: MatAccordion;

  form: FormGroup;
  error: boolean = false;
  isLoadingResults: boolean = true;
  id: number = 0;
  diaDoAgendamento: any[];
  idUnidade = 0;
  idUsuarioCadastro = 0;
  usuarioDefault = null;
  idAtendenteLogado = 0;
  usuariosUnidade: Observable<any[]>;
  showColaborador: boolean;

  constructor(
    private formService: FormService,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private routerActive: ActivatedRoute,
    private atendimentoService: AtendimentoService,
    private authService: AuthService,
    private usuarioService: UsuarioService,
    private router: Router,

    public dialogRef: MatDialogRef<QuaseLaComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ) {
    // Get id 
    const id = this.routerActive.snapshot.paramMap.get('id');
    this.id = id ? parseInt(id) : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // 
  ngOnInit(): void {

    if (this.data.dadosAlunoForm.diadoAgendamento != null) {
      this.diaDoAgendamento = this.data.dadosAlunoForm.diadoAgendamento._d;
      this.data.dadosAlunoForm.diadoAgendamento = this.diaDoAgendamento;
    }

    const token = this.authService.getToken();
    if (token != null) {
      this.idUnidade = token.user.unidade.id;
      this.data.dadosAlunoForm.usuarioLogado = token.user.id;
    }

    this.buildForm();
    this.getFiltarUsuario();

    Promise.all([])
      .then(() => {
        this.isLoadingResults = false;
        this.loadData();
      })
      .catch(() => this.isLoadingResults = false);
  }

  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      selecioneSeuLogin: [null, [Validators.required]]
    });

    this.form.get('selecioneSeuLogin').valueChanges.subscribe(val => {

      const usuarioId = this.usuarioDefault?.length >= 0 ? this.usuarioDefault.find(e => e.nome == val) : null;
      if (usuarioId && usuarioId.id) {
        this.form.get('usuarioId').setValue(usuarioId.id)
      }
      else {
        // this.form.get('usuarioId').setValue(null);
      }
    });
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  loadData(): void {

    if (this.id != 0) {

      this.isLoadingResults = true;

      this.atendimentoService.getPorId(this.id)
        .pipe(delay(750))
        .subscribe(val => {
          this.isLoadingResults = false;
          if (val['status'] === 'error') {
            this.error = true;
          }
          else {
            this.data.dadosAlunoForm = val;
            this.accordion.openAll();
          }
        })
    }
  }

  registrarAtendimento(): void {

    this.formService.validateAllFields(this.form);

    if (this.data.dadosAlunoForm) {

      this.data.dadosAlunoForm.usuarioCadastro = this.form.value.selecioneSeuLogin;
      this.data.dadosAlunoForm.unidadeCadastro = this.idUnidade;

      this.animationsService.showProgressBar(true);

      this.atendimentoService.cadastrar(this.data.dadosAlunoForm).subscribe(val => {
        if (val.length != 0) {

          const message = 'Cadastrado com sucesso';

          if (val['status'] == 'error') {
            return;
          }

          this.animationsService.showProgressBar(false);
          this.animationsService.showSuccessSnackBar(message);
          var resgistroSucesso = 'resgistroSucesso';

          if (resgistroSucesso === 'resgistroSucesso') {
            this.close();
            this.router.navigate(['menu-atendimento/m-a-home']);
          }
        }
      });
    }
    else {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
    }
  }

  getFiltarUsuario(): void {

    this.usuarioService.getBusarUsuarioPorUnidade({ unidadeId: this.idUnidade, ehAtendimento: true })
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          const usuario = this.authService.getToken();
          const data = val.filter(e => e?.id !== usuario?.user?.id);
          this.usuariosUnidade = data;
          this.showColaborador = true;
        }
      });
  }

  close(): void {
    this.dialogRef.close();
  }

}