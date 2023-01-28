using System;
using SQLite;

namespace XamarinPrismTemplateForMac.DbModels
{
	public class RSSNewsDbModel
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Date { get; set; }

        public string Link { get; set; }
    }
}

