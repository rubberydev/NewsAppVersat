﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using XamarinPrismTemplateForMac.DbModels;

namespace XamarinPrismTemplateForMac.Services
{
	public class DbService : IDbService
	{
        bool initialized;
        readonly SQLiteAsyncConnection database;

        public DbService()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "NewsVersatChallenge.db3");
            database = new SQLiteAsyncConnection(dbPath);
            Initialize();
        }

        private void Initialize()
        {
            try
            {
                if (!initialized) database.CreateTableAsync<RSSNewsDbModel>().Wait(); initialized = true;
            }
            catch (Exception)
            {
                initialized = false;
            }
        }

        #region News methods
        public Task<List<RSSNewsDbModel>> GetNewsAsync() => database.Table<RSSNewsDbModel>().ToListAsync();


        public Task<int> InsertNewsAsync(RSSNewsDbModel item) => database.InsertAsync(item);


        public Task<int> UpdateNewsAsync(RSSNewsDbModel item) => database.UpdateAsync(item);


        public Task<int> DeleteNewsAsync(RSSNewsDbModel item) => database.DeleteAsync(item);

       
        #endregion
    }
}

