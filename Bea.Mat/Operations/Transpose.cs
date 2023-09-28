namespace Bea.Mat.Operations
    {

    /// <summary>
    /// This class defines the process to compute the transpose.
    /// </summary>
    public static class Transpose
        {

        #region Static methods

        /// <summary>
        /// Computes the transpose of the given matrix.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>
        /// </param>
        /// <returns>
        /// Transpose.
        /// </returns>
        public static Matrix ComputeTraspose(this Matrix m)
            {
            double[,] data = new double[m.Columns, m.Rows];

            for (var r = 0; r < m.Rows; r++)
                for (var c = 0; c < m.Columns; c++)
                    data[c, r] = m[r, c];

            return new Matrix(data);
            }

        #endregion

        }

    }