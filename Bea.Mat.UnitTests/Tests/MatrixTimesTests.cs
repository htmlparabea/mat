namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixTimesTests
        {

        /// <summary>
        /// - Given: Two matrixes of similar dimensions.
        /// - When: Times method is called.
        /// - Then: A new matrix is created with the element-by-element product of both matrixes.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesOfSimilarDimensionsWhenTimesIsCalledThenNewMatrixIsCreated()
            {
            var data1 = new double[2, 2]
            {
                {  1.0,  3.0 },
                { -4.0,  2.0 }
            };

            var data2 = new double[2, 2]
            {
                {  1.0, -6.0 },
                {  0.0,  1.0 }
            };

            var expected = new double[2, 2]
            {
                {  1.0, -18.0 },
                {  0.0,  2.0 }
            };

            var m1 = new Matrix(data1);
            var m2 = new Matrix(data2);

            var matrix = m1.Times(m2);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(m1.Rows);
            matrix.Columns.Should().Be(m1.Columns);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: Two matrixes with different number of rows.
        /// - When: Times method is called.
        /// - Then: An invalid operation exception is thrown.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesWithDifferentRowsWhenTimesIsCalledThenExceptionIsThrown()
            {
            var m1 = new Matrix(2, 3, 0.0);
            var m2 = new Matrix(3, 3, 0.0);

            var act = () => { var matrix = m1.Times(m2); };

            act.Should().Throw<InvalidOperationException>();
            }

        /// <summary>
        /// - Given: Two matrixes with different number of columns.
        /// - When: Times method is called.
        /// - Then: An invalid operation exception is thrown.
        /// </summary>
        [Fact]
        public void GivenTwoMatrixesWithDifferentColumnsWhenTimesIsCalledThenExceptionIsThrown()
            {
            var m1 = new Matrix(3, 3, 0.0);
            var m2 = new Matrix(3, 4, 0.0);

            var act = () => { var matrix = m1.Times(m2); };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }