namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixCloneTests
        {

        /// <summary>
        /// - Given: Any matrix.
        /// - When: Clone is called.
        /// - Then: A new matrix with the same values is returned.
        /// </summary>
        [Fact]
        public void GivenAnyMatrixWhenCloneIsCalledThenNewMatrixIsReturned()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var matrix = new Matrix(data);
            var clone = matrix.Clone();

            clone.Should().NotBeNull();
            clone.Should().NotBe(matrix);
            clone.Rows.Should().Be(matrix.Rows);
            clone.Columns.Should().Be(matrix.Columns);

            Ensure.AllValuesAreEqual(clone, data);
            }

        /// <summary>
        /// - Given: Any matrix.
        /// - When: AsReadOnly is called.
        /// - Then: A new read-only matrix with the same values is returned.
        /// </summary>
        [Fact]
        public void GivenAnyMatrixWhenAsReadOnlyIsCalledThenNewReadOnlyMatrixIsReturned()
            {
            var data = new double[4, 3]
            {
                {  2.0,  2.0, -3.0 },
                {  0.0,  0.0, -1.0 },
                { -4.0,  3.0,  3.0 },
                {  5.0,  7.0, -1.0 }
            };

            var matrix = new Matrix(data);
            var clone = matrix.AsReadOnly();

            clone.Should().NotBeNull();
            clone.Should().NotBe(matrix);
            clone.Rows.Should().Be(matrix.Rows);
            clone.Columns.Should().Be(matrix.Columns);

            Ensure.AllValuesAreEqual(clone, data);

            var act1 = () => { clone[0, 0] = 0.0; };
            act1.Should().Throw<InvalidOperationException>();

            var act2 = () => { clone[0, 0, 1, 1] = new Matrix(2); };
            act2.Should().Throw<InvalidOperationException>();
            }

        }

    }