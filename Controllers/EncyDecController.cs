using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace ListOfItems.Controllers
{
    [Route("api/EncyDec")]
    [ApiController]
    public class EncyDecController : ControllerBase
    {
        [HttpGet]
        public string get()
        {
            string plaintext = "[{\"vendorCode\":\"HPOIL001\",\"accountId\":\"1028955\",\"txnDate\":\"2023-03-13T13:00:47.074\",\"tr\":\"MS2110402BVL59349040323101219\",\"am\":\"554.00\",\"txnId\":\"sgf44577\",\"rrn\":\"307200067167\",\"payerAddr\":\"lifetest@cnrb\",\"txnNote\":\"PaytoHPOIL\",\"pa\":\"hpoil.upibillpay@cnrb\",\"payerName\":\"ATHERJAHAN\",\"result\":\"SUCCESS\",\"respCode\":\"0\"},{\"vendorCode\":\"HPOIL001\",\"accountId\":\"1028956\",\"txnDate\":\"2023-03-09T17:04:21.432\",\"tr\":\"MS2110402BVL5201040323165917\",\"am\":\"488.00\",\"txnId\":\"4554dsg\",\"rrn\":\"306800067016\",\"payerAddr\":\"tmsec@cnrb\",\"txnNote\":\"PaytoHPOIL\",\"pa\":\"hpoil.upibillpay@cnrb\",\"payerName\":\"PNSHARADA\",\"result\":\"SUCCESS\",\"respCode\":\"0\"}]";
            //string text=EncyDecy.Encrypt(plaintext, "~Me$C0M^=");

            //string encrypted = "PnpSp+iljnjRkeM4dQMNMxAXvawZatD8psUnD2sfui/uvtEKHGkQo/QPH5QG6/3LuqYsXZ2tJTCU\r\negcbK4I6ZVWSN9iYnjokb6bWT87RAq7DtRgs4hXV6M4lr6Ba7hW3/a4QFQY2tHpt0zNd48aBEwiatUEQKj7Zh\r\n9fcSrsbSlQ/FT6/m06wLSOrHnHmDjl5gvnoFAv/t/wkv0wGTF/L6u3DPxV1VDNvzjVDDg2EwtBlzBlJKDg/R7Y\r\nC2piDGwjmBFPtkES+tm2F8XqZHLETwAk3JNH3vnQIPTQ6sR2ZhKizudAjdLHQNnaLUbBEBFlk2+3Fdxu6Yz Pqxhu/MUrHkCrGXxwNmjB495WRI12cDMNXzaUUmhSaPykcBVhLQjXGhF9W1Qsd+MfdZGxWYCubQV8La\r\nSzfbCMtFXHwfqOi10mP/7NsYBpgMcybT08YjIU8ypl4AE41UaE1HjVEyL7Mb12vdyIGdOk33TaoUvpy268Xbf\r\numrUQn/LBHCPS2p9hsPTGu4pQAj8UldYrIO+1PKo+SHJ5Jc5LxN5KCcONRDxgfc3uyZs7ZNrlZIIXr6fEI+ekXa\r\nw6WISefapRgPaagoLskFnXP3+lWvmCqfsSoR7Ng7CR0wYE1a79NpgdQlkcrZeatdE+uCnhGqQNEMy+s2Ge4H\r\ndcs9t+tn6HuO2iVr73mIQJ19gry3pFOq+pcgn+9Zkeq/6WFhg5O0C75WNKn1rdM5lRwh0jD9UOAbIRt9z3UN\r\n97kQbPlqNiFr828J988";
            //string dec = EncyDecy.Decrypt(encrypted, "~Me$C0M^=");

            //return text;

            //const string orginal = "Text to encrypt";
            //var aesGCM = new AesGCMEncryption();

            //var gcmkey = aesGCM.GenerateRandomNumber(16);
            //var none=aesGCM.GenerateRandomNumber(12);
            //try
            //{
            //    (byte[] ciphertext, byte[] tag)result  = aesGCM.Encrypt(Encoding.UTF8.GetBytes(orginal), gcmkey, none, Encoding.UTF8.GetBytes("some metadata"));
            //}
            //catch (Exception e)
            //{

            //}

            //const string original = "Text to encrypt";
            //var aes = new AesEncryption();
            //var key = aes.GenerateRandomNumber(16);
            //var iv = aes.GenerateRandomNumber(16);


            //var encrypted = aes.Encrypt(Encoding.UTF8.GetBytes(original), key, iv);
            //string encriptedtext=Encoding.UTF8.GetString(encrypted);

            //var decrypted = aes.Decrypt(encrypted, key, iv);

            //var decryptedMessage = Encoding.UTF8.GetString(decrypted);


            var (ciphertext, nonce, tag) = AesGcmEncryption1.EncryptWithNet(plaintext, "~Me$C0M^=");
            string ctext=Encoding.UTF8.GetString(ciphertext);
            var decryptedPlaintext = AesGcmEncryption1.DecryptWithNet(ciphertext, nonce, tag, "~Me$C0M^=");

            return "";
        }
    }
}
