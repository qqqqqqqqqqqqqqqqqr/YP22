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
                    return (User1.Firstname + "  "+ User1.Name + "  " + User1.LastName);
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

        public Visibility VisibilityBtnS1
        {
            get
            {
                if (ExecutionStageId == 1 || ExecutionStageId == 2)
                {

                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility VisibilityBtnS2
        {
            get
            {
                if (ExecutionStageId == 2 )
                {

                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }

        

        public Visibility VisibilityBtnS3
        {
            get
            {
                if (ExecutionStageId == 4)
                {

                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility VisibilityBtnS4
        {
            get
            {
                if (ExecutionStageId == 5)
                {

                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
            }
        }

        public Visibility VisibilityBtnS5
        {
            get
            {
                if (ExecutionStageId == 6)
                {

                    return Visibility.Visible;
                }
                else
                    return Visibility.Collapsed;
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
