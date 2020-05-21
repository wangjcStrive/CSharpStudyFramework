using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudyFramework.src
{
    public class LINQToSQL : System.IDisposable
    {
        DBStudyDataContext m_db;


        #region dispose
        //https://www.jianshu.com/p/da9be89a4206
        private bool m_alreadDispose = false;

        public LINQToSQL()
        {
            m_db = new DBStudyDataContext();
            m_db.Log = new DebugWriter();
            m_alreadDispose = false;
        }

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
            if (disposing)
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

        /*
            exer1. 查询" 01 "课程比" 02 "课程成绩高的学生的信息及课程分数
            根据一张表的查询结果去查询另一张表，可以考虑用join
            linq的写法跟sql感觉差不太多

            SQL
                -- 1 --
                select * from Student RIGHT JOIN (
                    select t1.SId, class1, class2 from
                          (select SId, score as class1 from sc where sc.CId = '01')as t1, 
                          (select SId, score as class2 from sc where sc.CId = '02')as t2
                    where t1.SId = t2.SId AND t1.class1 > t2.class2
                )r 
                on Student.SId = r.SId;


                -- 2 --
                select * from  (
                    select t1.SId, class1, class2 
                    from
                        (SELECT SId, score as class1 FROM sc WHERE sc.CId = '01') AS t1, 
                        (SELECT SId, score as class2 FROM sc WHERE sc.CId = '02') AS t2
                    where t1.SId = t2.SId and t1.class1 > t2.class2
                ) r 
                LEFT JOIN Student
                ON Student.SId = r.SId;
        */
        public void exer1()
        {
            using (m_db)
            {
                var result =
                from stu in m_db.Students
                join subTable in
                (
                    from t1 in (from a in m_db.SCs where a.CId == "01" select new { a.SId, class1 = a.score })
                    from t2 in (from b in m_db.SCs where b.CId == "02" select new { b.SId, class2 = b.score })
                    where t1.SId == t2.SId && t1.class1 > t2.class2
                    select new { t1.SId, t1.class1, t2.class2 }
                ) on stu.SId equals subTable.SId
                select new
                {
                    stu.SId,
                    stu.Sname,
                    stu.Sage,
                    subTable.class1,
                    subTable.class2
                };


                foreach (var item in result)
                {
                    Console.WriteLine($"{item.SId}  {item.Sname}  {item.Sage}  {item.class1}  {item.class2}");
                }
            }
        }

        /*
            2. 查询平均成绩大于等于 60 分的同学的学生编号和学生姓名和平均成绩
            涉及多表查询时，写linq并不方便？

            SQL
                select * from Student right join (
                    select SId, avg(score) as x
                    from SC
                    group by SId
                    having avg(score)>60
                )r
                on Student.SId = r.SId;
        */
        public void exer2()
        {
            //每人的平均分 + 平均分大于60。group bu后已经分组了，然后分组放入到g中
            var groupByTest = from x in m_db.SCs
                     group x by x.SId into g
                     where g.Average(x=>x.score) >=60
                     select new
                     {
                         g.Key,
                         avgScore = g.Average(x => x.score)
                     };

            var result = from stu in m_db.Students
                         join s in
                         (
                             from x in m_db.SCs
                             group x by x.SId into g
                             where g.Average(x => x.score) >= 60
                             select new
                             {
                                 g.Key,
                                 avgScore = g.Average(x => x.score)
                             }
                         ) on stu.SId equals s.Key
                         select new
                         {
                             id = stu.SId,
                             name = stu.Sname,
                             avgScore = s.avgScore
                         };

            foreach (var item in result)
            {
                Console.WriteLine($"{item.id}   {item.name}     {item.avgScore}");
            }
        }
    }
}
