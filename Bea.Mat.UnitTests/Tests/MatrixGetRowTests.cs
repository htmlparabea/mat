namespace Bea.Mat.Tests
{

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixGetRowTests
        {

        /// <summary>
        /// - Given: Any matrix and a valid value for row index.
        /// - When: GetRow is called.
        /// - Then: A new matrix is returned.
        /// </summary>
        [Fact]
        public void GivenValidIndexWhenGetRowIsCalledThenRowMatrixIsReturned()
            {
            var expected = new double[1, 3]
            {
                { -4.0,  3.0,  3.0 }
            };

            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var index = 2;

            var matrix = new Matrix(data);
            var sub = matrix.GetRow(index);

            sub.Should().NotBeNull();
            sub.Rows.Should().Be(1);
            sub.Columns.Should().Be(matrix.Columns);

            Ensure.AllValuesAreEqual(sub, expected);
            }

        /// <summary>
        /// - Given: Any matrix and a negative row index.
        /// - When: GetRow is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenNegativeIndexWhenGetRowIsCalledThenExceptionIsThrown()
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
            var act = () => { var sub = matrix.GetRow(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("row");
            }

        /// <summary>
        /// - Given: Any matrix and a row index greater than the number of rows.
        /// - When: GetRow is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenIndexGreaterThanRowsWhenGetRowIsCalledThenExceptionIsThrown()
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
            var act = () => { var sub = matrix.GetRow(index); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("row");
            }

        }

    }