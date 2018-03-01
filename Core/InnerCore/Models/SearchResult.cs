﻿namespace Arachnee.InnerCore.Models
{
    public class SearchResult
    {
        public string EntryId { get; set; }

        public string Name { get; set; }

        public string Date { get; set; }

        public string ImagePath { get; set; }

        public SearchResultType SearchResultType { get; set; }
    }
}