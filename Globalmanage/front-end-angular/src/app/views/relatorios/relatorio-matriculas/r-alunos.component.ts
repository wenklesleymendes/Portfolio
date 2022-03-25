import { OnInit, Component } from "@angular/core";


@Component({
    selector: 'app-r-alunos',
    templateUrl: './r-alunos.component.html',
    styleUrls: ['./r-alunos.component.scss']
})

export class RAlunosComponent implements OnInit {
    telaFinanceiro = 0;

    ngOnInit(): void {

    }

    mudarPainelFinanceiro(tela: number): void {
        this.telaFinanceiro = tela;
      }
}