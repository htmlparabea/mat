namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixAdditionTests
        {

        /// <summary>
        /// - Given: Two matrixes of similar dimensions.
        /// - When: Sum operator is called.
        /// - Then: A new matrix is created with the sum of both matrixes.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesOfSimilarDimensionsWhenSumThenNewMatrixIsCreated()
            {
            var data1 = new double[3, 2]
            {
                {  1.0,  3.0 },
                { -4.0,  2.0 },
                { -5.0,  4.0 }
            };

            var data2 = new double[3, 2]
            {
                {  1.0, -6.0 },
                {  0.0,  1.0 },
                { 10.0,  3.0 }
            };

            var expected = new double[3, 2]
            {
                {  2.0, -3.0 },
                { -4.0,  3.0 },
                {  5.0,  7.0 }
            };

            var m1 = new Matrix(data1);
            var m2 = new Matrix(data2);

            var matrix = m1 + m2;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m1.Rows);
            matrix.Columns.Should().Be(m1.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: Two matrixes with different number of rows.
        /// - When: Sum operator is called.
        /// - Then: An invalid operation exception is thrown.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesWithDifferentRowsWhenSumThenExceptionIsThrown()
            {
            var m1 = new Matrix(2, 3, 0.0);
            var m2 = new Matrix(3, 3, 0.0);

            var act = () => { var matrix = m1 + m2; };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: Two matrixes with different number of columns.
        /// - When: Sum operator is called.
        /// - Then: An invalid operation exception is thrown.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesWithDifferentColumnsWhenSumThenExceptionIsThrown()
            {
            var m1 = new Matrix(3, 3, 0.0);
            var m2 = new Matrix(3, 4, 0.0);

            var act = () => { var matrix = m1 + m2; };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: A matrix and a scalar.
        /// - When: Sum operator is called.
        /// - Then: A new matrix is created with the sum of both elements.
        /// </summary>
        [Fact]
        public void GivenMatrixAndScalarWhenSumThenNewMatrixIsCreated()
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
                {  4.0,  6.0 },
                { -1.0,  5.0 },
                { -2.0,  7.0 }
            };

            var m = new Matrix(data);
            var matrix = m + scalar;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m.Rows);
            matrix.Columns.Should().Be(m.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: A scalar and a matrix.
        /// - When: Sum operator is called.
        /// - Then: A new matrix is created with the sum of both elements.
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
                {  4.0,  6.0 },
                { -1.0,  5.0 },
                { -2.0,  7.0 }
            };

            var m = new Matrix(data);
            var matrix = scalar + m;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m.Rows);
            matrix.Columns.Should().Be(m.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        }

    }