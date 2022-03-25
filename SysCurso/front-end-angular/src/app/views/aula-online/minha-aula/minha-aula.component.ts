import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/security/auth.service';
import { AulaOnlineService } from 'src/app/services/aula-online/aula-online.service';
import { VideoAulaService } from 'src/app/services/aula-online/video-aula.service';
import { Navigation, Router } from '@angular/router';
import Player from '@vimeo/player';

@Component({
  selector: 'app-minha-aula',
  templateUrl: './minha-aula.component.html',
  styleUrls: ['./minha-aula.component.scss']
})
export class MinhaAulaComponent implements OnInit {
  isLoadingResults: boolean = false;
  isLoadingVideo: boolean = false;
  vimeoUrl: string = null;
  vimeoProgress: number = null;
  videoId: string = null;
  error: boolean = false;
  materias: any[] = [];
  nomeAulaOnline: string = null;
  matriculaId: number = null;
  semAula: boolean = false;
  materiaId = 0;
  materiaFiltradas: any[] = [];
  videoFiltradas: any[] = [];

  options: any;
  player: any;

  vimeoUrl1: string = null;
  vimeoUrl2: string = null;

  videoCarregado = false;

  constructor(
    private aulaOnlineService: AulaOnlineService,
    private videoAulaService: VideoAulaService,
    private router: Router,
    private authService: AuthService,
  ) {}

  ngOnInit(): void {

    (<HTMLInputElement>document.getElementById('carregamento')).style['visibility'] = 'visible';
    const token = JSON.parse(window.localStorage.getItem('matricula'));

    this.matriculaId = Number(window.localStorage.getItem('matriculaIdLocalStorage'));
    this.materiaId = Number(window.localStorage.getItem('materiaIdLocalStorage'));

    this.getAll();
  }

  getAll(): void {
    Promise.all([
      this.getUltimaSessao(),
      this.getAulas(),
    ])
    .then((res) => {
      if(!res[1]?.materiaOnline) {
        this.semAula = true;
        return
      }

      this.isLoadingVideo = true;

      this.materiaFiltradas = this.materias.filter(x => x.id == this.materiaId);
      this.videoFiltradas = this.materiaFiltradas[0].videoAula;
      setTimeout(() => this.isLoadingVideo = false, 0);
    })
  }

  ngAfterViewChecked(): void{

    if(!this.videoCarregado)
    {
      for (let index = 0; index < this.videoFiltradas.length; index++)
      {
        if((<HTMLInputElement>document.getElementById('video'+index)) != null)
        {
          var new_url = this.videoFiltradas[index].urlVideo;
          new_url = new_url.substring(18,255);
          new_url = new_url.substring(0,new_url.indexOf("/"));
          new_url = "https://player.vimeo.com/video/" + new_url;
          (<HTMLInputElement>document.getElementById('video'+index)).src = new_url;
        }
        if((<HTMLInputElement>document.getElementById('video'+index)) != null && index == (this.videoFiltradas.length-1))
        {
          this.videoCarregado = true;
        }
      }
    }
  }

  getAulas(): Promise<any> {
    this.isLoadingVideo = true;
    return new Promise((res, rej) => {
      this.aulaOnlineService.minhasAulasOnline(this.matriculaId).subscribe(val => {
        (<HTMLInputElement>document.getElementById('carregamento')).style['visibility'] = 'hidden';
        this.isLoadingVideo = false;
        if (val?.status === 'error') {
          this.error = true;
          rej();
          return;
        }
        this.materias = val?.materiaOnline;
        this.nomeAulaOnline = val?.nomeAulaOnline;
        res(val);
        return;
      });
    });
  }

  getUltimaSessao(): Promise<any> {
    this.isLoadingVideo = true;
    return new Promise((res, rej) => {
      this.videoAulaService.buscarUltimaSessao(this.matriculaId).subscribe(val => {
        this.isLoadingVideo = false;
        if (val?.status === 'error') {
          this.error = true;
          rej();
          return;
        }
        res(val);
        return;
      });
    });
  }

  changeVideo(aula): void {
    this.isLoadingVideo = true;

    setTimeout(() => {
      const { id, urlVideo } = aula;
      this.vimeoUrl = urlVideo;
      this.videoId = id;
      this.vimeoProgress = 0;
      this.isLoadingVideo = false;
    }, 0);
  }

  salvarSessao(e: any): void {
    // const token = JSON.parse(window.localStorage.getItem('matricula'));
    // this.matriculaId = token.matriculaId;
    const { vimeoUrl, tempo } = e;
    const data = {
      tempo,
      id: 0,
      // matriculaId: token.matriculaId,
      matriculaId: this.matriculaId,
      url: vimeoUrl,
      videoId: this.videoId,
    }
    this.videoAulaService.salvarUltimaSessao(data).subscribe(val => {
    })
  }

  nextVideo(videoId: any): void {
    if(!videoId) return;
    let materiaAnteriorIndex: number = null;
    let videoAulaAnteriorIndex: number = null;

    this.materias.find((materia, cursoIndex) => {
      const videoAulas: any[] = materia?.videoAula;
      if(videoAulas) {
        const achouMateria = videoAulas.find((videoAula, videoAulaIndex) => {
          if(videoAula.id === videoId) {
            materiaAnteriorIndex = cursoIndex;
            videoAulaAnteriorIndex = videoAulaIndex;
            return videoAula;
          }
        });
        if(achouMateria) return materia;
      }
    });

    if(materiaAnteriorIndex === null || !videoAulaAnteriorIndex === null) return;
    if(this.materias[materiaAnteriorIndex].videoAula.length > (videoAulaAnteriorIndex + 1)) {
      const aula = this.materias[materiaAnteriorIndex].videoAula[(videoAulaAnteriorIndex + 1)];
      this.changeVideo(aula);
    }
    else if(this.materias.length > (materiaAnteriorIndex + 1)) {
      if(this.materias[(materiaAnteriorIndex + 1)].videoAula[0]) {
        const aula = this.materias[(materiaAnteriorIndex + 1)].videoAula[0];
        this.changeVideo(aula);
      }
    }
    else {}
  }

  openPanel(materia): boolean {
    if(materia?.id === null || materia?.id === undefined) return;
    const id = materia?.id;
    const hasMateria = this.materias.find(materia => {
      const videoAulas: any[] = materia?.videoAula;
      if(videoAulas) {
        const achouMateria = videoAulas.find(videoAula => videoAula.id === this.videoId);
        if(achouMateria) return materia;
      }
    });
    const idFound = hasMateria?.id;
    if(idFound === null || idFound === undefined) return;
    return id === idFound;
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

  goToCursoTurma(matriculaId: number): void {
    this.router.navigate(['/alunos/matricula-aluno/aluno-curso-turma'], { state: { matriculaId } });
  }
}
