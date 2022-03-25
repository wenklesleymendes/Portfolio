import { Component, OnInit, Input, Output, Inject, SimpleChanges, OnChanges, OnDestroy, EventEmitter } from '@angular/core';
import { PerguntaService } from 'src/app/services/aula-online/pergunta.service';
import { QuestionarioStateService } from './questionario-state.service';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Subscription } from 'rxjs';
import { EventEmitterService } from 'src/app/services/EventEmitterService';

@Component({
  selector: 'app-questionario-online',
  templateUrl: './questionario-online.component.html',
  styleUrls: ['./questionario-online.component.scss']
})
export class QuestionarioOnlineComponent implements OnInit, OnChanges, OnDestroy {
  @Input() videoId: any;
  @Input() preview: boolean;
  @Output() nextVideo: EventEmitter<any> = new EventEmitter();
  descricaoPergunta: string = null;
  perguntas: any[] = null;
  titulo: string = null;
  id: any;
  respondeuTudo: boolean = true;
  hasClosedDialog: boolean = false;
  hasOpenDialog: boolean = false;
  respostasObservable: Subscription = null;

  perguntasLocal: any[] = null;
  tituloLocal: string = null;
  modalMsgResposta: boolean = false;
  refreshEvento: any = null;

  constructor(
    private perguntaService: PerguntaService,
    private questionarioStateService: QuestionarioStateService,
    private dialog: MatDialog,
  ) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (!changes?.videoId) return;
    const { currentValue } = changes.videoId;
    if(currentValue) {
      this.videoId = currentValue;
      this.getPerguntas();
    }
  }

  ngOnInit(): void {
    this.questionarioStateService.initAllSubjects();
    this.questionarioStateService.getRespostas().subscribe((val: any[]) => {

    this.refreshEvento = EventEmitterService.get('refreshRefazer').subscribe(e => this.refazer());

    });
  }

  ngOnDestroy(): void {
    this.questionarioStateService.completeAllSubjects();
    if(this.respostasObservable) this.respostasObservable.unsubscribe();
  }

  getPerguntas(): void {
    this.perguntaService.buscarPorVideoAula(this.videoId).subscribe(val => {
      const { titulo, lista }  = val
      this.titulo = titulo;
      this.perguntas = lista;
    })
  }

  proximoVideo(): void {
    this.nextVideo.emit(this.videoId);
  }

  refazer(): void {

    var respondidoRadios = (document.getElementsByName("respondidoNameRadio")) ;
    if(respondidoRadios.length > 0)
    {
      for (let index = 0; index < respondidoRadios.length; index++) {
        ((document.getElementsByName("respondidoNameRadio"))[index] as HTMLInputElement).checked = false;
        ((document.getElementsByName("respondidoNameRadio"))[index] as HTMLInputElement).classList.remove("mat-radio-checked");
      }
    }

    this.respondeuTudo = false;
    this.hasOpenDialog = false;
    this.questionarioStateService.respondeuNext(false);
    this.questionarioStateService.respostaNext([]);
    if(this.respostasObservable != null)
      this.respostasObservable.unsubscribe();
  }

  responder(videoId: any): void {

    this.modalMsgResposta = true;
    this.questionarioStateService.respondeuNext(true);
    this.respostasObservable = this.questionarioStateService.getRespostas().subscribe((val: any[]) => {

      if(!this.modalMsgResposta) return;
      if(val?.length === 0) return;

      window.localStorage.setItem('videoIdRespondeuLocalStorage', videoId.toString());
      var resultado: any[] = [];
      val.forEach(elem => {
        if(elem?.pergunta?.videoAulaId == videoId)
        {
          if(elem != null && elem != undefined && elem != '')
            resultado.push(elem);
        }
      });

      const qtdPerguntas: number = resultado.length;
      let acertadas: number = 0;
      resultado.forEach(elem => {
        if(elem?.resposta?.correta) acertadas += 1;
      })
      const mediaAcertos = acertadas / qtdPerguntas;

      if(mediaAcertos === 1) this.showMensagemResposta(mediaAcertos, 'Parabéns, excelente resultado! Nota 10.', 'insert_emoticon');
      else if(mediaAcertos >= 0.7 && mediaAcertos <= 0.99) this.showMensagemResposta(mediaAcertos, 'Parabéns, você esta indo muito bem!', 'sentiment_satisfied_alt');
      else if(mediaAcertos >= 0.5 && mediaAcertos <= 0.69) this.showMensagemResposta(mediaAcertos, 'Você esta indo bem, continue estudando!', 'sentiment_satisfied');
      else if(mediaAcertos >= 0.2 && mediaAcertos <= 0.49) this.showMensagemResposta(mediaAcertos, 'Você esta quase lá, continue estudando.', 'sentiment_dissatisfied');
      else this.showMensagemResposta(mediaAcertos,'Você esta quase lá, tente novamente!', 'sentiment_very_dissatisfied');
      this.modalMsgResposta = false;
    });
    this.modalMsgResposta = false;
  }

  showMensagemResposta(percentage, text, icon) {
    this.hasClosedDialog = false;
    this.hasOpenDialog = true;
    const dialogRef = this.dialog.open(MensagemRespostaComponent, {
      autoFocus: false,
      data: { percentage,text, icon },
    });

    dialogRef.afterClosed().subscribe(result => {

      this.hasClosedDialog = true;
      this.hasOpenDialog = false;
    });
  }
}

@Component({
  selector: 'app-mensagem-resposta',
  template: `
    <div mat-dialog-content>
      <mat-icon class="emoji" color="primary">{{ icon }}</mat-icon>
      &nbsp;
      &nbsp;
      <span><b>Resultado: {{ percentage | percent }} de acertos. </b></span>
      <p> {{ text }} </p>
    </div>
    <div mat-dialog-actions>
      <button mat-button color="primary" [mat-dialog-close]="" cdkFocusInitial>Ok</button>
    </div>
  `,
  styles: [`
    .emoji {
      font-size: 75px;
      width: 75px;
      height: 75px;
    }
    .mat-dialog-content {
      display: flex;
      flex-direction: column;
      align-items: center;
    }
  `],
})
export class MensagemRespostaComponent implements OnInit {
  text: string = null;
  icon: string = null;
  percentage: string = null;
  constructor(
    public dialogRef: MatDialogRef<MensagemRespostaComponent>,
    @Inject(MAT_DIALOG_DATA) public data
  ){}

  ngOnInit(): void {
    this.text = this.data.text;
    this.icon = this.data.icon;
    this.percentage = this.data.percentage;
  }
}
