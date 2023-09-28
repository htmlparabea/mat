namespace Bea.Mat.Operations.Tests
    {

    /// <summary>
    /// Unit tests over transpose operation.
    /// </summary>
    public class TransposeTests
        {

        /// <summary>
        /// - Given: A matrix.
        /// - When: Get the transpose matrix.
        /// - Then: A new matrix is returned with the transposed values.
        /// </summary>
        [Fact]
        public void GivenAnyMatrixWhenGetTransposeThenMatrixIsRetuned()
            {
            var expected = new double[3, 2]
            {
                { 1,  8, },
                { 4, -6  },
                { 3, 14  }
            };
            var data = new double[2, 3]
            {
                { 1, 4, 3 },
                { 8, -6, 14 }
            };

            var matrix = new Matrix(data);
            var t = matrix.T;

            t.Should().NotBeNull();
            t.Rows.Should().Be(matrix.Columns);
            t.Columns.Should().Be(matrix.Rows);

            Ensure.AllValuesAreEqual(t, expected);
            }

        }

    }