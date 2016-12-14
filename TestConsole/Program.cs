using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //double[] a = new double[5] { 1, -35, 380, -1350, 1000};

            /*
            double[] a = new double[6] { 1, -4, 6, -3, 2, 1};
            
            Tuple<double, double> n = FindBorders.FindUpperAndLowerBorders(a);
            
            Console.WriteLine(n.Item1 + "\t" + n.Item2);
            Tuple<double, double> m = new Tuple<double, double>(0, 0);
            if (FindBorders.FindPositiveBorders(a, ref m))
            { Console.WriteLine(m.Item1 + "\t" + m.Item2); }
            else { Console.WriteLine("Положительных корней нет"); }

            Tuple<double, double> negative = new Tuple<double, double>(0, 0);
            if (FindBorders.FindNegativeBorders(a, ref negative))
            { Console.WriteLine(negative.Item1 + "\t" + negative.Item2); }
            else { Console.WriteLine("Отрицательных корней нет"); }
            */

            //for (int i = 0; i < arguments.Length; i++)
            //{
            //    a[i] = arguments[i];
            //}
            //for (int i = 0; i < a.Length; i++)
            //{
            //    Console.WriteLine("arg: " + arguments[i] + " a: " + a[i]);
            //}

            //for (int i = arguments.Length - 2; i >= 0; i = i - 2)
            //{
            //    a[i] = -arguments[i];
            //}
            //for (int i = 0; i < a.Length; i++)
            //{
            //    Console.WriteLine(i + ": arg: " + arguments[i] + " a: " + a[i]);
            //}
            //if (a[0]<0)
            //{
            //    for (int i = 0; i < a.Length; i++)
            //    {
            //        a[i] *= -1;
            //    }
            //}

            //for (int i = 0; i < a.Length; i++)
            //{
            //    Console.WriteLine(i + ": arg: " + arguments[i] + " a: " + a[i]);
            //}

            double[] a = //new double[5] { 1, -35, 380, -1350, 1000 };
            //new double[6] { 1, -4, 6, -3, 2, 1 };
            new double[3] { 1, 0, -1 };
            //double b = 0;
            //if (FindBorders.FindNutonUpperBorder(a, ref b))
            //    Console.WriteLine(b);
            //else Console.WriteLine("Нет верхней грани");

            Tuple<double, double> t = new Tuple<double, double>(0, 0);
            /*
            Console.WriteLine("T2:");
            if (FindBorders.FindPositiveBorders(a, ref t))
                Console.WriteLine(t.Item1 + " " + t.Item2);
            else Console.WriteLine("Нет положительных корней");

            Console.WriteLine("Ньтоном:");
            if (FindBorders.FindNutonPositiveBorders(a, ref t))
                Console.WriteLine( t.Item1+" " +t.Item2);
            else Console.WriteLine("Нет положительных корней");
            */
            Tuple<double, double> t1 = new Tuple<double, double>(0, 0);
            //Console.WriteLine("T2:");
            //if (FindBorders.FindNegativeBorders(a, ref t1))
            //    Console.WriteLine(t1.Item1 + " " + t1.Item2);
            //else Console.WriteLine("Нет отрицательных корней");
            
            Console.WriteLine("Ньтоном:");
            if (FindBorders.FindNutonNegativeBorders(a, ref t1))
                Console.WriteLine(t1.Item1 + " " + t1.Item2);
            else Console.WriteLine("Нет отрицательных корней");

            Console.ReadKey();

        }
    }
}
