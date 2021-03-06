using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using MyCTS.Components;
using MyCTS.Entities;
using MyCTS.Business;
using MyCTS.Presentation.Components;
using System.IO;
using System.Web;
using System.Reflection;
using MyCTS.Services.Contracts;

namespace MyCTS.Presentation
{
    public partial class frmMain : System.Windows.Forms.Form
    {
        #region variables
        public string mensajes;
        private string strScrollText;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        public IntPtr hWndSabres = IntPtr.Zero;
        private const string SABRE_RED_WORKSPACE = "Sabre® Red™ Workspace - Sabre Travel Network";
        private const string MYCTS_PROCESO = "MyCTS.Presentation";
        private const string SABRE_RED_WORKSPACE_PROCESO = "mysabre";
        private const int SW_SHOWNORMAL = 1;
        public const int SW_MAXIMIZE = 3;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        public static string AppName = "MyCTS2";
        private bool esFormControlMenu = true;
        //public long handleMod;
        private IntPtr hWndApp = IntPtr.Zero;
        public IntPtr HWndApp = IntPtr.Zero;
        private IntPtr hWndSabre = IntPtr.Zero;
        private bool m_TextPasted = false;
        private bool m_TextCopied = false;
        //public bool IsSigned = false;
        #endregion

        public frmMain()
            : base()
        {
            InitializeComponent();

            Thread mythread = new Thread(this.starthandler);
            mythread.IsBackground = true;
            mythread.Start();

            cb = new CallBackHandler.MySabreCallback(tempSabreHandler);
            cbMarkup = new CallBackHandler.MySabreMarkupCallback(tempSabreMarkupHandler);
        }

        private System.Timers.Timer _timerAnalyzer = new System.Timers.Timer();
        private Thread ThreadLogProcess;
        void _timerAnalyzer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ThreadLogProcess = new Thread(new ThreadStart(ReadQueueFiles));
            ThreadLogProcess.Start();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //TODO:Comentar estas 3 líneas si se requiere hacer deploy hasta que se termine las pruebas*****
            _timerAnalyzer.Elapsed += new System.Timers.ElapsedEventHandler(_timerAnalyzer_Elapsed);
            _timerAnalyzer.Interval = double.Parse(Parameters.SecondsLogs);
            //_timerAnalyzer.Interval = 20000;
            _timerAnalyzer.Enabled = true;
            //**********************************************************************************

            int w = this.Parent.Width / 2 + 83;
            this.Top = this.Parent.Top;
            this.Left = w;
            this.Width = w - 170;
            this.Height = this.Parent.Height - 10;

            lblVersion.Text = "       Versión: " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            #region Banner
            //Obtiene los valores del perfil para colocarle el texto del Banner.
            strScrollText = string.Format("{0} {1}", Login.BannerText, Login.NombreCompleto);
            string bannerColor = Common.GetProfileElement("BANNER_FONT_COLOR", Login.UserProfile.PropertyNames, Login.UserProfile.PropertyValuesString);
            string bannerSize = Common.GetProfileElement("BANNER_FONT_SIZE", Login.UserProfile.PropertyNames, Login.UserProfile.PropertyValuesString);

            if (string.IsNullOrEmpty(bannerColor))
                bannerColor = Resources.Constants.BANNER_FONT_COLOR;
            if (string.IsNullOrEmpty(bannerSize))
                bannerSize = Resources.Constants.BANNER_FONT_SIZE;

