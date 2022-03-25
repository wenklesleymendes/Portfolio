using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NBioBSPCOMLib;
using NITGEN.SDK.NBioBSP;
using EasyInnerSDK.UI;
using System.Windows.Forms;
using EasyInnerSDK.UI.FrmBIO;

namespace EasyInnerSDK.Entity
{
    class NitgenController
    {
        private NBioBSP NBioBSP;
        private IDevice Device;
        private IExtraction Extraction;
        private IFPImage FPImage;
        private IFPData FPData;
        private short DeviceId;
        private NBioBSPCOMLib.NBioBSP bsp;
        public NBioAPI m_NBioAPI;

        public short m_OpenedDeviceID;
        public NBioAPI.Type.HFIR m_hNewFIR;

        public NBioAPI.Type.FIR m_biFIR;
        public NBioAPI.Type.FIR_TEXTENCODE m_textFIR;
        public NBioAPI.Type.DEVICE_INFO_EX[] m_DeviceInfoEx;
        public NBioAPI.Type.WINDOW_OPTION m_WinOption;

        private FrmBIO frmBiometrico;
        private frmBio6xx frmBiometrico6xx;

        public NitgenController(FrmBIO frmBio, frmBio6xx frmbio6xx)
        {
            frmBiometrico = frmBio;
            frmBiometrico6xx = frmbio6xx;
            bsp = new NBioBSP();
            bsp.OnCaptureEvent += new _INBioBSPEvents_OnCaptureEventEventHandler(OnCaptureEvent);
            OpenHamster(bsp);
        }

        public object GetTemplateHamster(bool UseImagem, int ValorQuality, PictureBox pcbImagemDigital)
        {
            try
            {
                if (UseImagem == false)
                {
                    Extraction.WindowStyle = NBioAPI.Type.WINDOW_STYLE.POPUP;
                    pcbImagemDigital.Visible = false;
                }
                else
                {
                    Extraction.WindowStyle = NBioAPI.Type.WINDOW_STYLE.INVISIBLE;
                    Extraction.FingerWnd = (int)pcbImagemDigital.Handle;
                    pcbImagemDigital.Visible = true;
                }
                Extraction.VerifyImageQuality = ValorQuality;
                Extraction.Capture((int)NBioAPI.Type.FIR_PURPOSE.VERIFY);
                return Extraction.FIR;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        public string ExportarTemplate(object Fir)
        {
            byte[] Template = null;
            StringBuilder Digital = null;
            FPData.Export(Fir, (int)NBioAPI.Type.MINCONV_DATA_TYPE.MINCONV_TYPE_FIM01_HV);
            if (FPData.ErrorCode == 0)
            {
                Digital = new StringBuilder();
                int FingerID = FPData.get_FingerID(0);

                Template = (byte[])FPData.get_FPData(FingerID, 0);
                Digital.Append(Utils.ByteToHex(Template, 0, Template.Length));
                return Digital.ToString();
            }
            else
            {
                MessageBox.Show("Falha ao exportar template! Erro: " + FPData.ErrorCode);
                return null;
            }
        }
        #region QualidadeDigital
        /// <summary>
        /// Pega a qualidade da digital
        /// </summary>
        /// <param name="hFir"></param>
        /// <returns></returns>
        public int GetQualidadeDigital(object Fir)
        {
            int QualDigital = -1;
            FPData.CheckQuality(Fir, FPImage.AuditData);
            for (int index = 0; index < FPData.TotalFingerCount; index++)
            {
                for (int sample = 0; sample < FPData.SampleNumber; sample++)
                {
                    QualDigital = FPData.get_QualityInfo(FPData.get_FingerID(index), sample);
                    QualDigital = Qualidade(QualDigital);
                }
            }
            return QualDigital;
        }
        private int Qualidade(int Quali)
        {
            int QtdDigital = 0;
            switch (Quali)
            {
                case (int)NBioAPI.Type.QUALITY.QUALITY_POOR:
                    QtdDigital = 1;
                    break;
                case (int)NBioAPI.Type.QUALITY.QUALITY_BAD:
                    QtdDigital = 2;
                    break;
                case (int)NBioAPI.Type.QUALITY.QUALITY_NORMAL:
                    QtdDigital = 3;
                    break;
                case (int)NBioAPI.Type.QUALITY.QUALITY_GOOD:
                    QtdDigital = 4;
                    break;
                case (int)NBioAPI.Type.QUALITY.QUALITY_EXCELLENT:
                    QtdDigital = 5;
                    break;
            }
            return QtdDigital;
        }
        #endregion

        private void OpenHamster(NBioBSP bsp)
        {
            NBioBSP = bsp;
            Device = (NBioBSPCOMLib.IDevice)NBioBSP.Device;
            Extraction = (NBioBSPCOMLib.IExtraction)NBioBSP.Extraction;
            FPImage = (NBioBSPCOMLib.IFPImage)NBioBSP.FPImage;
            FPData = (NBioBSPCOMLib.IFPData)NBioBSP.FPData;

            DeviceId = NBioAPI.Type.DEVICE_ID.AUTO;

            Device.Close(DeviceId);
            Device.Open(DeviceId);
        }
        private void OnCaptureEvent(int Quality)
        {
            frmBiometrico.Invoke(new FrmBIO.AtualizarLblQualidadeImagem(frmBiometrico.AtualizarLabelQualidadeImagem), Quality);
        }

        public bool ConectarDLLLN()
        {
            NBioAPI.Type.VERSION version;
            m_NBioAPI = new NBioAPI();

            version = new NBioAPI.Type.VERSION();
            m_NBioAPI.GetVersion(out version);

            // Enumerate Device
            uint nNumDevice;
            short[] nDeviceID;
            uint ret = m_NBioAPI.EnumerateDevice(out nNumDevice, out nDeviceID, out m_DeviceInfoEx);
            if (nNumDevice > 0)
            {
                return true;
            }
            return false;           
        }
    }
}
