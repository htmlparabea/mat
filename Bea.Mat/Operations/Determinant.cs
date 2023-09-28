namespace Bea.Mat.Operations
    {

    /// <summary>
    /// This class defines the process to compute the determinant.
    /// </summary>
    public static class Determinant
        {

        #region Static methods

        /// <summary>
        /// Computes the value of the determinant for the given matrix.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>
        /// </param>
        /// <returns>
        /// Value of the determinant.
        /// </returns>
        public static double ComputeDeterminant(this Matrix m)
            {
            if (!m.IsSquare)
                throw new InvalidOperationException("Can not compute the determinant of a non square matrix.");

            double det = 1.0;

            int rows = m.Rows;
            int cols = m.Columns;
            int e = 0;
            double[,] data = m.ToArray();

            for (int c = 0; c < cols - 1; c++)
                {
                double pivot = 0.0;
                int index = 0;

                for (int r = c; r < rows; r++)
                    if (Math.Abs(data[r, c]) > Math.Abs(pivot))
                        {
                        pivot = data[r, c];
                        index = r;
                        }

                if (Math.Abs(pivot) < Matrix.Eps) { det = 0.0; break; }
                if (index != c) e++;

                for (int cc = 0; cc < cols; cc++)
                    {
                    var buffer = data[index, cc];
                    data[index, cc] = data[c, cc];
                    data[c, cc] = buffer;
                    }

                for (int r = c + 1; r < rows; r++)
                    {
                    double buffer = data[r, c] / pivot;

                    for (int cc = 0; cc < cols; cc++)
                        data[r, cc] -= buffer * data[c, cc];
                    }
                }

            for (int i = 0; i < rows; i++)
                det *= data[i, i];

            det *= Math.Pow(-1, e);

            return Math.Abs(det) > Matrix.Eps ? det : 0.0;
            }

        #endregion

        }

    }