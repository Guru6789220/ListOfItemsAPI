using System.Security.Cryptography;
using System.Text;

namespace ListOfItems
{
    public class AesGcmEncryption1
    {
        //public static void Main()
        //{
        //    var plaintextBytes = new byte[8192];
        //    RandomNumberGenerator.Fill(plaintextBytes);
        //    var plaintext = Convert.ToBase64String(plaintextBytes);

        //    var key = new byte[32];
        //    RandomNumberGenerator.Fill(key);

        //    var (ciphertext, nonce, tag) = EncryptWithNet(plaintext, key);
        //    // var (ciphertext, nonce, tag) = EncryptWithBouncyCastle(plaintext, key);
        //    var decryptedPlaintext = DecryptWithNet(ciphertext, nonce, tag, key);
        //    // var decryptedPlaintext = DecryptWithBouncyCastle(ciphertext, nonce, tag, key);

        //    if (decryptedPlaintext.Equals(plaintext)) Console.WriteLine("Decryption succesful!");
        //    else Console.WriteLine("Error!");
        //}


        public static (byte[] ciphertext, byte[] nonce, byte[] tag) EncryptWithNet(string plaintext, string Keys)
        {
            byte[] key = GetAESKey(Keys);
            using (var aes = new AesGcm(key))
            {
                var nonce = new byte[AesGcm.NonceByteSizes.MaxSize];
                RandomNumberGenerator.Fill(nonce);

                var tag = new byte[AesGcm.TagByteSizes.MaxSize];

                var plaintextBytes = Encoding.UTF8.GetBytes(plaintext);
                var ciphertext = new byte[plaintextBytes.Length];

                aes.Encrypt(nonce, plaintextBytes, ciphertext, tag);

                return (ciphertext, nonce, tag);
            }
        }

        public static string DecryptWithNet(byte[] ciphertext, byte[] nonce, byte[] tag, string keys)
        {
            byte[] key= GetAESKey(keys);
            using (var aes = new AesGcm(key))
            {
                var plaintextBytes = new byte[ciphertext.Length];

                aes.Decrypt(nonce, ciphertext, tag, plaintextBytes);

                return Encoding.UTF8.GetString(plaintextBytes);
            }
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

        
    }
}
