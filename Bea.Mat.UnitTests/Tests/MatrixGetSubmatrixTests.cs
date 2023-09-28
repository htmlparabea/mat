namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixGetSubmatrixTests
        {


        /// <summary>
        /// - Given: A matrix.
        /// - When: Get the value of submatrix.
        /// - Then: A new matrix is returned.
        /// </summary>
        [Fact]
        public void GivenValidLimitsWhenGetSubmatrixIsCalledThenNewMatrixIsReturned()
            {
            var expected = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 2;

            var matrix = new Matrix(data);
            var sub = matrix[startRow, startCol, endRow, endCol];

            sub.Should().NotBeNull();
            sub.Rows.Should().Be(endRow - startRow + 1);
            sub.Columns.Should().Be(endCol - startCol + 1);

            Ensure.AllValuesAreEqual(sub, expected);
            }

        /// <summary>
        /// - Given: A matrix and and negative value of start row.
        /// - When: Get the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNegativeStartRowWhenGetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };

            var startRow = -1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 2;

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix[startRow, startCol, endRow, endCol]; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startRow");
            }

        /// <summary>
        /// - Given: A matrix and and negative value of start column.
        /// - When: Get the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNegativeStartColWhenGetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };

            var startRow = 1;
            var startCol = -1;
            var endRow = 2;
            var endCol = 2;

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix[startRow, startCol, endRow, endCol]; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startCol");
            }

        /// <summary>
        /// - Given: A matrix and the value of end row is lower than of start row.
        /// - When: Get the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndRowLowerThanStartRowWhenGetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 0;
            var endCol = 2;

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix[startRow, startCol, endRow, endCol]; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startRow");
            }

        /// <summary>
        /// - Given: A matrix and the value of end column is lower than of start column.
        /// - When: Get the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndColumnLowerThanStartColumnWhenGetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 0;

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix[startRow, startCol, endRow, endCol]; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startCol");
            }

        /// <summary>
        /// - Given: A matrix and the value of end column exceeds the dimension of the matrix.
        /// - When: Get the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndColumnExceedsDimensionMatrixWhenGetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 5;

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix[startRow, startCol, endRow, endCol]; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("endCol");
            }

        /// <summary>
        /// - Given: A matrix and the value of end row exceeds the dimension of the matrix.
        /// - When: Get the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndRowExceedsDimensionMatrixWhenGetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 5;
            var endCol = 2;

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix[startRow, startCol, endRow, endCol]; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("endRow");
            }

        }

    }