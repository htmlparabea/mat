namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixSetRowTests
        {

        /// <summary>
        /// - Given: A valid value for row index.
        /// - When: SetRow is called.
        /// - Then: The matrix is updated.
        /// </summary>
        [Fact]
        public void GivenValidIndexWhenSetRowIsCalledThenMatrixIsUpdated()
            {
            var expected = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var rowdata = new double[1, 3]
            {
                {  0.0 , 0.0, -1.0}
            };
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  1.0,  1.0,  1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var index = 1;

            var row = new Matrix(rowdata);
            var matrix = new Matrix(data);
            matrix.SetRow(index, row);

            Ensure.AllValuesAreEqual(matrix, expected);
            }

        /// <summary>
        /// - Given: A negative row index.
        /// - When: SetRow is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenNegativeIndexWhenSetRowIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  1.0,  1.0,  1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var rowdata = new double[1, 3]
            {
                {  0.0 , 0.0, -1.0}
            };
            var index = -1;

            var row = new Matrix(rowdata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetRow(index, row); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("row");
            }

        /// <summary>
        /// - Given: A row index greater than the number of rows.
        /// - When: SetRow is called.
        /// - Then: An argument out of range exception is thrown.
        /// </summary>
        [Fact]
        public void GivenIndexGreaterThanRowsWhenSetRowIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  1.0,  1.0,  1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var rowdata = new double[1, 3]
            {
                {  0.0 , 0.0, -1.0}
            };
            var index = 8;

            var row = new Matrix(rowdata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetRow(index, row); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("row");
            }

        /// <summary>
        /// - Given: A row with more than one row.
        /// - When: SetRow is called.
        /// - Then: An argument exception is thrown.
        /// </summary>
        [Fact]
        public void GivenRowWithMoreThanOneRowWhenSetRowIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var rowdata = new double[4, 2]
            {
                {  2.0,  2.0 },
                {  0.0,  1.0 },
                {  3.0, -2.0 },
                {  7.0,  1.0 }
            };
            var index = 1;

            var row = new Matrix(rowdata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetRow(index, row); };

            act.Should().Throw<ArgumentException>().WithParameterName("m");
            }

        /// <summary>
        /// - Given: A row with different number of columns.
        /// - When: SetRow is called.
        /// - Then: An argument exception is thrown.
        /// </summary>
        [Fact]
        public void GivenRowWithDifferentColumnsWhenSetRowIsCalledThenExceptionIsThrown()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  1.0,  1.0,  1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };
            var rowdata = new double[1, 4]
            {
                {  0.0 , 0.0, -1.0, 0.0}
            };
            var index = 1;

            var row = new Matrix(rowdata);
            var matrix = new Matrix(data);
            var act = () => { matrix.SetRow(index, row); };

            act.Should().Throw<ArgumentException>().WithParameterName("m");
            }

        }

    }