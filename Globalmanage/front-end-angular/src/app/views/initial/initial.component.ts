import { Component, OnDestroy, OnInit, ChangeDetectorRef  } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/security/auth.service';
import { Store } from '@ngrx/store';
import { AlunoStoreActions, AlunoStoreState } from 'src/app/_store/aluno-store';
import { MatRadioChange } from '@angular/material/radio';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-initial',
  templateUrl: './initial.component.html',
  styleUrls: ['./initial.component.scss']
})
export class FirstPageAlunoComponent implements OnInit, OnDestroy {
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = [
    'select',
    'matricula',
    'unidade',
    'statusMatricula',
    'curso',
    'ano',
    'semestre',
    'financeiro',
    'options'];
  dataSource = new MatTableDataSource([{}]);
  nome: string = '';
  aluno: any = null;
  nacionalTech: boolean = true;
  alunoSelect: any = null;
  selection = new SelectionModel<MatTableDataSource<any>>(true, []);
  dadosMatricula: any = null;
  //variavel matricula: any seria receber qualquer coisa... null é nulo
  logoUnidade = '';
  tamanhoNomeCurso1 = false;
  tamanhoNomeCurso2 = false;
  idbody = false;

  constructor(
    private matriculaAlunoService: MatriculaAlunoService,
    private authService: AuthService,
    private store: Store<AlunoStoreState.Aluno>,
    private router: Router,
    private cdRef:ChangeDetectorRef
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    // //debugger

    this.getAluno();

  }

  ngAfterViewChecked(): void {
    var idLogoUnidade = (<HTMLInputElement>document.getElementById("idLogoUnidade"));
    if(idLogoUnidade != null)
    {
      if(this.dadosMatricula.length != 0)
      {
        for (var i = 0; i < this.dadosMatricula.length; i++)
        {
          if(this.dadosMatricula[i].unidadeId == 1)
            this.dadosMatricula[i].logo = "../../../assets/logo_escola_modelo.png";
          if(this.dadosMatricula[i].unidadeId == 2)
            this.dadosMatricula[i].logo = "../../../assets/logo_suporte_central.png";
          if(this.dadosMatricula[i].unidadeId == 3)
            this.dadosMatricula[i].logo = "../../../assets/logo_campinas.png";
          if(this.dadosMatricula[i].unidadeId == 4)
            this.dadosMatricula[i].logo = "../../../assets/logo_guarulhos.png";
          if(this.dadosMatricula[i].unidadeId == 5)
            this.dadosMatricula[i].logo = "../../../assets/logo_piracicaba.png";
          if(this.dadosMatricula[i].unidadeId == 6)
            this.dadosMatricula[i].logo = "../../../assets/logo_sorocaba.png";
          if(this.dadosMatricula[i].unidadeId == 7)
            this.dadosMatricula[i].logo = "../../../assets/logo_jundiai.png";
          if(this.dadosMatricula[i].unidadeId == 8)
            this.dadosMatricula[i].logo = "../../../assets/logo_santo_amaro.png";
          if(this.dadosMatricula[i].unidadeId == 9)
            this.dadosMatricula[i].logo = "../../../assets/logo_centro_sao_paulo.png";
          if(this.dadosMatricula[i].unidadeId == 10)
            this.dadosMatricula[i].logo = "../../../assets/logo_nacionaltec.png";
          if(this.dadosMatricula[i].unidadeId == 11)
            this.dadosMatricula[i].logo = "../../../assets/logo_supletivo_abc.png";

          if(this.dadosMatricula[i].unidadeId != 10)
          {
            this.dadosMatricula[i].curso = "Supletivo Preparatório";
          }
        }
      }
    }

    var nameBotaoAcessar = (document.getElementsByName("nameBotaoAcessar"));

    if(nameBotaoAcessar != null)
    {
      if(!this.tamanhoNomeCurso1)
      {

        if(this.dadosMatricula != null)
        {
          for (var i = 0; i < this.dadosMatricula.length; i++)
          {

            if(this.dadosMatricula[i].curso.length >= 23)
            {
              nameBotaoAcessar[i].style['margin-top'] = "13px";

            }
          }
          this.tamanhoNomeCurso1 = true;

        }
      }
    }

    var nomePCurso = (document.getElementsByName("nomePCurso"));

    if(nomePCurso != null)
    {
      if(!this.tamanhoNomeCurso2)
      {

        if(this.dadosMatricula != null)
        {
          for (var i = 0; i < this.dadosMatricula.length; i++)
          {

            if(this.dadosMatricula[i].curso.length >= 25)
            {
              nomePCurso[i].style['margin-top'] = "5px";

            }
          }
          this.tamanhoNomeCurso2 = true;

        }
      }
    }

    var idbody = (<HTMLInputElement>document.getElementById("body"));
    if(idbody != null)
    {
      if(this.dadosMatricula != null)
      {
        if(this.dadosMatricula.length < 2)
        {
          idbody.style['height'] = "100%";
        }
      }
      this.idbody = true;
    }

    this.cdRef.detectChanges();
  }

  ngOnDestroy(): void {
    // this.store.dispatch(AlunoStoreActions.deleteAluno());
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAluno(): void {
    // //debugger
    const token = this.authService.getToken();
    if(token != null)
    {
      this.aluno = token.user.aluno;
      this.nome = token.user.aluno.nome;

      window.localStorage.setItem('nomeAlunoLocalStorage', this.nome);

      console.log('token',token)
      this.nacionalTech
      this.store.dispatch(AlunoStoreActions.updateCadastroAluno({ payload: token.user.aluno}));
      this.getData(token.user.aluno.id);
    }
    else{
      this.router.navigate(['/login']);
    }
  }

  getData(id): void {
    this.isLoadingResults = true;
    const token = JSON.parse(window.localStorage.getItem('accessToken'));
    const usuarioLogadoId = token?.user.id;
    this.matriculaAlunoService.buscarMinhasMatriculas(id, usuarioLogadoId).subscribe(val => {
      if(val?.status == 'error') return;
      this.isLoadingResults = false;
      this.dadosMatricula = val;
      this.dataSource.data = val;
      this.adaptAlunoSelect(val);
    })
  }


  adaptAlunoSelect(matriculas: any[]): void {
    const alunoToken = this.getLocalStorageInMatricula();
    const find = matriculas.find(elem => elem.matriculaId === alunoToken?.matriculaId);
    if(find) this.alunoSelect = find;
    else {
      this.setLocalStorageInMatricula(matriculas[0]);
      this.alunoSelect = matriculas[0];
    }
  }

  goToMatricula(matriculaId: number, logo: string, curso: string, unidade: string, cursoId: string, alunoId: string): void {
    window.localStorage.setItem('matriculaIdLocalStorage', matriculaId.toString());
    window.localStorage.setItem('logoLocalStorage', logo);
    window.localStorage.setItem('cursoLocalStorage', curso);
    window.localStorage.setItem('unidadeLocalStorage', unidade);
    window.localStorage.setItem('cursoIdLocalStorage', cursoId);
    window.localStorage.setItem('alunoIdLocalStorage', alunoId);
    this.router.navigate(['/alunos/matricula-aluno'], { state: { matriculaId } });
  }

  changeMatricula(event: MatRadioChange): void {
    this.setLocalStorageInMatricula(event.value);
  }

  setLocalStorageInMatricula(data: any): void {
    window.localStorage.removeItem('matricula');
    window.localStorage.setItem('matricula', JSON.stringify(data));
  }

  getLocalStorageInMatricula(): any {
    return JSON.parse(window.localStorage.getItem('matricula'));
  }
}
