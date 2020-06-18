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
            db.CreateTable<TableExchangeRateEntity>();

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
                new SettingEntity("DatabaseDataValidUntil", DateTime.MinValue.ToString("s"))
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

        public List<TableExchangeRate> GetTableExchangeRates()
        {
            return db.Table<TableExchangeRateEntity>().Select(rateEntity => new TableExchangeRate()
            {
                CurrencyCode = rateEntity.CurrencyCode,
                CurrencyName = rateEntity.CurrencyName,
                Bid = rateEntity.Bid,
                Ask = rateEntity.Ask
            }).ToList();
        }

        public void UpdateSetting(SettingEntity setting)
        {
            db.Update(setting);
        }

        public void UpdateDisplayedCurrencySetting(SettingDisplayedCurrencyEntity setting)
        {
            db.Update(setting);
        }

        public void SaveTableExchangeRates(List<TableExchangeRate> tableExchangeRates)
        {
            List<TableExchangeRateEntity> tableExchangeRateEntities = tableExchangeRates.Select(
                rate => new TableExchangeRateEntity(rate)).ToList();

            db.BeginTransaction();
            db.DeleteAll<TableExchangeRateEntity>();
            db.InsertAll(tableExchangeRateEntities, runInTransaction: false);
            db.Commit();
        }

        public void Dispose()
        {
            db = null;
        }
    }
}