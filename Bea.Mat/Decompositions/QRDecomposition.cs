namespace Bea.Mat.Decompositions
    {

    /// <summary>
    /// QR decomposition (QR factorization) is a way to decompose a matrix into the product of two 
    /// matrices, one being an orthogonal matrix (Q) and the other being an upper triangular matrix (R).
    /// This decomposition is commonly used in numerical linear algebra and is particularly useful in
    /// solving various mathematical and engineering problems.
    /// One of the advantages of QR decomposition is that it can be used for rectangular matrices as
    /// well. When A is a rectangular matrix (i.e., it has more columns than rows or more rows than
    /// columns), the resulting Q and R matrices will also be rectangular.
    /// Here are some key points to remember:
    ///  - Square Matrix: If A is a square matrix(having the same number of rows and columns), then 
    ///  the resulting Q and R matrices will also be square.
    ///  - Rectangular Matrix: If A is a rectangular matrix(having more rows than columns or more columns
    ///  than rows), then the resulting Q matrix will be a rectangular orthogonal matrix, and the R 
    ///  matrix will be a rectangular upper triangular matrix. In this case, the dimensions of Q and
    ///  R may be different, depending on the dimensions of A
    /// </summary>
    public class QRDecomposition : Decomposition
        {

        #region Attributes

        private readonly Matrix _qr;

        private readonly double[] _rdiag;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the orthogonal matrix Q of the decomposition.
        /// </summary>
        public Matrix Q { get; init; }

        /// <summary>
        /// Gets the upper triangular matrix R of the decomposition.
        /// </summary>
        public Matrix R { get; init; }

        /// <summary>
        /// Gets a value indicating wheter the decomposed matrix is full rank.
        /// </summary>
        public bool IsFullRank
            {
            get
                {
                for (int j = 0; j < _rdiag.Length; j++)
                    if (Math.Abs(_rdiag[j]) < Matrix.Eps)
                        return false;

                return true;
                }
            }

        #endregion

        #region Constructors

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="matrix">
        /// Matrix to decompose.
        /// </param>
        public QRDecomposition(Matrix matrix) : base(matrix)
            {
            var tup = ComputeQR(matrix);
            _qr = tup.Item1;
            _rdiag = tup.Item2;

            Q = ComputeQ(_qr);
            R = ComputeR(_qr, _rdiag);
            }

        #endregion

        #region Static methods

        private static Tuple<Matrix, double[]> ComputeQR(Matrix matrix)
            {
            var qr = matrix.Clone();
            var rdiag = new double[qr.Columns];

            for (int k = 0; k < qr.Columns; k++)
                {
                double norm = 0;

                for (int i = k; i < qr.Rows; i++)
                    norm = Hypot(norm, qr[i, k]);

                if (norm != 0.0)
                    {
                    if (qr[k, k] < 0)
                        norm = -norm;

                    for (int i = k; i < qr.Rows; i++)
                        qr[i, k] /= norm;

                    qr[k, k] += 1.0;

                    for (int j = k + 1; j < qr.Columns; j++)
                        {
                        double s = 0.0;

                        for (int i = k; i < qr.Rows; i++)
                            s += qr[i, k] * qr[i, j];

                        s = -s / qr[k, k];

                        for (int i = k; i < qr.Rows; i++)
                            qr[i, j] += s * qr[i, k];
                        }
                    }

                rdiag[k] = -norm;
                }

            return new Tuple<Matrix, double[]>(qr, rdiag);
            }

        private static double Hypot(double a, double b)
            {
            double r;

            if (Math.Abs(a) > Math.Abs(b))
                {
                r = b / a;
                r = Math.Abs(a) * Math.Sqrt(1 + r * r);
                }
            else if (Math.Abs(b) > Matrix.Eps)
                {
                r = a / b;
                r = Math.Abs(b) * Math.Sqrt(1 + r * r);
                }
            else
                {
                r = 0.0;
                }

            return r;
            }

        private static Matrix ComputeQ(Matrix qr)
            {
            var q = new Matrix(qr.Rows, qr.Columns, 0.0);

            for (int k = q.Columns - 1; k >= 0; k--)
                {
                q[k, k] = 1.0;

                for (int j = k; j < q.Columns; j++)
                    {
                    if (qr[k, k] != 0)
                        {
                        double s = 0.0;

                        for (int i = k; i < q.Rows; i++)
                            s += qr[i, k] * q[i, j];

                        s = -s / qr[k, k];

                        for (int i = k; i < q.Rows; i++)
                            q[i, j] += s * qr[i, k];
                        }
                    }
                }

            return q;
            }

        private static Matrix ComputeR(Matrix qr, double[] rdiag)
            {
            var r = new Matrix(qr.Columns, 0.0);

            for (int i = 0; i < r.Rows; i++)
                {
                for (int j = 0; j < r.Columns; j++)
                    {
                    if (i < j)
                        r[i, j] = qr[i, j];
                    else if (i == j)
                        r[i, j] = rdiag[i];
                    else
                        r[i, j] = 0.0;
                    }
                }

            return r;
            }

        #endregion

        #region Methods (Own members)

        public Matrix Solve(Matrix b)
            {
            if (b.Columns != 1)
                throw new InvalidOperationException("The number of colums in b has to be 1.");
            if (b.Rows != _qr.Rows)
                throw new InvalidOperationException("The number of rows in b has to be equal to the number of rows in QR.");
            if (!IsFullRank)
                throw new InvalidOperationException("The matrix is not full rank.");

            Matrix x = b.Clone();

            // Compute Y = Q.T * B
            for (int k = 0; k < _qr.Columns; k++)
                {
                for (int j = 0; j < b.Columns; j++)
                    {
                    double s = 0.0;

                    for (int i = k; i < _qr.Rows; i++)
                        s += _qr[i, k] * x[i, j];

                    s = -s / _qr[k, k];

                    for (int i = k; i < _qr.Rows; i++)
                        x[i, j] += s * _qr[i, k];
                    }
                }

            // Solve R*X = Y;
            for (int k = _qr.Columns - 1; k >= 0; k--)
                {
                for (int j = 0; j < b.Columns; j++)
                    x[k, j] /= _rdiag[k];

                for (int i = 0; i < k; i++)
                    for (int j = 0; j < b.Columns; j++)
                        x[i, j] -= x[k, j] * _qr[i, k];
                }

            return x;
            }

        #endregion

        }

    }