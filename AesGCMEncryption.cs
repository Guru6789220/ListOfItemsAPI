using System.Security.Cryptography;

namespace ListOfItems
{
    public class AesGCMEncryption
    {
        public byte[] GenerateRandomNumber(int length)
        {
            using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
            {
                var randomNumber=new byte[length];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        public (byte[], byte[]) Encrypt(byte[] datatoEncrypt, byte[] key, byte[] none, byte[] associateddate)
        {
            byte[] tag = new byte[16];
            byte[] ciphertext = new byte[datatoEncrypt.Length];

            using(AesGcm aesGcm=new AesGcm(key))
            {
                aesGcm.Encrypt(none, datatoEncrypt, ciphertext, tag, associateddate);
            }
            return (ciphertext, tag);
        }

        public byte[] Decrypt(byte[] ciphertext, byte[] key, byte[] none,byte[] tag, byte[] associateddate)
        {
        
            byte[] decryptedtext = new byte[ciphertext.Length];

            using (AesGcm aesGcm = new AesGcm(key))
            {
                aesGcm.Encrypt(none, ciphertext, tag, associateddate);
            }
            return decryptedtext;
        }
    }
}
