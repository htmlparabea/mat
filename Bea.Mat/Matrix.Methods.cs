namespace Bea.Mat
    {

    /// <summary>
    /// The Matrix class defines the properties and operations of matrices.
    /// This partial class defines some useful methods.
    /// </summary>
    public partial class Matrix
        {

        #region Methods

        /// <summary>
        /// Gets a row of the matrix as a new matrix.
        /// </summary>
        /// <param name="row">
        /// Zero-based index of the row.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the row.
        /// </returns>
        public Matrix GetRow(int row)
            {
            if (row < 0 || row > Rows - 1)
                throw new ArgumentOutOfRangeException(nameof(row), "Valid range: 0, Rows - 1");

            var data = new double[1, Columns];

            for (int c = 0; c < Columns; c++)
                data[0, c] = _data[row, c];

            return new Matrix(data);
            }

        /// <summary>
        /// Sets the values of the specified row using the given vector.
        /// </summary>
        /// <param name="row">
        /// Zero-based index of the row.
        /// </param>
        /// <param name="m">
        /// <see cref="Matrix"/> containing the vector used to set the row.
        /// </param>
        public void SetRow(int row, Matrix m)
            {
            if (row < 0 || row > Rows - 1)
                throw new ArgumentOutOfRangeException(nameof(row), "Valid range: 0, Rows - 1");
            if (m.Rows != 1)
                throw new ArgumentException("Have to have one row.", nameof(m));
            if (m.Columns != Columns)
                throw new ArgumentException("The number of colums does not match.", nameof(m));

            for (int c = 0; c < Columns; c++)
                this[row, c] = m[0, c];
            }

        /// <summary>
        /// Gets a column of the matrix as a new matrix.
        /// </summary>
        /// <param name="column">
        /// Zero-based index of the column.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the column.
        /// </returns>
        public Matrix GetColumn(int column)
            {
            if (column < 0 || column > Columns - 1)
                throw new ArgumentOutOfRangeException(nameof(column), "Valid range: 0, Columns - 1");

            var data = new double[Rows, 1];

            for (int r = 0; r < Rows; r++)
                data[r, 0] = _data[r, column];

            return new Matrix(data);
            }

        /// <summary>
        /// Sets the values of the specified column using the given vector.
        /// </summary>
        /// <param name="column">
        /// Zero-based index of the column.
        /// </param>
        /// <param name="m">
        /// <see cref="Matrix"/> containing the vector used to set the column.
        /// </param>
        public void SetColumn(int column, Matrix m)
            {
            if (column < 0 || column > Columns - 1)
                throw new ArgumentOutOfRangeException(nameof(column), "Valid range: 0, Columns - 1");
            if (m.Columns != 1)
                throw new ArgumentException("Have to have one column.", nameof(m));
            if (m.Rows != Rows)
                throw new ArgumentException("The number of rows does not match.", nameof(m));

            for (int r = 0; r < Rows; r++)
                this[r, column] = m[r, 0];
            }

        /// <summary>
        /// Sums all the values of the specified row.
        /// </summary>
        /// <param name="row">
        /// Zero-based index of the row.
        /// </param>
        /// <returns>
        /// Double value containing the result of the sum.
        /// </returns>
        public double SumRow(int row)
            {
            if (row < 0 || row > Rows - 1)
                throw new ArgumentOutOfRangeException(nameof(row), "Valid range: 0, Rows - 1");

            double sum = 0.0;

            for (int c = 0; c < Columns; c++)
                sum += _data[row, c];

            return sum;
            }

        /// <summary>
        /// Sums all the values of the specified column.
        /// </summary>
        /// <param name="column">
        /// Zero-based index of the row.
        /// </param>
        /// <returns>
        /// Double value containing the result of the sum.
        /// </returns>
        public double SumColumn(int column)
            {
            if (column < 0 || column > Columns - 1)
                throw new ArgumentOutOfRangeException(nameof(column), "Valid range: 0, Columns - 1");

            double sum = 0.0;

            for (int r = 0; r < Rows; r++)
                sum += _data[r, column];

            return sum;
            }

        /// <summary>
        /// Gets a writable copy of the current instance.
        /// </summary>
        /// <returns>
        /// <see cref="Matrix"/> containing the copy of the current instance.
        /// </returns>
        public Matrix Clone()
            {
            return new Matrix(_data);
            }

        /// <summary>
        /// Gets a read-only copy of the current instance.
        /// </summary>
        /// <returns>
        /// <see cref="Matrix"/> containing the copy of the current instance.
        /// </returns>
        public Matrix AsReadOnly()
            {
            var tmp = new Matrix(_data) { _isReadOnly = true };
            return tmp;
            }

        /// <summary>
        /// Gets a resized copy of the current instance. The new matrix is
        /// filled with zeros if the new dimensions are greater.
        /// </summary>
        /// <param name="rows">
        /// New number of rows.
        /// </param>
        /// <param name="columns">
        /// New number of columns.
        /// </param>
        /// <returns>
        /// <see cref="Matrix"/> containing the resized copy of the matrix.
        /// </returns>
        public Matrix Resize(int rows, int columns)
            {
            if (rows < 1)
                throw new ArgumentOutOfRangeException(nameof(rows), "Valid range: Greater than zero.");
            if (columns < 1)
                throw new ArgumentOutOfRangeException(nameof(columns), "Valid range: Greater then zero.");

            double[,] data = new double[rows, columns];
            int rr = Math.Min(rows, Rows);
            int cc = Math.Min(columns, Columns);

            for (int r = 0; r < rr; r++)
                for (int c = 0; c < cc; c++)
                    data[r, c] = _data[r, c];

            return new Matrix(data);
            }

        /// <summary>
        /// Computes the n-th power of the current instance.
        /// </summary>
        /// <param name="n">
        /// Power.
        /// </param>
        /// <returns>
        /// A new matrix containing the n-th power of the current instance.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// If the matrix is not square.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// In n is lower than zero.
        /// </exception>
        public Matrix Pow(int n)
            {
            if (!IsSquare)
                throw new InvalidOperationException("The matrix have to be square.");
            if (n < 0)
                throw new ArgumentOutOfRangeException(nameof(n), "Valid value: Greater or equal to zero.");

            return ExpBySquaring(this, n);
            }

        /// <summary>
        /// Gets the values of the current instance as a bidimensional array.
        /// </summary>
        /// <returns>
        /// Bidimensional array containing the values.
        /// </returns>
        public double[,] ToArray()
            {
            var array = new double[Rows, Columns];

            for (var r = 0; r < Rows; r++)
                for (var c = 0; c < Columns; c++)
                    array[r, c] = this[r, c];

            return array;
            }

        #endregion

        #region Aux methods

        private static Matrix ExpBySquaring(Matrix m, int n)
            {
            if (n == 0)
                return Eye(m.Rows);

            var pr = m * m;

            var res = (n % 2 == 0)
                ? ExpBySquaring(pr, n / 2)               // n is even.
                : m * ExpBySquaring(pr, (n - 1) / 2);    // n is odd.

            return res;
            }

        #endregion

        }

    }