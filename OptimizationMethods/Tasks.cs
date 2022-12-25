using info.lundin.math;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OptimizationMethods
{
    public partial class Tasks : Form
    {
        public Tasks()
        {
            InitializeComponent();
        }





        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (cbConst.Checked)
            {
                WithConst();
            }
            else
            {
                WithoutConst();

            }

        }

        private void Tasks_Load(object sender, EventArgs e)
        {
            pbFunction.Load(@"Images\Function.png");
            pbFunctionZ.Load(@"Images\Function.png");
            pbFunctionF.Load(@"Images\Function.png");
            pbFunctionF.Load(@"Images\Function.png");
            pbFunctionJ.Load(@"Images\FunctionJ.png");
            pbFunctionN.Load(@"Images\FunctionJ.png");
            pbFunctionS.Load(@"Images\FunctionJ.png");
            cbConst.Checked = true;
        }

        private void cbConst_CheckedChanged(object sender, EventArgs e)
        {
            if (cbConst.Checked)
            {
                tbConst.Enabled = true;

            }
            else
            {
                tbConst.Enabled = false;
            }
        }





        //Метод дихотомии 
        double f(double x)
        {
            if (tbSignFirst.Text == "+" && tbSignTwo.Text == "-")
            {
                return int.Parse(tbFirstNumber.Text) * Math.Pow(x, int.Parse(tbDegree.Text)) + int.Parse(tbTwoNumber.Text) * x - int.Parse(tbThreeNumber.Text);
            }
            if (tbSignFirst.Text == "-" && tbSignTwo.Text == "-")
            {
                return int.Parse(tbFirstNumber.Text) * Math.Pow(x, int.Parse(tbDegree.Text)) - int.Parse(tbTwoNumber.Text) * x - int.Parse(tbThreeNumber.Text);
            }
            if (tbSignFirst.Text == "-" && tbSignTwo.Text == "+")
            {
                return int.Parse(tbFirstNumber.Text) * Math.Pow(x, int.Parse(tbDegree.Text)) - int.Parse(tbTwoNumber.Text) * x + int.Parse(tbThreeNumber.Text);
            }
            if (tbSignFirst.Text == "+" && tbSignTwo.Text == "+")
            {
                return int.Parse(tbFirstNumber.Text) * Math.Pow(x, int.Parse(tbDegree.Text)) + int.Parse(tbTwoNumber.Text) * x + int.Parse(tbThreeNumber.Text);
            }
            return x;
        }

        //с константой различимости
        void WithConst()
        {
            //a,b - границы отрезка, диапазон, e - точность, ep - Шаг приращения, константа различимости 

            double a /*= -1*/, b/*= 1*/, c = 1, e/* = 0.001*//*1e-5*/, ep/* = 0.001*//*2 * e*/, x/*, x1, x2, F1, F2*/; int i = 0, n = 1;

            a = double.Parse(tbIntervalFirst.Text);
            b = double.Parse(tbIntervalTwo.Text);
            e = double.Parse(tbAccuracy.Text);
            ep = double.Parse(tbConst.Text);

            while (c > 2 * e)
            {
                //x1 = ((a + b) / 2) - ep;
                //x2 = ((a + b) / 2) + ep;
                //e = ((b - a - ep) / Math.Pow(2, (n++) + 1)) + (ep / 2);

                //F1 = f(x1);
                //F2 = f(x2);

                x = (a + b) / 2;
                i++;
                if (f(x + ep) < f(x - ep))
                {
                    a = x;
                }
                else
                {
                    b = x;
                    //a = x1;
                    //x3 = (a + b) / 2;
                    c = b - a;
                    lIteration.Text = "Количество итераций: " + i;
                    lMiddle.Text = "Середина интервала [a,b]: " + ((a + b) / 2);
                    lMinimum.Text = "F(x)min: " + f(x);
                }
            }



            //while (c > e)
            //{
            //    i++; 
            //    x = (a + b) * 0.5;
            //    if
            //        (f(x + ep) < f(x - ep))
            //    {
            //        a = x;
            //    }
            //    else
            //    {
            //        b = x;
            //        c = b - a;
            //        tbResult.Text = i + ") " + a + " " + b + " " + c;
            //    }
            //}


        }
        //с константой различимости


        //без константы различимости
        void WithoutConst()
        {
            //a,b - границы отрезка, диапазон, e - точность, ep - Шаг приращения, константа различимости 

            double a, b, e, x, x4, F1, F2, F3, M;

            a = double.Parse(tbIntervalFirst.Text);
            b = double.Parse(tbIntervalTwo.Text);
            e = double.Parse(tbAccuracy.Text);


            double x2 = ((a + b) / 2);
            int i = 0;
            while (Math.Abs(b - a) > e)
            {
                double x1 = a + ((b - a) / 4), x3 = a + ((3 * (b - a)) / 4);

                F1 = f(x1);
                F2 = f(x2);
                F3 = f(x3);
                M = new[] { f(x1), f(x2), f(x3) }.Min(); //нахождение минимального числа

                i++;
                if (M == f(x1))
                {
                    b = x2;
                    x2 = x1;
                    F2 = F1;
                    lMiddle.Text = "Середина интервала [a,b]: " + x2;
                    lMinimum.Text = "F(x)min: " + F2;

                }
                if (M == f(x2))
                {
                    a = x1;
                    b = x3;
                    lMiddle.Text = "Середина интервала [a,b]: " + x2;
                    lMinimum.Text = "F(x)min: " + F2;
                }
                if (M == f(x3))
                {
                    a = x2;
                    x2 = x3;
                    F2 = F3;
                    //a = x1;
                    //x3 = (a + b) / 2;
                    x4 = x2;
                    lMiddle.Text = "Середина интервала [a,b]: " + x4;
                    lMinimum.Text = "F(x)min: " + F2;

                }
                lIteration.Text = "Количество итераций: " + i;
            }
        }
        //без константы различимости
        //Метод дихотомии




        //метод золотого сечения 
        double fZ(double x)
        {
            if (tbSignFirstZ.Text == "+" && tbSignTwoZ.Text == "-")
            {
                return int.Parse(tbFirstNumberZ.Text) * Math.Pow(x, int.Parse(tbDegreeZ.Text)) + int.Parse(tbTwoNumberZ.Text) * x - int.Parse(tbThreeNumberZ.Text);
            }
            if (tbSignFirstZ.Text == "-" && tbSignTwoZ.Text == "-")
            {
                return int.Parse(tbFirstNumberZ.Text) * Math.Pow(x, int.Parse(tbDegreeZ.Text)) - int.Parse(tbTwoNumberZ.Text) * x - int.Parse(tbThreeNumberZ.Text);
            }
            if (tbSignFirstZ.Text == "-" && tbSignTwoZ.Text == "+")
            {
                return int.Parse(tbFirstNumberZ.Text) * Math.Pow(x, int.Parse(tbDegreeZ.Text)) - int.Parse(tbTwoNumberZ.Text) * x + int.Parse(tbThreeNumberZ.Text);
            }
            if (tbSignFirstZ.Text == "+" && tbSignTwoZ.Text == "+")
            {
                return int.Parse(tbFirstNumberZ.Text) * Math.Pow(x, int.Parse(tbDegreeZ.Text)) + int.Parse(tbTwoNumberZ.Text) * x + int.Parse(tbThreeNumberZ.Text);
            }


            return x;
        }

        private void btnCalculateZ_Click(object sender, EventArgs e)
        {
            //a,b - границы отрезка, диапазон, eps - точность, z - тау, формула

            double a, b, eps, z = (3 - Math.Sqrt(5)) / 2;

            a = double.Parse(tbIntervalFirstZ.Text);
            b = double.Parse(tbIntervalTwoZ.Text);
            eps = double.Parse(tbAccuracyZ.Text);

            double x1 = a + z * (b - a), x2 = b - z * (b - a);

            int iteration = 0;
            for (int i = 0; b - a > eps; i++)
            {
                iteration++;
                if (fZ(x1) <= fZ(x2))
                {
                    b = x2;
                    x2 = x1;
                    x1 = a + b - x2;
                }
                else
                {
                    a = x1;
                    x1 = x2;
                    x2 = a + b - x1;
                }

                lIterationZ.Text = "Количество итераций: " + iteration;
            }
            //double x = (a + b) / 2;
            double x = x2;
            lMiddleZ.Text = "Середина интервала [a,b]: " + x;
            lMinimumZ.Text = "F(x)min: " + fZ(x);
        }
        //метод золотого сечения 




        //метод Фибоначчи
        int F(int n)
        {
            int f, f1 = 1, f2 = 1, m = 0;
            while (m < n - 1)
            {
                f = f1 + f2;
                f1 = f2;
                f2 = f;
                ++m;
            }
            return f1;
        }


        double fF(double x)
        {
            if (tbSignFirstF.Text == "+" && tbSignTwoF.Text == "-")
            {
                return int.Parse(tbFirstNumberF.Text) * Math.Pow(x, int.Parse(tbDegreeF.Text)) + int.Parse(tbTwoNumberF.Text) * x - int.Parse(tbThreeNumberF.Text);
            }
            if (tbSignFirstF.Text == "-" && tbSignTwoF.Text == "-")
            {
                return int.Parse(tbFirstNumberF.Text) * Math.Pow(x, int.Parse(tbDegreeF.Text)) - int.Parse(tbTwoNumberF.Text) * x - int.Parse(tbThreeNumberF.Text);
            }
            if (tbSignFirstF.Text == "-" && tbSignTwoF.Text == "+")
            {
                return int.Parse(tbFirstNumberF.Text) * Math.Pow(x, int.Parse(tbDegreeF.Text)) - int.Parse(tbTwoNumberF.Text) * x + int.Parse(tbThreeNumberF.Text);
            }
            if (tbSignFirstF.Text == "+" && tbSignTwoF.Text == "+")
            {
                return int.Parse(tbFirstNumberF.Text) * Math.Pow(x, int.Parse(tbDegreeF.Text)) + int.Parse(tbTwoNumberF.Text) * x + int.Parse(tbThreeNumberF.Text);
            }

            return x;
        }

        void Fib(double a, double b, double eps)
        {
            double x1, x2, _x, xf1, xf2;
            int k = 0;
            int N = 0;
            double fn1 = 1, fn2 = 1, fn, f = (b - a) / eps;

            while (fn1 < f)
            {
                fn = fn1 + fn2;
                fn1 = fn2;
                fn2 = fn;
                ++N;

            }
            bool bix;
            int ix = N & 1;
            if (ix == 1)
                bix = true;
            else
                bix = false;
            //формулы, пункт 3
            x1 = a + (double)F(N - 2) / F(N) * (b - a) - (bix ? -1 : 1) * eps / F(N);
            x2 = a + (double)F(N - 1) / F(N) * (b - a) + (bix ? -1 : 1) * eps / F(N);
            xf1 = fF(x1);
            xf2 = fF(x2);
        //формулы, пункт 3
        P:
            ++k;
            lIterationF.Text = "Количество итераций: " + k;

            //Если F1>F2, пункт 4,б
            if (xf1 >= xf2)
            {
                ix = (N - k) & 1;
                if (ix == 1)
                    bix = true;
                else
                    bix = false;
                a = x1;
                x1 = x2;
                xf1 = xf2;
                x2 = a + (double)F(N - k - 1) / F(N - k) * (b - a) + (bix ? -1 : 1) * eps / F(N - k);
                xf2 = fF(x2);
            }
            //Если F1>=F2, пункт 4,б
            //Если F1<F2, пункт 4,а
            else
            {
                ix = (N - k) & 1;
                if (ix == 1)
                    bix = true;
                else
                    bix = false;
                b = x2;
                x2 = x1;
                xf2 = xf1;
                x1 = a + (double)F(N - k - 2) / F(N - k) * (b - a) - (bix ? -1 : 1) * eps / F(N - k);
                xf1 = fF(x1);
            }
            //Если F1<F2, пункт 4,а
            if (Math.Abs(b - a) <= eps)
            {
                _x = (a + b) / 2;
                //_x = x1;
                lMiddleF.Text = "Середина интервала [a,b]: " + _x;
                lMinimumF.Text = "F(x)min: " + fF(_x);

            }
            else
                goto P;

        }

        private void btnCalculateF_Click(object sender, EventArgs e)
        {
            //a,b - границы отрезка, диапазон, eps - точность

            double a, b, eps;
            a = double.Parse(tbIntervalFirstF.Text);
            b = double.Parse(tbIntervalTwoF.Text);
            eps = double.Parse(tbAccuracyF.Text);
            Fib(a, b, eps);
        }
        //метод Фибоначчи





        //Метод прямого покоординатного поиска
        double x1_fixed, x2_fixed, left_border, right_border;

        public double fSearch(double x1, double x2)
        {
            return 2 * ((x2 - x1 * x1) * (x2 - x1 * x1)) + ((1 - x1) * (1 - x1));
        }


        public double test_f(int n, double x)
        {
            if (n == 1) return 2 * ((x2_fixed - x * x) * (x2_fixed - x * x)) + ((1 - x) * (1 - x));

            if (n == 2) return 2 * ((x - x1_fixed * x1_fixed) * (x - x1_fixed * x1_fixed)) + ((1 - x1_fixed) * (1 - x1_fixed));
            return 0;
        }

        public double kvadr(double a, double b, double h, int n)
        {
            double eps = double.Parse(tbAccuracyS.Text);
            double x1, x2, x3, xmin = 0, x, f1, f2, f3, denom, razn1, razn2;
            int it = 0;
            x1 = (a + b) / 2;
            do
            {
                it++;
                x2 = x1 + h;
                if (test_f(n, x1) > test_f(n, x2)) x3 = x1 + 2 * h;
                else x3 = x1 - h;

                f1 = test_f(n, x1);
                f2 = test_f(n, x2);
                f3 = test_f(n, x3);

                denom = (x2 - x3) * f1 + (x3 - x1) * f2 + (x1 - x2) * f3;
                if (f1 <= f2 && f1 <= f3) xmin = x1;
                if (f2 <= f1 && f2 <= f3) xmin = x2;
                if (f3 <= f1 && f3 <= f2) xmin = x3;

                if (denom == 0)
                {
                    x1 = xmin;
                    f1 = test_f(n, x1);
                }

                x = 0.5 * ((x2 * x2 - x3 * x3) * f1 + (x3 * x3 - x1 * x1) * f2 + (x1 * x1 - x2 * x2) * f3) / denom;

                razn1 = Math.Abs(test_f(n, xmin) - test_f(n, x));
                razn2 = Math.Abs(xmin - x);
                if (razn1 >= eps && razn2 >= eps)
                {
                    if (x1 <= x && x3 >= x)
                    {
                        if (test_f(n, xmin) < test_f(n, x)) x1 = xmin;
                        else x1 = x;
                    }
                    else
                    {
                        x1 = x;
                    }
                }
            } while (razn1 >= eps && razn2 >= eps);
            return x;
        }


        public void nevs(double x0, double h, int n)
        {
            double a, b, c;
            int it = 0;
            a = test_f(n, x0 - Math.Abs(h));
            b = test_f(n, x0);
            c = test_f(n, x0 + Math.Abs(h));
            if (a <= b && b >= c)
            {
                MessageBox.Show("Функция не умодальна в окрестностях точки x0", "Ошибка");
            }

            if (a < b && b < c) h = -h;
            while (!(a >= b && b <= c))
            {
                it++;
                x0 = x0 + h / 2;
                a = test_f(n, x0 - Math.Abs(h));
                b = test_f(n, x0);
                c = test_f(n, x0 + Math.Abs(h));
            }
            left_border = x0 - Math.Abs(h);
            right_border = x0 + Math.Abs(h);
        }

        public void coordinateSearch()
        {
            double eps = double.Parse(tbAccuracyS.Text);
            x1_fixed = int.Parse(tbPointOneS.Text);
            x2_fixed = int.Parse(tbPointTwoS.Text);
            int it = 0;
            double x1_fixed_0, x2_fixed_0;

            x1_fixed_0 = x1_fixed;
            x2_fixed_0 = x2_fixed;
            do
            {
                nevs(x1_fixed, 1, 1);
                x1_fixed = kvadr(left_border, right_border, 0.1, 1);

                it++;
                x1_fixed_0 = x1_fixed;
                nevs(x2_fixed, 1, 2);
                x2_fixed = kvadr(left_border, right_border, 0.1, 2);

                it++;
                x2_fixed_0 = x2_fixed;
            }
            while (Math.Abs(fSearch(x1_fixed, x2_fixed) - fSearch(x1_fixed_0, x2_fixed_0)) >= eps);
            lIterationS.Text = "Количество итераций: " + it;
            lPointS.Text = "x(" + x1_fixed + ";" + x2_fixed + ")";
            lMinimumS.Text = "F(x)min: " + fSearch(x1_fixed, x2_fixed);


        }
        private void btnCalculateSearch_Click(object sender, EventArgs e)
        {
            coordinateSearch();
        }
        //Метод прямого покоординатного поиска



        //метод  конфигураций (Хука-Дживса)
        double function; // функция f(x1,x2)
        double x1, x2, xt1, xt2; // точки
        double vpx1, vpx2; // вектор приращения
        double step; // коэффициент уменьшения шага
        double acc; // точность
        string func; // строка для сохранения функции
        Boolean flag; //флаг проверки
        double[] fun; //массив хранящий значения функций
        int count; //счетчик
        Boolean xflag; //переменная отражает успех или неудачу исследующего поиска x1,x2
        double f_old; // переменная нужна для сравнения новой функции
        double fkm1; // значение предыдущей функции
        Boolean iflag; // флаг отражает был ли весь поиск успешным или нет
        double[] mainx1, mainx2; //массивы хранящие базовые точки
        double kp1, kp2, kp1_old, kp2_old; // точки, построенные при движении по образцу
        double ftest, x1test, x2test; // функция для проверки поля
        int proverka; // счетчик для предотвращения зацикливания

        private void btnCalculateJeevesMethod_Click(object sender, EventArgs e)
        {
            // параметры формы
            try
            {
                //начальная точка
                x1 = Convert.ToDouble(tbPointOne.Text);
                x2 = Convert.ToDouble(tbPointTwo.Text);
                //начальная точка
                func = tbFunctionJ.Text; //функция
                //приращение
                vpx1 = Convert.ToDouble(tbIncrementOne.Text);
                vpx2 = Convert.ToDouble(tbIncrementTwo.Text);
                //приращение
                step = Convert.ToDouble(tbStepA.Text); //шаг

                fun = new double[1000];
                mainx1 = new double[1000];
                mainx2 = new double[1000];
                flag = true;
                if (step <= 1)
                {
                    MessageBox.Show("Шаг должен быть > 1", "Неверные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                acc = Convert.ToDouble(tbAccuracyJ.Text);//точность
                if (acc >= 1 || acc < 0)
                {
                    MessageBox.Show("Точность должна быть <1 и >0", "Неверные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                try
                {
                    x1test = x1;
                    x2test = x2;
                    ExpressionParser parser = new ExpressionParser();
                    parser.Values.Add("x1", x1test);
                    parser.Values.Add("x2", x2test);
                    ftest = parser.Parse(tbFunctionJ.Text);
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка при вычислении функции", "Неверные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    flag = false;
                }
                if (flag == true) start();

            }
            catch (FormatException)
            {
                MessageBox.Show("Введены некорректные данные в поля параметров", "Неверные данные", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public double regexpr(double x, double y)
        {
            ExpressionParser parser = new ExpressionParser();
            parser.Values.Add("x1", x);
            parser.Values.Add("x2", y);
            return parser.Parse(tbFunctionJ.Text);

        }

        public Boolean uspex(double a, double b)
        {
            if (a < b)
                return true;
            else
                return false;
        }

        public void ipoisk(double xodin, double xdva, int p)
        {
            function = regexpr(xodin, xdva);
            xflag = uspex(function, f_old);
            if (p == 0)
                tbOutputResult.AppendText("Фиксируя переменную x" + (p + 1) + "=" + xdva + ", дадим приращение x" + Environment.NewLine);
            else
                tbOutputResult.AppendText("Фиксируя переменную x" + (p + 1) + "=" + xodin + ", дадим приращение x" + Environment.NewLine);
            tbOutputResult.AppendText("f(" + xodin + "," + xdva + ")= " + function + " < " + f_old + " ? " + xflag + Environment.NewLine);
            if (xflag == true)
            {
                if (p == 0)
                    x1 = xt1;
                else
                    x2 = xt2;
                //f_old = f;
                iflag = true;
            }
        }


        public void start()
        {
            // Шаг 1. Инициализация
            count = 0;
            fun[count] = regexpr(x1, x2);
            tbOutputResult.AppendText("Вычислим значение функции в т. (" + x1 + "," + x2 + "). f=" + fun[count] + Environment.NewLine);
            mainx1[0] = x1;
            mainx2[0] = x2;
            tbOutputResult.AppendText("-----------------------------------------------------------------------------\n" + Environment.NewLine);
            proverka = 1;
            f_old = fun[count];
        // Шаг 2. Исследующий поиск
        shag2:
            tbOutputResult.AppendText("Исследующий поиск: " + Environment.NewLine);
            iflag = false;
            proverka = proverka + 1;
            // исследующий поиск x1
            xt1 = x1 + vpx1;
            ipoisk(xt1, x2, 0);
            // исследующий поиск x2
            xt2 = x2 + vpx2;
            ipoisk(x1, xt2, 1);
            if (iflag == false)
            {   // исследующий поиск x1 в противоположном направлении 
                xt1 = x1 - vpx1;
                ipoisk(xt1, x2, 0);
                // исследующий поиск x2 в противоположном направлении 
                xt2 = x2 - vpx2;
                ipoisk(x1, xt2, 1);
            }
            // Шаг 3. Проверка успеха исследующего поиска
            if (iflag == true)
                goto predshag5;
            else
                goto shag4;
            predshag5:
            count = count + 1;
            mainx1[count] = x1;
            mainx2[count] = x2;
            fun[count] = regexpr(mainx1[count], mainx2[count]);
            tbOutputResult.AppendText("x" + count + "=(" + x1 + "," + x2 + ")=" + fun[count] + Environment.NewLine);
            tbOutputResult.AppendText("-----------------------------------------------------------------------------\n" + Environment.NewLine);
            // Шаг 5. Поиск по образцу
            kp1_old = mainx1[count - 1];
            kp2_old = mainx2[count - 1];
        shag5:
            kp1 = mainx1[count] + (mainx1[count] - kp1_old);
            kp2 = mainx2[count] + (mainx2[count] - kp2_old);
            function = regexpr(kp1, kp2);
            proverka = proverka + 1;
            if (proverka > 100)
                goto konec2;

            tbOutputResult.AppendText("Поиск по образцу:" + Environment.NewLine);
            tbOutputResult.AppendText("p(" + kp1 + "," + kp2 + ")=" + function + Environment.NewLine);

            // Шаг 6. Исследующий поиск после поиска по образцу
            tbOutputResult.AppendText("Исследующий поиск после поиска по образцу: " + Environment.NewLine);
            f_old = function;
            iflag = false;
            x1 = kp1;
            x2 = kp2;
            // исследующий поиск x1
            xt1 = x1 + vpx1;
            ipoisk(xt1, x2, 0);
            // исследующий поиск x2
            xt2 = x2 + vpx2;
            ipoisk(x1, xt2, 1);
            if (iflag == false)
            {   // исследующий поиск x1 в противоположном направлении 
                xt1 = x1 - vpx1;
                ipoisk(xt1, x2, 0);
                // исследующий поиск x2 в противоположном направлении 
                xt2 = x2 - vpx2;
                ipoisk(x1, xt2, 1);
            }
            if (iflag == false) goto shag4;
            tbOutputResult.AppendText("f^k+1 = " + function + Environment.NewLine);
            tbOutputResult.AppendText("f^k = " + fun[count] + Environment.NewLine);

            // Шаг 7. Выполняется ли неравенство f(x^(k+1))<f(x^k)?
            if (function < fun[count])
            {
                kp1_old = kp1;
                kp2_old = kp2;
                fkm1 = regexpr(kp1, kp2);
                count++;
                mainx1[count] = x1;
                mainx2[count] = x2;
                fun[count] = regexpr(mainx1[count], mainx2[count]);
                tbOutputResult.AppendText("x^k-1(" + kp1_old + "," + kp2_old + ")=" + fkm1 + Environment.NewLine);
                tbOutputResult.AppendText("---- x" + count + "=(" + x1 + "," + x2 + ")=" + fun[count] + "----" + Environment.NewLine);
                tbOutputResult.AppendText("-----------------------------------------------------------------------------" + Environment.NewLine);
                goto shag5;
            }
            else
            {
                x1 = mainx1[count];
                x2 = mainx2[count];
                goto shag4;
            }
        // Шаг 4. Проверка на окончание поиска
        shag4:

            if (Math.Sqrt(vpx1 * vpx1 + vpx2 * vpx2) <= acc)
            {

                tbOutputResult.AppendText("Неравенство выполняется: " + Math.Sqrt(vpx1 * vpx1 + vpx2 * vpx2) + "<=" + acc + Environment.NewLine);
                goto konec;
            }
            else
            {
                vpx1 = vpx1 / step;
                vpx2 = vpx2 / step;
                tbOutputResult.AppendText("dX1=" + vpx1 + " dX2=" + vpx2 + Environment.NewLine);
                goto shag2;
            }
        konec:
            lIterationJ.Text = "Количество итераций: " + count.ToString();
            lPointX.Text = "x (" + kp1 + "; " + kp2 + ")";
            lMinimumJ.Text = "F(x)min: " + Math.Round(f_old, 2);

            tbOutputResult.AppendText("Ответ: x(" + kp1 + ";" + kp2 + "),f(x)=" + f_old);
        konec2:
            tbOutputResult.AppendText(Environment.NewLine + "Верное решение не найдено");
            //string outputResult = "Верное решение не найдено";

        }

        //метод конфигураций (Хука-Дживса)





        //метод деформируемого многогранника (Нелдера-Мида)
        const int NP = 2; // NP - число аргументов функции
        double[,] simplex = new double[NP, NP + 1]; // NP + 1 - число вершин симплекса
        double[] FN = new double[NP + 1];



        private double Func(double[] X, int NP) // Функциия 
        {
            double x1 = X[0];
            double p = 1.0 - x1;
            double p2 = X[1] - x1 * x1;
            return (p * p) + 2.0 * (p2 * p2);


        }



        // Создает из точки X регулярный симплекс с длиной ребра L и с NP + 1 вершиной
        // Формирует массив FN значений оптимизируемой функции F в вершинах симплекса
        private void makeSimplex(double[] X, double L, int NP, bool first)
        {
            double qn, q2, r1, r2;
            int i, j;
            qn = Math.Sqrt(1.0 + NP) - 1.0;
            q2 = L / Math.Sqrt(2.0) * (double)NP;
            r1 = q2 * (qn + (double)NP);
            r2 = q2 * qn;
            for (i = 0; i < NP; i++) simplex[i, 0] = X[i];
            for (i = 1; i < NP + 1; i++)
                for (j = 0; j < NP; j++)
                    simplex[j, i] = X[j] + r2;
            for (i = 1; i < NP + 1; i++) simplex[i - 1, i] = simplex[i - 1, i] - r2 + r1;
            for (i = 0; i < NP + 1; i++)
            {
                for (j = 0; j < NP; j++) X[j] = simplex[j, i];
                FN[i] = Func(X, NP); // Значения функции в вершинах начального симплекса
            }
        }



        private double[] center_of_gravity(int k, int NP) // Центр тяжести симплекса
        {
            int i, j;
            double s;
            double[] xc = new double[NP];
            for (i = 0; i < NP; i++)
            {
                s = 0;
                for (j = 0; j < NP + 1; j++) s += simplex[i, j];
                xc[i] = s;
            }
            for (i = 0; i < NP; i++) xc[i] = (xc[i] - simplex[i, k]) / (double)NP;
            return xc;
        }



        private void reflection(int k, double cR, int NP) // Отражение вершины с номером k относительно центра тяжести
        {
            double[] xc = center_of_gravity(k, NP); // cR – коэффициент отражения
            for (int i = 0; i < NP; i++) simplex[i, k] = (1.0 + cR) * xc[i] - simplex[i, k];
        }
        private void reduction(int k, double gamma, int NP) // Редукция симплекса к вершине k
        {
            int i, j; // gamma – коэффициент редукции
            double[] xk = new double[NP];
            for (i = 0; i < NP; i++) xk[i] = simplex[i, k];
            for (j = 0; j < NP; j++)
                for (i = 0; i < NP; i++)
                    simplex[i, j] = xk[i] + gamma * (simplex[i, j] - xk[i]);
            for (i = 0; i < NP; i++) simplex[i, k] = xk[i]; // Восстанавливаем симплекс в вершине k
        }
        private void shrinking_expansion(int k, double alpha_beta, int NP) // Сжатие/растяжение симплекса. alpha_beta – коэффициент растяжения/сжатия
        {
            double[] xc = center_of_gravity(k, NP);
            for (int i = 0; i < NP; i++)
                simplex[i, k] = xc[i] + alpha_beta * (simplex[i, k] - xc[i]);
        }



        private double findL(double[] X2, int NP) // Длиина ребра симплекса
        {
            double L = 0;
            for (int i = 0; i < NP; i++) L += X2[i] * X2[i];
            return Math.Sqrt(L);
        }
        private double minval(double[] F, int N1, ref int imi) // Минимальный элемент массива и его индекс
        {
            double fmi = double.MaxValue, f;
            for (int i = 0; i < N1; i++)
            {
                f = F[i];
                if (f < fmi)
                {
                    fmi = f;
                    imi = i;
                }
            }
            return fmi;
        }
        private double maxval(double[] F, int N1, ref int ima) // Максимальный элемент массива и его индекс
        {
            double fma = double.MinValue, f;
            for (int i = 0; i < N1; i++)
            {
                f = F[i];
                if (f > fma)
                {
                    fma = f;
                    ima = i;
                }
            }
            return fma;
        }
        private void simplexRestore(int NP) // Восстанавление симплекса
        {
            int i, imi = -1, imi2 = -1;
            double fmi, fmi2 = double.MaxValue, f;
            double[] X = new double[NP], X2 = new double[NP];
            fmi = minval(FN, NP + 1, ref imi);
            for (i = 0; i < NP + 1; i++)
            {
                f = FN[i];
                if (f != fmi && f < fmi2)
                {
                    fmi2 = f;
                    imi2 = i;
                }
            }
            for (i = 0; i < NP; i++)
            {
                X[i] = simplex[i, imi];
                X2[i] = simplex[i, imi] - simplex[i, imi2];
            }
            makeSimplex(X, findL(X2, NP), NP, false);
        }
        private bool notStopYet(double L_thres, int NP) // Возвращает true, если длина хотя бы одного ребра симплекса превышает L_thres, или false - в противном случае
        {
            int i, j, k;
            double[] X = new double[NP], X2 = new double[NP];
            for (i = 0; i < NP; i++)
            {
                for (j = 0; j < NP; j++) X[j] = simplex[j, i];
                for (j = i + 1; j < NP + 1; j++)
                {
                    for (k = 0; k < NP; k++) X2[k] = X[k] - simplex[k, j];
                    if (findL(X2, NP) > L_thres) return true;
                }
            }
            return false;
        }



        // Выполняет поиск экстремума (минимума) функции F
        private void nelMead(ref double[] X, int NP, double L, double L_thres, double cR, double alpha, double beta, double gamma)
        {
            int i, j2, imi = -1, ima = -1;
            int j = 0, kr = 0, jMx = 10000; // Предельное число шагов алгоритма (убрать после отладки)
            double[] X2 = new double[NP], X_R = new double[NP];
            double Fmi, Fma, F_R, F_S, F_E;
            const int kr_todo = 60; // kr_todo - число шагов алгоритма, после выполнения которых симплекс восстанавливается

            makeSimplex(X, L, NP, true);
            while (notStopYet(L_thres, NP) && j < jMx)
            {
                j++; // Число итераций
                kr++;
                if (kr == kr_todo)
                {
                    kr = 0;
                    simplexRestore(NP); // Восстановление симплекса
                }
                Fmi = minval(FN, NP + 1, ref imi);
                Fma = maxval(FN, NP + 1, ref ima); // ima - Номер отражаемой вершины
                for (i = 0; i < NP; i++) X[i] = simplex[i, ima];
                reflection(ima, cR, NP); // Отражение
                for (i = 0; i < NP; i++) X_R[i] = simplex[i, ima];
                F_R = Func(X_R, NP); // Значение функции в вершине ima симплекса после отражения
                if (F_R > Fma)
                {
                    shrinking_expansion(ima, beta, NP); // Сжатие
                    for (i = 0; i < NP; i++) X2[i] = simplex[i, ima];
                    F_S = Func(X2, NP); // Значение функции в вершине ima симплекса после его сжатия
                    if (F_S > Fma)
                    {
                        for (i = 0; i < NP; i++) simplex[i, ima] = X[i];
                        reduction(ima, gamma, NP); // Редукция
                        for (i = 0; i < NP + 1; i++)
                        {
                            if (i == ima) continue;
                            for (j2 = 0; j2 < NP; j2++) X2[j2] = simplex[j2, i];
                            // Значения функций в вершинах симплекса после редукции. В вершине ima значение функции сохраняется
                            FN[i] = Func(X2, NP);
                        }
                    }
                    else
                        FN[ima] = F_S;
                }
                else if (F_R < Fmi)
                {
                    shrinking_expansion(ima, alpha, NP); // Растяжение
                    for (j2 = 0; j2 < NP; j2++) X2[j2] = simplex[j2, ima];
                    F_E = Func(X2, NP); // Значение функции в вершине ima симплекса после его растяжения
                    if (F_E > Fmi)
                    {
                        for (j2 = 0; j2 < NP; j2++) simplex[j2, ima] = X_R[j2];
                        FN[ima] = F_R;
                    }
                    else
                        FN[ima] = F_E;
                }
                else
                    FN[ima] = F_R;
            }

            lIterationN.Text = "Количество итераций: " + j.ToString();
        }

        private void btnCalculateNelderMethod_Click(object sender, EventArgs e)
        {
            double[] X = { Convert.ToDouble(tbPointOneN.Text), Convert.ToDouble(tbPointTwoN.Text) }; // Первая вершина начального симплекса (начальная точка)

            double L, L_thres, cR, alpha, beta, gamma;
            L = 0.4; // Начальная длина ребра симплекса

            L_thres = Convert.ToDouble(tbAccuracyN.Text) /*0.001*//*1.0e-5*/; // Погрешность
            cR = /*1.0*/Convert.ToDouble(tbConstantAlphaN.Text); // Константа α обычно выбирается равной единице
            alpha = /*2.9*/Convert.ToDouble(tbCoefficientStretchingN.Text); // Коэффициент растяжения (обычно 2,8≤γ≤3)
            beta = /*0.5*/Convert.ToDouble(tbCompressionRatioN.Text); // Коэффициент сжатия (обычно 0,4≤β≤0,6)

            gamma = 0.5; // Коэффициент редукции симплекса

            double pointOne = 0, pointTwo = 0;

            //sW.WriteLine("Начальная точка:"); // Результат
            for (int i = 0; i < NP; i++)
            {
                lPointOneN.Text = Math.Round(X[i], 2).ToString() + ")";
                pointOne = X[i];
            }


            //    sW.WriteLine(X[i]);

            nelMead(ref X, NP, L, L_thres, cR, alpha, beta, gamma); // Поиск минимума функции
                                                                    //sW.WriteLine("Результат:");
                                                                    //sW.WriteLine("Аргументы:");
            for (int i = 0; i < NP; i++)
            {

                lPointTwoN.Text = "x(" + Math.Round(X[i], 2).ToString() + ";" + lPointOneN.Text;
                pointTwo = X[i];
            }
            //sW.WriteLine("Функция в вершинах симплекса:");
            double[] resultPoints = { pointTwo, pointOne };

            for (int i = 0; i < NP + 1; i++)
                lMinimumN.Text = "F(x)min: " + Math.Round(FN[i], 1).ToString();
        }
        //метод деформируемого многогранника (Нелдера-Мида)

    }
}



