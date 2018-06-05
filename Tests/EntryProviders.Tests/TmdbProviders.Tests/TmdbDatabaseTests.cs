using Arachnee.InnerCore.Models;
using NUnit.Framework;
using System;
using System.IO;

namespace Arachnee.TmdbProviders.Tests
{
    public class TmdbDatabaseTests
    {
        protected string GetResourceFolder()
        {
            var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameof(Arachnee));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            return folder;
        }

        protected void AssertMovie(Movie movie)
        {
            Assert.IsNotNull(movie);

            Assert.AreEqual("Movie-218", movie.Id);
            Assert.AreEqual("The Terminator", movie.Title);

            Assert.AreEqual("/6yFoLNQgFdVbA8TZMdfgVpszOla.jpg", movie.BackdropPath);
            Assert.AreEqual(6400000, movie.Budget);
            Assert.AreEqual(3, movie.Tags.Count);
            Assert.AreEqual("http://www.mgm.com/#/our-titles/1970/The-Terminator/", movie.Homepage);
            Assert.AreEqual("tt0088247", movie.ImdbId);
            Assert.AreEqual("en", movie.OriginalLanguage);
            Assert.AreEqual("The Terminator", movie.OriginalTitle);
            Assert.AreEqual("In the post-apocalyptic future, reigning tyrannical supercomputers teleport " +
                            "a cyborg assassin known as the \"Terminator\" back to 1984 to kill Sarah Connor, " +
                            "whose unborn son is destined to lead insurgents against 21st century mechanical hegemony. " +
                            "Meanwhile, the human-resistance movement dispatches a lone warrior to safeguard Sarah. " +
                            "Can he stop the virtually indestructible killing machine?", movie.Overview);
            Assert.IsTrue(movie.Popularity > 0);
            Assert.AreEqual("/q8ffBuxQlYOHrvPniLgCbmKK4Lv.jpg", movie.MainImagePath);
            Assert.AreEqual("1984-10-26", movie.ReleaseDate);
            Assert.AreEqual(78371200, movie.Revenue);
            Assert.AreEqual(108, movie.Runtime);
            Assert.AreEqual("Released", movie.Status);
            Assert.AreEqual("Your future is in his hands.", movie.Tagline);
            Assert.IsTrue(movie.VoteAverage > 0);
            Assert.IsTrue(movie.VoteCount > 0);
        }

        protected void AssertArtist(Artist artist)
        {
            Assert.IsNotNull(artist);

            Assert.AreEqual("Artist-1100", artist.Id);
            Assert.AreEqual("Arnold Schwarzenegger", artist.Name);

            Assert.IsTrue(artist.Biography.StartsWith(
                "Arnold Alois Schwarzenegger (born July 30, 1947) is an Austrian-American former professional bodybuilder, " +
                "actor, model, businessman and politician who served as the 38th Governor of California (2003–2011)."));
            Assert.AreEqual("1947-07-30", artist.Birthday);
            Assert.IsNull(artist.Deathday);
            Assert.AreEqual("nm0000216", artist.ImdbId);
            Assert.IsTrue(artist.NickNames.Count > 3);
            Assert.IsTrue(artist.Popularity > 0);
            Assert.IsFalse(string.IsNullOrEmpty(artist.MainImagePath));
        }

        protected void AssertTvSeries(TvSeries tv)
        {
            Assert.IsNotNull(tv);

            Assert.AreEqual("TvSeries-1668", tv.Id);
            Assert.AreEqual("Friends", tv.Name);

            Assert.AreEqual("/efiX8iir6GEBWCD0uCFIi5NAyYA.jpg", tv.BackdropPath);
            Assert.IsNotNull(tv.EpisodeRunTime);
            Assert.AreEqual(1, tv.EpisodeRunTime.Count);
            Assert.AreEqual(22, tv.EpisodeRunTime[0].TotalMinutes);
            Assert.AreEqual(new DateTime(1994, 09, 22), tv.FirstAirDate);
            Assert.AreEqual(1, tv.Genres.Count);
            Assert.AreEqual(false, tv.InProduction);
            Assert.AreEqual(new DateTime(2004, 05, 06), tv.LastAirDate?.Date);

            Assert.AreEqual(236, tv.NumberOfEpisodes);
            Assert.AreEqual(10, tv.NumberOfSeasons);

            Assert.AreEqual("Friends", tv.OriginalName);
            Assert.AreEqual("The misadventures of a group of friends as they navigate the pitfalls of work, life and love in Manhattan.", tv.Overview);
            Assert.IsTrue(tv.Popularity > 50);

            Assert.AreEqual("/7buCWBTpiPrCF5Lt023dSC60rgS.jpg", tv.MainImagePath);
            
            Assert.IsTrue(tv.VoteAverage > 0);
            Assert.IsTrue(tv.VoteCount > 0);
        }
    }
}