using System;

namespace Arachnee.InnerCore.Models
{
    public class Id
    {
        public const char Separator = '-';
        
        public static Id Default { get; } = new Id(nameof(DefaultEntry), 0);
        
        public string FullIdentifier { get; }
        public int Number { get; }
        public string TypeName { get; }
        
        private Id(string typeName, int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Number must be positive.", nameof(number));
            }
            
            TypeName = typeName;
            Number = number;
            FullIdentifier = typeName + Separator + number;
        }

        public static bool IsNullOrDefault(Id id)
        {
            return id == null || Equals(id, Default);
        }

        public static Id FromMovieNumber(int movieNumber)
        {
            return new Id(nameof(Movie), movieNumber);
        }

        public static Id FromArtistNumber(int artistNumber)
        {
            return new Id(nameof(Artist), artistNumber);
        }

        public static Id FromTvSeriesNumber(int tvSeriesNumber)
        {
            return new Id(nameof(TvSeries), tvSeriesNumber);
        }

        public static bool TryParse(string identifier, out Id id)
        {
            id = Default;
            if (string.IsNullOrEmpty(identifier))
            {
                return false;
            }

            if (identifier == Default.FullIdentifier)
            {
                return true;
            }

            var split = identifier.Split(Separator);
            if (split.Length != 2)
            {
                return false;
            }

            var typeChunck = split[0];

            if (typeChunck != nameof(Movie) &&
                typeChunck != nameof(Artist) &&
                typeChunck != nameof(TvSeries))
            {
                return false;
            }

            var numberChunk = split[1];
            int number;
            if (!int.TryParse(numberChunk, out number))
            {
                return false;
            }

            id = new Id(typeChunck, number);
            return true;
        }

        public static bool operator ==(Id id1, Id id2)
        {
            if (ReferenceEquals(id1, id2))
            {
                return true;
            }

            return !object.Equals(id1, null) && id1.Equals(id2);
        }

        public static bool operator !=(Id id1, Id id2)
        {
            return !(id1 == id2);
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as Id);
        }

        public bool Equals(Id otherId)
        {
            if (otherId == null)
            {
                return false;
            }

            return FullIdentifier == otherId.FullIdentifier;
        }

        public override int GetHashCode()
        {
            return FullIdentifier.GetHashCode();
        }

        public override string ToString()
        {
            return FullIdentifier;
        }
    }
}