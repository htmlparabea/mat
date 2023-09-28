namespace Bea.Mat.Operations
    {

    /// <summary>
    /// This class defines the process to compute the rank.
    /// </summary>
    public static class Rank
        {

        #region Static methods

        /// <summary>
        /// Computes the value of the rank for the given matrix.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>
        /// </param>
        /// <returns>
        /// Value of the rank.
        /// </returns>
        public static int ComputeRank(this Matrix m)
            {
            int rows = m.Rows;
            int cols = m.Columns;

            int rank = 0;
            double[,] data = m.ToArray();
            bool[] rs = new bool[rows];

            for (int i = 0; i < rows; ++i)
                {
                int j;
                for (j = 0; j < cols; ++j)
                    {
                    if (!rs[j] && Math.Abs(data[j, i]) > Matrix.Eps) break;
                    }

                if (j != cols)
                    {
                    rank++;
                    rs[j] = true;

                    for (int p = i + 1; p < rows; ++p)
                        data[j, p] /= data[j, i];

                    for (int k = 0; k < cols; ++k)
                        {
                        if (k != j && Math.Abs(data[k, i]) > Matrix.Eps)
                            {
                            for (int p = i + 1; p < rows; ++p)
                                data[k, p] -= data[j, p] * data[k, i];
                            }
                        }
                    }
                }

            return rank;
            }

        #endregion

        }

    }