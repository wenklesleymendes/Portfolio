import { Component, OnInit, OnDestroy, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatTableDataSource } from '@angular/material/table';
import { AnexoService } from 'src/app/services/anexo.service';
import { DeleteService } from 'src/app/services/delete.service';
import { AnimationsService } from 'src/app/services/animations.service';
import { Anexos } from 'src/app/utils/variables/anexos';
import { Store, select } from '@ngrx/store';
import { AlunoStoreState, AlunoStoreSelectors } from 'src/app/_store/aluno-store';
import { Subscription } from 'rxjs';
import { Navigation, Router } from '@angular/router';
import { DocumentoAlunoService } from 'src/app/services/aluno/documento-aluno.service';
import { MatDialog } from '@angular/material/dialog';
import { MsgRecusarComponent } from './msg-recusar/msg-recusar.component';
import { MsgDocumentoAlunoComponent } from './msg-documento-aluno/msg-documento-aluno.component';
import { InconsistenciaDocumentoService } from 'src/app/services/aluno/inconsistencia-documento.service';
import { AuthService } from 'src/app/security/auth.service';
import { SelectionModel } from '@angular/cdk/collections';
import { MatriculaAlunoService } from 'src/app/services/aluno/matricula-aluno.service';

@Component({
  selector: 'app-aluno-documentos',
  templateUrl: './aluno-documentos.component.html',
  styleUrls: ['./aluno-documentos.component.scss']
})
export class AlunoDocumentosComponent implements OnInit, OnDestroy {
  @Output() updateMatricula: EventEmitter<any> = new EventEmitter();
  error: boolean = false;
  isLoadingResults: boolean = false;
  form: FormGroup;
  displayedColumns: string[] = ['tipoAnexo', 'dataAnexo', 'options'];
  dataSource = new MatTableDataSource([]);
  nome: string = '';
  sendingAnexo: boolean = false;
  anexos = Anexos;
  documentosPendentes: any[] = [];
  documentosExcessao: number[] = [0,1,2,3,4,5,10,11,12,13,14,15,16,17,18,26,33];
  documentosExibir: any[] = [];
  documentosExibirTabela: any[] = [];
  matriculaAluno: any;
  matriculaId: number = null;
  cadastroAluno: any;
  matriculaAluno$: Subscription;
  cadastroAluno$: Subscription;
  declaracaoPendenciaDocumental: boolean = true;
  documentosInconsistenteAll: any[] = [];
  documentosInconsistente: any[] = [];
  inconsistentes: FormControl = new FormControl([]);
  isAluno: boolean = false;
  selection = new SelectionModel<MatTableDataSource<any>>(true, []);

  nomeAluno = "";
  logoLocalStorage = "";
  cursoLocalStorage = "";
  unidadeLocalStorage = "";

  constructor(
    private fb: FormBuilder,
    private dialog: MatDialog,
    private anexoService: AnexoService,
    private animationService: AnimationsService,
    private deleteService: DeleteService,
    private documentoAlunoService: DocumentoAlunoService,
    private store: Store<AlunoStoreState.Aluno>,
    private inconsistenciaDocumentoService: InconsistenciaDocumentoService,
    private router: Router,
    private authService: AuthService,
    private matriculaAlunoService: MatriculaAlunoService
  ) {
    // Get State
    this.logoLocalStorage = window.localStorage.getItem('logoLocalStorage');
    this.cursoLocalStorage = window.localStorage.getItem('cursoLocalStorage');
    this.unidadeLocalStorage = window.localStorage.getItem('unidadeLocalStorage');
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.matriculaId = state.matriculaId;
    }
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {

    this.isAluno = this.authService.isAluno();

    if(this.matriculaId == null && this.isAluno == true)
    {
      this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
    }

    this.matriculaAluno$ = this.store.pipe(select(AlunoStoreSelectors.selectMatriculaAluno)).subscribe(val => this.matriculaAluno = val);
    this.cadastroAluno$ = this.store.pipe(select(AlunoStoreSelectors.selectCadastroAluno)).subscribe(val => this.cadastroAluno = val);

    if(this.matriculaId == null)
      this.matriculaId = this.matriculaAluno?.id;

    this.buildForm();
    this.loadData();
    this.pegarNome();
  }

