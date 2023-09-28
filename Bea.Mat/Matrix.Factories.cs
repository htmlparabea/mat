namespace Bea.Mat
    {

    /// <summary>
    /// The Matrix class defines the properties and operations of matrices.
    /// This partial class defines factory methods.
    /// </summary>
    public partial class Matrix
        {

        #region Factory methods

        /// <summary>
        /// Creates a new identity matrix.
        /// </summary>
        /// <param name="dimension">
        /// Dimension for the new matrix.
        /// </param>
        /// <returns>
        /// A new identity matrix.
        /// </returns>
        public static Matrix Eye(int dimension)
            {
            var matrix = new Matrix(dimension);

            for (var r = 0; r < matrix.Rows; r++)
                matrix[r, r] = 1.0;

            return matrix;
            }

        #endregion

        }

    }