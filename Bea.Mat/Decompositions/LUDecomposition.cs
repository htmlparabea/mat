namespace Bea.Mat.Decompositions
    {

    /// <summary>
    /// Lower–upper (LU) decomposition factors a matrix as the product of a lower triangular matrix (L)
    /// and an upper triangular matrix (U).
    /// Let A be a square matrix. An LU factorization refers to the factorization of A, with proper
    /// row and/or column orderings or permutations, into two factors – a lower triangular matrix L 
    /// and an upper triangular matrix U:
    /// A = L * U.
    /// </summary>
    /// <see cref="https://en.wikipedia.org/wiki/LU_decomposition"/>
    public class LUDecomposition : Decomposition
        {

        #region Attributes

        private readonly Matrix _lu;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the lower triangular matrix of the decomposition.
        /// </summary>
        public Matrix L { get; init; }

        /// <summary>
        /// Gets the upper triangular matrix of the decomposition.
        /// </summary>
        public Matrix U { get; init; }

        /// <summary>
        /// Gets a value indicating wheter the decomposed matrix is non-singular.
        /// </summary>
        public bool IsNonSingular
            {
            get
                {
                for (int j = 0; j < _lu.Rows; j++)
                    if (Math.Abs(_lu[j, j]) < Matrix.Eps)
                        return false;

                return true;
                }
            }

        /// <summary>
        /// Gets the value of the determinant for the decomposed matrix.
        /// </summary>
        public double Det
            {
            get
                {
                double det = 1.0;

                for (int j = 0; j < _lu.Rows; j++)
                    det *= _lu[j, j];

                return det;
                }
            }

        #endregion

        #region Constructors

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="matrix">
        /// Matrix to decompose.
        /// </param>
        public LUDecomposition(Matrix matrix) : base(matrix)
            {
            if (!matrix.IsSquare)
                throw new InvalidOperationException("Can not compute the LU decomposition of a non-square matrix.");

            _lu = ComputeLU(matrix);
            L = ComputeL(_lu);
            U = ComputeU(_lu);
            }

        #endregion

        #region Static methods

        private static Matrix ComputeLU(Matrix matrix)
            {
            var lu = new Matrix(matrix.Rows, matrix.Columns);

            for (int i = 0; i < matrix.Rows; i++)
                {
                double sum;

                for (int j = i; j < matrix.Columns; j++)
                    {
                    sum = 0;

                    for (int k = 0; k < i; k++)
                        sum += lu[i, k] * lu[k, j];

                    lu[i, j] = matrix[i, j] - sum;
                    }

                for (int j = i + 1; j < matrix.Rows; j++)
                    {
                    sum = 0;

                    for (int k = 0; k < i; k++)
                        sum += lu[j, k] * lu[k, i];

                    lu[j, i] = (1 / lu[i, i]) * (matrix[j, i] - sum);
                    }
                }

            return lu;
            }

        private static Matrix ComputeL(Matrix lu)
            {
            var l = new Matrix(lu.Rows, lu.Columns);

            for (int i = 0; i < lu.Rows; i++)
                {
                for (int j = 0; j < lu.Columns; j++)
                    {
                    if (i > j)
                        l[i, j] = lu[i, j];
                    else if (i == j)
                        l[i, j] = 1.0;
                    else
                        l[i, j] = 0.0;
                    }
                }

            return l.AsReadOnly();
            }

        private static Matrix ComputeU(Matrix lu)
            {
            var u = new Matrix(lu.Rows, lu.Columns);

            for (int i = 0; i < lu.Rows; i++)
                {
                for (int j = 0; j < lu.Columns; j++)
                    {
                    if (i <= j)
                        u[i, j] = lu[i, j];
                    else
                        u[i, j] = 0.0;
                    }
                }

            return u.AsReadOnly();
            }

        #endregion

        #region Methods (Own members)

        /// <summary>
        /// Solve a system of linear equations.
        /// Given a system of linear equations in matrix form A*x = b, we want to solve the equation
        /// for x, given A and b. Suppose we have already obtained the LU decomposition of A such
        /// that A = L*U, so L*U*x = b.
        /// In this case the solution is done in two logical steps:
        /// - First, we solve the equation L*y = b for y.
        /// - Second, we solve the equation U*x = y for x.
        /// In both cases we are dealing with triangular matrices(L and U), which can be solved directly
        /// by forward and backward substitution without using the Gaussian elimination process.
        /// </summary>
        /// <param name="b">
        /// The value of matrix b in equation.
        /// </param>
        /// <returns>
        /// The solution for matrix x in equation.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// If the number of rows in b is different to the number of rows in LU matrix.
        /// If the original matrix A is singular.
        /// </exception>
        public Matrix Solve(Matrix b)
            {
            if (b.Columns != 1)
                throw new InvalidOperationException("The number of colums in b has to be 1.");
            if (b.Rows != _lu.Rows)
                throw new InvalidOperationException("The number of rows in b has to be equal to the number of rows in LU.");
            if (!IsNonSingular)
                throw new InvalidOperationException("The matrix is singular.");

            // Forward substituion: y = L * b
            var y = new Matrix(b.Rows, b.Columns);

            for (int r = 0; r < y.Rows; r++)
                {
                var sum = 0.0;

                for (int j = 0; j < r; j++)
                    sum += L[r, j] * b[j, 0];

                y[r, 0] = b[r, 0] - sum;
                }

            // Backward substitution: x = U * y
            var x = new Matrix(y.Rows, y.Columns);

            for (int r = x.Rows - 1; r >= 0; r--)
                {
                var sum = 0.0;

                for (int j = r + 1; j < y.Rows; j++)
                    sum += U[r, j] * x[j, 0];

                x[r, 0] = (y[r, 0] - sum) / U[r, r];
                }

            return x;
            }

        #endregion

        }

    }