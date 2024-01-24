using Core.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Service.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DriverService
    {
        private readonly IMongoCollection<Driver> _driverCollection;

        public DriverService(IOptions<DriversDBSettings> settings)
        {
            var Client = new MongoClient(settings.Value.ConnectionString);  
            var Database = Client.GetDatabase(settings.Value.DatabaseName);
            _driverCollection = Database.GetCollection<Driver>(settings.Value.CollectionName);
        }

        public async Task<List<Driver>> Get() => await _driverCollection.Find( _=> true).ToListAsync();
        public async Task<Driver> Get(string Id) => await _driverCollection.Find(d => d.Id == Id).FirstOrDefaultAsync();

        public async Task Create(Driver driver) => await _driverCollection.InsertOneAsync(driver);
        public async Task Update(Driver driver) => await _driverCollection.ReplaceOneAsync(d => d.Id == driver.Id ,driver);
        public async Task Delete(string id) => await _driverCollection.DeleteOneAsync(d => d.Id == id);  


    }
}
