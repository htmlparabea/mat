namespace Bea.Mat
    {

    using Operations;

    /// <summary>
    /// The Matrix class defines the properties and operations of matrices.
    /// This partial class defines basic properties and constructors.
    /// </summary>
    public partial class Matrix
        {

        #region Constants

        /// <summary>
        /// Epsilon value to ensure convergence in some operations.
        /// </summary>
        public const double Eps = 1E-12;

        #endregion

        #region Attributes

        /// <summary>
        /// Underlying data array.
        /// </summary>
        private readonly double[,] _data;

        /// <summary>
        /// Value indicating wheter a matrix is read-only and can not be modified.
        /// </summary>
        private bool _isReadOnly;

        /// <summary>
        /// Value of the trace.
        /// </summary>
        private double? _tr;

        /// <summary>
        /// Value of the determinant.
        /// </summary>
        private double? _det;

        /// <summary>
        /// Value of the rank.
        /// </summary>
        private int? _rank;

        /// <summary>
        /// Transpose of the matrix.
        /// </summary>
        private Matrix? _t;

        /// <summary>
        /// Value of the inverse matrix.
        /// </summary>
        private Matrix? _inv;

        #endregion

        #region Indexes

        /// <summary>
        /// Gets or sets the value for the r-th row and c-th column.
        /// </summary>
        /// <param name="r">
        /// Zero-based index for the row.
        /// </param>
        /// <param name="c">
        /// Zero-based index for the column.
        /// </param>
        /// <returns>
        /// Double value which contains the value at the given location of the matrix.
        /// </returns>
        public double this[int r, int c]
            {
            get { return _data[r, c]; }
            set
                {
                if (_isReadOnly)
                    throw new InvalidOperationException("Matrix is read-only.");

                _data[r, c] = value;
                Invalidate();
                }
            }

        /// <summary>
        /// Gets or sets the submatrix of the matrix.
        /// </summary>
        /// <param name="sr">
        /// Zero-based index for the starting row.
        /// </param>
        /// <param name="sc">
        /// Zero-based index for the starting column.
        /// </param>
        /// <param name="er">
        /// Zero-based index for the end row.
        /// </param>
        /// <param name="ec">
        /// Zero-based index for the end column.
        /// </param>
        /// <returns>
        /// Submatriz.
        /// </returns>
        public Matrix this[int sr, int sc, int er, int ec]
            {
            get { return GetMatrix(sr, sc, er, ec); }
            set
                {
                if (_isReadOnly)
                    throw new InvalidOperationException("Matrix is read-only.");

                SetMatrix(sr, sc, er, ec, value);
                Invalidate();
                }
            }

        #endregion

        #region Properties (Own members)

        /// <summary>
        /// Gets the number of total rows.
        /// </summary>
        public int Rows => _data.GetLength(0);

        /// <summary>
        /// Gets the number of total columns.
        /// </summary>
        public int Columns => _data.GetLength(1);

        /// <summary>
        /// Gets a value indicating whether the matrix is square.
        /// </summary>
        public bool IsSquare => Rows == Columns;

        /// <summary>
        /// Gets a value indicating whether the matrix is symmetric.
        /// </summary>
        public bool IsSymmetric
            {
            get
                {
                for (var i = 0; i < Columns; i++)
                    {
                    for (var j = 0; j < Columns; j++)
                        {
                        var d = Math.Abs(_data[i, j] - _data[j, i]);
                        if (d > Eps) return false;
                        }
                    }

                return true;
                }
            }

        /// <summary>
        /// Gets a value indicating whether all the values of the matrix are zero.
        /// </summary>
        public bool IsZero
            {
            get
                {
                for (var i = 0; i < Rows; i++)
                    {
                    for (var j = 0; j < Columns; j++)
                        {
                        var d = Math.Abs(_data[i, j]) > Eps;
                        if (d) return false;
                        }
                    }

                return true;
                }
            }

        /// <summary>
        /// Gets a value indicating whether some of the values of the matrix are non numerics (NaN).
        /// </summary>
        public bool IsNaN
            {
            get
                {
                for (var i = 0; i < Rows; i++)
                    {
                    for (var j = 0; j < Columns; j++)
                        {
                        var d = double.IsNaN(_data[i, j]);
                        if (d) return true;
                        }
                    }

                return false;
                }
            }

        /// <summary>
        /// Gets the value of the trace.
        /// </summary>
        public double Tr
            {
            get
                {
                if (!_tr.HasValue) _tr = this.ComputeTrace();
                return _tr.Value;
                }
            }

        /// <summary>
        /// Gets the value of the determinant.
        /// </summary>
        public double Det
            {
            get
                {
                if (!_det.HasValue) _det = this.ComputeDeterminant();
                return _det.Value;
                }
            }

        /// <summary>
        /// Gets the value of the rank.
        /// </summary>
        public int Rank
            {
            get
                {
                if (!_rank.HasValue) _rank = this.ComputeRank();
                return _rank.Value;
                }
            }

        /// <summary>
        /// Gets the transpose.
        /// </summary>
        public Matrix T
            {
            get
                {
                _t ??= this.ComputeTraspose();
                return _t;
                }
            }

        /// <summary>
        /// Gets the inverse matrix.
        /// </summary>
        public Matrix Inv
            {
            get
                {
                _inv ??= this.ComputeInverse();
                return _inv;
                }
            }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance of a square matrix.
        /// </summary>
        /// <param name="dimension">
        /// Integer setting the matrix dimension.
        /// </param>
        public Matrix(int dimension) : this(dimension, 0.0) { }

        /// <summary>
        /// Creates a new instance of a square matrix and initializes with
        /// the given value.
        /// </summary>
        /// <param name="dimension">
        /// Integer setting the matrix dimension.
        /// </param>
        /// <param name="value">
        /// Double setting the value of each matrix position. If not given, the
        /// matrix is initialized with 0.
        /// </param>
        public Matrix(int dimension, double value = 0.0) : this(dimension, dimension, value) { }

        /// <summary>
        /// Creates a new instance of a matrix and initializes with
        /// the given value.
        /// </summary>
        /// <param name="rows">
        /// Integer setting the number of rows.
        /// </param>
        /// <param name="columns">
        /// Integer setting the number of columns.
        /// </param>
        /// <param name="value">
        /// Double setting the value of each matrix position. If not given, the
        /// matrix is initialized with 0.
        /// </param>
        public Matrix(int rows, int columns, double value = 0.0)
            {
            if (rows < 1)
                throw new ArgumentException("Have to be a value greater than zero.", nameof(rows));
            if (columns < 1)
                throw new ArgumentException("Have to be a value greater than zero.", nameof(columns));

            _data = InitData(rows, columns, value);
            _isReadOnly = false;
            }

        /// <summary>
        /// Creates a new instance of a matrix from the given bidimensional array.
        /// </summary>
        /// <param name="data">
        /// Bidimensional array used to initialize the matrix.
        /// </param>
        public Matrix(double[,] data)
            {
            _data = InitData(data);
            _isReadOnly = false;
            }

        #endregion

        #region Static Methods

        /// <summary>
        /// Initializes the underlying array.
        /// </summary>
        private static double[,] InitData(int rows, int columns, double value = 0.0)
            {
            var tmp = new double[rows, columns];

            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    tmp[r, c] = value;

            return tmp;
            }

        /// <summary>
        /// Initializes the underlying array.
        /// </summary>
        private static double[,] InitData(double[,] data)
            {
            var rows = data.GetLength(0);
            var columns = data.GetLength(1);

            var tmp = new double[rows, columns];

            for (var r = 0; r < rows; r++)
                for (var c = 0; c < columns; c++)
                    tmp[r, c] = data[r, c];

            return tmp;
            }

        #endregion

        #region Methods (Own members)

        /// <summary>
        /// Aux method to set the values from a given submatrix.
        /// </summary>
        /// <param name="startRow">
        /// Zero-based index for the starting row.
        /// </param>
        /// <param name="startCol">
        /// Zero-based index for the starting column.
        /// </param>
        /// <param name="endRow">
        /// Zero-based index for the end row.
        /// </param>
        /// <param name="endCol">
        /// Zero-based index for the end column.
        /// </param>
        /// <param name="m">
        /// Submatrix.
        /// </param>
        private void SetMatrix(int startRow, int startCol, int endRow, int endCol, Matrix m)
            {
            if (startRow < 0 || startRow > endRow)
                throw new ArgumentOutOfRangeException(nameof(startRow), "Valid range: 0, endRow");
            if (endRow < 0 || endRow > Rows - 1)
                throw new ArgumentOutOfRangeException(nameof(endRow), "Valid range: 0, Rows - 1");
            if (startCol < 0 || startCol > endCol)
                throw new ArgumentOutOfRangeException(nameof(startCol), "Valid range: 0, endCol");
            if (endCol < 0 || endCol > Columns - 1)
                throw new ArgumentOutOfRangeException(nameof(endCol), "Valid range: 0, Columns - 1");

            for (var r = startRow; r <= endRow; r++)
                for (var c = startCol; c <= endCol; c++)
                    _data[r, c] = m[r - startRow, c - startCol];
            }

        /// <summary>
        /// Aux method to get the value of a given submatrix.
        /// </summary>
        /// <param name="startRow">
        /// Zero-based index for the starting row.
        /// </param>
        /// <param name="startCol">
        /// Zero-based index for the starting column.
        /// </param>
        /// <param name="endRow">
        /// Zero-based index for the end row.
        /// </param>
        /// <param name="endCol">
        /// Zero-based index for the end column.
        /// </param>
        /// <returns>
        /// Submatrix.
        /// </returns>
        private Matrix GetMatrix(int startRow, int startCol, int endRow, int endCol)
            {
            if (startRow < 0 || startRow > endRow)
                throw new ArgumentOutOfRangeException(nameof(startRow), "Valid range: 0, endRow");
            if (endRow < 0 || endRow > Rows - 1)
                throw new ArgumentOutOfRangeException(nameof(endRow), "Valid range: 0, Rows - 1");
            if (startCol < 0 || startCol > endCol)
                throw new ArgumentOutOfRangeException(nameof(startCol), "Valid range: 0, endCol");
            if (endCol < 0 || endCol > Columns - 1)
                throw new ArgumentOutOfRangeException(nameof(endCol), "Valid range: 0, Columns - 1");

            var res = new Matrix(endRow - startRow + 1, endCol - startCol + 1);

            for (var r = startRow; r <= endRow; r++)
                for (var c = startCol; c <= endCol; c++)
                    res[r - startRow, c - startCol] = _data[r, c];

            return res;
            }

        /// <summary>
        /// Invalidates the state of the current instance.
        /// </summary>
        private void Invalidate()
            {
            _tr = null;
            _det = null;
            _rank = null;
            _t = null;
            _inv = null;
            }

        #endregion

        }

    }