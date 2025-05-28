using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Barberia2.Model;
using SQLite;

namespace Barberia2.Data
{
    public   class CarritoDatabase
    {
        private readonly SQLiteAsyncConnection dataBase;
        public CarritoDatabase(string url)
        {
            dataBase = new SQLiteAsyncConnection(url);
            dataBase.CreateTableAsync<carrito>().Wait();
        }
        public async Task<List<carrito>> GetAll()
        {
            return await dataBase.Table<carrito>().ToListAsync();

        }
        public async Task<carrito> GetOne(int id)
        {
            return await dataBase.Table<carrito>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> InsertProducto(carrito newUbicacion)
        {
            return await dataBase.InsertAsync(newUbicacion);
        }
        public async Task<int> UpdateProducto(carrito newUbicacion)
        {
            return await dataBase.UpdateAsync(newUbicacion);
        }
        public async Task<int> Delete(carrito newUbicacion)
        {
            return await dataBase.DeleteAsync(newUbicacion);
        }
    }
}

