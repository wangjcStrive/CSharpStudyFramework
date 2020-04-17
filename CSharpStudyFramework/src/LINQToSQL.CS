using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudyFramework.src
{
    public class LINQToSQL : System.IDisposable
    {
        DBStudyDataContext m_db = new DBStudyDataContext();


        #region dispose
        //https://www.jianshu.com/p/da9be89a4206
        private bool m_alreadDispose = false;
        //供显式调用
        public void Dispose()
        {
            //调用带参数的Dispose方法, 释放托管和非托管资源
            Dispose(true);
            //手动调用了Dispose释放资源，那么析构函数就是不必要的了, 这里阻止GC调用析构函数
            System.GC.SuppressFinalize(this);
        }
        //protected的Dispose方法, 保证不会被外部调用。
        //传入bool值disposing以确定是否释放托管资源
        protected void Dispose(bool disposing)
        {
            if (m_alreadDispose)
                return;
            //清理托管资源代码
            if(disposing)
            {

            }
            //这里清理非托管资源
            m_db.Dispose();

            m_alreadDispose = true;
        }

        //GC调用
        ~LINQToSQL()
        {
            Dispose(false);
        }
        #endregion

        public void exer2()
        {
            using(DBStudyDataContext db = new DBStudyDataContext())
            {
                db.Log = new DebugWriter();
                var result = from p in db.Students select p;
                foreach (var item in result)
                {
                    Console.WriteLine($"{item.SId} {item.Sname}");
                }
            }
        }
    }
}
