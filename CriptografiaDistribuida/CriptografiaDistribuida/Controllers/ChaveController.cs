using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CriptografiaDistribuida.Models;

namespace CriptografiaDistribuida.Controllers
{
    public class ChaveController : Controller
    {

        private static Random random = new Random();

        [Route]
        public string GeraChave()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 5)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        public void AddChaveBanco(string chave)
        {

            Chave x = new Chave();
            x.ChaveRandom = chave;    
            ChaveModel.Add(x);
        }

        [HttpPost]
        public string GetChaveBanco(string chave)
        {
           string retorno = "";

           var chaves = ChaveModel.GetChave().Find(x => x.ChaveRandom.Equals(chave)).ToArray();

            if (chaves.Length == 0)
                retorno = "false";
            else if (chaves.Length > 1)
                retorno = "duplicada";
            else
                foreach (var iten in chaves)
                {
                    if (iten.ChaveRandom == chave)
                        retorno = "true";
                    else
                        retorno = "false";
                }
            return retorno;
        }

    }
}