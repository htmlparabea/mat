namespace Bea.Mat.Tests
{

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixTests
        {

        #region Constructor tests

        /// <summary>
        /// - Given: A positive value for dimension.
        /// - When: Constructor is called.
        /// - Then: A new squared matrix is created with zero values.
        /// </summary>
        [Fact]
        public void GivenPositiveDimensionWhenCalledConstructorThenMatrixIsCreated()
            {
            var dim = 4;

            var matrix = new Matrix(dim);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(dim);
            matrix.Columns.Should().Be(dim);
            matrix.IsSquare.Should().BeTrue();
            matrix.IsSymmetric.Should().BeTrue();
            matrix.IsZero.Should().BeTrue();
            matrix.IsNaN.Should().BeFalse();
            }

        /// <summary>
        /// - Given: A positive value for dimension and an initial double value.
        /// - When: Constructor is called.
        /// - Then: A new squared matrix is created with given value in all matrix positions.
        /// </summary>
        [Fact]
        public void GivenPositiveDimensionWithInitialValueWhenCalledConstructorThenMatrixIsCreated()
            {
            var dim = 4;
            var value = 2.5;

            var matrix = new Matrix(dim, value);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(dim);
            matrix.Columns.Should().Be(dim);
            matrix.IsSquare.Should().BeTrue();
            matrix.IsSymmetric.Should().BeTrue();
            matrix.IsZero.Should().BeFalse();
            matrix.IsNaN.Should().BeFalse();
            }

        /// <summary>
        /// - Given: A positive value for dimension with NaN value.
        /// - When: Constructor is called.
        /// - Then: A new squared matrix is created with a NaN value.
        /// </summary>
        [Fact]
        public void GivenPositiveDimensionWithNaNValueWhenCalledConstructorThenMatrixIsCreated()
            {
            var dim = 1;

            var matrix = new Matrix(dim, double.NaN);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(dim);
            matrix.Columns.Should().Be(dim);
            matrix.IsSquare.Should().BeTrue();
            matrix.IsSymmetric.Should().BeTrue();
            matrix.IsNaN.Should().BeTrue();
            }

        /// <summary>
        /// - Given: A negative value for dimensions.
        /// - When: Constructor is called.
        /// - Then: An argument exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNegativeDimensionWhenCalledConstructorThenExceptionIsThrown()
            {
            var dim = -1;

            var act = () => { var matrix = new Matrix(dim); };

            act.Should().Throw<ArgumentException>().WithParameterName("rows");
            }

        /// <summary>
        /// - Given: A positive value for rows and a positive value for columns.
        /// - When: Constructor is called.
        /// - Then: A new non-squared matrix is created with zero values.
        /// </summary>
        [Fact]
        public void GivenPositiveRowsAndColumnsWhenCalledConstructorThenMatrixIsCreated()
            {
            var rows = 3;
            var cols = 4;
            var matrix = new Matrix(rows, cols);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(rows);
            matrix.Columns.Should().Be(cols);
            matrix.IsSquare.Should().BeFalse();
            matrix.IsZero.Should().BeTrue();
            matrix.IsNaN.Should().BeFalse();
            }

        /// <summary>
        /// - Given: A positive value for rows, a positive value for columns and an initial double value.
        /// - When: Constructor is called.
        /// - Then: A new squared matrix is created with given value in all matrix positions.
        /// </summary>
        [Fact]
        public void GivenPositiveRowsAndColumnsWithInitialValueWhenCalledConstructorThenMatrixIsCreated()
            {
            var rows = 3;
            var cols = 4;
            var value = 2.5;

            var matrix = new Matrix(rows, cols, value);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(rows);
            matrix.Columns.Should().Be(cols);
            matrix.IsSquare.Should().BeFalse();
            matrix.IsZero.Should().BeFalse();
            matrix.IsNaN.Should().BeFalse();
            }

        /// <summary>
        /// - Given: A negative value for rows.
        /// - When: Constructor is called.
        /// - Then: An argument exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNegativeRowsWhenCalledConstructorThenExceptionIsThrown()
            {
            var rows = -1;
            var cols = 4;
            var value = 2.5;

            var act = () => { var matrix = new Matrix(rows, cols, value); };

            act.Should().Throw<ArgumentException>().WithParameterName("rows");
            }

        /// <summary>
        /// - Given: A negative value for columns.
        /// - When: Constructor is called.
        /// - Then: An argument exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNegativeColumnsWhenCalledConstructorThenExceptionIsThrown()
            {
            var rows = 2;
            var cols = -1;
            var value = 2.5;

            var act = () => { var matrix = new Matrix(rows, cols, value); };

            act.Should().Throw<ArgumentException>().WithParameterName("columns");
            }

        /// <summary>
        /// - Given: A bidimensional array of values.
        /// - When: Constructor is called.
        /// - Then: A new matrix is created with given value in all matrix positions.
        /// </summary>
        [Fact]
        public void GivenDataArrayWhenCalledConstructorThenMatrixIsCreated()
            {
            var data = new double[3, 2]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
                };
            var rows = data.GetLength(0);
            var cols = data.GetLength(1);

            var matrix = new Matrix(data);

            matrix.Should().NotBeNull();
            matrix.Rows.Should().Be(rows);
            matrix.Columns.Should().Be(cols);

            Ensure.AllValuesAreEqual(matrix, data);
            }

        #endregion

        }

    }