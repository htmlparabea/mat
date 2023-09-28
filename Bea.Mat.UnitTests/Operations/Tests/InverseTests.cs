namespace Bea.Mat.Operations.Tests
    {

    /// <summary>
    /// Unit tests over inverse operation.
    /// </summary>
    public class InverseTests
        {

        /// <summary>
        /// - Given: An identity matrix.
        /// - When: Get the value of the inverse matrix.
        /// - Then: A new identity matrix is returned.
        /// </summary>
        [Fact]
        public void GivenIdentityMatrixWhenGetInverseThenNewIdentiyMatrixIsReturned()
            {
            var eye = new double[3, 3]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            };

            var matrix = new Matrix(eye);
            var inv = matrix.Inv;

            inv.Should().NotBeNull();
            inv.Rows.Should().Be(matrix.Rows);
            inv.Columns.Should().Be(matrix.Columns);

            Ensure.AllValuesAreEqual(inv, eye);
            }

        /// <summary>
        /// - Given: An invertible matrix.
        /// - When: Get the value of the inverse matrix.
        /// - Then: A new matrix is returned.
        /// </summary>
        [Fact]
        public void GivenIvertibleMatrixWhenGetInverseThenNewMatrixIsReturned()
            {
            var expected = new double[2, 2]
            {
                { 0.5 , -1.0 / 3.0 },
                { 0.0 ,  1.0 / 3.0 }
            };
            var data = new double[2, 2]
            {
                { 2.0, 2.0 },
                { 0.0, 3.0 }
            };

            var matrix = new Matrix(data);
            var inv = matrix.Inv;

            inv.Should().NotBeNull();
            inv.Rows.Should().Be(matrix.Rows);
            inv.Columns.Should().Be(matrix.Columns);

            Ensure.AllValuesAreEqual(inv, expected);
            }

        /// <summary>
        /// - Given: An non-square matrix.
        /// - When: Get the value of the inverse matrix.
        /// - Then: An invalid operation exception is thrown,
        /// </summary>
        [Fact]
        public void GivenNonSquareMatrixWhenGetInverseThenExceptionIsThrown()
            {
            var data = new double[2, 3]
            {
                { 2.0, 2.0, -1.0 },
                { 0.0, 3.0, -8.0 }
            };

            var matrix = new Matrix(data);
            var act = () => { var inv = matrix.Inv; };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }