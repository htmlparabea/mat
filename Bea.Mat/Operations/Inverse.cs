namespace Bea.Mat.Operations
    {

    /// <summary>
    /// This class defines the process to compute the inverse matrix.
    /// https://en.wikipedia.org/wiki/Gaussian_elimination
    /// </summary>
    public static class Inverse
        {

        #region Static methods

        /// <summary>
        /// Computes the inverse matrix of the given matrix using Gauss' method.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/> 
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the inverse matrix.
        /// </returns>
        public static Matrix ComputeInverse(this Matrix m)
            {
            if (!m.IsSquare)
                throw new InvalidOperationException("Can not compute the inverse of a non square matrix.");

            int rows = m.Rows;
            int cols = m.Columns;
            int totalCols = 2 * cols;

            var tmp = new double[rows, totalCols];

            // Copy values from matrix...
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    tmp[r, c] = m[r, c];

            // Copy identity matrix...
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    tmp[r, c + cols] = r == c ? 1.0 : 0.0;

            for (int c = 0; c < cols; c++)
                {
                int index = 0;
                double pivot = 0.0;

                // Find the pivot...
                for (int r = c; r < rows; r++)
                    if (Math.Abs(tmp[r, c]) > Math.Abs(pivot))
                        {
                        pivot = tmp[r, c];
                        index = r;
                        }

                // Rows swap and normalization...
                for (int cc = 0; cc < totalCols; cc++)
                    {
                    double buffer = tmp[index, cc] / pivot;
                    tmp[index, cc] = tmp[c, cc];
                    tmp[c, cc] = buffer;
                    }

                // Row substraction...
                for (int r = 0; r < rows; r++)
                    if (r != c)
                        {
                        double buffer = tmp[r, c];
                        for (int cc = 0; cc < totalCols; cc++)
                            tmp[r, cc] -= tmp[c, cc] * buffer;
                        }
                }

            var res = new Matrix(rows, cols);

            // Matrix building...
            for (int c = 0; c < cols; c++)
                for (int r = 0; r < rows; r++)
                    res[r, c] = tmp[r, c + cols];

            return res;
            }

        #endregion

        }

    }