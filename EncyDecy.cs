using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.Security.Cryptography;
using System.Text;
namespace ListOfItems
{
    public class EncyDecy
    {



        byte[] iv;  //16 for AES

        public static string Encrypt(string plaintext, string keyy)
        {
            byte[] iv = GenerateRandomBytes(16);
            byte[] Key = GetAESKey(keyy);
            using (Aes aesAlg = Aes.Create())
            {

                aesAlg.Key = Key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plaintext);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public static string Decrypt(string encryptedText, string keyy)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);

            byte[] iv = GenerateRandomBytes(16);
            byte[] Key = GetAESKey(keyy);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = iv;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        public static byte[] GenerateRandomBytes(int length)
        {
            byte[] bytes = new byte[length];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }
            return bytes;
        }

        public static byte[] GetAESKey(string skey)
        {
            try
            {
                byte[] kksec = Encoding.UTF8.GetBytes(skey);
                using (SHA512 sha = SHA512.Create())
                {
                    kksec = sha.ComputeHash(kksec);
                }
                // Use only the first 16 bytes for AES key (128 bits)
                Array.Resize(ref kksec, 16);
                return kksec;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e}");
                return null;
            }
        }


        //public static string Encrypt(string plaintext, string keyy)
        //{
        //    byte[] key = GetAESKey(keyy);
        //    using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
        //    {
        //        aesAlg.Key = key;
        //        aesAlg.GenerateIV(); // Generate a random IV for AES-GCM

        //        // Use AES-GCM mode
        //        aesAlg.Mode = CipherMode.GCM;
        //        aesAlg.Padding = PaddingMode.None;

        //        // Encryptor with authentication
        //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            // Write IV to the beginning of the encrypted stream
        //            msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //            {
        //                swEncrypt.Write(plaintext);
        //            }

        //            // Append authentication tag to the encrypted data
        //            byte[] tag = aesAlg.Tag;
        //            msEncrypt.Write(tag, 0, tag.Length);

        //            return Convert.ToBase64String(msEncrypt.ToArray());
        //        }
        //    }
        //}

        //public static string Decrypt(string encryptedText, string keyy)
        //{
        //    byte[] key = GetAESKey(keyy);
        //    byte[] cipherText = Convert.FromBase64String(encryptedText);

        //    using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
        //    {
        //        aesAlg.Key = key;

        //        // Extract IV from the beginning of the ciphertext
        //        byte[] iv = new byte[16];
        //        Array.Copy(cipherText, 0, iv, 0, 16);

        //        // Use AES-GCM mode
        //        aesAlg.IV = iv;
        //        aesAlg.Mode = CipherMode.GCM;
        //        aesAlg.Padding = PaddingMode.None;

        //        // Extract authentication tag from the end of the ciphertext
        //        byte[] tag = new byte[16];
        //        Array.Copy(cipherText, cipherText.Length - 16, tag, 0, 16);
        //        aesAlg.Tag = tag;

        //        // Decryptor with authentication
        //        ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        //        using (MemoryStream msDecrypt = new MemoryStream(cipherText, 16, cipherText.Length - 16 - 16)) // Exclude IV and Tag
        //        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //        {
        //            return srDecrypt.ReadToEnd();
        //        }
        //    }
        //}

        //public static byte[] GetAESKey(string skey)
        //{
        //    byte[] kksec = Encoding.UTF8.GetBytes(skey);
        //    using (SHA512 sha = SHA512.Create())
        //    {
        //        kksec = sha.ComputeHash(kksec);
        //    }
        //    // Use only the first 32 bytes for AES key (256 bits)
        //    Array.Resize(ref kksec, 32); // Adjust size for AES-256
        //    return kksec;
        //}
    }
}
