using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBPCurrencyCore.Models;
using SQLite;

namespace NBPCurrencyMobile.Models
{
    public class DatabaseHelper : IDisposable
    {
        SQLiteConnection db;

        public DatabaseHelper()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "NbpCurrencyDb.db3");

            db = new SQLiteConnection(path,
                SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.FullMutex);
        }

        public void Initialize()
        {
            db.CreateTable<SettingDisplayedCurrencyEntity>();
            db.CreateTable<SettingEntity>();

            if (db.Table<SettingEntity>().Count() == 0)
            {
                SeedDefaultSettings();
            }

            if (db.Table<SettingDisplayedCurrencyEntity>().Count() == 0)
            {
                SeedDefaultDisplayedCurrenciesSettings();
            }
        }

        public void SeedDefaultSettings()
        {
            List<SettingEntity> defaultSettings = new List<SettingEntity>
            {
                new SettingEntity("DefaultStartDate", DateTime.Now.AddDays(-30).ToString("s")),
                new SettingEntity("DefaultEndDate", DateTime.Now.ToString("s")),
            };

            db.BeginTransaction();
            db.DeleteAll<SettingEntity>();
            db.InsertAll(defaultSettings, runInTransaction: false);
            db.Commit();
        }

        public async Task SeedDefaultDisplayedCurrenciesSettings()
        {
            ExchangeRatesTable exchangeRatesTable = await App.NbpClient.GetCurrentTable();

            List<SettingDisplayedCurrencyEntity> currencySettings = exchangeRatesTable.ExchangeRates.Select(
                rate => new SettingDisplayedCurrencyEntity(rate.CurrencyCode, rate.CurrencyName, true)).ToList();

            db.BeginTransaction();
            db.DeleteAll<SettingDisplayedCurrencyEntity>();
            db.InsertAll(currencySettings, runInTransaction: false);
            db.Commit();
        }

        public void SeedDisplayedCurrenciesSettings(List<SettingDisplayedCurrencyEntity> currencySettings)
        {
            db.BeginTransaction();
            db.DeleteAll<SettingDisplayedCurrencyEntity>();
            db.InsertAll(currencySettings, runInTransaction: false);
            db.Commit();
        }

        public SettingEntity GetSetting(string settingName)
        {
            return db.Get<SettingEntity>(settingName);
        }

        public List<SettingDisplayedCurrencyEntity> GetDisplayedCurrenciesSettings()
        {
            return db.Table<SettingDisplayedCurrencyEntity>().ToList();
        }

        public void UpdateSetting(SettingEntity setting)
        {
            db.Update(setting);
        }

        public void UpdateDisplayedCurrencySetting(SettingDisplayedCurrencyEntity setting)
        {
            db.Update(setting);
        }

        public void Dispose()
        {
            db = null;
        }
    }
}