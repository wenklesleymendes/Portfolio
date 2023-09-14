using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyInnerSDK.UI.FrmBIO;
using EasyInnerSDK.Entity;
using System.Windows.Forms;

namespace EasyInnerSDK.Controle
{
    class LCControle
    {
        private frmBio6xx frmBiometrico;
        private ASO15_DEF.SFEP_USER_FPDATA stRegTem;
        private byte[] stTemplates;
  
        public LCControle(frmBio6xx frmBio)
        {
            frmBiometrico = frmBio;
        }

        private void AtualizarStatusCaptura(string mensagem)
        {
            frmBiometrico.Invoke(new frmBio6xx.AtualizarStCaptura(frmBiometrico.AtualizarStatusCaptura), new object[] {mensagem});
        }

        public bool InicializarDispositivo()
        {
            int nRet = -1;
            nRet = ASO15_DEF.SFEP_Initialize();
            if (nRet != ASO15_DEF.RES_OK)
            {
                if (nRet == ASO15_DEF.ERR_NOT_USBDEV)
                    return false;
                else
                    return false;
            }
            else
            {
                stRegTem.rbData = new byte[ASO15_DEF.SFEP_UFPDATA_SIZE];
                stTemplates = new byte[ASO15_DEF.SFEP_UFPDATA_SIZE * 3];
                ASO15_DEF.SFEP_SetConfig(frmBiometrico.pcbImagemDigital.Handle);
                return true;
            }
        }

        public string CapturarTemplate()
        {
            int nEnrollCount = 0;
            int nRet = 0;
            UInt32 TimeOut = 5;
            while (nEnrollCount < 3)
            {
                if (nEnrollCount == 0)
                {
                    AtualizarStatusCaptura("Coloque o dedo no leitor!");
                }
                else if (nEnrollCount == 1)
                {
                    AtualizarStatusCaptura("Coloque o dedo no leitor novamente!");
                }
                else if (nEnrollCount == 2)
                {
                    AtualizarStatusCaptura("Coloque o dedo no leitor novamente!");
                }
                nRet = ASO15_DEF.SFEP_CaptureFingerImage(TimeOut);
                if (nRet == ASO15_DEF.ERR_FP_TIMEOUT)
                {
                    AtualizarStatusCaptura("Falha na leitura da digital!");
                    return "";
                }

                if (nRet != ASO15_DEF.RES_OK)
                {
                    AtualizarStatusCaptura("Falha na leitura da digital!");
                    return "";
                }

                nRet = ASO15_DEF.SFEP_CreateTemplate(ref stTemplates[nEnrollCount * ASO15_DEF.SFEP_UFPDATA_SIZE]);
                if (nRet != ASO15_DEF.RES_OK)
                {
                    AtualizarStatusCaptura("Falha na qualidade da digital!");
                    return "";
                }

                if (nEnrollCount > 0)
                {
                    nRet = ASO15_DEF.SFEP_Match2Template(ref stTemplates[0], ref stTemplates[nEnrollCount * ASO15_DEF.SFEP_UFPDATA_SIZE], 3);

                    if (nRet != ASO15_DEF.RES_OK)
                    {
                        AtualizarStatusCaptura("As digitais capturadas são diferentes!");
                        return "";
                    }
                }

                nEnrollCount = nEnrollCount + 1;
                setChkCaptura(nEnrollCount);

                if (ASO15_DEF.SFEP_IsFingerPress())
                {
                    AtualizarStatusCaptura("Retire o dedo!");
                    MessageBox.Show("Retire o dedo", "Cadastro Leitor USB");
                }
            }

            if (nEnrollCount == 3)
            {
                AtualizarStatusCaptura("Captura realizada com sucesso!");

                nRet = ASO15_DEF.SFEP_GetTemplateForRegister(ref stTemplates[0], ref stRegTem.rbData[0]);

                StringBuilder strBuilder = new StringBuilder();
                foreach (byte b in stRegTem.rbData)
                {
                    strBuilder.Append(b.ToString("x").PadLeft(2, '0'));
                }
                ZerarChkCapturas();
                return "00001000" + strBuilder.ToString();
            }
            return "";
        }

        private void ZerarChkCapturas()
        {
            frmBiometrico.chkCaptura1.Checked = false;
            frmBiometrico.chkCaptura2.Checked = false;
            frmBiometrico.chkCaptura3.Checked = false;
        }

        private void setChkCaptura(int nEnrollCount)
        {
            switch (nEnrollCount)
            {
                case 1:
                    frmBiometrico.chkCaptura1.Checked = true;
                    break;
                case 2:
                    frmBiometrico.chkCaptura2.Checked = true;
                    break;
                case 3:
                    frmBiometrico.chkCaptura3.Checked = true;
                    break;
            }
        }
    }
}
