import { Component, OnInit, Input } from '@angular/core';
import { QuestionarioStateService } from '../questionario-state.service';
import { PerguntaService } from 'src/app/services/aula-online/pergunta.service';
import { EventEmitterService } from 'src/app/services/EventEmitterService';

@Component({
  selector: 'app-questao-online',
  templateUrl: './questao-online.component.html',
  styleUrls: ['./questao-online.component.scss']
})
export class QuestaoOnlineComponent implements OnInit {
  @Input() pergunta;
  @Input() index;
  descricaoPergunta: string = null;
  anexoId: number = null;
  imgPergunta: any = null;
  imgPerguntaExtensao: string = null;
  imgRespostas: any[] = [];
  respostas: any[] = null;
  respondeu: boolean = false;
  respondido: any = null;

  constructor(
    private questionarioStateService: QuestionarioStateService,
    private perguntaService: PerguntaService
  ) {
   }

  ngOnInit(): void {

    this.descricaoPergunta = this.pergunta?.descricaoPergunta;
    this.anexoId = this.pergunta?.anexoId;
    this.respostas = this.pergunta?.resposta;
    this.imgPerguntaExtensao = this.pergunta?.extensao;
    this.questionarioStateService.getRespondeu().subscribe(val => this.respondeu = val);
    this.questionarioStateService.getRespostas().subscribe(val => {
      if(val?.length === 0) {
          this.respondido = null;
          return;
      }
    });

    this.getImgPergunta();
    this.getImgRespostas();
  }

  liberarRefazer(): void{
    if(this.respondeu)
    {
      this.respondeu = false;

      document.getElementById("r").click();
    }
  }

  OnResponder(): void {

    let respostasAtuais = [];
    this.questionarioStateService.getRespostas().subscribe(val => respostasAtuais = val);
    respostasAtuais.push({ index: this.index, resposta: this.respondido, pergunta: this.pergunta });
    this.questionarioStateService.respostaNext(respostasAtuais);
  }

  getImgPergunta(): void {
    if(!this.anexoId || !this.imgPerguntaExtensao) return;
    this.perguntaService.getImg(this.anexoId, this.imgPerguntaExtensao).subscribe(val => {
      this.imgPergunta = `data:${this.imgPerguntaExtensao};base64,${val}`;
    })
  }

  getImgRespostas(): void {
    this.respostas.forEach(elem => {
      if(!elem.anexoId || !elem.extensao) {
        this.imgRespostas.push(null);
        return;
      } else {
        this.perguntaService.getImg(elem.anexoId, elem.extensao).subscribe(val => {
          this.imgRespostas.push({ respostaId: elem.id, img:`data:${elem.extensao};base64,${val}` });
        })
      }
    })
  }

  showImgResposta(resposta): void {
    if(!(this.imgRespostas?.length > 0) || !resposta) return;
    const { id } = resposta
    if(!id) return;
    const img = this.imgRespostas.find(elem => elem?.respostaId === id);
    return img ? img.img : null;
  }

  styleResposta(resposta, pergunta): any {

    // var idModalMsgResposta = (<HTMLInputElement>document.getElementById('idModalMsgResposta'));
    // if(idModalMsgResposta != null)
    // {
    //   if(idModalMsgResposta.value) return;
    // }

    if(!resposta || !this.respondeu || !this.respondido) return;

    var videoIdResposta = Number(window.localStorage.getItem('videoIdRespondeuLocalStorage'));
    if(videoIdResposta == pergunta.videoAulaId)
    {
      if(resposta.correta) {
        return {
          icon: 'check',
          iconClass: 'green',
          class: 'bg-light-green'
        };
      }

      if((resposta.opcao === this.respondido.opcao) && !this.respondido.correta) {
        return {
          icon: 'clear',
          iconClass: 'red',
          class: 'bg-light-red'
        };
      }
    }

    return null;
  }
}
