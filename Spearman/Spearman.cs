using System;
using System.Collections.Generic;

namespace Spearman
{
    /// <summary>The class that calculates the SpearmanRealization's coefficient and gives the results.</summary>
    public class SpearmanRealization
    {

        /// <summary>The table for coefficient calculating.</summary>
        public readonly Row[] table;

        /// <summary>  SpearmanRealization's coefficient.</summary>
        /// <value>The result.</value>
        public double Result{ get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Server.SpearmanRealization"/> class.
        /// Creates a table and calculates a coefficient.
        /// </summary>
        /// <param name="x">The x attributes.</param>
        /// <param name="y">The y attributes.</param>
        public SpearmanRealization(int[] x, int[] y)
        {
            int n = x.Length;
            double sum = 0;
            table = new Row[n];

            for (int i = 0; i < n; i++)
            {
                table[i] = new Row();
                table[i].X = x[i];
                table[i].Y = y[i];
            }

            SortingY(table);
            double[] rnkY = Rank(y);
            for (int i = 0; i < n; i++)
            {
                table[i].RY = rnkY[i];
            }

            SortingX(table);
            double[] rnkX = Rank(x);
            for (int i = 0; i < n; i++)
            {
                table[i].RX = rnkX[i];
            }

            for (int i = 0; i < n; i++)
            {
                table[i].d = table[i].RX - table[i].RY;
                table[i].d2 = table[i].d * table[i].d;
                sum += table[i].d2;
            }

            Result = Math.Round(1 - 6 * (sum / (n * n * n - n)), 3);
        }

        /// <summary>Makes a conclusion about the correlation based on the calculated coefficient.</summary>
        /// <returns>
        /// System.String.
        /// Conclusion
        /// </returns>
        public string Conclusion()
        {
            string сoncl = Convert.ToString(Result) + " => ";
            
            if (Result == 0)
            {
                сoncl += "Корреляция отсутствует";
            }
            else
            {
                double abs = Math.Abs(Result);
                if (abs < 0.2)
                {
                    сoncl += "Cтатичтическая взаимосвязь очень слабая ";
                }
                else if (abs < 0.5)
                {
                    сoncl += "Статистическая взаимосвязь слабая ";
                }
                else if (abs < 0.7)
                {
                    сoncl += "Статистическая взаимосвязь средняя ";
                }
                else
                {
                    сoncl += "Статистическая взаимосвязь сильная ";
                }

                if(Result < 0)
                {
                    сoncl += "и обратная.";
                }
                else
                {
                    сoncl += "и прямая.";
                }
            }
            return сoncl;
        }

        /// <summary>Sortings by x.</summary>
        /// <param name="arr">The array with x attributes.</param>
        private void SortingX(Row[] arr)
        {
            Array.Sort(arr, Sort_By_X);
        }

        /// <summary>Sortings by y.</summary>
        /// <param name="arr">The array with y attributes.</param>
        private void SortingY(Row[] arr)
        {
            Array.Sort(arr, Sort_By_Y);
        }

        /// <summary>  Rule for sorting by x.</summary>
        /// <param name="r1">The row.</param>
        /// <param name="r2">The row.</param>
        /// <returns>System.Int32.</returns>
        private int Sort_By_X(Row r1, Row r2)
        {
            return r1.X.CompareTo(r2.X);
        }

        /// <summary>  Rule for sorting by y.</summary>
        /// <param name="r1">The row.</param>
        /// <param name="r2">The row.</param>
        /// <returns>System.Int32.</returns>
        private int Sort_By_Y(Row r1, Row r2)
        {
            return r1.Y.CompareTo(r2.Y);
        }

        /// <summary>Ranks the specified array according to the ranking rules for the SpearmanRealization's coefficient.</summary>
        /// <param name="array">The array to rank.</param>
        /// <returns>System.Double[].
        /// Ranks</returns>
        private double[] Rank(int[] array)
        {
            int[] mas = new int[array.Length];
            double[] rank = new double[array.Length];
            List<int[]> list = new List<int[]>();
            int start = 0;
            int end = 0;
            bool flag = false;

            Array.Copy(array, mas, mas.Length);
            Array.Sort(mas);

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (mas[i] == mas[i + 1])
                {
                    if (!flag)
                    {
                        start = i;
                        flag = true;
                    }
                }
                else
                {
                    if (flag)
                    {
                        end = i;
                        int[] intA = { start, end };
                        list.Add(intA);
                        flag = false;
                    }
                    else
                    {
                        rank[i] = Convert.ToDouble(i + 1);
                    }
                }
            }

            if (mas[mas.Length - 1] == mas[mas.Length - 2])
            {
                end = mas.Length - 1;
                int[] intA = { start, end };
                list.Add(intA);
            }
            else
            {
                rank[mas.Length - 1] = Convert.ToDouble(mas.Length);
            }

            for (int i = 0; i < list.Count; i++)
            {
                int n = list[i][1] - list[i][0] + 1;
                double rnk = Convert.ToDouble(((list[i][0] + list[i][1] + 2) * n)) / (2 * n);

                for (int k = list[i][0]; k <= list[i][1]; k++)
                {
                    rank[k] = rnk;
                }
            }
            return rank;
        }
    }
}
