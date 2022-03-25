import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder, FormArray, AbstractControl } from '@angular/forms';
import { ActivatedRoute, Router, Navigation } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { DeleteService } from 'src/app/services/delete.service';
import { AnexoService } from 'src/app/services/anexo.service';
import { PerguntaService } from 'src/app/services/aula-online/pergunta.service';
import { BehaviorSubject } from 'rxjs';
import {SelectionModel} from '@angular/cdk/collections';


@Component({
  selector: 'app-perguntas-online-individual',
  templateUrl: './perguntas-online-individual.component.html',
  styleUrls: ['./perguntas-online-individual.component.scss']
})
export class PerguntasOnlineIndividualComponent implements OnInit {
  form: FormGroup;
  respostas: FormArray;
  id: number = 0;
  videoId: number = 0;
  aulaOnlineId: number = 0;
  materiaId: number = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  nomeAula: string = null;
  respostaSelected: any = null;
  respostaCertaSelected: any = null;
  displayedColumns: string[] = ['opcao', 'descricao', 'correta', 'img', 'options'];
  dataSource = new BehaviorSubject<FormArray | AbstractControl>(null);
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private animationsService: AnimationsService,
    private perguntaService: PerguntaService,
    private deleteService: DeleteService,
    private anexoService: AnexoService,
    private routerActive: ActivatedRoute,
    private router: Router,
    private formService: FormService
  ) {
    // Get id
    const videoId = this.routerActive.snapshot.paramMap.get('videoId');
    this.videoId = videoId ? parseInt(videoId) : 0;
    const aulaOnlineId = this.routerActive.snapshot.paramMap.get('aulaOnlineId');
    this.aulaOnlineId = aulaOnlineId ? parseInt(aulaOnlineId) : 0;
    const materiaId = this.routerActive.snapshot.paramMap.get('materiaId');
    this.materiaId = materiaId ? parseInt(materiaId) : 0;
    const perguntaId = this.routerActive.snapshot.paramMap.get('perguntaId');
    this.id = perguntaId ? parseInt(perguntaId) : 0;
    // Get State
    const currentNavigation: Navigation = this.router.getCurrentNavigation();
    if (currentNavigation && currentNavigation.extras && currentNavigation.extras.state) {
      const state = currentNavigation.extras.state;
      this.nomeAula = state.nomeAula;
    }
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.loadData();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      descricaoPergunta: [null],
      videoAulaId: [this.videoId],
      anexoId: [null],
      extensao: [null],
      arquivoString: [null],
      resposta: this.fb.array([])
    })

    this.respostas = this.form.get('resposta') as FormArray;
    this.addResposta();

    this.form.get('id').setValue(this.id);
  }

  loadData(): void {
    if(this.id === 0) return;
    this.isLoadingResults = true;
    this.perguntaService.getPorId(this.id).subscribe(val => {
      if (!val || val?.status === 'error') return this.error = true;
      
      const patch: any = {};
      for(let key in val) {
        if (val[key]) patch[key] = val[key];
      }
      this.form.patchValue(patch);
      const { resposta } = val;

      if(resposta?.length > 0) {
        this.respostas.clear();
        resposta.forEach((elem, index) => {
          const { id, descricao, opcao, correta, perguntaId, anexoId, extensao, arquivoString } = elem;
          this.respostas.push(
            this.fb.group({
              id: [id],
              descricao: [descricao],
              opcao: [opcao],
              correta: [correta],
              perguntaId: [perguntaId],
              anexoId: [anexoId],
              extensao: [extensao],
              arquivoString: [arquivoString],
            })
          );
          if(correta) this.respostaCertaSelected = index;
        });
        this.dataSource.next(null);
        this.dataSource.next(this.respostas);
      }
      this.isLoadingResults = false;
    })
  }

  getControls(): any{
    return this.respostas.controls;
  }

  addResposta(): void {
    this.respostas.push(
      this.fb.group({
        id: [0],
        descricao: [null],
        opcao: [null],
        correta: [false],
        perguntaId: [0],
        arquivoString: [null],
        extensao: [null],
        anexoId: [null]
      })
    );

    this.dataSource.next(null);
    this.dataSource.next(this.respostas);
  }

  getImgResposta(i): string {
    return this.respostas.at(i).get('imagem').value;
  }

  showDownload(control, index?:number): boolean {
    if(index !== null && index !== undefined) {
      const data = control.at(index).value;
      const { anexoId } = data;
      return anexoId ? true : false;
    } else {
      const { anexoId } = control.value;
      return (anexoId) ? true : false;
    }
  }

  hasIdResposta(control, index): boolean {
    const data = control.at(index).value;
    return data?.id > 0;
  }

  hasImgResposta(control, index): boolean {
    const data = control.at(index).value;
    return data?.anexoId > 0;
  }

  setResposta(constrol, index) {
    this.respostaSelected = constrol.at(index).value;
  }

  setCertaResposta(index) {
    this.respostaCertaSelected = index;
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  salvarData(): void {
    // Validating form
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const formValue: any = this.form.value;
    if(formValue?.resposta) {
      const data = formValue.resposta.map((elem, index) => {
        elem.correta = (index === this.respostaCertaSelected) ? true : false;
        return elem;
      });
      formValue.resposta = data;
    }
    // Make request
    this.perguntaService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
        this.form.get('id').setValue(id);
        this.loadData();
      }
    })
  }

  loadImgQuestao(event): void {
    let reader = new FileReader();
    // Select file
    let file = event.target.files[0];
    // Render file
    reader.readAsArrayBuffer(file);
    
    reader.onloadend = e => {
      const formData: FormData = new FormData();
      formData.append('file', file);
      formData.append('descricao', '');
      formData.append('tipoAnexo', '0');

      // RESPOSTAS
      if(this.respostaSelected) {
        formData.append('RespostaId', this.respostaSelected.id.toString());
        // UPDATE
        if(this.respostaSelected.anexoId) {
          this.anexoService.deletarAnexo(this.respostaSelected.anexoId).subscribe(val => {
            this.anexoService.upload(formData).subscribe(val => {
              if (val?.status == 'done') {  }
              this.loadData();
            });
          });
          // CREATE
        } else {
          this.anexoService.upload(formData).subscribe(val => {
            if (val?.status == 'done') {  }
            this.loadData();
          });
        }
        // PERGUNTA
      } else {
        formData.append('PerguntaId', this.id.toString());
        // UPDATE
        if(this.form.get('anexoId').value) {
          this.anexoService.deletarAnexo(this.form.get('anexoId').value).subscribe(val => {
            this.anexoService.upload(formData).subscribe(val => {
              if (val?.status == 'done') {  }
              this.loadData();
            });
          });
          // CREATE
        } else {
          this.anexoService.upload(formData).subscribe(val => {
            if (val?.status == 'done') {  }
            this.loadData();
          });
        }
      }

      this.respostaSelected = null;
    };
  }

  removeDoFormArray(controls: any, index: number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      controls as FormArray;
      controls.removeAt(index);
      this.dataSource.next(null);
      this.dataSource.next(this.respostas);
    });
  }

  downloadResposta(controls: any, index: number): void {
    const data = controls.at(index).value;
    const { anexoId, arquivoString, extensao } = data;
    this.anexoService.download(anexoId, arquivoString, extensao);
  }

  downloadQuestao(): void {
    const { anexoId, arquivoString, extensao } = this.form.value;
    this.anexoService.download(anexoId, arquivoString, extensao);
  }

  deleteImg(control, index?:number): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      if(index !== null && index !== undefined) {
        const data = control.at(index).value;
        const { anexoId } = data;
        this.anexoService.deletarAnexo(anexoId).subscribe(val => {
          this.loadData();
        });
      } else {
        const { anexoId } = control.value;
        this.anexoService.deletarAnexo(anexoId).subscribe(val => {
          this.loadData();
        });
      }
    });
  }
}