            lblBanner.Text = strScrollText;
            lblBanner.ForeColor = System.Drawing.Color.FromName(bannerColor);
            lblBanner.Font = new System.Drawing.Font(lblBanner.Font.Name,
                float.Parse(bannerSize),
                System.Drawing.FontStyle.Bold,
                System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            lblBanner.AutoSize = true;
            lblBanner.Left = panel1.Width;
            timer1.Enabled = true;
            #endregion

            string strBienvenida = "";
            string strInstrucciones = "";
            string strMensajeTA = "";

            if (Login.IsFirstTime)
            {
                strBienvenida = "Bienvenido. Si eres un usuario nuevo, deberas seguir las siguientes instrucciones:\n";
                strInstrucciones = "1.-Llena el perfil en la ventana de lado izquierdo.\n\n2.-En el apartado que indica LNIATA ingrese el siguiente dato: " + Login.TA + "\n\n3.-Seleccione la opcion \"SABRE Virtual Private Network\".\n\n4.-Al terminar de clic en el boton de \"Continuar\".\n\n5.-De clic en el boton \"Terminar la Configuración\".\n\n6.-Espere a cargar SABRE e inicie session.\n";
                strMensajeTA = string.Format("El número de TA que le corresponde es la: {0}", Login.TA);
            }
            else
            {
                strBienvenida = "Para ingresar a MyCTS debe dar clic en Inicio de Sesión en MySabre.\n";
                strInstrucciones = "Esperando activación de MySabre.......\n";
            }

            Loader.AddToPanelWithParameters(Loader.Zone.Middle, this, "ucWelcome", strMensajeTA, strBienvenida, strInstrucciones);
            //pnlMiddle.Controls.Add(Loader.GetReferenceUserControl("ucProductivityReport"));
            //pnlArea.Controls.Add(Loader.GetReferenceUserControl("ucArea"));
            //pnlLeft.Controls.Add(Loader.GetReferenceUserControl("ucMenuReservations"));

            this.PerformLayout();
            GC.Collect();
            this.KeyPreview = true;

        }

        #region Captura las teclas presionadas para usar el menu
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            pnlLeft.Enabled = true;

