using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;
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

        public async Task ClearAllNews()
        {
            var allNewsInLocal = await this.GetNewsAsync();

            if(allNewsInLocal.Count > 0)
            {
                bool isSureUser = await Application.Current.MainPage.DisplayAlert("??", $"esta seguro que desea borrar: {allNewsInLocal.Count} noticia(s) guardada(s) en local", "Si", "No");

                if (!isSureUser) return;

                foreach (var news in allNewsInLocal)
                {
                    try
                    {
                        await this.DeleteNewsAsync(news);
                        await Application.Current.MainPage.DisplayAlert(":)", "noticia borrada exitosamente", "Aceptar!");

                    }
                    catch (Exception ex)
                    {
                        await Application.Current.MainPage.DisplayAlert(":(", $"No se pudo borrar una noticia ERROR: {ex.Message} consulte con el dev", "Aceptar");

                    }

                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(":(", "No tienes noticias guardadas para borrar", "Aceptar");
            }


        }


        #endregion
    }
}


