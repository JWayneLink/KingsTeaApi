using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
//using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Library
{
    public class DESWrapper
    {
        #region README
        /*
            二、DES加密
            DES加密算法爲密碼體制中的對稱密碼體制，又被稱爲美國數據加密標準，是1972年美國IBM公司研製的對稱密碼體制加密算法。 明文按64位進行分組，密鑰長64位，密鑰事實上是56位參與DES運算（第8、16、24、32、40、48、56、64位是校驗位， 使得每個密鑰都有奇數個1）分組後的明文組和56位的密鑰按位替代或交換的方法形成密文組的加密方法。
            DES，全稱Data Encryption Standard，是一種對稱加密算法。由於其安全性比較高（有限時間內,沒有一種加密方法可以說是100%安全）,很可能是最廣泛的密鑰系統（我們公司也在用，估計你們也有在用....），唯一一種方法可以破解該算法，那就是窮舉法。
        */
        #endregion

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="data">加密數據</param>
        /// <param name="key">8位字符的密鑰字符串</param>
        /// <param name="iv">8位字符的初始化向量字符串</param>
        /// <returns></returns>
        //public string DESEncrypt(string data, string key, string iv)
        //{
        //    byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
        //    byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

        //    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        //    int i = cryptoProvider.KeySize;
        //    MemoryStream ms = new MemoryStream();
        //    CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateEncryptor(byKey, byIV), CryptoStreamMode.Write);

        //    StreamWriter sw = new StreamWriter(cst);
        //    sw.Write(data);
        //    sw.Flush();
        //    cst.FlushFinalBlock();
        //    sw.Flush();
        //    return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        //}

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="data">解密數據</param>
        /// <param name="key">8位字符的密鑰字符串(需要和加密時相同)</param>
        /// <param name="iv">8位字符的初始化向量字符串(需要和加密時相同)</param>
        /// <returns></returns>
        //public string DESDecrypt(string data, string key, string iv)
        //{
        //    byte[] byKey = System.Text.ASCIIEncoding.ASCII.GetBytes(key);
        //    byte[] byIV = System.Text.ASCIIEncoding.ASCII.GetBytes(iv);

        //    byte[] byEnc;
        //    try
        //    {
        //        byEnc = Convert.FromBase64String(data);
        //    }
        //    catch
        //    {
        //        return null;
        //    }

        //    DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
        //    MemoryStream ms = new MemoryStream(byEnc);
        //    CryptoStream cst = new CryptoStream(ms, cryptoProvider.CreateDecryptor(byKey, byIV), CryptoStreamMode.Read);
        //    StreamReader sr = new StreamReader(cst);
        //    return sr.ReadToEnd();
        //}
    }
}
