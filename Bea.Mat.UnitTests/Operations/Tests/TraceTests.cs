namespace Bea.Mat.Operations.Tests
    {

    /// <summary>
    /// Unit tests over trace operation.
    /// </summary>
    public class TraceTests
        {

        /// <summary>
        /// - Given: A square matrix.
        /// - When: Get the trace value.
        /// - Then: The sum of values in the main diagonal is returned,
        /// </summary>
        [Fact]
        public void GivenSquareMatrixWhenGetTraceThenValueIsRetuned()
            {
            var expected = 7.0;
            var data = new double[2, 2]
            {
                { 1, 2 },
                { 8, 6 }
            };

            var matrix = new Matrix(data);
            var tr = matrix.Tr;

            tr.Should().BeApproximately(expected, Matrix.Eps);
            }

        /// <summary>
        /// - Given: A non-square matrix.
        /// - When: Get the trace value.
        /// - Then: An invalid operation exception is thrown.
        /// </summary>
        [Fact]
        public void GivenNonSquareMatrixWhenGetTraceThenExceptionIsThrown()
            {
            var data = new double[3, 2] 
            { 
                { 1, 2 }, 
                { 3, 4 }, 
                { 5, 6 } 
            };

            var matrix = new Matrix(data);
            var act = () => { var tmp = matrix.Tr; };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }