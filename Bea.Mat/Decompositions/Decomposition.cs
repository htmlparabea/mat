namespace Bea.Mat.Decompositions
    {

    /// <summary>
    /// Base class for matrix decomposition operations.
    /// </summary>
    public abstract class Decomposition
        {

        #region Properties

        /// <summary>
        /// Read-only copy of the matrix to decompose.
        /// </summary>
        public Matrix Matrix { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="matrix">
        /// Matrix to decompose.
        /// </param>
        protected Decomposition(Matrix matrix)
            {
            Matrix = matrix.AsReadOnly();
            }

        #endregion

        #region Aux static methods

        /// <summary>
        /// Performs forward substitution to solve a system of linear aquations. Forward substitution
        /// is the process of solving a system of linear equations, L * y = b, with a lower triangular
        /// coefficient matrix L. The matrix L is a factor of the matrix A and results from either the
        /// LU-decomposition or other types of decomposition.
        /// </summary>
        /// <param name="l">
        /// Lower triangular matrix.
        /// </param>
        /// <param name="b">
        /// The b vector from the equation.
        /// </param>
        /// <returns>
        /// The solution for the y vector.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// If the number of rows in b is different to the number of rows in l.
        /// </exception>
        protected static Matrix ForwardSubstitute(Matrix l, Matrix b)
            {
            if (b.Columns != 1)
                throw new InvalidOperationException("The number of colums in b has to be 1.");
            if (l.Rows != b.Rows)
                throw new InvalidOperationException("The number of rows in b has to be equal to the number of rows in L.");

            var y = new Matrix(l.Rows, b.Columns);

            for (int r = 0; r < y.Rows; r++)
                {
                var sum = 0.0;
                for (int j = 0; j < r; j++)
                    sum += l[r, j] * b[j, 0];

                y[r, 0] = b[r, 0] - sum;
                }

            return y;
            }

        /// <summary>
        /// Performs backward  substitution to solve a system of linear aquations. Backward substitution
        /// is a procedure of solving a system of linear algebraic equations, U * x = y, where U is an
        /// upper triangular matrix whose diagonal elements are not equal to zero. The matrix U can be
        /// a factor of another matrix A in its decomposition LU, where L is a lower triangular matrix. 
        /// </summary>
        /// <param name="u">
        /// The U matrix from the equation.
        /// </param>
        /// <param name="y">
        /// The y vector from the equation.
        /// </param>
        /// <returns>
        /// The solution for the x vector.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// If the number of rows in y is different to the number of rows in U.
        /// </exception>
        protected static Matrix BackwardSubstitute(Matrix u, Matrix y)
            {
            if (y.Columns != 1)
                throw new InvalidOperationException("The number of colums in y has to be 1.");
            if (u.Rows != y.Rows)
                throw new InvalidOperationException("The number of rows of in y have to be equal to the number of rows in U.");

            var x = new Matrix(u.Rows, y.Columns);

            for (int r = x.Rows - 1; r >= 0; r--)
                {
                var sum = 0.0;

                for (int j = r + 1; j < y.Rows; j++)
                    sum += u[r, j] * x[j, 0];

                x[r, 0] = (y[r, 0] - sum) / u[r, r];
                }

            return x;
            }

        //protected static double Hypot(double a, double b)
        //    {
        //    double r;

        //    if (Math.Abs(a) > Math.Abs(b))
        //        {
        //        r = b / a;
        //        r = Math.Abs(a) * Math.Sqrt(1 + r * r);
        //        }
        //    else if (Math.Abs(b) > Matrix.Eps) /* b != 0*/
        //        {
        //        r = a / b;
        //        r = Math.Abs(b) * Math.Sqrt(1 + r * r);
        //        }
        //    else
        //        {
        //        r = 0.0;
        //        }

        //    return r;
        //    }

        #endregion

        }

    }