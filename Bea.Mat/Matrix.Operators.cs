namespace Bea.Mat
    {

    /// <summary>
    /// The Matrix class defines the properties and operations of matrices.
    /// This partial class defines the operators.
    /// </summary>
    public partial class Matrix
        {

        #region Operators

        /// <summary>
        /// Performs the addition of two matrices with appropiate dimensions.
        /// </summary>
        /// <param name="m1">
        /// First <see cref="Matrix"/>.
        /// </param>
        /// <param name="m2">
        /// Second <see cref="Matrix"/>.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator +(Matrix m1, Matrix m2)
            {
            if (m1.Rows != m2.Rows)
                throw new InvalidOperationException("The number of rows are different.");
            if (m1.Columns != m2.Columns)
                throw new InvalidOperationException("The number of columns are different.");

            var res = new Matrix(m1.Rows, m1.Columns);

            for (var r = 0; r < res.Rows; r++)
                for (var c = 0; c < res.Columns; c++)
                    res[r, c] = m1[r, c] + m2[r, c];

            return res;
            }

        /// <summary>
        /// Sums the scalar value to the all values of the matrix.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>.
        /// </param>
        /// <param name="x">
        /// Scalar value.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator +(Matrix m, double x)
            {
            var res = new Matrix(m.Rows, m.Columns);

            for (var r = 0; r < res.Rows; r++)
                for (var c = 0; c < res.Columns; c++)
                    res[r, c] = m[r, c] + x;

            return res;
            }

        /// <summary>
        /// Sums the scalar value to the all values of the matrix.
        /// </summary>
        /// <param name="x">
        /// Scalar value.
        /// </param>
        /// <param name="m">
        /// <see cref="Matrix"/>.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator +(double x, Matrix m)
            {
            return m + x;
            }

        /// <summary>
        /// Performs the substraction of two matrices with appropiate dimensions.
        /// </summary>
        /// <param name="m1">
        /// First <see cref="Matrix"/>.
        /// </param>
        /// <param name="m2">
        /// Second <see cref="Matrix"/>.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator -(Matrix m1, Matrix m2)
            {
            return m1 + m2 * (-1.0);
            }

        /// <summary>
        /// Substracts the scalar value to the all values of the matrix.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>.
        /// </param>
        /// <param name="x">
        /// Scalar value.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator -(Matrix m, double x)
            {
            return m + (-1.0) * x;
            }

        /// <summary>
        /// Performs the multiplication of two matrices with appropiate
        /// dimensions.
        /// </summary>
        /// <param name="m1">
        /// First <see cref="Matrix"/>.
        /// </param>
        /// <param name="m2">
        /// Second <see cref="Matrix"/>.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator *(Matrix m1, Matrix m2)
            {
            if (m1.Columns != m2.Rows)
                throw new InvalidOperationException("The number of columns in first matrix have to be equal to the number of rows in the second one.");

            var res = new Matrix(m1.Rows, m2.Columns);

            for (var r = 0; r < m1.Rows; r++)
                for (var c = 0; c < m2.Columns; c++)
                    for (var i = 0; i < m1.Columns; i++)
                        res[r, c] += m1[r, i] * m2[i, c];

            return res;
            }

        /// <summary>
        /// Multiply the scalar value to the all values of the matrix.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>.
        /// </param>
        /// <param name="x">
        /// Scalar value.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator *(Matrix m, double x)
            {
            var res = new Matrix(m.Rows, m.Columns);

            for (var r = 0; r < res.Rows; r++)
                for (var c = 0; c < res.Columns; c++)
                    res[r, c] = m[r, c] * x;

            return res;
            }

        /// <summary>
        /// Multiply the scalar value to the all values of the matrix.
        /// </summary>
        /// <param name="x">
        /// Scalar value.
        /// </param>
        /// <param name="m">
        /// <see cref="Matrix"/>.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator *(double x, Matrix m)
            {
            return m * x;
            }

        /// <summary>
        /// Divides all the values of the matrix by the given scalar value.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>.
        /// </param>
        /// <param name="x">
        /// Scalar value.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the result of the operation.
        /// </returns>
        public static Matrix operator /(Matrix m, double x)
            {
            return m * (1.0 / x);
            }

        #endregion

        }

    }