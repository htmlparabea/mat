namespace Bea.Mat.Tests
    {

    /// <summary>
    /// Unit tests over Matrix class.
    /// </summary>
    public class MatrixPowTests
        {

        /// <summary>
        /// - Given: A square matrix and power equals to 0.
        /// - When: Pow is called.
        /// - Then: A new identity matrix is returned.
        /// </summary>
        [Fact]
        public void GivenPowerEqualToZeroWhenPowIsCalledThenIdentityMatrixIsReturned()
            {
            var expected = new double[2, 2]
            {
                { 1.0, 0.0 },
                { 0.0, 1.0 }
            };

            var data = new double[2, 2]
            {
                {  2.0, -2.0 },
                {  3.0,  1.0 }
            };

            var power = 0;

            var matrix = new Matrix(data);
            var pow = matrix.Pow(power);

            Ensure.AllValuesAreEqual(pow, expected);
            }

        /// <summary>
        /// - Given: A square matrix and power is even.
        /// - When: Pow is called.
        /// - Then: A new matrix is returned.
        /// </summary>
        [Fact]
        public void GivenPowerIsEvenWhenPowIsCalledThenMatrixIsReturned()
            {
            var expected = new double[2, 2]
            {
                { -2.0, -6.0 },
                {  9.0, -5.0 }
            };

            var data = new double[2, 2]
            {
                {  2.0, -2.0 },
                {  3.0,  1.0 }
            };

            var power = 2;

            var matrix = new Matrix(data);
            var pow = matrix.Pow(power);

            Ensure.AllValuesAreEqual(pow, expected);
            }

        /// <summary>
        /// - Given: A square matrix and power is odd.
        /// - When: Pow is called.
        /// - Then: A new matrix is returned.
        /// </summary>
        [Fact]
        public void GivenPowerIsOddWhenPowIsCalledThenMatrixIsReturned()
            {
            var expected = new double[2, 2]
            {
                { -22.0,  -2.0 },
                {   3.0, -23.0 }
            };

            var data = new double[2, 2]
            {
                {  2.0, -2.0 },
                {  3.0,  1.0 }
            };

            var power = 3;

            var matrix = new Matrix(data);
            var pow = matrix.Pow(power);

            Ensure.AllValuesAreEqual(pow, expected);
            }

        /// <summary>
        /// - Given: Power is negative.
        /// - When: Pow is called.
        /// - Then: An argument out of range exception is raised.
        /// </summary>
        [Fact]
        public void GivenPowerIsNegativeWhenPowIsCalledThenExceptionIsThrown()
            {
            var data = new double[2, 2]
            {
                {  2.0, -2.0 },
                {  3.0,  1.0 }
            };

            var power = -3;

            var matrix = new Matrix(data);
            var act = () => { var pow = matrix.Pow(power); };

            act.Should().Throw<ArgumentOutOfRangeException>().WithParameterName("n");
            }

        /// <summary>
        /// - Given: Non-square matrix.
        /// - When: Pow is called.
        /// - Then: An invalid operation exception is raised.
        /// </summary>
        [Fact]
        public void GivenNonSquareMatrixWhenPowIsCalledThenExceptionIsThrown()
            {
            var data = new double[2, 3]
            {
                {  2.0, -2.0,  1.0 },
                {  3.0,  1.0, -2.0 }
            };

            var power = 2;

            var matrix = new Matrix(data);
            var act = () => { var pow = matrix.Pow(power); };

            act.Should().Throw<InvalidOperationException>();
            }

        }

    }