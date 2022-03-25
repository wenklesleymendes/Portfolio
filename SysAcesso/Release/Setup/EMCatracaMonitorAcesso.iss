[Languages]
Name: Portugues; MessagesFile: Portugues.isl

[Setup]
AppName=Escolar Manager Catraca
AppVerName=Escolar Manager Catraca
AppComments=Escolar Manager - Sistema de Gestão Escolar
AppPublisher=Escolar Manager Softwares para Gestão Escolar
AppPublisherURL=http://www.escolarmanager.com.br/
AppVersion=20201101
DisableDirPage=no

DefaultDirName={sd}\Terabyte\EMCatraca
DefaultGroupName=Escolar Manager
PrivilegesRequired=admin

OutputDir=Compilados
OutputBaseFilename=EMCatraca.MonitorAcesso
WizardImageFile=Imagens\WizardImage.bmp
WizardSmallImageFile=Imagens\Icone.bmp
SetupIconFile=Imagens\Setup.ico

[Dirs]
Name: {app}\Executaveis; Permissions: everyone-readexec

[Files]
; .NET
Source: ..\..\Binarios\EMCatraca.MonitorAcesso.exe; DestDir: {app}\Executaveis; Flags: ignoreversion; 

; Copia arquivos de configuração
Source: ..\..\Binarios\EmCatraca.Acesso.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Loader.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Catracas.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Liberacao.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Servidor.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist

; Copia DLLs
Source: ..\..\Binarios\Newtonsoft.Json.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\EMCatraca.Core.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\System.Net.Http.Formatting.dll; DestDir: {app}\Executaveis; Flags: ignoreversion


;[Icons]
;Name: {group}\Escolar Manager; Filename: {app}\Executaveis\EscolarManager.exe; WorkingDir: {app}\Executaveis
;Name: {group}\Escolar Manager Suporte; Filename: {app}\TeamViewerQS.exe; WorkingDir: {app}\Executaveis
;Name: {group}\Ferramentas\Configurações; Filename: {app}\Executaveis\TeraByte.Configuracoes.exe; WorkingDir: {app}\Executaveis

[Registry]
;Para eliminar a tela de "Aviso de Segurança"
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Policies\Associations"; ValueType: string; ValueName: "LowRiskFileTypes"; ValueData: ".exe"; Flags: uninsdeletekey

[UninstallRun]
Filename: "{app}\Executaveis\EMRemoteServicesHost.exe"; Parameters: "--uninstall"

[InstallDelete]
Name: {app}\Executaveis\EscolarManagerAnalytics.exe; Type: files;
Name: {app}\Executaveis\EscolarManagerSimulado.exe; Type: files;
Name: {app}\Executaveis\Terabyte.ProcessadorDeRegistrosAcesso.exe; Type: files;
Name: {app}\Executaveis\Extensoes.dll; Type: files;

