using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudyFramework.src
{
    class LINQToSQL
    {
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
