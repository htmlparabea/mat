namespace Bea.Mat.Decompositions.Tests
    {

    /// <summary>
    /// Unit tests over QRDecomposition class.
    /// </summary>
    public class QRDecompositionTests
        {

        /// <summary>
        /// - Given: Full rank matrix.
        /// - When: Constructor is called.
        /// - Then: A new QR decomposition is created.
        /// </summary>
        [Fact]
        public void GivenFullRankMatrixWhenConstructorIsCalledThenQRDecompositionIsCreated()
            {
            // Arrange...
            var data = new double[3, 3]
            {
                {  4.0,  3.0, -1.0},
                {  6.0,  2.0,  0.0 },
                { -2.0,  1.0,  4.0 },
            };

            var expectedQ = new double[3, 3]
            {
                { -0.53452248382484857,  0.61721339984836743,  0.577350269189626 },
                { -0.80178372573727308, -0.15430334996209177, -0.577350269189626 },
                {  0.26726124191242434,  0.77151674981045981, -0.5773502691896254 }
            };

            var expectedR = new double[3, 3]
            {
                { -7.4833147735478835, -2.9398736610366667,  1.6035674514745457 },
                {  0.0,                 2.3145502494313779,  2.4688535993934719 },
                {  0.0,                 0.0,                -2.8867513459481273 }
            };

            // Act...
            var matrix = new Matrix(data);
            var dec = new QRDecomposition(matrix);
            var res = dec.Q * dec.R;

            // Assert...
            dec.IsFullRank.Should().BeTrue();

            Ensure.AllValuesAreEqual(dec.Q, expectedQ);
            Ensure.AllValuesAreEqual(dec.R, expectedR);
            Ensure.AllValuesAreEqual(res, data);
            }

        /// <summary>
        /// - Given: Non full rank matrix.
        /// - When: Constructor is called.
        /// - Then: A new QR decomposition is created.
        /// </summary>
        [Fact]
        public void GivenNonFullRankMatrixWhenConstructorIsCalledThenQRDecompositionIsCreated()
            {
            // Arrange...
            var data = new double[3, 3]
            {
                {  4.0,  3.0,  1.0},
                {  6.0,  2.0,  4.0 },
                { -2.0,  1.0, -3.0 },
            };

            var expectedQ = new double[3, 3]
            {
                { -0.53452248382484857,  0.61721339984836743, -0.57735026918962529 },
                { -0.80178372573727308, -0.15430334996209177,  0.57735026918962518 },
                {  0.26726124191242434,  0.77151674981045981,  0.57735026918962462 }
            };

            var expectedR = new double[3, 3]
            {
                { -7.4833147735478835, -2.9398736610366667, -4.5434411125112142},
                {  0.0,                 2.3145502494313779, -2.3145502494313792 },
                {  0.0,                 0.0,                 0.0 }
            };

            // Act...
            var matrix = new Matrix(data);
            var dec = new QRDecomposition(matrix);
            var res = dec.Q * dec.R;

            // Assert...
            dec.IsFullRank.Should().BeFalse();

            Ensure.AllValuesAreEqual(dec.Q, expectedQ);
            Ensure.AllValuesAreEqual(dec.R, expectedR);
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
            var dec = new QRDecomposition(A);
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
            var dec = new QRDecomposition(a);
            var x = dec.Solve(b);

            Ensure.AllValuesAreEqual(x, xv);
            }

        /// <summary>
        /// - Given: A linear system of non independent equations.
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
            var dec = new QRDecomposition(A);
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
            var dec = new QRDecomposition(A);
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
            var dec = new QRDecomposition(A);
            var act = () => { var x = dec.Solve(b); };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }