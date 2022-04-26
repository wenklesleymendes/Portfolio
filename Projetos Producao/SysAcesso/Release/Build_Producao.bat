@ECHO OFF

CD %~dp0

"%MSBUILD_PATH%" ..\EMCatraca.sln /t:Rebuild /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=Release /verbosity:quiet
IF NOT %ERRORLEVEL%==0 GOTO fail

DEL /Y Setup\Compilados*.*

SET innoSetup="%programfiles(x86)%\Inno Setup 6\iscc.exe"

%innoSetup% Setup\EMCatracaMonitorAcesso.iss
IF NOT %ERRORLEVEL%==0 GOTO fail

%innoSetup% Setup\EMCatracaConfiguracao.iss
IF NOT %ERRORLEVEL%==0 GOTO fail

%innoSetup% Setup\EMCatracaRemoteServiceHost.iss
IF NOT %ERRORLEVEL%==0 GOTO fail

%innoSetup% Setup\EMCatracaValidacaoExterna.iss
IF NOT %ERRORLEVEL%==0 GOTO fail

ECHO:
ECHO Setup do Monitor de Acesso
ECHO Setup do Configuracao
ECHO Setup do Remote Service Host
ECHO Setup do ValidacaoExterna - Neokoros
ECHO ****************************** 
ECHO ********** SUCESSO! ********** 
ECHO ******************************

GOTO end

:fail
ECHO:
ECHO =======================================================
ECHO ============ ERRO DURANTE A COMPILACAO ================
ECHO =======================================================

:end
ECHO:
pause