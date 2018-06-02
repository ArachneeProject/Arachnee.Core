using Arachnee.InnerCore.Models;
using NUnit.Framework;
using System;

namespace Arachnee.InnerCore.Tests.Models.Tests
{
    [TestFixture]
    public class IdTests
    {
        private const int IdNumber = 280;

        // 'From' methods

        [Test]
        public void FromMovie_ValidNumber_ValidId()
        {
            var id = Id.FromMovieNumber(IdNumber);

            Assert.AreEqual(IdType.Movie, id.Type);
            Assert.AreEqual(IdNumber, id.Number);
            Assert.AreEqual("Movie-280", id.FullIdentifier);
        }

        [Test]
        public void FromMovie_InvalidNumber_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Id.FromMovieNumber(-1));
        }

        [Test]
        public void FromArtist_ValidNumber_ValidId()
        {
            var id = Id.FromArtistNumber(IdNumber);

            Assert.AreEqual(IdType.Artist, id.Type);
            Assert.AreEqual(IdNumber, id.Number);
            Assert.AreEqual("Artist-280", id.FullIdentifier);
        }

        [Test]
        public void FromArtist_InvalidNumber_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Id.FromArtistNumber(-1));
        }

        [Test]
        public void FromTvSeries_ValidNumber_ValidId()
        {
            var id = Id.FromTvSeriesNumber(IdNumber);

            Assert.AreEqual(IdType.TvSeries, id.Type);
            Assert.AreEqual(IdNumber, id.Number);
            Assert.AreEqual("TvSeries-280", id.FullIdentifier);
        }

        [Test]
        public void FromTvSeries_InvalidNumber_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() => Id.FromTvSeriesNumber(-1));
        }

        // Equals

        [Test]
        public void Equals_SameInstanceTwice_ReturnsTrue()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = id1;

            Assert.IsTrue(id1.Equals(id2));
        }

        [Test]
        public void Equals_TwoInstancesWithSameData_ReturnsTrue()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = Id.FromMovieNumber(IdNumber);

            Assert.IsTrue(id1.Equals(id2));
        }

        [Test]
        public void Equals_TwoInstancesWithDifferentTypeNames_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = Id.FromArtistNumber(IdNumber);

            Assert.IsFalse(id1.Equals(id2));
        }

        [Test]
        public void Equals_TwoInstancesWithDifferentNumbers_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(10);
            var id2 = Id.FromMovieNumber(999);

            Assert.IsFalse(id1.Equals(id2));
        }

        [Test]
        public void Equals_InstanceAndNull_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(IdNumber);

            Assert.IsFalse(id1.Equals(null));
        }

        // == operator

        [Test]
        public void DoubleEqualOperator_SameInstanceTwice_ReturnsTrue()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = id1;

            Assert.IsTrue(id1 == id2);
        }

        [Test]
        public void DoubleEqualOperator_TwoInstancesWithSameData_ReturnsTrue()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = Id.FromMovieNumber(IdNumber);

            Assert.IsTrue(id1 == id2);
        }

        [Test]
        public void DoubleEqualOperator_TwoInstancesWithDifferentTypeNames_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = Id.FromArtistNumber(IdNumber);

            Assert.IsFalse(id1 == id2);
        }

        [Test]
        public void DoubleEqualOperator_TwoInstancesWithDifferentNumbers_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(10);
            var id2 = Id.FromMovieNumber(999);

            Assert.IsFalse(id1 == id2);
        }

        [Test]
        public void DoubleEqualOperator_InstanceAndNull_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(IdNumber);

            Assert.IsFalse(id1 == null);
        }

        [Test]
        public void DoubleEqualOperator_NullTwice_ReturnsTrue()
        {
            Id id1 = null;
            Id id2 = null;

            Assert.IsTrue(id1 == id2);
        }

        // != operator

        [Test]
        public void NotEqualOperator_SameInstanceTwice_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = id1;

            Assert.IsFalse(id1 != id2);
        }

        [Test]
        public void NotEqualOperator_TwoInstancesWithSameData_ReturnsFalse()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = Id.FromMovieNumber(IdNumber);

            Assert.IsFalse(id1 != id2);
        }

        [Test]
        public void NotEqualOperator_TwoInstancesWithDifferentTypeNames_ReturnsTrue()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = Id.FromArtistNumber(IdNumber);

            Assert.IsTrue(id1 != id2);
        }

        [Test]
        public void NotEqualOperator_TwoInstancesWithDifferentNumbers_ReturnsTrue()
        {
            var id1 = Id.FromMovieNumber(10);
            var id2 = Id.FromMovieNumber(999);

            Assert.IsTrue(id1 != id2);
        }

        [Test]
        public void NotEqualOperator_InstanceAndNull_ReturnsTrue()
        {
            var id1 = Id.FromMovieNumber(IdNumber);

            Assert.IsTrue(id1 != null);
        }

        [Test]
        public void NotEqualOperator_NullTwice_ReturnsFalse()
        {
            Id id1 = null;
            Id id2 = null;

            Assert.IsFalse(id1 != id2);
        }

        // GetHashCode

        [Test]
        public void GetHashCode_TwoInstancesWithSameData_SameHashCode()
        {
            var id1 = Id.FromMovieNumber(IdNumber);
            var id2 = Id.FromMovieNumber(IdNumber);

            Assert.AreEqual(id1.GetHashCode(), id2.GetHashCode());
        }

        // static fields
        [Test]
        public void Statics_CorrectValues()
        {
            Assert.AreEqual(IdType.Default, Id.Default.Type);
            Assert.AreEqual(0, Id.Default.Number);
            Assert.AreEqual("Default-0", Id.Default.FullIdentifier);

            Assert.AreEqual('-', Id.Separator);
        }

        // Parse
        [Test]
        public void TryParse_CorrectString_ReturnsTrueAndValidId()
        {
            var identifier = "Movie-280";

            Id id;
            var success = Id.TryParse(identifier, out id);

            Assert.IsTrue(success);
            Assert.AreNotEqual(Id.Default, id);
            Assert.AreEqual(Id.FromMovieNumber(280), id);
        }

        [Test]
        public void TryParse_DefaultIdString_ReturnsTrueAndDefaultId()
        {
            Id id;
            var success = Id.TryParse(Id.Default.ToString(), out id);

            Assert.IsTrue(success);
            Assert.AreEqual(Id.Default, id);
        }

        [Test]
        public void TryParse_NoSeparator_ReturnsFalseAndDefaultId()
        {
            Id id;
            var success = Id.TryParse("Movie280", out id);

            Assert.IsFalse(success);
            Assert.AreEqual(Id.Default, id);
        }

        [Test]
        public void TryParse_NoTypeName_ReturnsFalseAndDefaultId()
        {
            Id id;
            var success = Id.TryParse("-280", out id);

            Assert.IsFalse(success);
            Assert.AreEqual(Id.Default, id);
        }

        [Test]
        public void TryParse_NoNumber_ReturnsFalseAndDefaultId()
        {
            Id id;
            var success = Id.TryParse("Movie-", out id);

            Assert.IsFalse(success);
            Assert.AreEqual(Id.Default, id);
        }

        [Test]
        public void TryParse_EmptyString_ReturnsFalseAndDefaultId()
        {
            Id id;
            var success = Id.TryParse(string.Empty, out id);

            Assert.IsFalse(success);
            Assert.AreEqual(Id.Default, id);
        }

        [Test]
        public void TryParse_Null_ReturnsFalseAndDefaultId()
        {
            Id id;
            var success = Id.TryParse(null, out id);

            Assert.IsFalse(success);
            Assert.AreEqual(Id.Default, id);
        }
    }
}