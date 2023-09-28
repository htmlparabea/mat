namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixSubtractionTests
        {

        /// <summary>
        /// - Given: Two matrixes of similar dimensions.
        /// - When: Substract operator is called.
        /// - Then: A new matrix is created with the substraction of both matrixes.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesOfSimilarDimensionsWhenSubstractThenNewMatrixIsCreated()
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
                {   0.0,  9.0 },
                {  -4.0,  1.0 },
                { -15.0,  1.0 }
            };

            var m1 = new Matrix(data1);
            var m2 = new Matrix(data2);

            var matrix = m1 - m2;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m1.Rows);
            matrix.Columns.Should().Be(m1.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: A matrix and a scalar.
        /// - When: Substract operator is called.
        /// - Then: A new matrix is created with the substraction of both elements.
        /// </summary>
        [Fact]
        public void GivenMatrixAndScalarWhenSubstractThenNewMatrixIsCreated()
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
                { -2.0,  0.0 },
                { -7.0, -1.0 },
                { -8.0,  1.0 }
            };

            var m = new Matrix(data);
            var matrix = m - scalar;

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m.Rows);
            matrix.Columns.Should().Be(m.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        }

    }