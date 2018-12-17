
using System;

namespace popMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            DAO dao = new DAO();
            String obj = "{teste: 'dados'}";//teste
            dao.connect(obj);
        }
    }
}
