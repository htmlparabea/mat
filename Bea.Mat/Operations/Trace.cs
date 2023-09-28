namespace Bea.Mat.Operations
    {

    /// <summary>
    /// This class defines the process to compute the trace.
    /// </summary>
    public static class Trace
        {

        #region Static methods

        /// <summary>
        /// Computes the value of the trace for the given matrix.
        /// </summary>
        /// <param name="m">
        /// <see cref="Matrix"/>
        /// </param>
        /// <returns>
        /// Value of the trace.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// If the given matrix is non-square.
        /// </exception>
        public static double ComputeTrace(this Matrix m)
            {
            if (!m.IsSquare)
                throw new InvalidOperationException("Can not compute the trace of a non square matrix.");

            var tr = 0.0;

            for (var i = 0; i < m.Rows; i++) tr += m[i, i];

            return tr;
            }

        #endregion

        }

    }