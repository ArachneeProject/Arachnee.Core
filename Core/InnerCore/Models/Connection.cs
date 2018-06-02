using System;
using System.Collections.Generic;
using System.Linq;

namespace Arachnee.InnerCore.Models
{
    public class Connection
    {
        public Id ConnectedId { get; set; }

        public ConnectionType Type { get; set; }

        public string Label { get; set; }

        /// <summary>
        /// Returns all available ConnectionType values.
        /// </summary>
        public static ICollection<ConnectionType> AllTypes()
        {
            var types = Enum.GetValues(typeof(ConnectionType)).Cast<ConnectionType>();
            return new HashSet<ConnectionType>(types);
        }
    }
}
