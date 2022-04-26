[Languages]
Name: Portugues; MessagesFile: Portugues.isl

[Setup]
AppName=Escolar Manager Catraca Remote Service Host
AppVerName=Escolar Manager Catraca Remote Service Host
AppComments=Escolar Manager - Sistema de Gestão Escolar
AppPublisher=Escolar Manager Softwares para Gestão Escolar
AppPublisherURL=http://www.escolarmanager.com.br/
AppVersion=20201101
DisableDirPage=no

DefaultDirName={sd}\Terabyte\EMCatraca
DefaultGroupName=Escolar Manager
PrivilegesRequired=admin

OutputDir=Compilados
OutputBaseFilename=EMCatracaRemoteServiceHost
WizardImageFile=Imagens\WizardImage.bmp
WizardSmallImageFile=Imagens\Icone.bmp
SetupIconFile=Imagens\Setup.ico

[Dirs]
Name: {app}\Executaveis; Permissions: everyone-readexec

[Files]
; .NET
Source: ..\..\Binarios\EMCatraca.ServiceHost.exe; DestDir: {app}\Executaveis; Flags: ignoreversion; BeforeInstall: PararServico(ExpandConstant('{app}\Executaveis\EMCatraca.ServiceHost.exe'));
Source: ..\..\Binarios\EMCatraca.ServiceHost.exe.config; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist

; Copia arquivos de configuração
Source: ..\..\Binarios\EmCatraca.Acesso.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Loader.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Catracas.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Liberacao.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist
Source: ..\..\Binarios\EmCatraca.Servidor.cfg; DestDir: {app}\Executaveis; Flags: onlyifdoesntexist

; Copia DLLs
Source: ..\..\Binarios\EM.Infra.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\EMCatraca.Core.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\EMCatraca.Henry.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\EMCatraca.RegrasAcesso.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\EMCatraca.Server.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\EMCatraca.TopData.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\FirebirdSql.Data.FirebirdClient.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\Newtonsoft.Json.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\System.Net.Http.Formatting.dll; DestDir: {app}\Executaveis; Flags: ignoreversion
Source: ..\..\Binarios\TeraByte.dll; DestDir: {app}\Executaveis; Flags: ignoreversion

[Icons]
Name: {group}\Escolar Manager; Filename: {app}\Executaveis\EMCatraca.ServiceHost.exe; WorkingDir: {app}\Executaveis

[Run]
Filename: {app}\Executaveis\EMCatraca.ServiceHost.exe; Parameters: "--install"

[Registry]
;Para eliminar a tela de "Aviso de Segurança"
Root: HKCU; Subkey: "Software\Microsoft\Windows\CurrentVersion\Policies\Associations"; ValueType: string; ValueName: "LowRiskFileTypes"; ValueData: ".exe"; Flags: uninsdeletekey

[UninstallRun]
Filename: "{app}\Executaveis\EMCatraca.ServiceHost.exe"; Parameters: "--uninstall"

[Code]
procedure PararServico(FileName: String);
  var ResultCode: Integer;
begin
  if not Exec(FileName, '--stop', '', SW_SHOW, ewWaitUntilTerminated, ResultCode) then
  begin
    MsgBox('Não foi possível parar o serviço ' + FileName + '.', mbInformation, MB_OK);
  end;
end;
