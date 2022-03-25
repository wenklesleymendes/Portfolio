import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class QuestionarioStateService {
  private respostas : BehaviorSubject<any>;
  private respondeu : BehaviorSubject<any>;

  getRespostas(): Observable<any> {
    return this.respostas;
  } 

  getRespondeu(): Observable<any> {
    return this.respondeu;
  } 

  initAllSubjects(): void {
    this.respostas = new BehaviorSubject([]);
    this.respondeu = new BehaviorSubject(false);
  }

  completeAllSubjects(): void {
    this.respostas.complete();
    this.respondeu.complete();
  }

  respostaNext(val): void { 
    this.respostas.next(val);
  }

  respondeuNext(val): void { 
    this.respondeu.next(val);
  }
}
