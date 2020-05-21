using System;
using System.Collections.Generic;
using System.Text;

//for attribute
using System.Diagnostics;

namespace CSharpStudyFramework.src
{
    public class ToolKit
    {
        [ConditionalAttribute("Li")]//Attribute名称的长记法
        [ConditionalAttribute("Buged")]
        public static void FuncA()
        {
            Console.WriteLine("Created by Li, Buged");
        }

        [Conditional("Li")]//Attribute名称的短记法
        [Conditional("NoBug")]
        public static void FuncB()
        {
            Console.WriteLine("Created by Li, NoBug");
        }

        [Conditional("Zhang")]
        [Conditional("Buged")]
        public static void FuncC()
        {
            Console.WriteLine("Created by Zhang, Buged");
        }

        [Conditional("Zhang")]
        [Conditional("NoBug")]
        public static void FuncD()
        {
            Console.WriteLine("Created by Zhang, NoBug");
        }
    }



    //自定义特性
    //可以使用反射来查看特性
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
        AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,
        AllowMultiple = true)]
    class Debuginfo : System.Attribute
    {
        private int bugNo;
        private string developer;
        private string lastReview;
        public string message;

        public Debuginfo(int bg, string dev, string d)
        {
            this.bugNo = bg;
            this.developer = dev;
            this.lastReview = d;
        }

        public int BugNo { get => bugNo; }
        public string LastReview { get => lastReview; }
        public string Developer { get => developer; }
        public string Message { get => message; set => message = value; }
    }

    [Debuginfo(1, "wang", "2019/04/25", Message = "return type mismatch")]
    [Debuginfo(2, "jichen", "2019/01/27", Message = "other type")]
    class Rectangle
    {
        protected double length;
        protected double width;
        public Rectangle(double l, double w)
        {
            length = l;
            width = w;
        }

        [Debuginfo(3, "lm", "2018/11/27", Message = "type 1")]
        public double getArea()
        {
            return length * width;
        }

        [Debuginfo(4, "wy", "2018/11/27", Message = "type 2")]
        public void display()
        {
            Console.Write("length: {0}\t", length);
            Console.Write("width: {0}\t", width);
            Console.Write("area: {0}\t", getArea());
            Console.WriteLine();
        }

    }
}
