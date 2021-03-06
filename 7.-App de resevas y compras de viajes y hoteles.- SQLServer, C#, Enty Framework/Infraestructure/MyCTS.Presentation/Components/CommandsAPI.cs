using System;
using System.Collections.Generic;
using System.Text;
using MyCTS.Components;

namespace MyCTS.Presentation.Components
{

    public class CommandsAPILog
    {
        private static void WriteCommandIntoDBCallBack(string command)
        {
            LogProductivity.AddLogProductivity(command, 0);
        }

        private delegate void SenderDB(string command);
        private delegate void SenderDBCallBack(string command);
        public static void WriteCommandIntoFile(string command)
        {
            SenderDBCallBack smh = new SenderDBCallBack(WriteCommandIntoDBCallBack);
            smh.BeginInvoke(command, null, null);
        }
    }

    /// <summary>
    /// Permite recibir el último mensaje que envió el emulador
    /// </summary>
    public class CommandsAPI : IDisposable
    {
        private int timeAPIResponse = (Parameters.TimeExtendedAPI) ? Int32.Parse(Parameters.CurrentTimeAPIExtended) : Int32.Parse(Parameters.CurrentTimeAPINormal);
        private const long MESSAGE_TYPE = 1 | 2;
        CallBackHandler.MySabreCallback cbCustom;
        private string singleMessage = string.Empty;
        private string allMessage = string.Empty;
        private string currentmsg = string.Empty;
        private const string WAIT_MESSAGE = "PLEASE WAIT";

        public CommandsAPI()
        {
            try
            {
                cbCustom = new CallBackHandler.MySabreCallback(CustomDelegate);
                long output = CallBackHandler.beginSabreTrafficListener_stdcall(Convert.ToInt32(MESSAGE_TYPE), cbCustom);
            }
            catch( Exception EX)
            {
            }
        }

        private string sabreCommand = string.Empty;
        private int CustomDelegate(int a, string b)
        {
            if (a.Equals(1))
                sabreCommand = b; //guarda el comando que ha sido ingresado al host

            if (a.Equals(2))
            {
                singleMessage = b;
                allMessage += b;

                if (singleMessage.Contains(WAIT_MESSAGE))
                    singleMessage = string.Empty;
                else
                {
                    isfinish = true;
                    Common.FindPNR(singleMessage);
                    //Components.CommandsAPILog.WriteCommandIntoFile(sabreCommand);
                }

            }
            else
            {
                isfinish = false;
            }
            return 0;
        }

        private bool isfinish = false;
        public static bool ValidateMarkups = true; //Bandera para no validar markups
        private int counter = 1;

        public string SendReceive(string message)
        {
            return SendReceive(message, false);
        }

        public string SendReceiveEmission(string message)
        {
            return SendReceiveEmission(message, false);
        }

        public string SendReceiveProfile(string message)
        {
            return SendReceiveProfile(message, false);
        }

        public string SendReceive(string message, bool showAllContent)
        {
            return SendReceive(message, 1, 1, showAllContent);
        }

        public string SendReceiveEmission(string message, bool showAllContent)
        {
            return SendReceiveEmission(message, 1, 1, showAllContent);
        }

        public string SendReceiveProfile(string message, bool showAllContent)
        {
            return SendReceiveProfile(message, 1, 1, showAllContent);
        }

        public string SendReceive(string message, int showCommand)
        {
            return SendReceive(message, showCommand, 1, false);
        }

        public string SendReceiveEmission(string message, int showCommand)
        {
            return SendReceiveEmission(message, showCommand, 1, false);
        }

        public string SendReceive(string message, int showCommand, bool showAllContent)
        {
            return SendReceive(message, showCommand, 1, showAllContent);
        }

        public string SendReceiveEmission(string message, int showCommand, bool showAllContent)
        {
            return SendReceiveEmission(message, showCommand, 1, showAllContent);
        }

        public string SendReceive(string message, int showCommand, int showResponse)
        {
            return SendReceive(message, showCommand, showResponse, false);
        }


