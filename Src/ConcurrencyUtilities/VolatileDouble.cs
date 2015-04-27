using System.Threading;

namespace ConcurrencyUtilities
{
    /// <summary>
    /// Double value on which the GetValue/SetValue operations are performed using Volatile.Read/Volatile.Write.
    /// </summary>
    /// <remarks>
    /// This datastructure is a struct. If a member is declared readonly VolatileDouble calling set will *NOT* modify the value.
    /// GetValue/SetValue are expressed as methods to make it obvious that a non-trivial operation is performed.
    /// </remarks>
    public struct VolatileDouble : VolatileValue<double>
    {
        private double value;

        /// <summary>
        /// Initialize the value of this instance
        /// </summary>
        /// <param name="value">Initial value of the instance.</param>
        public VolatileDouble(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Set the the value of this instance to <paramref name="newValue"/>
        /// </summary>
        /// <remarks>
        /// Don't call Set on readonly fields.
        /// </remarks>
        /// <param name="newValue">New value for this instance</param>
        public void SetValue(double newValue)
        {
            Volatile.Write(ref this.value, newValue);
        }

        /// <summary>
        /// From the Java Version:
        /// Eventually sets to the given value.
        /// The semantics are that the write is guaranteed not to be re-ordered with any previous write, 
        /// but may be reordered with subsequent operations (or equivalently, might not be visible to other threads) 
        /// until some other volatile write or synchronizing action occurs).
        /// </summary>
        /// <remarks>
        /// Currently implemented by calling Volatile.Write which is different from the java version. 
        /// Not sure if it is possible on CLR to implement this.
        /// </remarks>
        /// <param name="value">The new value for this instance.</param>
        public void LazySetValue(double value)
        {
            Volatile.Write(ref this.value, value);
        }

        /// <summary>
        /// Set the value without using Volatile.Write fence & ordering.
        /// </summary>
        /// <param name="value">The new value for this instance.</param>
        public void NonVolatileSetValue(double value)
        {
            this.value = value;
        }

        /// <summary>
        /// Get the current value of this instance
        /// </summary>
        /// <returns>The current value of the instance</returns>
        public double GetValue()
        {
            return Volatile.Read(ref this.value);
        }

        /// <summary>
        /// Returns the current value of the instance without using Volatile.Read fence & ordering.  
        /// </summary>
        /// <returns>The current value of the instance in a non-volatile way (might not observe changes on other threads).</returns>
        public double NonVolatileGetValue()
        {
            return this.value;
        }
    }
}
