namespace Bea.Mat.Decompositions
    {

    /// <summary>
    /// The Cholesky decomposition is a factorization of a symmetric, positive-definite matrix into
    /// the product of a lower triangular matrix and its conjugate transpose, which is the transpose
    /// of the lower triangular matrix. Mathematically, if you have a matrix A that meets these criteria,
    /// the Cholesky decomposition can be represented as: A = L * L.T
    /// Where:
    /// - A is the original symmetric positive-definite matrix.
    /// - L is a lower triangular matrix.
    /// - L.T is the transpose of L.
    /// Some important things to note about the Cholesky decomposition:
    /// - The matrix A must be symmetric.
    /// - The matrix A must be positive definite. This means that for any nonzero vector x, the quadratic
    /// form x.T * A * x is always positive.
    /// - The Cholesky decomposition is particularly useful for solving linear systems of equations and
    /// for various numerical algorithms, such as those used in numerical optimization and statistics.
    /// The Cholesky decomposition can be used to efficiently solve systems of linear equations of 
    /// the form A * x = b, where A is the original matrix, L is the lower triangular Cholesky factor
    /// and x is the vector of unknowns.
    /// The Cholesky decomposition is computationally more efficient than other matrix factorizations
    /// like LU decomposition for symmetric positive-definite matrices because it requires fewer operations.
    /// </summary>
    public class CholDecomposition : Decomposition
        {

        #region Attributes

        private readonly Matrix _chol;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the lower triangular matrix of the decomposition.
        /// </summary>
        public Matrix L { get; init; }

        #endregion

        #region Constructors

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="matrix">
        /// Matrix to decompose.
        /// </param>
        public CholDecomposition(Matrix matrix) : base(matrix)
            {
            if (!matrix.IsSymmetric)
                throw new InvalidOperationException("Can not compute the Cholesky decomposition of a non-symetric matrix.");

            _chol = ComputeChol(matrix);
            L = ComputeL(_chol);
            }

        #endregion

        #region Static methods

        private static Matrix ComputeChol(Matrix matrix)
            {
            var chol = new Matrix(matrix.Rows, matrix.Columns);

            for (var i = 0; i < chol.Rows; i++)
                {
                for (var j = 0; j <= i; j++)
                    {
                    if (i == j)
                        {
                        double sum = 0;

                        for (int k = 0; k < j; k++)
                            sum += chol[j, k] * chol[j, k];

                        var value = matrix[j, j] - sum;

                        if (value <= 0)
                            throw new InvalidOperationException("Can not compute the Cholesky decomposition of a non-positive definite matrix.");

                        chol[j, j] = Math.Sqrt(value);
                        }
                    else
                        {
                        double sum = 0;

                        for (int k = 0; k < j; k++)
                            sum += chol[i, k] * chol[j, k];

                        var value = (matrix[i, j] - sum) / chol[j, j];
                        chol[i, j] = value;
                        }
                    }
                }

            return chol;
            }

        private static Matrix ComputeL(Matrix chol)
            {
            var l = new Matrix(chol.Rows, chol.Columns);

            for (int i = 0; i < chol.Rows; i++)
                {
                for (int j = 0; j < chol.Columns; j++)
                    {
                    if (i < j)
                        l[i, j] = 0.0;
                    else
                        l[i, j] = chol[i, j];
                    }
                }

            return l.AsReadOnly();
            }

        #endregion

        #region Methods (Own members)

        /// <summary>
        /// Solve a system of linear equations.
        /// Given a system of linear equations in matrix form A * x = b, we want to solve the equation
        /// for x, given A and b. Suppose we have already obtained the Cholesky decomposition of A such
        /// that A = L * L.T, so L * L.T * x = b.
        /// In this case the solution is done in two logical steps:
        /// - First, we solve the equation L * y = b for y.
        /// - Second, we solve the equation L.T * x = y for x.
        /// In both cases we are dealing with triangular matrices(L and L.T), which can be solved directly
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
        /// </exception>
        public Matrix Solve(Matrix b)
            {
            if (b.Columns != 1)
                throw new InvalidOperationException("The number of colums in b has to be 1.");
            if (b.Rows != _chol.Rows)
                throw new InvalidOperationException("The number of rows in b has to be equal to the number of rows in L.");

            // Solve L * y = b for y using forward substitution
            var y = new Matrix(b.Rows, 1);

            for (var i = 0; i < y.Rows; i++)
                {
                var sum = 0.0;

                for (var j = 0; j < i; j++)
                    sum += L[i, j] * y[j, 0];

                y[i, 0] = (b[i, 0] - sum) / L[i, i];
                }

            // Solve L.T * x = y for x using back substitution
            var x = new Matrix(b.Rows, 1);

            for (var i = x.Rows - 1; i >= 0; i--)
                {
                var sum = 0.0;

                for (var j = i + 1; j < x.Rows; j++)
                    sum += L[j, i] * x[j, 0];

                x[i, 0] = (y[i, 0] - sum) / L[i, i];
                }

            return x;
            }

        #endregion

        }

    }