  async pegarNome(): Promise<void>{
  if(this.matriculaId != null)
    {
      await this.matriculaAlunoService.buscarPorId(this.matriculaId).subscribe(async val => {
        if (val['status'] === 'error')
        {
          this.error = true;
        }
        else
        {
          this.nomeAluno = val.aluno.nome;
        }
      });
    }
  }

  ngOnDestroy(): void {
    this.matriculaAluno$.unsubscribe();
    this.cadastroAluno$.unsubscribe();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      tipoAnexo: [0]
    });
  }

  async loadData(): Promise<void> {
    this.isLoadingResults = true;
    await this.getAnexo();
    await this.consultarDocumentosPendentes();
    await this.getIconsistenciaAll();
    await this.getIconsistencia();
    this.isLoadingResults = false;
  }

  consultarDocumentosPendentes(): Promise<any> {
    return new Promise((resolve) => {
      this.documentoAlunoService.consultarDocumentosPendentes(this.matriculaId).subscribe(val => {
        if(val?.status) return;
        this.documentosPendentes = val?.documentosPendentes;
        this.declaracaoPendenciaDocumental = val?.declaracaoPendenciaDocumental;
        this.documentosExibir = this.anexos.filter(elem => this.documentosExcessao.find(excessao => elem.value == excessao) ? null : elem);
        this.tipoDocumentosExibir();
        resolve(val);
      })
    });
  }

  tipoDocumentosExibir() : void {
    var documentosExibirValores = [];
    for (var i = 0; i < this.documentosExibir.length; i++) {
      documentosExibirValores.push(this.documentosExibir[i].value);
    }

    var dataSourceValores = [];
    for (var i = 0; i < this.documentosExibirTabela.length; i++) {
      if (!this.documentosExibirTabela[i].isRecusado)
        dataSourceValores.push(this.documentosExibirTabela[i].tipoAnexo);
    }

    var documentosExibirIDs = documentosExibirValores.filter(function (objeto) {
      return dataSourceValores.indexOf(objeto) == -1
    });

    this.documentosExibir = this.documentosExibir.filter(x => documentosExibirIDs.find(y => y == x.value));

    if (dataSourceValores.find(x => x == 0) != 0)
      this.documentosExibir.unshift({ label: "Outros", value: 0 });

    this.form.get('tipoAnexo').setValue(null);
  }

  getAnexo(): Promise<any> {
    return new Promise((resolve) => {
      this.anexoService.getAnexo({ matriculaAlunoId: this.matriculaId}).subscribe(val => {
        this.dataSource.data = val;
        this.documentosExibirTabela = val;
        resolve(val);
      })
    });
  }

  getIconsistenciaAll(): Promise<any> {
    return new Promise((resolve) => {
      this.inconsistenciaDocumentoService.getAll(this.matriculaId).subscribe((docs: number[]) => {
        this.documentosInconsistenteAll = this.anexos.filter(elem => docs.find(elemDoc => elem.value == elemDoc) ? elem : null);
        resolve(docs);
      })
    });
  }

  getIconsistencia(): Promise<any> {
    return new Promise((resolve) => {
      this.inconsistenciaDocumentoService.buscarPorMatriculaId(this.matriculaId).subscribe((val: any[]) => {
        this.inconsistentes.setValue(val.map(elem => elem.documentoEnum));
        resolve(val);
      })
    });
  }

  labelPendente(id): string {
    let label = this.anexos.find(anexo => anexo.value == id ).label;
    return label ? label : '-';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------

  next(): void {

  }

  removeAnexo(id, index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      const dataOriginal = this.dataSource.data;
      let data = this.dataSource.data;
      data.splice(index, 1);
      this.dataSource.data = data;

      this.anexoService.deletarAnexo(id).subscribe(val => {
        if (!val) this.dataSource.data = dataOriginal;
        this.updateMatricula.emit(new Date());
        this.getAnexo();
        this.consultarDocumentosPendentes();
      })
    })
  }

  recusar(element: any): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(MsgRecusarComponent, {
      width: isMobileResolution ? '98vw' : '50vw',
      data: { element },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {
      this.loadData();
      this.updateMatricula.emit(new Date());
    });
  }

  opemMsg(mensagem, tipoRecusa): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;

    const dialogRefMsg = this.dialog.open(MsgDocumentoAlunoComponent, {
      width: isMobileResolution ? '98vw' : '30vw',
      data: {
        mensagem,
        tipoRecusa
      },
      autoFocus: false
    });
    dialogRefMsg.afterClosed().subscribe(result => {

    });
  }

  /**
   * @description Get file
   * @param {event} event - Input's event
   */
  loadFile(event): void {
    let reader = new FileReader();
    // Select file
    let file: File = event.target.files[0];

    const maxSize: number = 50*1024*1024;
    if(file?.size > maxSize) {
      this.animationService.showErrorSnackBar('Arquivo ultrapassa 50mb');
      return;
    }

    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      this.sendingAnexo = true;
      const formData: FormData = new FormData();
      // Set FormData
      formData.append('file', file);
      formData.append('tipoAnexo', this.form.get('tipoAnexo').value);
      formData.append('MatriculaAlunoId', this.matriculaId.toString());

      this.anexoService.upload(formData).subscribe(val => {
        if (val && val['status'] && val['status'] == 'done') {
          // console.log('VAL', val)
          this.sendingAnexo = false;
          let data = this.dataSource.data;
          data.push({ dataAnexo: new Date(), id: val['response']['id'], tipoAnexo: val['response']['tipoAnexo'] });
          this.dataSource.data = data;
          this.form.get('tipoAnexo').setValue(0);
          this.getAnexo();
          this.consultarDocumentosPendentes();
          this.updateMatricula.emit(new Date());
        }
        if(val?.status == 'error') this.sendingAnexo = false;

      });
    };
  }

  download(id, arquivoString, extensao, idTipoAnexo): void {
    const nameFile: string = arquivoString;
    const partsNameFile: string[] = nameFile.split('.');
    const extensaoFile: string = partsNameFile[(partsNameFile.length - 1)];
    // let nameAluno: string = this.cadastroAluno?.nome;
    let nameAluno: string = this.nomeAluno;
    nameAluno = nameAluno.replace(new RegExp(' ', 'g'), '_');
    let nameTipoAnexo: string = this.anexos.find(anexo => anexo.value === idTipoAnexo ).label;
    nameTipoAnexo = nameTipoAnexo.replace(new RegExp(' ', 'g'), '_');

    const nameDownloadFile = `${nameAluno}-${nameTipoAnexo}.${extensaoFile}`;

    this.anexoService.download(id, nameDownloadFile, extensao);
  }

  gerarDeclaracao(): void {
    this.documentoAlunoService.gerarPendenciaDocumental(this.matriculaId);
  }

  uploadDeclaracao(event): void {
    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsArrayBuffer(file);

    reader.onloadend = () => {
      if(file.type != 'application/pdf') {
        this.animationService.showErrorSnackBar('Insira somente arquivo PDF.');
        return;
      }

      this.sendingAnexo = true;
      const formData: FormData = new FormData();
      // Set FormData
      formData.append('file', file);
      formData.append('MatriculaAlunoId', this.matriculaId.toString());

      this.documentoAlunoService.upload(formData).subscribe(val => {
        this.consultarDocumentosPendentes();
        this.sendingAnexo = false;
      });
    };
  }

  downloadDeclaracao(): void {
    this.documentoAlunoService.download(this.matriculaId);
  }

  visualizarDeclaracao(): void {
    this.documentoAlunoService.imprimir(this.matriculaId);
  }

  salvarPendencia(): void {
    const data = { matriculaId: this.matriculaId, tipoAnexoEnum: this.inconsistentes.value }
    this.inconsistenciaDocumentoService.cadastrar(data).subscribe(val => {
      this.loadData();
      this.updateMatricula.emit(new Date());
    });
  }

  gotoMinhasAulas(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-curso-turma'], { state: { matriculaId } });
  }

  goToFinaceiro(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-financeiro-contrato'], { state: { matriculaId } });
  }

  goToSolicitacoes(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-solicitacoes'], { state: { matriculaId } });
  }

  goToDocumentos(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-documentos'], { state: { matriculaId } });
  }

  goToEja(matriculaId: number): void {
    this.router.navigate(['/alunos/eja-encceja'], { state: { matriculaId } });
  }
}
