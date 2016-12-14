using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class FindBorders
    {
        /// <summary>
        /// Теорема 1. Нахождение нижней и верхней границы корней
        /// Все корни уравнения (1) расположены в кольце 
        /// </summary>
        /// <param name="arguments">Массив коэффициентов a_0, a_1,..., a_n</param>
        /// <returns>Кортеж из нижней и верхней граней</returns>
        /// 
        public static Tuple<double, double> FindUpperAndLowerBorders(double[] arguments)
        {
            double a_0 = arguments[0];
            double a_max_1_n = Math.Abs(arguments[1]);
            double upper_border, lower_border;
            for (int i = 2; i < arguments.Length; i++)
            {
                if (Math.Abs(a_max_1_n) < Math.Abs(arguments[i]))
                {
                    a_max_1_n = Math.Abs(arguments[i]);
                }
            }
            double a_max_0_n_1 = Math.Abs(arguments[0]);
            for (int i = 1; i < arguments.Length - 1; i++)
            {
                if (Math.Abs(a_max_0_n_1) < Math.Abs(arguments[i]))
                {
                    a_max_0_n_1 = Math.Abs(arguments[i]);
                }
            }
            upper_border = 1 + a_max_1_n / Math.Abs(arguments[0]);
            lower_border = Math.Abs(arguments[arguments.Length - 1]) / (a_max_0_n_1 + Math.Abs(arguments[arguments.Length - 1]));
            return new Tuple<double, double>(lower_border, upper_border);
        }

        /// <summary>
        /// Теорема 2.
        /// </summary>
        /// <param name="arguments">Массив коэффициентов a_0, a_1,..., a_n</param>
        /// <param name="border">Верхней граней положительных корней</param>
        /// <returns>Бит: true, если есть положительные корни</returns>
        /// 
        public static bool FindPositiveUpperBorder(double[] arguments, ref double border)
        {
            bool q = false;
            int k = 0;
            double neg_max_abs = 0;//макс модуль отрицательного коэффициента
            int ind_first_neg = 0;//инд первого отрицательного элемента
            
            double[] a = arguments;
            if (a[0] < 0)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] *= -1;
                }
            }
            for (int i = 0; i < arguments.Length; i++)
            {
                if (arguments[i] < 0)
                {
                    q = true;
                    if (k == 0)
                    {
                        ind_first_neg = i;
                    }
                    k++;
                    if (neg_max_abs < Math.Abs(arguments[i]))
                    {
                        neg_max_abs = Math.Abs(arguments[i]);
                    }
                }
            }
            Debug.WriteLine("a_max = " + neg_max_abs + "\t a_0 = " + arguments[0] + "\tm = " + ind_first_neg);
            if (k == arguments.Length) { q = false; Debug.WriteLine("Все корни отрицательны"); }
            if (q == false)
            {
                return q;//Корней нет, т.к. нет ни одного отрицательного коэффицента
            }
            border = 1 + Math.Pow((neg_max_abs / a[0]), 1 / ind_first_neg);
            return q;
        }

        /// <summary>
        /// Теорема 2. Нахождение нижней и верхней границы положительных корней
        /// </summary>
        /// <param name="arguments">Массив коэффициентов a_0, a_1,..., a_n</param>
        /// <param name="borders">Кортеж из нижней и верхней границы положительных корней</param>
        /// <returns>Бит: true, если есть положительные корни</returns>
        /// 
        public static bool FindPositiveBorders(double[] arguments, ref Tuple<double, double> positiveBorders)
        {
            bool q;
            double upperBorder = 0;
            q = FindPositiveUpperBorder(arguments, ref upperBorder);
            if (q == false)
            {
                return q;
            }
            double[] inverseArguments = new double[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                inverseArguments[i] = arguments[arguments.Length - i - 1];
            }
            double lowerBorder = 0;
            FindPositiveUpperBorder(inverseArguments, ref lowerBorder);
            lowerBorder = 1 / lowerBorder;
            positiveBorders = new Tuple<double, double>(lowerBorder, upperBorder);
            return q;
        }

        public static bool FindNegativeBorders(double[] arguments, ref Tuple<double, double> negativeBorders)
        {
            double[] a = new double[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                a[i] = arguments[i];
            }
            for (int i = arguments.Length - 2; i >= 0; i = i - 2)
            {
                a[i] = -arguments[i];
            }
            if (a[0] < 0)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] *= -1;
                }
            }

            for (int i = 0; i < a.Length; i++)
            {
                Debug.WriteLine("arg: " + arguments[i] + " \t a: " + a[i]);
            }
            return (FindPositiveBorders(a, ref negativeBorders));
        }

        public static bool FindNutonUpperBorder(double[] arguments, ref double border)
        {
            Debug.WriteLine("FindNutonUpperBorder START)");

            int n = arguments.Length;
            double[,] a = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                a[0, i] = arguments[i];
            }
            for (int j = 1; j < n; j++)
            {
                for (int i = 0; i < n-j; i++)
                {
                    a[j, i] = a[j - 1, i] * (n - j - i);
                }
            }
            ////int l = 0;
            //for (int i = 0; i < n; i++)
            //{
            //    //Debug.WriteLine("i= " + i);
            //    for (int j = 0; j < n; j++)
            //    {
            //        Debug.Write(a[i, j] + "\t");
                    
            //    }
            //    Debug.WriteLine("");
            //}
            double v = 0;

            double sum = 0;
            bool q = true;
            bool solutionFound = false;
            double limit = 0;
            bool solutionCanBeFound = FindPositiveUpperBorder(arguments, ref limit);
            Debug.WriteLine("solutionCanBeFound" + solutionCanBeFound);
            if (solutionCanBeFound == false)
            {
                return false;
            }

            while ((q) || (v<=limit))
            {
                Debug.WriteLine(v+"\n");

                int positiveNumber = 0;
                for (int i = 0; i < n; i++)
                {
                    sum = 0;
                    for (int j = 0; j < n - i; j++)
                    {
                        sum += a[i, j] * Math.Pow(v, n-i-j-1);
                        //int l = (n - i - j-1);
                        //Debug.WriteLine("Степень: " + l + "\t");
                    }
                    if (sum >= 0) // для работы примера из учбеника надо >=
                    {
                        positiveNumber++;
                    }
                    Debug.WriteLine(i + " " + sum);
                }
                if (positiveNumber == n) { solutionFound = true; border = v ; break; }
                v++;
            }
            Debug.WriteLine("FindNutonUpperBorder END)");

            return solutionFound;
        }
        /// <summary>
        /// Теорема 2. Нахождение нижней и верхней границы положительных корней
        /// </summary>
        /// <param name="arguments">Массив коэффициентов a_0, a_1,..., a_n</param>
        /// <param name="borders">Кортеж из нижней и верхней границы положительных корней</param>
        /// <returns>Бит: true, если есть положительные корни</returns>
        /// 
        public static bool FindNutonPositiveBorders(double[] arguments, ref Tuple<double, double> positiveBorders)
        {
            Debug.WriteLine("FindNutonPositiveBorders START)");

            bool q;
            double upperBorder = 0;
            Debug.WriteLine("FindNutonPositiveBorders UPPER ONE START)");

            q = FindNutonUpperBorder(arguments, ref upperBorder);
            if (q == false)
            {
                return q;
            }
            Debug.WriteLine("FindNutonPositiveBorders UPPER ONE END)");

            double[] inverseArguments = new double[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                inverseArguments[i] = arguments[arguments.Length - i - 1];
            }
            double lowerBorder = 0;
            Debug.WriteLine("FindNutonPositiveBorders LOWER ONE START)");

            FindNutonUpperBorder(inverseArguments, ref lowerBorder);
            Debug.WriteLine("FindNutonPositiveBorders LOWER ONE END)");

            lowerBorder = 1 / lowerBorder;
            positiveBorders = new Tuple<double, double>(lowerBorder, upperBorder);
            Debug.WriteLine("FindNutonPositiveBorders END)");
            return q;
        }

        public static bool FindNutonNegativeBorders(double[] arguments, ref Tuple<double, double> negativeBorders)
        {
            Debug.WriteLine("FindNutonNegativeBorders START)");

            double[] a = new double[arguments.Length];
            for (int i = 0; i < arguments.Length; i++)
            {
                a[i] = arguments[i];
            }
            for (int i = arguments.Length - 2; i >= 0; i = i - 2)
            {
                a[i] = -arguments[i];
            }
            if (a[0] < 0)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    a[i] *= -1;
                }
            }
            Debug.WriteLine("MAX = " + a.Length);
            for (int i = 0; i < a.Length; i++)
            {
                Debug.WriteLine("arg: " + arguments[i] + " \t a: " + a[i]);
            }
            Debug.WriteLine("FindNutonNegativeBorders END\nFindNutonPositiveBorders AIMED TO BE STARTED");

            return (FindNutonPositiveBorders(a, ref negativeBorders));
        }

    }
}
