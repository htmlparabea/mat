namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixSetColumnTests
        {

        /// <summary>
        /// - Given: A valid value for column index.
        /// - When: SetColumn is called.
        /// - Then: The matrix is updated.
        /// </summary>
        [Fact]
        public void GivenValidIndexWhenSetColumnIsCalledThenMatrixIsUpdated()
            {
            var expected = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var coldata = new double[4, 1]
            {
                {  2.0 },
                {  0.0 },
                {  3.0 },
                {  7.0 }
            };
            var data = new double[4, 3]
            {
                {  2.0,  1.0, -3.0 },
                {  0.0,  1.0, -1.0 },
                { -4.0,  1.0,  3.0 },
                {  5.0,  1.0, -1.0 }
            };

            var index = 1;

            var col = new Matrix(coldata);
            var matrix = new Matrix(data);
            matrix.SetColumn(index, col);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: A negative column index.
        /// - When: SetColumn is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenNegativeIndexWhenSetColumnIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var coldata = new double[4, 1]
            {
                {  2.0 },
                {  0.0 },
                {  3.0 },
                {  7.0 }
            };
            var index = -1;

            var col = new Matrix(coldata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetColumn(index, col); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("column");
            }

        /// <summary>
        /// - Given: A column index greater than the number of columns.
        /// - When: SetColumn is called.
        /// - Then: An argument out of range exception is thrown.
        /// </summary>
        [Fact]
        public void GivenIndexGreaterThanColumnsWhenSetColumnIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var coldata = new double[4, 1]
            {
                {  2.0 },
                {  0.0 },
                {  3.0 },
                {  7.0 }
            };
            var index = 8;

            var col = new Matrix(coldata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetColumn(index, col); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("column");
            }

        /// <summary>
        /// - Given: A column with more than one column.
        /// - When: SetColumn is called.
        /// - Then: An argument exception is thrown.
        /// </summary>
        [Fact]
        public void GivenColumnWithMoreThanOneColumnWhenSetColumnIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var coldata = new double[4, 2]
            {
                {  2.0,  2.0 },
                {  0.0,  1.0 },
                {  3.0, -2.0 },
                {  7.0,  1.0 }
            };
            var index = 1;

            var col = new Matrix(coldata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetColumn(index, col); };

            act.Should().Throw<ArgumentException>().WithParameterName("m");
            }

        /// <summary>
        /// - Given: A column with different number of rows.
        /// - When: SetColumn is called.
        /// - Then: An argument exception is thrown.
        /// </summary>
        [Fact]
        public void GivenColumnWithDifferentRowsWhenSetColumnIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var coldata = new double[5, 1]
            {
                {  2.0 },
                {  0.0 },
                {  3.0 },
                {  7.0 },
                {  2.0 }
            };
            var index = 1;

            var col = new Matrix(coldata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetColumn(index, col); };

            act.Should().Throw<ArgumentException>().WithParameterName("m");
            }

        }

    }