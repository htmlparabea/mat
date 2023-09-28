namespace Bea.Mat.Operations.Tests
    {

    /// <summary>
    /// Unit tests over determinant operation.
    /// </summary>
    public class DeterminantTests
        {

        /// <summary>
        /// - Given: A square matrix with linear-independent columns.
        /// - When: Get the value of the determinant.
        /// - Then: The value is non-zero.
        /// </summary>
        [Fact]
        public void GivenSquareMatrixWhenGetDeterminantThenValueIsRetuned()
            {
            var expected = -10.0;
            var data = new double[2, 2]
            {
                { 1, 2 },
                { 8, 6 }
            };

            var matrix = new Matrix(data);
            var det = matrix.Det;

            det.Should().BeApproximately(expected, Matrix.Eps);
            }

        /// <summary>
        /// - Given: A square matrix in which a column is the linear combination of others.
        /// - When. Get the value of the determinant.
        /// - Then: The value is zero.
        /// </summary>
        [Fact]
        public void GivenLinearColumnsWhenGetDeterminantThenValueIsZero()
            {
            var expected = 0.0;
            var data = new double[3, 3]
            {
                { 1, 2, 3 },
                { 8, 6, 14 },
                { 5, 1, 6 }
            };

            var matrix = new Matrix(data);
            var det = matrix.Det;

            det.Should().BeApproximately(expected, Matrix.Eps);
            }

        /// <summary>
        /// - Given: A square matrix in which a column contains all zeros.
        /// - When. Get the value of the determinant.
        /// - Then: The value is zero.
        /// </summary>
        [Fact]
        public void GivenZeroColumnWhenGetDeterminantThenValueIsZero()
            {
            var expected = 0.0;
            var data = new double[3, 3]
            {
                { 0, 2, 3 },
                { 0, 6, 14 },
                { 0, 1, -6 }
            };

            var matrix = new Matrix(data);
            var det = matrix.Det;

            det.Should().BeApproximately(expected, Matrix.Eps);
            }

        /// <summary>
        /// - Given: A non-square matrix.
        /// - When: Get the value of the determinant.
        /// - Then: An invalid operation exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNonSquareMatrixWhenGetDeterminantThenExceptionIsThrown()
            {
            var data = new double[3, 2]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            };

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix.Det; };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }