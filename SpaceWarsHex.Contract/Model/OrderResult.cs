namespace SpaceWars.Model
{
    /// <summary>
    /// The result of an <see cref="Interfaces.Orders.IOrder"/>
    /// </summary>
    public class OrderResult
    {
        /// <summary>
        /// <see cref="OrderStatus"/>
        /// </summary>
        public OrderStatus Status;

        /// <summary>
        /// Any message to return along with success/failure
        /// </summary>
        public string Message;

        /// <summary>
        /// Creates a <see cref="OrderResult"/> with the status <see cref="OrderStatus.Ok"/>.
        /// </summary>
        /// <param name="message">The message to add the the result.</param>
        /// <returns></returns>
        public static OrderResult Ok(string message = null) => new() { Status = OrderStatus.Ok, Message = message };

        /// <summary>
        /// Creates a <see cref="OrderResult"/> with the status <see cref="OrderStatus.NotModified"/>.
        /// </summary>
        /// <param name="message">The message to add the the result.</param>
        /// <returns></returns>
        public static OrderResult NotModified(string message = null) => new() { Status=OrderStatus.NotModified, Message = message };

        /// <summary>
        /// Creates a <see cref="OrderResult"/> with the status <see cref="OrderStatus.NotAllowed"/>.
        /// </summary>
        /// <param name="message">The message to add the the result.</param>
        /// <returns></returns>
        public static OrderResult NotAllowed(string message = null) => new() { Status = OrderStatus.NotAllowed, Message = message };

        /// <summary>
        /// Creates a <see cref="OrderResult"/> with the status <see cref="OrderStatus.NotValid"/>.
        /// </summary>
        /// <param name="message">The message to add the the result.</param>
        /// <returns></returns>
        public static OrderResult NotValid(string message = null) => new() { Status = OrderStatus.NotValid, Message = message };

        public override string ToString()
        {
            return $"{Status}: {Message}";
        }
    }
}
