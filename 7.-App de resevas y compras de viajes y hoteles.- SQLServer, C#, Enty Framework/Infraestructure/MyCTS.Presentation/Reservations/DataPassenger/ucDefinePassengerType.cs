using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MyCTS.Presentation.Components;


namespace MyCTS.Presentation
{
    public partial class ucDefinePassengerType : CustomUserControl
    {

        /// <summary>
        /// Descripcion: Permite definir que tipo de pasajero es la persona que tomara el viaje,
        ///              pertenece al flujo de Reservaciones
        /// Creación:    Diciembre 08 -Marzo 09 , Modificación:*
        /// Cambio:      *    , Solicito *
        /// Autor:       Jessica Gutierrez 
        /// </summary>

        #region ====== Declaration of Varibles ======

        private string passTypeS;
        private string passTypeST2;
        private string passTypeS2;
        private string passTypeST3;
        private string passTypeS3;
        private string passTypeST4;
        private string passTypeS4;
        private string passTypeST5;
        private string passTypeS5;
        private string passTypeST6;
        private string passTypeS6;
        private string passTypeST7;
        private string passTypeS7;
        private string passTypeST8;
        private string passTypeS8;
        private string passTypeST9;
        private string passTypeS9;
        private string passTypeST;
        private string result;

        #endregion

