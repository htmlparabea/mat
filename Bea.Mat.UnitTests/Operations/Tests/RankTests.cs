namespace Bea.Mat.Operations.Tests
    {

    /// <summary>
    /// Unit tests over rank operation.
    /// </summary>
    public class RankTests
        {

        /// <summary>
        /// - Given: A matrix with linear independent columns.
        /// - When: Get the value of the rank.
        /// - Then: The value is equal to the number of columns.
        /// </summary>
        [Fact]
        public void GivenMatrixWithIndependentColumnsWhenGetRankThenValueEqualToColumnsIsReturned()
            {
            var data = new double[3, 3]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            };

            var matrix = new Matrix(data);
            var rank = matrix.Rank;

            rank.Should().Be(matrix.Columns);
            }

        /// <summary>
        /// - Given: A matrix in which a column is the linear combination of other two.
        /// - When: Get the value of the rank.
        /// - Then: The value is equal to the number of columns minus one.
        /// </summary>
        [Fact]
        public void GivenMatrixWithNonIndependentColumnsWhenGetRankThenValueEqualToColumnsMinusOneIsReturned()
            {
            var data = new double[3, 3]
            {
                { 1, 2, 3 },
                { 8, 6, 14 },
                { 5, 1, 6 }
            };

            var matrix = new Matrix(data);
            var rank = matrix.Rank;

            rank.Should().Be(matrix.Columns - 1);
            }

        /// <summary>
        /// - Given: An empty matrix.
        /// - When: Get the value of the rank.
        /// - Then: The value is equal to zero.
        /// </summary>
        [Fact]
        public void GivenEmptyMatrixWhenGetRankThenZeroIsReturned()
            {
            var data = new double[3, 3]
            {
                { 0, 0, 0 },
                { 0, 0, 0 },
                { 0, 0, 0 }
            };

            var matrix = new Matrix(data);
            var rank = matrix.Rank;

            rank.Should().Be(0);
            }

        }

    }