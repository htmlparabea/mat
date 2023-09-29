namespace Bea.Mat.Decompositions.Tests
    {

    /// <summary>
    /// Unit tests over CholDecomposition class.
    /// </summary>
    public class CholDecompositionTests
        {

        /// <summary>
        /// - Given: A symetric matrix.
        /// - When: Constructor is called.
        /// - Then: A new Cholesky decomposition is created.
        /// </summary>
        [Fact]
        public void GivenSymetricMatrixWhenConstructorIsCalledThenCholDecompositionIsCreated()
            {
            var data = new double[3, 3]
            {
                { 25, 15,  5 },
                { 15, 13, 11 },
                {  5, 11, 21 }
            };

            var expectedL = new double[3, 3]
            {
                { 5.0, 0.0, 0.0 },
                { 3.0, 2.0, 0.0 },
                { 1.0, 4.0, 2.0 }
            };

            var matrix = new Matrix(data);
            var dec = new CholDecomposition(matrix);
            var res = dec.L * dec.L.T;

            Ensure.AllValuesAreEqual(dec.L, expectedL);
            Ensure.AllValuesAreEqual(dec.Matrix, data);
            Ensure.AllValuesAreEqual(res, data);
            }

        /// <summary>
        /// - Given: A non-positive definite matrix.
        /// - When: Constructor is called.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNonPositiveDefiniteMatrixWhenConstructorIsCalledThenAnExceptionIsThrown()
            {
            var data = new double[3, 3]
            {
                { -25, 15,  5 },
                {  15, 13, 11 },
                {   5, 11, 21 }
            };

            var matrix = new Matrix(data);
            var act = () => { var dec = new CholDecomposition(matrix); };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: A non-symetric matrix.
        /// - When: Constructor is called.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNonSymetricMatrixWhenConstructorIsCalledThenExceptionIsThrown()
            {
            var data = new double[3, 3]
            {
                { 4, 2, 2 },
                { 2, 5, 5 },
                { 3, 5, 8 }
            };

            var matrix = new Matrix(data);
            var act = () => { var dec = new CholDecomposition(matrix); };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: A non-square matrix.
        /// - When: Constructor is called.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNonSquareMatrixWhenConstructorIsCalledThenExceptionIsThrown()
            {
            var data = new double[2, 3]
            {
                {  2.0,  3.0, -1.0},
                {  6.0,  3.0,  5.0}
            };

            var matrix = new Matrix(data);
            var act = () => { var dec = new CholDecomposition(matrix); };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: A system of linear and independent equations.
        /// - When: Method solve is called.
        /// - Then: The solution to the system of equations is returned.
        /// </summary>
        [Fact]
        public void GivenIndependentEquationsWhenSolveIsCalledThenTheSolutionIsReturned()
            {
            var xv = new double[2, 1]
            {
                { 0.25 },
                { 0.5 }
            };
            var av = new double[2, 2]
            {
                {  2.0,  0.0 },
                {  0.0,  2.0 }
            };
            var bv = new double[2, 1]
            {
                {  0.5 },
                {  1.0 }
            };

            var A = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new CholDecomposition(A);
            var x = dec.Solve(b);

            Ensure.AllValuesAreEqual(x, xv);
            }

        /// <summary>
        /// - Given: The number of rows in vector b is not equal to the number of row in A.
        /// - When: Method solve is called.
        /// - Then: Exception is thrown.
        /// </summary>
        [Fact]
        public void GivenInvalidRowsInBWhenSolveIsCalledThenExceptionIsThrown()
            {
            var av = new double[2, 2]
            {
                {  4.0,  2.0 },
                {  2.0,  3.0 }
            };
            var bv = new double[3, 1]
            {
                {  1.0 },
                {  1.5 },
                {  3.0 }
            };

            var A = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new CholDecomposition(A);
            var act = () => { var x = dec.Solve(b); };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: The number of columns in vector b is greater than 1.
        /// - When: Method solve is called.
        /// - Then: Exception is thrown.
        /// </summary>
        [Fact]
        public void GivenInvalidColumnsInBWhenSolveIsCalledThenExceptionIsThrown()
            {
            var av = new double[2, 2]
            {
                {  4.0,  2.0 },
                {  2.0,  3.0 }
            };
            var bv = new double[2, 2]
            {
                {  1.0,  3.0 },
                {  1.5, -1.0 }
            };

            var A = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new CholDecomposition(A);
            var act = () => { var x = dec.Solve(b); };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }