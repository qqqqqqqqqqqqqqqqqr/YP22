using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YP22.DBConnect
{
    public partial class Order
    {

     public string ExecutorPC
        {
            get
            {
                if (Executor == null)
                    return "Исполнитель не назначен.Ожидайте";
                else
                    return User1.Name;
            }
        }

        public Visibility VisibilityBtnExt
        {
            get
            {
                if (ExecutionStageId == 1)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }

        public Visibility ManagerOrder
        {
            get
            {
                if (Executor == YP22.Classes.AuthUser.user.id)
                {
                    return Visibility.Visible;
                }
                else
                    return Visibility.Hidden;
            }
        }

        
    }
}