        public ucDefinePassengerType()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
              ControlStyles.UserPaint |
              ControlStyles.DoubleBuffer, true);
            this.InitialControlFocus = cmbPassengerType1;
            this.LastControlFocus = btnAccept;
        }


        //User Control Load
        /// <summary>
        /// Se extraen los nombres del emulador y se asignan en los controles
        /// Se asigna el foco al primer combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucDefinePassengerType_Load(object sender, EventArgs e)
        {
            ucAvailability.IsInterJetProcess = false;
            ExtractNamesFromResponse();
            APIResponse();
            cmbPassengerType1.Focus();
        }

        //Button Accept
        /// <summary>
        /// Se realizan las validaciones despues de que el usuario ingresa datos, 
        /// se mandan los comandos y termina el proceso llamando a otro User Control 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (IsValidateBusinessRules)
                CommandsSend();
        }

        //KeyDown
        /// <summary>
        /// Este se usa para todos los controles en general si se oprime 
        /// Esc se manda a el User control de Welcome 
        /// Enter se manda la accion de botón de Aceptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackEnterUserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCADD_MORE_PASSENGER);

            if (e.KeyData == Keys.Enter)
                btnAccept_Click(sender, e);
        }


        #region ===== Selected Passenger Type =====

        /// <summary>
        /// Extraigo el codigo de los combos seleccionados para asignar
        /// el tipo de pasajero 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPassengerType1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType1.SelectedIndex > 0)
            {
                passTypeST = cmbPassengerType1.Text;
                passTypeS = passTypeST.Substring(0, 3);
            }
            else
                passTypeS = string.Empty;
        }

        private void cmbPassengerType2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType2.SelectedIndex > 0)
            {
                passTypeST2 = cmbPassengerType2.Text;
                passTypeS2 = passTypeST2.Substring(0, 3);
            }
            else
                passTypeS2 = string.Empty;
        }

        private void cmbPassengerType3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType3.SelectedIndex > 0)
            {
                passTypeST3 = cmbPassengerType3.Text;
                passTypeS3 = passTypeST3.Substring(0, 3);
            }
            else
                passTypeS3 = string.Empty;
        }

        private void cmbPassengerType4_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType4.SelectedIndex > 0)
            {
                passTypeST4 = cmbPassengerType4.Text;
                passTypeS4 = passTypeST4.Substring(0, 3);
            }
            else
                passTypeS4 = string.Empty;
        }

        private void cmbPassengerType5_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType5.SelectedIndex > 0)
            {
                passTypeST5 = cmbPassengerType5.Text;
                passTypeS5 = passTypeST5.Substring(0, 3);
            }
            else
                passTypeS5 = string.Empty;
        }

        private void cmbPassengerType6_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType6.SelectedIndex > 0)
            {
                passTypeST6 = cmbPassengerType6.Text;
                passTypeS6 = passTypeST6.Substring(0, 3);
            }
            else
                passTypeS6 = string.Empty;
        }

        private void cmbPassengerType7_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType7.SelectedIndex > 0)
            {
                passTypeST7 = cmbPassengerType7.Text;
                passTypeS7 = passTypeST7.Substring(0, 3);
            }
            else
                passTypeS7 = string.Empty;
        }

        private void cmbPassengerType8_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType1.SelectedIndex > 0)
            {
                passTypeST8 = cmbPassengerType8.Text;
                passTypeS8 = passTypeST8.Substring(0, 3);
            }
            else
                passTypeS8 = string.Empty;
        }

        private void cmbPassengerType9_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbPassengerType1.SelectedIndex > 0)
            {
                passTypeST9 = cmbPassengerType9.Text;
                passTypeS9 = passTypeST9.Substring(0, 3);
            }
            else
                passTypeS9 = string.Empty;
        }

        #endregion


        #region ===== methodsClass =====

        /// <summary>
        /// Valido que los campos obligatorios no esten vacios y que no 
        /// contengan datos no permitidos.
        /// </summary>
        private bool IsValidateBusinessRules
        {
            get
            {
                if (passTypeS == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS2 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS3 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS4 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS5 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS6 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS7 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS8 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT ||
                             passTypeS9 == Resources.Constants.INDENT || passTypeS == Resources.Constants.IDENT_IDENT || passTypeS == Resources.Constants.IDENT_IDENT_IDENT)
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO_VALIDO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                else if ((!string.IsNullOrEmpty(txtPossicionPassenger1.Text)) &&
                              string.IsNullOrEmpty(passTypeS))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType1.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger2.Text)) &&
                          string.IsNullOrEmpty(passTypeS2))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType2.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger3.Text)) &&
                        string.IsNullOrEmpty(passTypeS3))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType3.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger4.Text)) &&
                        string.IsNullOrEmpty(passTypeS4))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType4.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger5.Text)) &&
                    string.IsNullOrEmpty(passTypeS5))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType5.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger6.Text)) &&
                    string.IsNullOrEmpty(passTypeS6))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType6.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger7.Text)) &&
                    string.IsNullOrEmpty(passTypeS7))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType7.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger8.Text)) &&
                    string.IsNullOrEmpty(passTypeS8))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType8.Focus();
                    return false;
                }
                else if ((!string.IsNullOrEmpty(txtPossicionPassenger9.Text)) &&
               string.IsNullOrEmpty(passTypeS9))
                {
                    MessageBox.Show(Resources.Reservations.ELIGE_TIPO_PASAJERO, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cmbPassengerType9.Focus();
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// Se envia el comando de acuerdo a los campos ingresados
        /// </summary>
        private void CommandsSend()
        {
            #region ====== Declaration of Varibles=======

            string firstPassenger = string.Empty;
            string secondName = string.Empty;
            string thirdName = string.Empty;
            string fourthName = string.Empty;
            string fifthName = string.Empty;
            string sixthName = string.Empty;
            string seventhName = string.Empty;
            string eighthName = string.Empty;
            string nineName = string.Empty;
            string send = string.Empty;

            #endregion

            firstPassenger = Resources.Constants.COMMANDS_PDT + passTypeS + Resources.Constants.INDENT + txtPossicionPassenger1.Text;
            secondName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS2 + Resources.Constants.INDENT + txtPossicionPassenger2.Text;
            thirdName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS3 + Resources.Constants.INDENT + txtPossicionPassenger3.Text;
            fourthName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS4 + Resources.Constants.INDENT + txtPossicionPassenger4.Text;
            fifthName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS5 + Resources.Constants.INDENT + txtPossicionPassenger5.Text;
            sixthName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS6 + Resources.Constants.INDENT + txtPossicionPassenger6.Text;
            seventhName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS7 + Resources.Constants.INDENT + txtPossicionPassenger7.Text;
            eighthName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS8 + Resources.Constants.INDENT + txtPossicionPassenger8.Text;
            nineName = Resources.Constants.COMMANDS_END_IT_PDT + passTypeS9 + Resources.Constants.INDENT + txtPossicionPassenger9.Text;

            if ((!string.IsNullOrEmpty(passTypeS)) && (!string.IsNullOrEmpty(txtPossicionPassenger1.Text)))
                send = string.Concat(send, firstPassenger);
            if ((!string.IsNullOrEmpty(passTypeS2)) && (!string.IsNullOrEmpty(txtPossicionPassenger2.Text)))
                send = string.Concat(send, secondName);
            if ((!string.IsNullOrEmpty(passTypeS3)) && (!string.IsNullOrEmpty(txtPossicionPassenger3.Text)))
                send = string.Concat(send, thirdName);
            if ((!string.IsNullOrEmpty(passTypeS4)) && (!string.IsNullOrEmpty(txtPossicionPassenger4.Text)))
                send = string.Concat(send, fourthName);
            if ((!string.IsNullOrEmpty(passTypeS5)) && (!string.IsNullOrEmpty(txtPossicionPassenger5.Text)))
                send = string.Concat(send, fifthName);
            if ((!string.IsNullOrEmpty(passTypeS6)) && (!string.IsNullOrEmpty(txtPossicionPassenger6.Text)))
                send = string.Concat(send, sixthName);
            if ((!string.IsNullOrEmpty(passTypeS7)) && (!string.IsNullOrEmpty(txtPossicionPassenger7.Text)))
                send = string.Concat(send, seventhName);
            if ((!string.IsNullOrEmpty(passTypeS8)) && (!string.IsNullOrEmpty(txtPossicionPassenger8.Text)))
                send = string.Concat(send, eighthName);
            if ((!string.IsNullOrEmpty(passTypeS9)) && (!string.IsNullOrEmpty(txtPossicionPassenger9.Text)))
                send = string.Concat(send, nineName);

            using (CommandsAPI objCommands = new CommandsAPI())
            {
                objCommands.SendReceive(send);
                objCommands.SendReceive(Resources.Constants.COMMANDS_PD);
            }

            if (passTypeS == Resources.Constants.CHOOSES_INF ||
                 passTypeS2 == Resources.Constants.CHOOSES_INF ||
                 passTypeS3 == Resources.Constants.CHOOSES_INF ||
                 passTypeS4 == Resources.Constants.CHOOSES_INF ||
                 passTypeS5 == Resources.Constants.CHOOSES_INF ||
                 passTypeS6 == Resources.Constants.CHOOSES_INF ||
                 passTypeS7 == Resources.Constants.CHOOSES_INF ||
                 passTypeS8 == Resources.Constants.CHOOSES_INF ||
                 passTypeS9 == Resources.Constants.CHOOSES_INF)
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCINF_PASSENGER_TYPE);
            else
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCBOLETAGE_DATEANDRECEIVED);
        }

        /// <summary>
        /// Se manda un comando a Sabre y deacuerdo a la respuesta se extraen
        /// las posiciones y nombres de los pasajeros que fueron ingresados para
        /// la reservación
        /// </summary>
        private void ExtractNamesFromResponse()
        {
            #region====== Declaration of variables =======
            string name1;
            string position;
            string nameone;
            string name2;
            string position2;
            string nametwo;
            string name3;
            string position3;
            string namethree;
            string name4;
            string position4;
            string namefour;
            string name5;
            string position5;
            string namefive;
            string name6;
            string position6;
            string namesix;
            string name7;
            string position7;
            string nameseven;
            string name8;
            string position8;
            string nameight;
            string name9;
            string position9;
            string namenine;
            #endregion

            using (CommandsAPI objCommands = new CommandsAPI())
            {
                result = objCommands.SendReceive(Resources.Constants.COMMANDS_PD);
            }

            ERR_DefinePassengerType.err_definepassengertype(result);

            if (ERR_DefinePassengerType.Status)
            {
                name1 = ERR_DefinePassengerType.Namesend1;
                position = (name1.Substring(0, 4));
                nameone = (name1.Substring(4, 18));
                txtNamePassenger1.Text = nameone;
                txtPossicionPassenger1.Text = Resources.ErrorMessages.POSITION_ONE;
                cmbPassengerType1.SelectedIndex = 1;
            }
            if (ERR_DefinePassengerType.Name2)
            {
                name2 = ERR_DefinePassengerType.Namesend2;
                position2 = (name2.Substring(0, 4));
                nametwo = (name2.Substring(4, 18));
                txtNamePassenger2.Text = nametwo;
                txtPossicionPassenger2.Text = Resources.ErrorMessages.POSITION_TWO;
                cmbPassengerType2.SelectedIndex = 1;
            }
            if (ERR_DefinePassengerType.Name3)
            {
                name3 = ERR_DefinePassengerType.Namesend3;
                position3 = (name3.Substring(0, 4));
                namethree = (name3.Substring(4, 18));
                txtNamePassenger3.Text = namethree;
                txtPossicionPassenger3.Text = Resources.ErrorMessages.POSITION_THREE;
                cmbPassengerType3.SelectedIndex = 1;
            }
            if (ERR_DefinePassengerType.Name4)
            {
                name4 = ERR_DefinePassengerType.Namesend4;
                position4 = (name4.Substring(0, 4));
                namefour = (name4.Substring(4, 18));
                txtNamePassenger4.Text = namefour;
                txtPossicionPassenger4.Text = Resources.ErrorMessages.POSITION_FOUR;
                cmbPassengerType4.SelectedIndex = 1;


            }
            if (ERR_DefinePassengerType.Name5)
            {
                name5 = ERR_DefinePassengerType.Namesend5;
                position5 = (name5.Substring(0, 4));
                namefive = (name5.Substring(4, 18));
                txtNamePassenger5.Text = namefive;
                txtPossicionPassenger5.Text = Resources.ErrorMessages.POSITION_FIVE;
                cmbPassengerType5.SelectedIndex = 1;
            }
            if (ERR_DefinePassengerType.Name6)
            {
                name6 = ERR_DefinePassengerType.Namesend6;
                position6 = (name6.Substring(0, 4));
                namesix = (name6.Substring(4, 18));
                txtNamePassenger6.Text = namesix;
                txtPossicionPassenger6.Text = Resources.ErrorMessages.POSITION_SIX;
                cmbPassengerType6.SelectedIndex = 1;
            }
            if (ERR_DefinePassengerType.Name7)
            {
                name7 = ERR_DefinePassengerType.Namesend7;
                position7 = (name7.Substring(0, 4));
                nameseven = (name7.Substring(4, 18));
                txtNamePassenger7.Text = nameseven;
                txtPossicionPassenger7.Text = Resources.ErrorMessages.POSITION_SEVEN;
                cmbPassengerType7.SelectedIndex = 1;
            }
            if (ERR_DefinePassengerType.Name8)
            {
                name8 = ERR_DefinePassengerType.Namesend8;
                position8 = (name8.Substring(0, 4));
                nameight = (name8.Substring(4, 18));
                txtNamePassenger8.Text = nameight;
                txtPossicionPassenger8.Text = Resources.ErrorMessages.POSITION_EIGHT;
                cmbPassengerType8.SelectedIndex = 1;
            }
            if (ERR_DefinePassengerType.Name9)
            {
                name9 = ERR_DefinePassengerType.Namesend9;
                position9 = (name9.Substring(0, 4));
                namenine = (name9.Substring(4, 18));
                txtNamePassenger9.Text = namenine;
                txtPossicionPassenger9.Text = Resources.ErrorMessages.POSITION_NINE;
                cmbPassengerType9.SelectedIndex = 1;
            }
        }

        #region ===== Commons =====
        /// <summary>
        /// Busca errores en la clase de ERR_DefinePassengerType de acuerdo a las respuestas recibidas por el 
        /// Emulador de Sabre y de acuerdo a ellas se realizan ciertas acciones ya sea
        /// mandar un mensaje de error al usuario notificando del mismo o mandando a otro 
        /// User Control
        /// </summary>
        private void APIResponse()
        {
            if (!ERR_DefinePassengerType.Status)
            {
                MessageBox.Show(Resources.Reservations.NO_EXISTEN_PASAJEROS_RECORD, Resources.Constants.MYCTS, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCBOLETAGE_DATEANDRECEIVED);
            }
        }

        #endregion // End Commons

        #endregion


        public static bool ReturnForMisc = false;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Escape))
            {
                ReturnForMisc = false;
                Loader.AddToPanel(Loader.Zone.Middle, this, Resources.Constants.UCADD_MORE_PASSENGER);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
