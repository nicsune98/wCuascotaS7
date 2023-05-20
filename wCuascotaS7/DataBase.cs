using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace wCuascotaS7
{
    public interface DataBase
    {
        SQLiteAsyncConnection GetConnection();//Defino el método de conexión
    }
}
