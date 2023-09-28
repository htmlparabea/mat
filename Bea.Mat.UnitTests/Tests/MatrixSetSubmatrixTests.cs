namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixSetSubmatrixTests
        {

        /// <summary>
        /// - Given: A matrix and valid limits.
        /// - When: Set the value of submatrix.
        /// - Then: The operation is done.
        /// </summary>
        [Fact]
        public void GivenValidLimitsWhenSetSubmatrixIsCalledThenMatrixIsUpdated()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  0.0, -2.0, -1.0 },
                { -4.0, -1.0,  3.0,  3.0 },
                {  0.0,  5.0,  7.0, -1.0 }
            };
            var sub = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };
            var expected = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0 },
                { -4.0, -2.0,  8.0,  3.0 },
                {  0.0,  5.0,  7.0, -1.0 }
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 2;

            var tmp = new Matrix(sub);
            var matrix = new Matrix(data);
            matrix[startRow, startCol, endRow, endCol] = tmp;

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: A negative value of start row.
        /// - When: Set the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNegativeStartRowWhenSetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };
            var sub = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };

            var startRow = -1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 2;

            var matrix = new Matrix(data);
            var tmp = new Matrix(sub);
            var act = () => { matrix[startRow, startCol, endRow, endCol] = tmp; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startRow");
            }

        /// <summary>
        /// - Given: A negative value of start column.
        /// - When: Set the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNegativeStartColWhenSetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };
            var sub = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };

            var startRow = 1;
            var startCol = -1;
            var endRow = 2;
            var endCol = 2;

            var matrix = new Matrix(data);
            var tmp = new Matrix(sub);
            var act = () => { matrix[startRow, startCol, endRow, endCol] = tmp; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startCol");
            }

        /// <summary>
        /// - Given: The value of end row is lower than of start row.
        /// - When: Set the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndRowLowerThanStartRowWhenSetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };
            var sub = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 0;
            var endCol = 2;

            var matrix = new Matrix(data);
            var tmp = new Matrix(sub);
            var act = () => { matrix[startRow, startCol, endRow, endCol] = tmp; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startRow");
            }

        /// <summary>
        /// - Given: The value of end column is lower than of start column.
        /// - When: Set the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndColumnLowerThanStartColumnWhenSetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };
            var sub = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 0;

            var matrix = new Matrix(data);
            var tmp = new Matrix(sub);
            var act = () => { matrix[startRow, startCol, endRow, endCol] = tmp; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("startCol");
            }

        /// <summary>
        /// - Given: The value of end column exceeds the dimension of the matrix.
        /// - When: Set the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndColumnExceedsDimensionMatrixWhenSetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };
            var sub = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 2;
            var endCol = 5;

            var matrix = new Matrix(data);
            var tmp = new Matrix(sub);
            var act = () => { matrix[startRow, startCol, endRow, endCol] = tmp; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("endCol");
            }

        /// <summary>
        /// - Given: The value of end row exceeds the dimension of the matrix.
        /// - When: Set the value of submatrix.
        /// - Then: An exception is thrown.
        /// </summary>
        [Fact]
        public void GivenEndRowExceedsDimensionMatrixWhenSetSubmatrixIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 4]
            {
                {  2.0,  2.0, -1.0, -3.0 },
                {  0.0,  3.0,  2.0, -1.0},
                { -4.0, -2.0,  8.0,  3.0},
                {  0.0,  5.0,  7.0, -1.0}
            };
            var sub = new double[2, 2]
            {
                {  3.0,  2.0 },
                { -2.0,  8.0 }
            };

            var startRow = 1;
            var startCol = 1;
            var endRow = 5;
            var endCol = 2;

            var matrix = new Matrix(data);
            var tmp = new Matrix(sub);
            var act = () => { matrix[startRow, startCol, endRow, endCol] = tmp; };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("endRow");
            }

        }

    }