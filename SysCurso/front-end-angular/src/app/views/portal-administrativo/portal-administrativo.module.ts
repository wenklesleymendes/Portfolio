// Modules
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortalAdministrativoRoutingModule } from './portal-administrativo-routing.module';
import { MaterialModule } from 'src/app/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { UtilsComponentModule } from 'src/app/utils/components/utils-components.module';
import { PipeModule } from 'src/app/utils/pipes/pipe.module';
import { NgxMaskModule } from 'ngx-mask';
// Services
import { PerfilUsuarioService } from 'src/app/services/portal-adm/perfil-usuario.service';
import { UsuarioService } from 'src/app/services/portal-adm/usuario.service';
// Components
import { CadastroUsuarioComponent } from './cadastro-usuario/cadastro-usuario.component';
import { PermissaoUsuarioComponent } from './permissao-usuario/permissao-usuario.component';
import { ConfiguracaoParametrosComponent } from './configuracao-parametros/configuracao-parametros.component';
import { PermissaoUsuarioIndividualComponent } from './permissao-usuario/permissao-usuario-individual/permissao-usuario-individual.component';
import { CadastroUsuarioIndividualComponent } from './cadastro-usuario/cadastro-usuario-individual/cadastro-usuario-individual.component';
import { AuthInterceptor } from 'src/app/security/auth-interceptor.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    CadastroUsuarioComponent,
    PermissaoUsuarioComponent,
    ConfiguracaoParametrosComponent,
    PermissaoUsuarioIndividualComponent,
    CadastroUsuarioIndividualComponent
  ],
  imports: [
    CommonModule,
    PortalAdministrativoRoutingModule,
    CommonModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    UtilsComponentModule,
    PipeModule,
    NgxMaskModule.forRoot(),
  ],
  providers: [
    PerfilUsuarioService,
    UsuarioService,
    //{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
  ]
})
export class PortalAdministrativoModule { }
