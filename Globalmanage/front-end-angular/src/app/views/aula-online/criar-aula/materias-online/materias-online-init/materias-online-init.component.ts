import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MatTableDataSource } from '@angular/material/table';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DeleteService } from 'src/app/services/delete.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AnimationsService } from 'src/app/services/animations.service';
import { FormService } from 'src/app/services/form.service';
import { VideoAulaService } from 'src/app/services/aula-online/video-aula.service';
import { MatDialog } from '@angular/material/dialog';
import { PreviewVideoComponent } from './preview-video/preview-video.component';
import {SelectionModel} from '@angular/cdk/collections';

@Component({
  selector: 'app-materias-online-init',
  templateUrl: './materias-online-init.component.html',
  styleUrls: ['./materias-online-init.component.scss']
})
export class MateriasOnlineInitComponent implements OnInit {
  form: FormGroup;
  id: number = 0;
  materia: string = null;
  aulaOnlineId: number = 0;
  materiaId: number = 0;
  error: boolean = false;
  isLoadingResults: boolean = false;
  displayedColumns: string[] = ['tituloAula', 'urlVideo', 'options'];
  dataSource = new MatTableDataSource();
  selection = new SelectionModel<any>(true, []);

  constructor(
    private location: Location,
    private fb: FormBuilder,
    private deleteService: DeleteService,
    private videoAulaService: VideoAulaService,
    private dialog: MatDialog,
    private animationsService: AnimationsService,
    private router: Router,
    private routerActive: ActivatedRoute,
    private formService: FormService
  ) {
    // Get id
    const aulaOnlineId = this.routerActive.snapshot.paramMap.get('aulaOnlineId');
    this.aulaOnlineId = aulaOnlineId ? parseInt(aulaOnlineId) : 0;
    const materiaId = this.routerActive.snapshot.paramMap.get('materiaId');
    this.materiaId = materiaId ? parseInt(materiaId) : 0;
  }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit(): void {
    this.buildForm();
    this.getTabela()
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  buildForm(): void {
    this.form = this.fb.group({
      id: [0],
      tituloAula: [null],
      urlVideo: [null],
      materiaId: [this.materiaId]
    });
  }

  getTabela(): void {
    this.videoAulaService.buscarPorMateria(this.materiaId)
      .subscribe(val => {
        if (val['status'] === 'error') {
          this.error = true;
        }
        else {
          const { titulo, lista } = val;
          this.materia = titulo;
          this.dataSource.data = lista;
        }
      });
  }

  resetForm(): void {
    this.form.reset();
    this.form.get('id').setValue(0);
    this.form.get('materiaId').setValue(this.materiaId);
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  voltar(): void {
    this.location.back();
  }

  goPerguntas(id: string = '0', nome: string = null): void {
    this.router.navigateByUrl(`aula-online/materias-online/${this.aulaOnlineId}/${this.materiaId}/${id}`, { state: { nomeAula: nome } });
  }

  openPreview(id: number = 0, nome: string = '', url: string = ''): void {
    const isMobileResolution = window.innerWidth < 768 ? true : false;
    const dialogRef = this.dialog.open(PreviewVideoComponent, {
      width: isMobileResolution ? '98vw' : '75vw',
      data: { id, nome, url, preview: true },
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe(result => { });
  }

  salvarData(): void {
    this.formService.validateAllFields(this.form);
    if (!this.form.valid) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    const formValue = this.form.value;
    const { tituloAula, urlVideo } = formValue;
    if(!tituloAula && !urlVideo) {
      this.animationsService.showErrorSnackBar('Preencha todos os campos obrigatórios');
      return;
    }

    // Make request
    this.videoAulaService.cadastrar(formValue).subscribe(val => {
      if (val && !val['status']) {
        this.animationsService.showSuccessSnackBar(this.form.get('id').value == 0 ? 'Incluido com sucesso' : 'Salvo com sucesso');
        const { id } = val;
        this.id = id;
      }
      this.getTabela();
      this.resetForm();
    })
  }

  editar(elem): void {
    this.form.patchValue(elem);
    const codigoURL = elem.urlVideo.split('vimeo.com/')[1];
    this.form.get('urlVideo').setValue(codigoURL);
  }

  delete(id): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.videoAulaService.deletePorId(id).subscribe(val => {
        this.getTabela();
      });
    });
  }

}
