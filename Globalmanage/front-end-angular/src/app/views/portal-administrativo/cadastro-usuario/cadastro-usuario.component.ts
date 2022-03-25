import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { DeleteService } from 'src/app/services/delete.service';
import { CPFMask } from 'src/app/utils/mask/mask';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
import { Perfis } from 'src/app/utils/variables/perfis';
import { AuthService } from 'src/app/security/auth.service';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-cadastro-usuario',
  templateUrl: './cadastro-usuario.component.html',
  styleUrls: ['./cadastro-usuario.component.scss']
})
export class CadastroUsuarioComponent implements OnInit {
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  perfis = Perfis;
  isLoadingResults: boolean = false;
  error: boolean = false;
  cpfMask = CPFMask;
  displayedColumns: string[] = [
    'id',
    'nome',
    'cpf',
    'rg',
    'email',
    'status',
    'perfil',
    'unidade',
    'departamento',
    'options'
  ];

  dataSource = new MatTableDataSource([]);
  pesquisou: boolean = false;
  larestlabelSelected: string[];
  selection = new SelectionModel<any>(true, []);

  constructor(
    private deleteService: DeleteService,
    private usuarioService: UsuarioService,
    private authService: AuthService,
    private router: Router
  ) { }

  // --------------------------------------------------------------------------
  //  LIFECYCLE HOOKS
  // --------------------------------------------------------------------------
  ngOnInit() {
    // //debugger
    this.dataSource.paginator = this.paginator;
    this.getAll();
  }

  // --------------------------------------------------------------------------
  //  BUSSINES RULE
  // --------------------------------------------------------------------------
  getAll() {
    this.isLoadingResults = true;
    this.error = false;
    const usuario = this.authService.getToken();
    this.usuarioService.getAll(usuario?.user?.id)
      .subscribe(val => {
        this.isLoadingResults = false;
        if (val['status'] === 'error') this.error = true;
        else this.dataSource.data = val;
      });
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
    this.router.navigateByUrl(`portal-adm/cadastro-usuario/adicionar/${id}`)
  }

  mudarStatus(id: string = '0'): void {
    this.usuarioService.desativarAtivar(id).subscribe(val => this.getAll());
  }

  excluir(id: string = '0'): void {
    this.deleteService.openDelete().then(res => {
      if (!res) return;
      this.usuarioService.deletarPorId(id).subscribe(val => {
        if (val) this.getAll();
      })
    })
  }
}
