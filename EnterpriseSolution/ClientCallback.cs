using ChattingInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Shell;

namespace EnterpriseSolution
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ClientCallback : IClient
    {
        public void GetMessage(string message, string username)
        {
            //Use line 20 if your program is in XAML, Line 21 is messy but it will have to do since it's almost daisy chaining
            /* ((MainProgram)System.Windows.Application.Current.MainWindow).TakeMessage(message, username);*/
            Startup.mp.TakeMessage(message, username);
           
        }


    }
}
