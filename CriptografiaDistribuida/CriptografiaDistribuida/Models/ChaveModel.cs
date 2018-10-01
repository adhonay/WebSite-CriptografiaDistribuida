using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CriptografiaDistribuida.Models
{
    public class ChaveModel
    {

        public static void Add(Chave x)
        {
            var db = Db.Connect();
            var col = db.GetCollection<Chave>("chave");
            col.Insert(x);
        }
        public static LiteCollection<Chave> GetChave()
        {
            var db = Db.Connect();
            var col = db.GetCollection<Chave>("chave");
            return col;
        }
        public static dynamic GetAll()
        {
            var db = Db.Connect();
            var col = db.GetCollection<Chave>("chave");
            return col.FindAll();
        }

        public static void Update(Chave p)
        {
            var db = Db.Connect();
            var col = db.GetCollection<Chave>("chave");
            col.Update(p);
        }
    
    }

    public class Chave
    {
        public int Id { get; set; }
        public string ChaveRandom { get; set; }
    }
}