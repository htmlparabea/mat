namespace Bea.Mat.Decompositions
    {

    /// <summary>
    /// Base class for matrix decomposition operations.
    /// </summary>
    public abstract class Decomposition
        {

        #region Properties

        /// <summary>
        /// Read-only copy of the matrix to decompose.
        /// </summary>
        public Matrix Matrix { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="matrix">
        /// Matrix to decompose.
        /// </param>
        protected Decomposition(Matrix matrix)
            {
            Matrix = matrix.AsReadOnly();
            }

        #endregion

        }

    }