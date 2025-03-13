using System.Data;

namespace HxH.App.Models
{
    /// <summary>
    /// Represents the result of an operation, encapsulating a value and a collection of errors.
    /// </summary>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    public sealed class Result<T> : IDisposable
    {
        /// <summary>
        /// Value of the result.
        /// You need check IfSuccess or IsFail before getting the value,
        /// or getter throw <see cref="NoNullAllowedException"/>.
        /// </summary>
        private T _value = default!;

        private List<Exception> _errors = new List<Exception>();

        /// <summary>
        /// Gets or sets the value of the result.
        /// </summary>
        public T Value
        {
            get
            {

                if (IsFail)
                {
                    throw new NoNullAllowedException("Getting value are not allowed if the result is failed.", _errors[0]);
                }

                return _value;
            }

            set { _value = value; }
        }

        /// <summary>
        /// Gets or sets the collection of errors associated with the result.
        /// </summary>
        public List<Exception> Errors
        {
            get { return _errors; }
        }

        /// <summary>
        /// Gets a value indicating whether the result was successful (no errors).
        /// </summary>
        public bool IsSuccess
        {
            get { return _errors.Count == 0; }
        }

        /// <summary>
        /// Gets a value indicating whether the result failed (contains errors).
        /// </summary>
        public bool IsFail
        {
            get { return _errors.Count != 0; }
        }

        /// <summary>
        /// Add value to the result.
        /// </summary>
        public static Result<T> operator +(Result<T> result, T value)
        {
            result.Value = value;
            return result;
        }

        /// <summary>
        /// Add error to the result error list.
        /// </summary>
        public static Result<T> operator +(Result<T> result, Exception exception)
        {
            result.Errors.Add(exception);
            return result;
        }

        private bool _disposed = false;

        /// <summary>
        /// Disposes of the resources used by the <see cref="Result{T}"/> instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases the resources used by the <see cref="Result{T}"/> instance.
        /// </summary>
        /// <param name="disposing">Indicates whether the method is being called from the Dispose method.</param>
        private void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose of managed resources
                    _errors.Clear();
                }

                // Set unmanaged resources to default values
                _value = default!;

                _disposed = true;
            }
        }

        /// <summary>
        /// Destructor for the <see cref="Result{T}"/> class to ensure proper cleanup.
        /// </summary>
        ~Result()
        {
            Dispose(disposing: false);
        }
    }
}
