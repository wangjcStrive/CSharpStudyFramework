using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharpStudyFramework.src
{
    class ExpressionDemo
    {
        public void createExpression()
        {
            Expression<Func<int, int, int, int, int>> func = (i, j, x, y) => (i * j) + (x * y);
            Console.WriteLine(func);        //输出表达式
            Func<int, int, int, int, int> compile = func.Compile();
            Console.WriteLine(compile(12, 13, 14, 15));
        }
    }
}
