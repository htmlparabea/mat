namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixGetColumnTests
        {

        /// <summary>
        /// - Given: Any matrix and a valid value for column index.
        /// - When: GetColumn is called.
        /// - Then: A new matrix is returned with one column.
        /// </summary>
        [Fact]
        public void GivenValidIndexWhenGetColumnIsCalledThenColumnMatrixIsReturned()
            {
            var expected = new double[4, 1]
            {
                {  2.0 },
                {  0.0 },
                {  3.0 },
                {  7.0 }
            };

            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var index = 1;

            var matrix = new Matrix(data);
            var sub = matrix.GetColumn(index);

            sub.Should().NotBeNull();
            sub.Columns.Should().Be(1);
            sub.Rows.Should().Be(matrix.Rows);

            Ensure.AllValuesAreEqual(sub, expected);
            }

        /// <summary>
        /// - Given: Any matrix and a negative column index.
        /// - When: GetColumn is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenNegativeIndexWhenGetColumnIsCalledThenExceptionIsThrown()
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
            var act = () => { var sub = matrix.GetColumn(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("column");
            }

        /// <summary>
        /// - Given: Any matrix and a column index greater than the number of columns.
        /// - When: GetColumn is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenIndexGreaterThanColumnsWhenGetColumnIsCalledThenExceptionIsThrown()
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
            var act = () => { var sub = matrix.GetColumn(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("column");
            }

        }

    }