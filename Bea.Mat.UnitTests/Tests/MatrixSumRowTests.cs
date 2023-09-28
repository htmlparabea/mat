namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixSumRowTests
        {

        /// <summary>
        /// - Given: Any matrix and a valid value for row index.
        /// - When: SumRow is called.
        /// - Then: The value containing the sum of the row is returned.
        /// </summary>
        [Fact]
        public void GivenValidIndexWhenSumRowIsCalledThenTheSumIsReturned()
            {
            var expected = 11.0;

            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var index = 3;

            var matrix = new Matrix(data);
            var sum = matrix.SumRow(index);

            sum.Should().BeApproximately(expected, Matrix.Eps);
            }

        /// <summary>
        /// - Given: Any matrix and a negative row index.
        /// - When: SumRow is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenNegativeIndexWhenSumRowIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var index = -1;

            var matrix = new Matrix(data);
            var act = () => { var sub = matrix.SumRow(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("row");
            }

        /// <summary>
        /// - Given: Any matrix and a row index greater than the number of rows.
        /// - When: SumRow is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenIndexGreaterThanRowsWhenSumRowIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var index = 8;

            var matrix = new Matrix(data);
            var act = () => { var sub = matrix.SumRow(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("row");
            }

        }

    }