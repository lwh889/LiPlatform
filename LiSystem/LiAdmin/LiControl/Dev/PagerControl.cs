using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LiControl.Dev
{
    public partial class PagerControl : UserControl
    {
        private int allCount = 0;
        private int pageSize = 1000;
        private int curPage = 1;
        public delegate void MyPagerEvents(int curPage, int pageSize);
        public delegate void ExportEvents(bool singlePage);//单页，所有
        public event MyPagerEvents myPagerEvents;
        public event ExportEvents exportEvents;

        public PagerControl()
        {
            InitializeComponent();
        }
        //计算分页,分页大小，总记录数。
        public void RefreshPager(int allCount, int curPage)
        {
            this.allCount = allCount;
            this.pageSize = Convert.ToInt32(comboBoxEditPageSize.Text);
            this.curPage = curPage;
            this.textEditAllPageCount.Text = GetPageCount().ToString();
            lcStatus.Text = string.Format("(共{0}条记录，每页{1}条，共{2}页)", allCount, pageSize, GetPageCount());
            textEditCurPage.Text = curPage.ToString();
            textEditToPage.Text = curPage.ToString();
            //comboBoxEditPageSize.Text = pageSize.ToString();

            if (curPage == 0)
            {
                if (GetPageCount() > 0)
                {
                    curPage = 1;
                    myPagerEvents(curPage, pageSize);
                }
            }
            if (curPage > GetPageCount())
            {
                curPage = GetPageCount();
                myPagerEvents(curPage, pageSize);
            }

        }
        //获取总记录数
        public int GetAllCount()
        {
            return allCount;
        }
        //获得当前页编号，从1开始
        public int GetCurPage()
        {
            return curPage;
        }
        //获得总页数
        public int GetPageCount()
        {
            int count = 0;
            if (allCount % pageSize == 0)
            {
                count = allCount / pageSize;
            }
            else
                count = allCount / pageSize + 1;
            return count;
        }

        private void simpleButtonNext_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonEnd_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonPre_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonFirst_Click(object sender, EventArgs e)
        {

        }



        private void simpleButtonToPage_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonExportCurPage_Click(object sender, EventArgs e)
        {

        }

        private void simpleButtonExportAllPage_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxEditPageSize_EditValueChanged(object sender, EventArgs e)
        {

        }
    }
}
