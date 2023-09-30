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

        #region Aux static methods

        //protected static double Hypot(double a, double b)
        //    {
        //    double r;

        //    if (Math.Abs(a) > Math.Abs(b))
        //        {
        //        r = b / a;
        //        r = Math.Abs(a) * Math.Sqrt(1 + r * r);
        //        }
        //    else if (Math.Abs(b) > Matrix.Eps) /* b != 0*/
        //        {
        //        r = a / b;
        //        r = Math.Abs(b) * Math.Sqrt(1 + r * r);
        //        }
        //    else
        //        {
        //        r = 0.0;
        //        }

        //    return r;
        //    }

        #endregion

        }

    }