        public string SendReceive(string message, int showCommand, int showResponse, bool showAllContent)
        {
            ValidateMarkups = false;

            currentmsg = message;
            singleMessage = string.Empty;
            message = CustomFunction.convertToSabreChars(message);
            MySabreAPI.sendCommandToHost2(ref message, showCommand, showResponse);
            cbCustom.Invoke(0, string.Empty);

            string finalresult = string.Empty;

            while (!isfinish)
            {
                if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                    isfinish = true;
                else
                {
                    if (counter > timeAPIResponse) isfinish = true;
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        counter++;
                    }
                }
            }

            counter = 1;
            IsFinish(true);
            Components.CommandsAPILog.WriteCommandIntoFile(string. Concat(sabreCommand, " -APP"));
            ValidateMarkups = true;

            if (string.IsNullOrEmpty(singleMessage))
                singleMessage = "NOTHING";

            return (showAllContent) ? allMessage : singleMessage;
        }

        public string SendReceiveEmail(string message, int showCommand, int showResponse, bool showAllContent)
        {

            ValidateMarkups = false;

            currentmsg = message;
            singleMessage = string.Empty;
            message = CustomFunction.convertToSabreCharsEmail(message);
            MySabreAPI.sendCommandToHost2(ref message, showCommand, showResponse);
            cbCustom.Invoke(0, string.Empty);

            string finalresult = string.Empty;

            while (!isfinish)
            {
                if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                    isfinish = true;
                else
                {
                    if (counter > timeAPIResponse) isfinish = true;
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        counter++;
                    }
                }
            }

            counter = 1;
            IsFinish(true);
            Components.CommandsAPILog.WriteCommandIntoFile(string.Concat(sabreCommand, " -APP"));
            ValidateMarkups = true;

            if (string.IsNullOrEmpty(singleMessage))
                singleMessage = "NOTHING";

