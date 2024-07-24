using System.Security.Cryptography;
using System.Text;
using Shared.Login;

namespace ServerHost.Model
{
    public class CryptUtilities
    {
        #region Algoritmi usati per criptare/decriptare

        /// <summary>
        /// Metodo che cripta un testo utilizzando AES, ai byte
        /// criptati aggiunge all'inizio i 32 byte della key ed in fondo 
        /// i 16 byte dell IV, infine trasforma l'array di byte totale in una 
        /// stringa in formato Base64
        /// key[32] + messaggioCriptato[n] + IV[16]
        /// </summary>
        /// <param name="testo"></param>
        /// <returns></returns>
        public static string EncryptAXT_AES(string testo)
        {
            string result = "";
            try
            {
                byte[] encrypted;
                using Aes aes = Aes.Create();

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream msEncrypt = new();
                using CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new(csEncrypt))
                {
                    //scrivo il testo nello stream
                    swEncrypt.Write(testo);
                }
                //messaggio criptato
                encrypted = msEncrypt.ToArray();
                //concateno i byte in un array unico in quest'ordine: key[32] + messaggioCriptato[n] + IV[16]
                var sumArray = aes.Key.ToList().Concat(encrypted.ToList()).Concat(aes.IV.ToList()).ToArray();
                //converto in Base64
                result = Convert.ToBase64String(sumArray);
            }
            catch (Exception) { }

            return result;
        }

        /// <summary>
        /// Metodo che utilizza AES per decriptare un testo precedentemente
        /// criptato a cui è stato aggiunto all inizio i 32 byte della key
        /// in fondo i 16 byte dell IV e poi trasformato in una stringa Base64.
        /// key[32] + messaggioCriptato[n] + IV[16]
        /// </summary>
        /// <param name="testoCriptato">testo criptato</param>
        /// <returns></returns>
        public static string DecryptAXT_AES(string testoCriptato)
        {
            //converto la stringa da Base64 in Byte[]
            var sum = Convert.FromBase64String(testoCriptato);

            string testo = "";
            try
            {
                //Crea l'oggetto AES
                using Aes aesAlg = Aes.Create();
                //i primi 32 byte sono la chiave
                aesAlg.Key = sum.Take(32).ToArray();
                //gli ultimi 16 sono l IV
                aesAlg.IV = sum.TakeLast(16).ToArray();
                //i byte centrali sono il messaggio da decriptare
                var crypt = sum.Skip(32).SkipLast(16).ToArray();

                //Crea un decryptor per eseguire la trasformazione del flusso.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                //Creazione dello stream per la decriptazione.
                using MemoryStream msDecrypt = new(crypt);
                using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new(csDecrypt);
                //legge i byte decriptati e li inserisce nella stringa
                testo = srDecrypt.ReadToEnd();
            }
            catch (Exception) { }

            return testo;
        }

        /// <summary>
        /// Metodo statico per criptare una stringa di testo in SHA512
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string Sha512(string text)
        {
            byte[] convertedToBytes = Encoding.UTF8.GetBytes(text);
            byte[] hashBytes = SHA512.HashData(convertedToBytes);
            string hashedResult = Convert.ToBase64String(hashBytes);
            return hashedResult;
        }

        #endregion

        #region Creazione e criptazione password 

        public static PasswordModel CreateNewPassword(string text)
        {
            if (text == "")
            {
                text = CreateRandomPassword();
            }
            var salt = CreateRandomString();
            var cryptedPassword = CryptPassword(text, salt);
            return new PasswordModel { Password = text, CryptedPassword = cryptedPassword, Salt = salt };
        }

        public static string CryptPassword(string password, string salt)
        {
            var text = password + salt;
            return Sha512(text);
        }

        #endregion

        #region Generazione di stringhe e password casuali

        public static string CreateRandomString(int number = 50)
        {
            string mix = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+=][}{<>";
            string salt = "";
            Random rnd1 = new();
            for (int i = 1; i <= number; i++)
            {
                int x = rnd1.Next(mix.Length);
                salt += mix.Substring(x, 1);
            }
            return salt;
        }

        public static string CreateRandomPassword(int number = 10)
        {
            string mix = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%&";
            string salt = "";
            Random rnd1 = new();
            for (int i = 1; i <= number; i++)
            {
                int x = rnd1.Next(mix.Length);
                salt += mix.Substring(x, 1);
            }
            return salt;
        }

        #endregion
    }
}
