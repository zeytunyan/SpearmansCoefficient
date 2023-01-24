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

        /// <summary>Conclusion about the correlation based on the calculated coefficient.</summary>
        /// <value>The conclusion.</value>
        public string Conclusion => $"{Result} => {(Result == 0 ? "Корреляция отсутствует" : DetermineCorrelation())}";

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Spearman.SpearmanRealization"/> class.
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

            Array.Sort(table, (r1, r2) => r1.Y.CompareTo(r2.Y));
            double[] rnkY = Rank(y);
            for (int i = 0; i < n; i++)
            {
                table[i].RY = rnkY[i];
            }

            Array.Sort(table, (r1, r2) => r1.X.CompareTo(r2.X));
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


        private string DetermineCorrelation()
        {            
            var resAbs = Math.Abs(Result);
            
            string strength;
            
            if (resAbs < 0.2) 
                strength = "очень слабая";
            else if 
                (resAbs < 0.5) strength = "слабая";
            else if 
                (resAbs < 0.7) strength = "средняя";
            else 
                strength = " сильная ";

            return $"Cтатиcтическая взаимосвязь {strength} и { (Result > 0 ? "прямая" : "обратная") }.";
    }


        /// <summary>Ranks the specified array according to the ranking rules for the SpearmanRealization's coefficient.</summary>
        /// <param name="array">The array to rank.</param>
        /// <returns>System.Double[].
        /// Ranks</returns>
        private double[] Rank(int[] array)
        {
            int[] mas = new int[array.Length];
            double[] rank = new double[array.Length];
            var list = new List<(int Start, int End)>();
            var (flag, start) = (false, 0);

            Array.Copy(array, mas, mas.Length);
            Array.Sort(mas);

            for (int i = 0; i < array.Length - 1; i++)
            {
                if (mas[i] != mas[i + 1] && !flag) 
                    rank[i] = Convert.ToDouble(i + 1);
               
                if (mas[i] == mas[i + 1] && !flag) 
                    (flag, start) = (true, i);

                if (mas[i] != mas[i + 1])
                {
                    list.Add((start, i));
                    flag = false;
                }
            }

            if (mas[mas.Length - 1] == mas[mas.Length - 2])
            {
                list.Add((start, mas.Length - 1));
            }
            else
            {
                rank[mas.Length - 1] = Convert.ToDouble(mas.Length);
            }

            foreach (var tuple in list)
            {
                for (int k = tuple.Start; k <= tuple.End; k++)
                {
                    rank[k] = (tuple.Start + tuple.End) / 2 + 1;
                }
            }

            return rank;
        }
    }
}
