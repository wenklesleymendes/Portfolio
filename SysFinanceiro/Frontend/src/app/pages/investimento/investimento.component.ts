import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DadosCDB } from 'src/app/models/DadosCDB';
import { AuthService } from 'src/app/services/auth.service';
import { InvestimentoService } from 'src/app/services/investimento.service';
import { MenuService } from 'src/app/services/menu.service';
import { SistemaService } from 'src/app/services/sistema.service';
import { Observable } from 'rxjs';

@Component({
    selector: 'app-investimento',
    templateUrl: './investimento.component.html',
    styleUrls: ['./investimento.component.scss']
})
export class InvestimentoComponent implements OnInit {

    constructor(
        public menuService: MenuService,
        public formBuilder: FormBuilder,
        public sistemaService: SistemaService,
        public authService: AuthService,
        public investimentoService: InvestimentoService
    ) { }

    investimentoForm: FormGroup;
    resultadoCalculo: Observable<DadosCDB>;

    ngOnInit() {
        this.menuService.menuSelecionado = 3;

        this.investimentoForm = this.formBuilder.group({
            valor: ['', [Validators.required]],
            qtdMeses: ['', [Validators.required]]
        });
    }

    enviar() {
        debugger;
        var dados = this.investimentoForm.value;
    
        let item = new DadosCDB();
        item.Valor = dados.valor;
        item.QtdMeses = dados.qtdMeses;
    
        console.log('Dados do formulário:', item);
    
        this.resultadoCalculo = this.investimentoService.calcularRendimento(item);
    
        this.resultadoCalculo.subscribe(response => {
            this.investimentoForm.reset();
            console.log('Resposta do serviço:', response);
        }, error => console.error(error));
    }
    
}