            if (keyData == (Keys.Control | Keys.E))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UC_CONCLUDERESERVATION);
                return true;
            }
            if (keyData == (Keys.Escape))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCWELCOME);
            }
            if (keyData == (Keys.Control | Keys.D1))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCAVAILABILITY);
            }
            if (keyData == (Keys.Control | Keys.NumPad1))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCAVAILABILITY);
            }
            if (keyData == (Keys.Control | Keys.D0))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCSELL_AIR_SEGMENT);
            }
            if (keyData == (Keys.Control | Keys.NumPad0))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCSELL_AIR_SEGMENT);
            }
            if (keyData == (Keys.Control | Keys.D4))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCSETS_MAP);
            }
            if (keyData == (Keys.Control | Keys.NumPad4))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCSETS_MAP);
            }
            if (keyData == (Keys.Control | Keys.D6))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCBOLETAGERECEIVED);
            }
            if (keyData == (Keys.Control | Keys.NumPad6))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCBOLETAGERECEIVED);
            }
            if (keyData == (Keys.Control | Keys.D7))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCBOLETAGEDATE);
            }
            if (keyData == (Keys.Control | Keys.NumPad7))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCBOLETAGEDATE);
            }
            if (keyData == (Keys.Control | Keys.B))
            {
                ucMenuReservations.qualityControls = false;
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.TicketEmission.Constants.UCFIRST_VALIDATIONS);
            }
            if (keyData == (Keys.Control | Keys.C) | (keyData == (Keys.Control | Keys.X)))
            {
                m_TextCopied = true;
            }
            if (keyData == (Keys.Control | Keys.D))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCCODE_DECODE);
            }
            if (keyData == (Keys.Alt | Keys.D))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCGETDIX);
            }
            if (keyData == (Keys.Control | Keys.E))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UC_CONCLUDERESERVATION);
            }
            if (keyData == (Keys.Control | Keys.I))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCIGNORE_RECORD);
            }
            if (keyData == (Keys.Control | Keys.J))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCRECOVERRECORD);
            }
            if (keyData == (Keys.Control | Keys.N))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCDEFINE_PASSENGER_TYPE);
            }
            if (keyData == (Keys.Control | Keys.O))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Queues.Constants.UC_WORKING_PNR_IN_QUEUE);
            }
            if (keyData == (Keys.Control | Keys.V))
            {
                m_TextPasted = true;
                //base.OnKeyDown(e);
            }
            if (keyData == (Keys.Control | Keys.W))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCAIR_RATE);
            }
            if (keyData == (Keys.Control | Keys.Z))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.TicketEmission.Constants.UC_PHASEIV_FIRST_STEPS);
            }
            if (keyData == (Keys.Control | Keys.Enter))
            {
                ucMenuReservations.DeployRecordCommandsSend();
            }
            if (keyData == (Keys.Control | Keys.Back))
            {
            }
            if (keyData == (Keys.Escape))
            {
            }
            if (keyData == (Keys.Alt | Keys.M))
            {
            }
            if (keyData == Keys.Control)
            {
            }
            if (keyData == (Keys.Control | Keys.E))
            {
            }
            if (keyData == (Keys.Control | Keys.LShiftKey | Keys.LButton))
            {
            }
            if (keyData == (Keys.Control | Keys.Delete))
            {
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCCANCEL_SEGMENTS);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion



        void txtErrorHandler_ErrorAssign(string errorValue)
        {
            //this.txtError.Text = errorValue;
        }

        public CheckStatusDelegate CheckStatus;
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("¿Estás seguro que deseas salir?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
                CheckStatus(sender, e);
            }
            else
            {
                if (IsSigned)
                {
                    MessageBox.Show("Antes de cerrar MyCTS debe desfirmarse!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                    return;
                }
                timer1.Enabled = false;

                try
                {
                    client_Disconnect();
                }
                catch { }

                try
                {
                    unload_DLL();
                }
                catch { }

                CheckStatus(sender, e);

            }

        }

        private List<CommandsActions> ListCommandsActions;
        private void LoadCommandsRestricted()
        {
            ListCommandsActions = CommandsActionsBL.GetCommandsActions();

            string commandsRestricted = string.Empty;
            string commandsAllowed = string.Empty;
            string commandsToRegister = string.Empty;

            foreach (CommandsActions item in ListCommandsActions)
            {
                commandsToRegister += item.Command + ",";
            }

            commandsToRegister = commandsToRegister.Remove(commandsToRegister.Length - 1);

            if (IsRegmarkup)
                unRegisterSabreMarkup();

            registerSabreMarkup(commandsToRegister);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblBanner.Left -= 1;
            if (lblBanner.Left + lblBanner.Width < 0)
                lblBanner.Left = panel1.Width;
            ActiveDataBase();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void CheckConnection()
        {
            if (IsSigned)
                return;

            CommandsAPI.ValidateMarkups = false;

            string res = string.Empty;
            using (CommandsAPI command = new CommandsAPI())
            {
                res = command.SendReceive("¤A", 0, 0);
            }

            if (!(res.Contains("SIGN IN")))
            {
                IsSigned = true;
                ReceiverSigned();
            }

            CommandsAPI.ValidateMarkups = true;

        }

        #region Eventos ToolStrip
        private void toolStripButtonIntegra_Click(object sender, EventArgs e)
        {
            string url = ParameterBL.GetParameterValue("IntegraButtonURL").Values;
            System.Diagnostics.Process.Start(url);
        }

        private void toolStripButtonGEA_Click(object sender, EventArgs e)
        {
            string url = ParameterBL.GetParameterValue("GEAButtonURL").Values;
            System.Diagnostics.Process.Start(url);
        }

        private void toolStripButtonInforma_Click(object sender, EventArgs e)
        {
            string url = ParameterBL.GetParameterValue("InformaButtonURL").Values;
            System.Diagnostics.Process.Start(url);
        }

        private void toolStripButtonTotalTrip_Click(object sender, EventArgs e)
        {
            string url = ParameterBL.GetParameterValue("TotalTripButtonURL").Values;
            System.Diagnostics.Process.Start(url);
        }

        private void toolStripButtonReports_Click(object sender, EventArgs e)
        {
            string url = ParameterBL.GetParameterValue("ReportsButtonURL").Values;
            System.Diagnostics.Process.Start(url);
        }

        private void toolStripButtonEinvoice_Click(object sender, EventArgs e)
        {
            string url = ParameterBL.GetParameterValue("eInvoicedButtonURL").Values;
            System.Diagnostics.Process.Start(url);
        }

        private void toolStripButtonQueues_Click(object sender, EventArgs e)
        {
            string messageQueue = ParameterBL.GetParameterValue("MessageQueue").Values;
            string numQueue = Login.Queue;

            string sabreAnswer = string.Empty;
            string commandQueue = string.Format("QC/{0}", numQueue);

            using (CommandsAPI command = new CommandsAPI())
            {
                sabreAnswer = command.SendReceive(commandQueue, 0, 0);
            }

            if (string.IsNullOrEmpty(sabreAnswer) | sabreAnswer.Equals("NOTHING"))
                return;

            int row = 0;
            int col = 0;
            string result = string.Empty;
            try
            {
                CommandsQik.searchResponse(sabreAnswer, "HAS", ref row, ref col, 2);
                col += 4;
                CommandsQik.CopyResponse(sabreAnswer, ref result, row, col, 9);
                int queues = 0;
                Int32.TryParse(result, out queues);

                if (queues > 0)
                {
                    lblQueue.Text = string.Format(messageQueue, Login.Queue, queues);
                }
            }
            catch { }

        }

        private void toolStripButtonViewInvoice_Click(object sender, EventArgs e)
        {
            BuildElectronicTicketContract ws = new BuildElectronicTicketContract();
            string request = ws.Encrypt(string.Concat(Login.Firm, ";", Login.passWord, ";", Login.PCC));
            string url = string.Concat("http://201.149.13.14/Login.aspx?byPass=", request);
            Process.Start(url);
        }


        private void toolStripButtonPassengerReceive_Click(object sender, EventArgs e)
        {
            Loader.AddToPanelWithParameters(Loader.Zone.Middle, this, Resources.Constants.UC_GET_INVOICE, new string[] { "2" });
        }
        #endregion


        private void lblBanner_DoubleClick(object sender, EventArgs e)
        {
            string passwordCommands = Parameters.PasswordManualCommands;

            if (string.IsNullOrEmpty(passwordCommands))
            {
                MessageBox.Show("No ha sido carga la contraseña, verifique que exista el parámetro\nen la BD o consulte con el administrador",
                    "Información", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            InputBoxDialog ib = new InputBoxDialog();
            ib.FormPrompt = "Contraseña";
            ib.FormCaption = "Comandos";
            ib.DefaultValue = string.Empty;
            ib.ModeToShow = InputBoxDialog.ModeTextBox.Password;
            ib.ShowDialog();

            string s = ib.InputResponse;
            ib.Close();

            if (s.Equals(Parameters.PasswordManualCommands))
            {
                frmManualCommands frm = new frmManualCommands();
                frm.ShowDialog();
            }
            else
                MessageBox.Show("Contraseña incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        /// <summary>
        /// Verifica archivos que están en queue para insertar
        /// instrucciones contenidas
        /// </summary>
        private void ReadQueueFiles()
        {
            //if (Common.IsManualCommandTransactions)
            //    return;

            _timerAnalyzer.Stop();
            Common.ReadQueueFiles();
            _timerAnalyzer.Start();

        }

        /// <summary>
        /// Actives the data base.
        /// </summary>
        private void ActiveDataBase()
        {
            statusNotification.BringToFront();
            statusNotification.Items[0].BackColor = Color.Lime;
            statusNotification.Items[0].Text = "Status Connection Producción";
            if (ConfigurationManager.AppSettings["MirrorOn"]=="On")
            {
                statusNotification.Items[0].BackColor = Color.Yellow;
                statusNotification.Items[0].Text = "Status Connection BackUp";
            }
        
        }
    }
}