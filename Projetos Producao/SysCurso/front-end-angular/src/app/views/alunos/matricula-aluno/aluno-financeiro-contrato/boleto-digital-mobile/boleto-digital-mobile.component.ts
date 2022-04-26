import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Clipboard } from '@angular/cdk/clipboard';

@Component({
    selector: 'app-boleto-digital-mobile',
    templateUrl: './boleto-digital-mobile.component.html',
    styleUrls: ['./boleto-digital-mobile.component.scss']
})

export class BoletoDigitalMobileComponent {

    pagamento: any;
    imglogo: string;
    unidadeLocalStorage: number;
    nomeAluno: string;

    constructor(
        private dialogRef: MatDialogRef<BoletoDigitalMobileComponent>,
        @Inject(MAT_DIALOG_DATA) public data,
        private clipboard: Clipboard,
      ) { }


    ngOnInit(): void {
        this.pagamento = this.data?.fin;

        this.imglogo = window.localStorage.getItem('logoLocalStorage');

        this.nomeAluno = window.localStorage.getItem('nomeAlunoLocalStorage');

        // if(this.unidadeLocalStorage == 1)
        //     this.imglogo = "../../../../../../assets/logo_escola_modelo.png";
        //   if(this.unidadeLocalStorage == 2)
        //     this.imglogo = "../../../../../../assets/logo_suporte_central.png";
        //   if(this.unidadeLocalStorage == 3)
        //     this.imglogo = "../../../../../../assets/logo_campinas.png";
        //   if(this.unidadeLocalStorage == 4)
        //     this.imglogo = "../../../../../../assets/logo_guarulhos.png";
        //   if(this.unidadeLocalStorage == 5)
        //     this.imglogo = "../../../../../../assets/logo_piracicaba.png";
        //   if(this.unidadeLocalStorage == 6)
        //     this.imglogo = "../../../../../../assets/logo_sorocaba.png";
        //   if(this.unidadeLocalStorage == 7)
        //     this.imglogo = "../../../../../../assets/logo_jundiai.png";
        //   if(this.unidadeLocalStorage == 8)
        //     this.imglogo = "../../../../../../assets/logo_santo_amaro.png";
        //   if(this.unidadeLocalStorage == 9)
        //     this.imglogo = "../../../../../../assets/logo_centro_sao_paulo.png";
        //   if(this.unidadeLocalStorage == 10)
        //     this.imglogo = "../../../../../../assets/logo-nacionaltec.png";
    }

    close(): void {
        this.dialogRef.close();
    }

    copyToClipboard(item) {
        document.addEventListener<'copy'>('copy', (e: ClipboardEvent) => {
          e.clipboardData.setData('text/plain', (item));
          e.preventDefault();
          document.removeEventListener<'copy'>('copy', null);
        }, {passive: false});
        document.execCommand('copy');
        alert('Copiado com sucesso');
    }

    alertCopy() {
        alert('Copiado com sucesso');
    }
}

