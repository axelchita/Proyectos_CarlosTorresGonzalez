using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyCTS.Entities;
using MyCTS.Business;
using MyCTS.Presentation.Components;
using System.Text.RegularExpressions;

namespace MyCTS.Presentation
{
    class ERR_SellSegmentGK
    {
        private static string answer;
        public static string Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        private static bool status;
        public static bool Status
        {
            get { return status; }
            set { status = value; }
        }

        private static string customusermsg;
        public static string CustomUserMsg
        {
            get { return customusermsg; }
            set { customusermsg = value; }
        }


        public static void err_boletagedataandreceived(string result)
        {
            Answer = result;
            status = false;
            int row = 0;
            int col = 0;

            CommandsQik.searchResponse(result, Resources.Group.ErrorMessage.CROSSLORAINE, ref row, ref col,1,1,1,1);
            if (row != 0 || col != 0)
            {
               SabreErrorMsg userErrorMessage = SabreErrorMsgBL.GetSabreErrorMsg(Resources.Group.ErrorMessage.CROSSLORAINE, 20);
                status = true;
                row = 0;
                col = 0;
                return;
            }
        }
    }
}