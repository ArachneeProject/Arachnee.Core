using System.Collections.Generic;

namespace Arachnee.InnerCore.Models
{
    public abstract class Entry
    {
        public string Id { get; }

        public string MainImagePath { get; set; }

        public List<Connection> Connections { get; set; } = new List<Connection>();

        protected Entry(string id)
        {
            Id = id;
        }

        /// <summary>
        /// Returns true if the given Entry is null or the default entry, false otherwise.
        /// </summary>
        public static bool IsNullOrDefault(Entry entry)
        {
            return entry == null || entry == DefaultEntry.Instance;
        }

        public override string ToString()
        {
            return Id;
        }
    }
}