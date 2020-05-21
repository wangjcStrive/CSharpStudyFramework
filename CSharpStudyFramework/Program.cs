//#define DEBUG
//#define NoBug
//#define Li
//#define Zhang
#define ReflectionTest
//#define AttributeTest
//#define IndexTest
//#define DelegateAndEvent
//#define LINQStudy
//#define AnonymousFunction
//#define LINQTOXML
//#define LINQTOSQL
//#define TASK_THREAD_DEMO
#define Expression_DEMO
using CSharpStudyFramework.src;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudyFramework
{
    class Program
    {
        static void Main(string[] args)
        {
#if Expression_DEMO
            ExpressionDemo ins = new ExpressionDemo();
            ins.createExpression();
#endif
#if TASK_THREAD_DEMO
            ThreadTaskDemo ins = new ThreadTaskDemo();
            ins.createTask();
#endif
#if LINQTOSQL
            LINQToSQL ins = new LINQToSQL();
            ins.exer2();
#endif
#if LINQTOXML
            LINQToXMLDemo ins = new LINQToXMLDemo();
            ins.orderXML();

            //ins.createTPDefaultRecipeXML();

            //uint index;
            //List<string> options = new List<string>();
            //ins.test("MEAS_TYPE", out index, out options);
            //Console.WriteLine("index: {0}", index);
            //foreach (var item in options)
            //{
            //    Console.WriteLine(item);
            //}
#endif


#if LINQStudy
            var nums = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            var numQuery =
                from num in nums
                where (num % 2) == 0
                select num;

            foreach (var num in numQuery)
            {
                Console.WriteLine("{0}", num);
            }
#elif ReflectionTest
            Rectangle r = new Rectangle(4.5, 7.5);
            r.display();

            Type type = typeof(Rectangle);

            //遍历类特性
            foreach (object attributes in type.GetCustomAttributes(false))
            {
                Debuginfo dbi = (Debuginfo)attributes;
                if (null != dbi)
                {
                    Console.Write("Bug no: {0}\t", dbi.BugNo);
                    Console.Write("Developer: {0}\t", dbi.Developer);
                    Console.Write("Last Reviewed: {0}\t", dbi.LastReview);
                    Console.Write("Remarks: {0}\t", dbi.Message);
                }
                Console.WriteLine();
            }

            //遍历方法特性
            foreach (MethodInfo method in type.GetMethods())
            {
                foreach (Attribute a in method.GetCustomAttributes(true))
                {
                    Debuginfo dbi = (Debuginfo)a;
                    if (null != dbi)
                    {
                        Console.Write("Bug no: {0}, for Method: {1} \t",
                                                      dbi.BugNo, method.Name);
                        Console.Write("Developer: {0}\t", dbi.Developer);
                        Console.Write("Last Reviewed: {0}\t",
                                                      dbi.LastReview);
                        Console.Write("Remarks: {0}\t", dbi.Message);
                    }
                }
            }
#elif AttributeTest
            ToolKit.FuncA();
            ToolKit.FuncB();
            ToolKit.FuncC();
            ToolKit.FuncD();
            Console.ReadKey();
#elif IndexTest
            IndexerStudy names = new IndexerStudy();
            names[0] = "wang";
            names[1] = "ji";
            names[2] = "chen";
            for (int i = 0; i < IndexerStudy.size; i++)
            {
                Console.WriteLine(names[i]);
            }
#elif DelegateAndEvent
            DelegateAndEventStudyPublisher publisher = new DelegateAndEventStudyPublisher();
            Subscriber scriber = new Subscriber();
            /***** !!! *****/
            publisher.ChangeNum += new DelegateAndEventStudyPublisher.NumManipulationHandler(scriber.printf);
            /***** !!! *****/
            publisher.SetVal(7);
            publisher.SetVal(11);
#elif AnonymousFunction
            x=>x+1;
            x => {return x+1;}

#endif
            Console.ReadKey();
        }
    }
}
