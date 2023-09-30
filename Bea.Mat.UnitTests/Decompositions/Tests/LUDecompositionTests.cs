namespace Bea.Mat.Decompositions.Tests
    {

    /// <summary>
    /// Unit tests over LUDecomposition class.
    /// </summary>
    public class LUDecompositionTests
        {

        /// <summary>
        /// - Given: A square matrix.
        /// - When: Constructor is called.
        /// - Then: A new LU decomposition is created.
        /// </summary>
        [Fact]
        public void GivenSquareMatrixWhenConstructorIsCalledThenLUDecompositionIsCreated()
            {
            var data = new double[2, 2]
            {
                {  4.0,  3.0 },
                {  6.0,  3.0 }
            };

            var expectedL = new double[2, 2]
            {
                {  1.0,  0.0 },
                {  1.5,  1.0 }
            };

            var expectedU = new double[2, 2]
            {
                {  4.0,  3.0 },
                {  0.0, -1.5 }
            };

            var matrix = new Matrix(data);
            var dec = new LUDecomposition(matrix);
            var res = dec.L * dec.U;

            dec.IsNonSingular.Should().BeTrue();
            dec.Det.Should().BeApproximately(matrix.Det, Matrix.Eps);

            Ensure.AllValuesAreEqual(dec.L, expectedL);
            Ensure.AllValuesAreEqual(dec.U, expectedU);
            Ensure.AllValuesAreEqual(dec.Matrix, data);
            Ensure.AllValuesAreEqual(res, data);
            }

        /// <summary>
        /// - Given: A non-square matrix.
        /// - When: Constructor is called.
        /// - Then: A new LU decomposition is created.
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
            var act = () => { var dec = new LUDecomposition(matrix); };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: A singular square matrix.
        /// - When: Constructor is called.
        /// - Then: A new LU decomposition is created.
        /// </summary>
        [Fact]
        public void GivenSingularSquareMatrixWhenConstructorIsCalledThenLUDecompositionIsCreated()
            {
            var data = new double[2, 2]
            {
                {  4.0,  0.0 },
                {  6.0,  0.0 }
            };

            var expectedL = new double[2, 2]
            {
                {  1.0,  0.0 },
                {  1.5,  1.0 }
            };

            var expectedU = new double[2, 2]
            {
                {  4.0,  0.0 },
                {  0.0,  0.0 }
            };

            var matrix = new Matrix(data);
            var dec = new LUDecomposition(matrix);
            var res = dec.L * dec.U;

            dec.IsNonSingular.Should().BeFalse();
            dec.Det.Should().BeApproximately(0.0, Matrix.Eps);

            Ensure.AllValuesAreEqual(dec.L, expectedL);
            Ensure.AllValuesAreEqual(dec.U, expectedU);
            Ensure.AllValuesAreEqual(dec.Matrix, data);
            Ensure.AllValuesAreEqual(res, data);
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
                { 0.00 }
            };
            var av = new double[2, 2]
            {
                {  4.0,  3.0 },
                {  6.0,  3.0 }
            };
            var bv = new double[2, 1]
            {
                {  1.0 },
                {  1.5 }
            };

            var A = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new LUDecomposition(A);
            var x = dec.Solve(b);

            Ensure.AllValuesAreEqual(x, xv);
            }

        /// <summary>
        /// - Given: A system of linear and independent equations.
        /// - When: Method solve is called.
        /// - Then: The solution to the system of equations is returned.
        /// </summary>
        [Fact]
        public void GivenOneMoreIndependentEquationsWhenSolveIsCalledThenTheSolutionIsReturned()
            {
            var xv = new double[2, 1]
            {
                { 0.25 },
                { 1.25 }
            };
            var av = new double[2, 2]
            {
                {  2.0, -1.0 },
                {  6.0,  3.0 }
            };
            var bv = new double[2, 1]
            {
                { -0.75 },
                {  5.25 }
            };

            var a = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new LUDecomposition(a);
            var x = dec.Solve(b);

            Ensure.AllValuesAreEqual(x, xv);
            }

        /// <summary>
        /// - Given: A system of linear and non independent equations.
        /// - When: Method solve is called.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNonIndependentEquationsWhenSolveIsCalledThenExceptionIsThrown()
            {
            var av = new double[3, 3]
            {
                {   4.0,  3.0, -1.0 },
                {   6.0,  3.0,  2.0 },
                {  12.0,  6.0,  4.0 }
            };
            var bv = new double[3, 1]
            {
                {  1.0 },
                {  1.5 },
                {  1.5 }
            };

            var A = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new LUDecomposition(A);
            var act = () => { var x = dec.Solve(b); };

            act.Should().Throw<InvalidOperationException>();
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
                {  4.0,  3.0 },
                {  6.0,  3.0 }
            };
            var bv = new double[3, 1]
            {
                {  1.0 },
                {  1.5 },
                {  3.0 }
            };

            var A = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new LUDecomposition(A);
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
                {  4.0,  3.0 },
                {  6.0,  3.0 }
            };
            var bv = new double[2, 2]
            {
                {  1.0,  3.0 },
                {  1.5, -1.0 }
            };

            var A = new Matrix(av);
            var b = new Matrix(bv);
            var dec = new LUDecomposition(A);
            var act = () => { var x = dec.Solve(b); };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }