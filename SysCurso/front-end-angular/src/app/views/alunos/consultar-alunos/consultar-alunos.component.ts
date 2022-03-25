import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CPFMask, TelMask, CelMask } from 'src/app/utils/mask/mask';
import { MatDialog } from '@angular/material/dialog';
import { FiltroAlunosComponent } from './filtro-alunos/filtro-alunos.component';
import { AlunoService } from 'src/app/services/aluno/aluno.service';
import { MatriculasComponent } from './matriculas/matriculas.component';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreActions, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { AuthService } from 'src/app/security/auth.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-consultar-alunos',
  templateUrl: './consultar-alunos.component.html',
  styleUrls: ['./consultar-alunos.component.scss']
})
export class ConsultarAlunosComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfMask = CPFMask;
  unidadesDefault = [];
  displayedColumns: string[] = [
    'unidade',
    'nome',
    'cpf',
    'email',
    'celular',
    'options'
  ];
  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  bodyRequest: any = {};
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private alunoService: AlunoService,
    private authService: AuthService,
    private store: Store<AlunoStoreState.Aluno>,
    private dialog: MatDialog,
    private router: Router,
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getAll(false);
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAll(filtrar) {
    this.isLoadingResults = false;
    this.error = false;

    const usuario = this.authService.getToken()

    this.bodyRequest.usuarioId = usuario?.user?.id
    this.bodyRequest.usuario

    if ((<HTMLInputElement>document.getElementById('verificarFiltraUnidade')).value == "1") {
      this.bodyRequest.FiltraUnidade = true;
    }

    if (filtrar) {
      this.isLoadingResults = true;
      this.alunoService.getAllFilter(this.bodyRequest)
        .subscribe(val => {
          this.isLoadingResults = false;
          if (val['status'] === 'error') this.error = true;
          else this.dataSource.data = val;
        });
    }

  }

  ajustarMaskTelefone(telefoneFixo: string): string {
    if (!telefoneFixo) return '';
    else if (telefoneFixo.length === 10) return TelMask;
    else if (telefoneFixo.length === 11) return CelMask;
    return '';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`alunos/cadastrar-aluno/adicionar/${id}`)
  }

  openFiltro(): void {
    const dialogRef = this.dialog.open(FiltroAlunosComponent, {
      width: '90vw',
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.bodyRequest = result;
        this.getAll(true);
      }
    });
  }

  openMatricula(id: string = '0', nome: string = '', elem?: any): void {
    this.store.dispatch(AlunoStoreActions.updateCadastroAluno({ payload: elem }));
    const dialogRef = this.dialog.open(MatriculasComponent, {
      width: '90vw',
      data: { id, nome },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getAll(true);
    });
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.alunoService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll(true);
      })
    })
  }
}
