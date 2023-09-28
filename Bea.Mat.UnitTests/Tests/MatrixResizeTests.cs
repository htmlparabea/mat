namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixResizeTests
        {

        /// <summary>
        /// - Given: Any matrix and limits lower than dimensions of the matrix.
        /// - When: Resize is called.
        /// - Then: A new matrix with the new dimensions is returned.
        /// </summary>
        [Fact]
        public void GivenLimitsLowerThanDimensionsWhenResizeIsCalledThenNewMatrixIsReturned()
            {
            var rows = 2;
            var cols = 1;
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var expected = new double[2, 1]
            {
                {  2.0 },
                {  0.0 },
            };

            var matrix = new Matrix(data);
            var sub = matrix.Resize(rows, cols);

            sub.Should().NotBeNull();
            sub.Should().NotBe(matrix);
            sub.Rows.Should().Be(rows);
            sub.Columns.Should().Be(cols);

            Ensure.AllValuesAreEqual(sub, expected);
            }

        /// <summary>
        /// - Given: Any matrix and limits greater than dimensions of the matrix.
        /// - When: Resize is called.
        /// - Then: A new matrix with the new dimensions is returned and filled with zeros.
        /// </summary>
        [Fact]
        public void GivenLimitsGreaterThanDimensionsWhenResizeIsCalledThenNewMatrixIsReturned()
            {
            var rows = 3;
            var cols = 3;
            var data = new double[2, 2]
            {
                { -4.0,  3.0 },
                {  5.0,  7.0 }
            };

            var expected = new double[3, 3]
            {
                { -4.0,  3.0, 0.0 },
                {  5.0,  7.0, 0.0 },
                {  0.0,  0.0, 0.0 }
            };

            var matrix = new Matrix(data);
            var sub = matrix.Resize(rows, cols);

            sub.Should().NotBeNull();
            sub.Should().NotBe(matrix);
            sub.Rows.Should().Be(rows);
            sub.Columns.Should().Be(cols);

            Ensure.AllValuesAreEqual(sub, expected);
            }

        /// <summary>
        /// - Given: Any matrix and a row count lower than one.
        /// - When: Resize is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenRowCountLowerThanOneWhenResizeIsCalledThenExceptionIsThrown()
            {
            var rows = 0;
            var cols = 3;
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var matrix = new Matrix(data);
            var act = () => { var sub = matrix.Resize(rows, cols); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("rows");
            }

        /// <summary>
        /// - Given: Any matrix and a column count lower than one.
        /// - When: Resize is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenColumnCountLowerThanOneWhenResizeIsCalledThenExceptionIsThrown()
            {
            var rows = 2;
            var cols = 0;
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var matrix = new Matrix(data);
            var act = () => { var sub = matrix.Resize(rows, cols); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("columns");
            }

        }

    }