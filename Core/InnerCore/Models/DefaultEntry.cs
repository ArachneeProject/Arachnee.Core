namespace Arachnee.InnerCore.Models
{
    public class DefaultEntry : Entry
    {
        public static DefaultEntry Instance = new DefaultEntry();

        private DefaultEntry() : base(string.Empty)
        {
            MainImagePath = string.Empty;
        }
    }
}
