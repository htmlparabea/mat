namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixDivisionTests
        {

        /// <summary>
        /// - Given: A matrix and a scalar.
        /// - When: Division operator is called.
        /// - Then: A new matrix is created with the division of both elements.
        /// </summary>
        [Fact]
        public void GivenMatrixAndScalarWhenDivisionThenNewMatrixIsCreated()
            {
            var scalar = 3.0;
            var data = new double[3, 2]
            {
                {  1.0,  8.0 },
                { -4.0,  2.0 },
                { -5.0,  4.0 }
            };

            var expected = new double[3, 2]
            {
                {  1.0 / 3.0,  8.0 / 3.0 },
                { -4.0 / 3.0,  2.0 / 3.0 },
                { -5.0 / 3.0,  4.0 / 3.0 }
            };

            var m = new Matrix(data);
            var matrix = m / scalar;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m.Rows);
            matrix.Columns.Should().Be(m.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        }

    }