            return (showAllContent) ? allMessage : singleMessage;
        }


        public string SendReceiveEmission(string message, int showCommand, int showResponse, bool showAllContent)
        {
            ValidateMarkups = false;

            currentmsg = message;
            singleMessage = string.Empty;
            message = CustomFunction.convertToSabreChars(message);
            MySabreAPI.sendCommandToHost2(ref message, showCommand, showResponse);
            cbCustom.Invoke(0, string.Empty);

            string finalresult = string.Empty;

            while (!isfinish)
            {
                if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                    isfinish = true;
                else
                {
                    if (counter > timeAPIResponse) isfinish = true;
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        counter++;
                    }
                }
            }

            counter = 1;
            IsFinish(true);
            Components.CommandsAPILog.WriteCommandIntoFile(string.Concat(sabreCommand, "|", allMessage));
            ValidateMarkups = true;

            if (string.IsNullOrEmpty(singleMessage))
                singleMessage = "NOTHING";

            return (showAllContent) ? allMessage : singleMessage;
        }

        public string SendReceiveProfile(string message, int showCommand, int showResponse, bool showAllContent)
        {
            ValidateMarkups = false;

            currentmsg = message;
            singleMessage = string.Empty;
            message = CustomFunction.convertToSabreCharsProfile(message);
            MySabreAPI.sendCommandToHost2(ref message, showCommand, showResponse);
            cbCustom.Invoke(0, string.Empty);

            string finalresult = string.Empty;

            while (!isfinish)
            {
                if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                    isfinish = true;
                else
                {
                    if (counter > timeAPIResponse) isfinish = true;
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        counter++;
                    }
                }
            }

            counter = 1;
            IsFinish(true);
            Components.CommandsAPILog.WriteCommandIntoFile(string.Concat(sabreCommand ," -APP"));
            ValidateMarkups = true;

            if (string.IsNullOrEmpty(singleMessage))
                singleMessage = "NOTHING";

            return (showAllContent) ? allMessage : singleMessage;
        }

        private void IsFinish(bool ready)
        {
            if (ready)
            {
                System.Threading.Thread.Sleep(500);
                while (!isfinish)
                {
                    if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                        isfinish = true;
                    else
                    {
                        if (counter > timeAPIResponse) isfinish = true;
                        else
                        {
                            IsFinish(false);
                            counter++;
                        }
                    }
                }

            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            long output = CallBackHandler.endSabreTrafficListener_stdcall(cbCustom);
        }

        #endregion
    }

    public class CustomFunction
    {
        private static char SABRE_CROSS_OF_LORRAINE_CHARACTER = '‡';
        private static char SABRE_CHANGE_CHARACTER = Convert.ToChar(164);
        private static char SABRE_END_ITEM_CHARACTER = '§';

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string convertToSabreChars(string text)
        {

            string str = text;
            if (str.Length != 0)
            {
                str = str.Replace("¥", SABRE_CROSS_OF_LORRAINE_CHARACTER.ToString());
                str = str.Replace("¤", SABRE_CHANGE_CHARACTER.ToString());
                str = str.Replace("Σ", SABRE_END_ITEM_CHARACTER.ToString());
                str = str.Replace("@", SABRE_CHANGE_CHARACTER.ToString());
            }

            return str;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string convertToSabreCharsEmail(string text)
        {
            string str = text;
            if (str.Length != 0)
            {

                str = str.Replace("¥", SABRE_CROSS_OF_LORRAINE_CHARACTER.ToString());
                str = str.Replace("¤", SABRE_CHANGE_CHARACTER.ToString());
                str = str.Replace("Σ", SABRE_END_ITEM_CHARACTER.ToString());
            }

            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string convertToSabreCharsProfile(string text)
        {
            string str = text;
            if (str.Length != 0)
            {
                str = str.Replace("¥", SABRE_CROSS_OF_LORRAINE_CHARACTER.ToString());
                str = str.Replace("¤", SABRE_CHANGE_CHARACTER.ToString());
                str = str.Replace("Σ", SABRE_END_ITEM_CHARACTER.ToString());
            }

            return str;
        }
    }

    /// <summary>
    /// Permite recibir toda la respuesta de multiples 
    /// mensajes del emulador
    /// </summary>
    public class CommandsAPI2 : IDisposable
    {
        private int timeAPIResponse = (Parameters.TimeExtendedAPI) ? Int32.Parse(Parameters.CurrentTimeAPIExtended) : Int32.Parse(Parameters.CurrentTimeAPINormal);
        private const long MESSAGE_TYPE = 1 | 2;
        CallBackHandler.MySabreCallback cbCustom;
        private string myresult = string.Empty;
        private string currentmsg = string.Empty;
        private const string WAIT_MESSAGE = "PLEASE WAIT";
        private bool isfinish = false;

        private int counter = 1;
        private string sabreCommand = string.Empty;

        public CommandsAPI2()
        {
            cbCustom = new CallBackHandler.MySabreCallback(CustomDelegate);
            long output = CallBackHandler.beginSabreTrafficListener_stdcall(Convert.ToInt32(MESSAGE_TYPE), cbCustom);
        }
        //
        private int CustomDelegate(int a, string b)
        {
            if (a.Equals(1))
                sabreCommand = b; //guarda el comando que ha sido ingresado al host

            if (a.Equals(2))
            {
                isfinish = true;
                myresult += "\n" + b;
                Common.FindPNR(b);
            }
            else
            {
                isfinish = false;
            }
            return 0;
        }

        private int defaultmillisecondsTimeout = 500;

        public string SendReceive(string message)
        {
            return SendReceive(message, 1, 1);
        }

        public string SendReceive(string message, int showCommand)
        {
            return SendReceive(message, showCommand, 1);
        }

        public string SendReceive(string message, int showCommand, int showResponse)
        {
            return SendReceive(message, showCommand, showResponse, defaultmillisecondsTimeout);
        }


        public string SendReceiveEmission(string message)
        {
            return SendReceiveEmission(message, 1, 1);
        }

        public string SendReceiveEmission(string message, int showCommand)
        {
            return SendReceiveEmission(message, showCommand, 1);
        }

        public string SendReceiveEmission(string message, int showCommand, int showResponse)
        {
            return SendReceiveEmission(message, showCommand, showResponse, defaultmillisecondsTimeout);
        }

        public string SendReceive(string message, int showCommand, int showResponse, int millisecondsTimeout)
        {
            CommandsAPI.ValidateMarkups = false;
            defaultmillisecondsTimeout = millisecondsTimeout;

            currentmsg = message;
            myresult = string.Empty;
            message = CustomFunction.convertToSabreChars(message);
            MySabreAPI.sendCommandToHost2(ref message, showCommand, showResponse);
            cbCustom.Invoke(0, string.Empty);

            string finalresult = string.Empty;

            while (!isfinish)
            {
                if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                    isfinish = true;
                else
                {
                    if (counter > timeAPIResponse) isfinish = true;
                    else
                    {
                        System.Threading.Thread.Sleep(defaultmillisecondsTimeout);
                        counter++;
                    }

                }

            }

            counter = 1;
            IsFinish(true);

            if (myresult.Contains("PREVIOUS ENTRY ACTIVE-PLEASE WAIT FOR RESPONSE"))
                IsFinish(true);

            Components.CommandsAPILog.WriteCommandIntoFile(string.Concat(sabreCommand, " -APP"));

            CommandsAPI.ValidateMarkups = true;

            if (string.IsNullOrEmpty(myresult))
                myresult = "NOTHING";

            return myresult;

        }

        public string SendReceiveEmission(string message, int showCommand, int showResponse, int millisecondsTimeout)
        {
            CommandsAPI.ValidateMarkups = false;
            defaultmillisecondsTimeout = millisecondsTimeout;

            currentmsg = message;
            myresult = string.Empty;
            message = CustomFunction.convertToSabreChars(message);
            MySabreAPI.sendCommandToHost2(ref message, showCommand, showResponse);
            cbCustom.Invoke(0, string.Empty);

            string finalresult = string.Empty;

            while (!isfinish)
            {
                if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                    isfinish = true;
                else
                {
                    if (counter > timeAPIResponse) isfinish = true;
                    else
                    {
                        System.Threading.Thread.Sleep(defaultmillisecondsTimeout);
                        counter++;
                    }

                }

            }

            counter = 1;
            IsFinish(true);

            if (myresult.Contains("PREVIOUS ENTRY ACTIVE-PLEASE WAIT FOR RESPONSE"))
                IsFinish(true);

            Components.CommandsAPILog.WriteCommandIntoFile(string.Concat(sabreCommand, "|", myresult));

            CommandsAPI.ValidateMarkups = true;

            if (string.IsNullOrEmpty(myresult))
                myresult = "NOTHING";

            return myresult;

        }

        private void IsFinish(bool ready)
        {
            if (ready)
            {
                System.Threading.Thread.Sleep(defaultmillisecondsTimeout);
                while (!isfinish)
                {
                    if (frmMain.getApiStateFromEvents().Equals(frmMain.API_UNAVAILABLE))
                        isfinish = true;
                    else
                    {
                        if (counter > timeAPIResponse) isfinish = true;
                        else
                        {
                            IsFinish(false);
                            counter++;
                        }
                    }
                }

            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            long output = CallBackHandler.endSabreTrafficListener_stdcall(cbCustom);
        }

        #endregion

        #region Messages To Emulator

        public static void send_CommandToEmulator(string message)
        {
            long output;
            message = CustomFunction.convertToSabreChars(message);
            output = MySabreAPI.sendCommandToEmulator(ref message);
        }
        public static void send_MessageToEmulator(string message)
        {
            long output;
            Components.CommandsAPILog.WriteCommandIntoFile(message);
            message = CustomFunction.convertToSabreChars(message);
            output = MySabreAPI.sendMessageToEmulator(ref message);
        }

        #endregion

    }
}
