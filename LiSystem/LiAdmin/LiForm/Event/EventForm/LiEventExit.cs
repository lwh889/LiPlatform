using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using DevExpress.XtraEditors;
using LiFlow.Enums;

namespace LiForm.Event.EventForm
{
    public class LiEventExit : LiAEvent
    {
        public override void receiveEvent()
        {
            //TreeListLookUpEdit treeListLookUpEdit = liForm.liControlDict["cMaterialGroup"] as TreeListLookUpEdit;
            //treeListLookUpEdit.EditValue = Convert.ToInt64("8");
            //treeListLookUpEdit.Refresh();
            //this.liForm.revokeFlow(RevokeType.UnSubmit);
            liForm.Close();
        }
        public override void sendEvent()
        {
            Console.WriteLine("退出发出请求。");
            eventMediator.relay(this); //请中介者转发
        }
    }
}
