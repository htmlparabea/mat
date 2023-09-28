namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixSumColumnTests
        {

        /// <summary>
        /// - Given: Any matrix and a valid value for column index.
        /// - When: SumColum is called.
        /// - Then: The value containing the sum of the column is returned.
        /// </summary>
        [Fact]
        public void GivenValidIndexWhenSumColumnIsCalledThenTheSumIsReturned()
            {
            var expected = 12.0;

            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var index = 1;

            var matrix = new Matrix(data);
            var sum = matrix.SumColumn(index);

            sum.Should().BeApproximately(expected, Matrix.Eps);
            }

        /// <summary>
        /// - Given: Any matrix and a negative column index.
        /// - When: SumColumn is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenNegativeIndexWhenSumColumnIsCalledThenExceptionIsThrown()
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
            var act = () => { var sub = matrix.SumColumn(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("column");
            }

        /// <summary>
        /// - Given: Any matrix and a column index greater than the number of columns.
        /// - When: SumColumn is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenIndexGreaterThanColumnsWhenSumColumnsIsCalledThenExceptionIsThrown()
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
            var act = () => { var sub = matrix.SumColumn(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("column");
            }

        }

    }