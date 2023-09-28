namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixMultiplyTests
        {

        /// <summary>
        /// - Given: Two matrixes of correct dimensions.
        /// - When: Multiply operator is called.
        /// - Then: A new matrix is created with the number of rows of the first matrix and the number of columns of the second one.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesOfCorrectDimensionsWhenMultiplyThenNewMatrixIsCreated()
            {
            var data1 = new double[3, 2]
            {
                {  1.0,  3.0 },
                { -4.0,  2.0 },
                { -5.0,  4.0 }
            };

            var data2 = new double[2, 1]
            {
                {  1.0 },
                {  1.0 }
            };

            var expected = new double[3, 1]
            {
                {  4.0 },
                { -2.0 },
                { -1.0 }
            };

            var m1 = new Matrix(data1);
            var m2 = new Matrix(data2);

            var matrix = m1 * m2;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m1.Rows);
            matrix.Columns.Should().Be(m2.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: Two matrixes of incorrect dimensions.
        /// - When: Multiply operator is called.
        /// - Then: An invalid operation exception is thrown.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesOfIncorrectDimensionsWhenMultiplyThenExceptionIsThrown()
            {
            var m1 = new Matrix(2, 2, 0.0);
            var m2 = new Matrix(3, 3, 0.0);

            var act = () => { var matrix = m1 * m2; };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: A matrix and a scalar.
        /// - When: Multiply operator is called.
        /// - Then: A new matrix is created with the product of both elements.
        /// </summary>
        [Fact]
        public void GivenMatrixAndScalarWhenMultiplyThenNewMatrixIsCreated()
            {
            var scalar = 3.0;
            var data = new double[3, 2]
            {
                {  1.0,  3.0 },
                { -4.0,  2.0 },
                { -5.0,  4.0 }
            };

            var expected = new double[3, 2]
            {
                {   3.0,  9.0 },
                { -12.0,  6.0 },
                { -15.0, 12.0 }
            };

            var m = new Matrix(data);
            var matrix = m * scalar;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m.Rows);
            matrix.Columns.Should().Be(m.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: A scalar and a matrix.
        /// - When: Multiply operator is called.
        /// - Then: A new matrix is created with the product of both elements.
        /// </summary>
        [Fact]
        public void GivenScalarAndMatrixWhenSumThenNewMatrixIsCreated()
            {
            var scalar = 3.0;
            var data = new double[3, 2]
            {
                {  1.0,  3.0 },
                { -4.0,  2.0 },
                { -5.0,  4.0 }
            };

            var expected = new double[3, 2]
            {
                {   3.0,  9.0 },
                { -12.0,  6.0 },
                { -15.0, 12.0 }
            };

            var m = new Matrix(data);
            var matrix = scalar * m;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m.Rows);
            matrix.Columns.Should().Be(m.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }


        }

    }