import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { PerfilUsuarioService } from 'src/app/services/portal-adm/perfil-usuario.service';
import { HourMinuteMask } from 'src/app/utils/mask/mask';
import { Perfis } from 'src/app/utils/variables/perfis';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-permissao-usuario',
  templateUrl: './permissao-usuario.component.html',
  styleUrls: ['./permissao-usuario.component.scss']
})
export class PermissaoUsuarioComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;

  isLoadingResults: boolean = false;
  error: boolean = false;
  hourMinute = HourMinuteMask;
  perfis = Perfis;
  displayedColumns: string[] = [
    'nome',
    'liberarAcesso',
    'status',
    'options'
  ];

  AllAcess: Array<{ label: string, value: string[] }> = [
    { label: 'Alunos', value: ['consultarAluno']},
    { label: 'Relatório', value: ['relatorios']},
    { label: 'Recursos humanos', value: ['cadastroFuncionario', 'controlePonto', 'escalaServico', 'uploadPonto']},
    { label: 'Portal do Adminstrador', value: ['criacaoUsuario', 'criacaoPerfil', 'configuradorParametros']},
    { label: 'Ticket', value: ['ticketAdministracao', 'ticketPainel']},
    { label: 'Comunicação', value: ['comunicacao']},
    { label: 'Financeiro', value: ['cadastroFornecedorCliente', 'contasAPagar', 'estoque', 'folhaPagamento']},
    { label: 'Provas', value: ['criarColegioAutorizado', 'criarAgendaProva', 'listaPassageiros', 'historicoViagem']},
    { label: 'Gerenciador', value: ['cursoTurma', 'unidade', 'planoPagamento', 'promocoesBolsaConvenio', 'solicitacao']},
    { label: 'Metas e comissões', value: ['criarMeta', 'metaPainel', 'criarComissoes']},
    { label: 'Aulas On-line', value: ['criarAulaOnline', 'minhasAulas']},
    { label: 'Menu Atendimento', value: ['acessoMenuAtendimento']},
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private perfilUsuarioService: PerfilUsuarioService,
    private router: Router
  ) { }
  
  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    this.dataSource.paginator = this.paginator;
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAll() {
    this.isLoadingResults = true;
    this.error = false;

    this.perfilUsuarioService.getAll()
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
  }

  ajustarAcesso(element: any): string[] {
    let labels: string[] = [];

    for(let key in element) {
      if (element[key] === true) {
        this.AllAcess.forEach(elem => {
          if (elem.value.find(item => item == key)) {
            if (!labels.find(label => label == elem.label)) {
              labels.push(elem.label);
            }
          }
        })
      }
    }
    return labels;
  }

  labelDescricao(perfilSistemaEnum: number): string {
    if(!perfilSistemaEnum) return ' - ';
    const label = this.perfis.find(elem => elem.value === perfilSistemaEnum);
    return label ? label.label : ' - ';
  }

  // --------------------------------------------------------------------------
  //  BTN EVENTS
  // --------------------------------------------------------------------------
  goTelaIndividual(id: string = '0'): void {
    this.router.navigateByUrl(`portal-adm/permissoes-usuarios/adicionar/${id}`)
  }

  mudarStatus(id: string = '0'): void {
    this.perfilUsuarioService.desativarAtivar(id).subscribe(val => this.getAll());
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.perfilUsuarioService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
