using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudyFramework
{
        public class DebugWriter : TextWriter
        {
            public override void WriteLine(string value)
            {
                Debug.WriteLine(value);
            }

            public override void WriteLine(string format, params object[] arg)
            {
                Debug.WriteLine(format, arg);
            }

            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }

        public static class DataContextHelper
        {
            public static void InitForModification(this DataContext dc)
            {
                dc.DeferredLoadingEnabled = false;
#if DEBUG
                dc.Log = new DebugWriter();
#endif
            }

            /// <summary>
            /// 如果一个表中的某一列引用到别的表，默认情况下LINQ to SQL会在遍历搜索结果的时候
            /// 动态地去获取别的表的内容，这样就可能产生大量的SQL查询
            /// 当把DeferredLoadingEnabled设置为false之后，LINQ to SQL则关闭这项功能，省去了大量的开销
            /// 事实上，我们更多的时候会用到LoadWith，直接生成一条联表查询
            /// </summary>
            public static void InitForQuery(this DataContext dc)
            {
                dc.ObjectTrackingEnabled = false;
                dc.DeferredLoadingEnabled = false;
#if DEBUG
                dc.Log = new DebugWriter();
#endif
            }

            public static DateTime GetDbDateTime(this DataContext dc)
            {
                try
                {
                    return dc.ExecuteQuery<DateTime>("SELECT GETDATE()").Single();
                }
                catch (Exception ex)
                {
                    throw new Exception("Database error.", ex); //建议替换成你的自定义异常类型
                }
            }
        }
}
