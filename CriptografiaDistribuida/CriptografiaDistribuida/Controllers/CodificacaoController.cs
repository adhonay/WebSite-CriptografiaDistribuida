using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace CriptografiaDistribuida.Controllers
{
    public class CodificacaoController : Controller
    {
        [Route]
        public string Criptografar(string palavra, string chave)
        {
            string EncryptionKey = chave;
            try
            {
                byte[] clearBytes = Encoding.Unicode.GetBytes(palavra);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        palavra = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                palavra = "";
            }

            return palavra;
        }

        //Apos gerar uma chave aleatoria pelo metodo basico, o Rfc2898DeriveBytes atraves dessa chave gera um uma outra chave usando gerador de numero pseudo aleatorio
        //por uma tabela hash, com essa chave gerada passandoa como um dos parametros e tambem a mensagem em questão (digitada pelo usuario) utiliza o CryptoStream 
        //para criptografia/descriptografia a mensgem atraves do padrao da chave e por fim converte essa mensagem criptografada em base64 e retorna para o usuario final.


        [HttpPost]
        public string Descriptografar(string palavra, string chave)
        {
            string EncryptionKey = chave;
            palavra = palavra.Replace(" ", "+");
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(palavra);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {

                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.Close();
                            }
                            palavra = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                char[] arrChar = palavra.ToCharArray();
                Array.Sort(arrChar);
                palavra = new String(arrChar);
            }
            return palavra;
        }

    }
}