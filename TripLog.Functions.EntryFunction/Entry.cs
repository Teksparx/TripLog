using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsAzure.Storage.Table;

namespace TripLog.Functions.EntryFunction
{
    public class Entry  
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
        // Required for Table Storage entities
        public string PartitionKey => "ENTRY";
        public string RowKey => Id;
    }

    public class EntryTableEntity : TableEntity
    {
        public string Title { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        public string Notes { get; set; }
    }
    public static class Mappings
    {
        public static EntryTableEntity ToTableEntity(this Entry entry)
        {
            return new EntryTableEntity()
            {
                PartitionKey = "ENTRY",
                RowKey = entry.Id,
                Title = entry.Title,
                Longitude = entry.Longitude,
                Latitude = entry.Latitude,
                Date = entry.Date,
                Notes = entry.Notes,
                Rating = entry.Rating
            };
        }

        public static Entry ToEntry(this EntryTableEntity entry)
        {
            return new Entry()
            {
                Id = entry.RowKey,
                Title = entry.Title,
                Longitude = entry.Longitude,
                Latitude = entry.Latitude,
                Date = entry.Date,
                Notes = entry.Notes,
                Rating = entry.Rating
            };
        }
    }
}
