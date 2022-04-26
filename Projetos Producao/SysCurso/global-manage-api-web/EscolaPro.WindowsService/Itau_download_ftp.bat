:loop

echo Baixando arquivos do FTP #ITAU-UNIBANCO

start C:\ITAU-UNIBANCO\program\stcpclt.exe "C:\ITAU-UNIBANCO\CTCP.INI" -p O0055ITAU-UNIBANCO -r 5 -t 30 -m B

timeout /t 10000

goto loop 
