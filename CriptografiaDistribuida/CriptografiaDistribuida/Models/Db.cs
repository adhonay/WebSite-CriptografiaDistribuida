using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;

namespace CriptografiaDistribuida.Models
{
    class Db
    {
        public static LiteDatabase Connect()
        {
            //string de conexao 

            //pc adhonay
            //return new LiteDatabase(@"D:\Users\I$ync\source\repos\Criptografia Distribuída\CriptografiaDistribuida\CriptografiaDistribuida\bin\BancoChave.db");

            //azure
            return new LiteDatabase(@"d:\home\site\wwwroot\BancoChave.db");

            //noteboook
            //return new LiteDatabase(@"C:\Users\Adhonay Júnior\source\repos\Criptografia Distribuída\CriptografiaDistribuida\CriptografiaDistribuida\bin\BancoChave.db");



        }


    }
}
