namespace Bea.Mat
    {

    /// <summary>
    /// Aux class to check matrix values.
    /// </summary>
    internal static class Ensure
        {

        public static void AllValuesAreEqual(Matrix result, double[,] expected)
            {
            for (int r = 0; r < result.Rows; r++)
                for (int c = 0; c < result.Columns; c++)
                    result[r, c].Should().BeApproximately(expected[r, c], Matrix.Eps);
            }

        }

    }