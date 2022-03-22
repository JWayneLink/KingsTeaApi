using KTA.Model.Interface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KTA.Model.Library
{
    public class MD5Wrapper : IEncryptService
	{
        #region README
        /*
             一、MD5消息摘要算法
            我想這是大家都常聽過的算法，可能也用的比較多。那麼什麼是MD5算法呢？MD5全稱是message-digest algorithm 5，簡單的說就是單向的加密，也就是說無法根據密文推導出明文。

            MD5主要用途：
            1、對一段信息生成信息摘要，該摘要對該信息具有唯一性,可以作爲數字簽名
            2、用於驗證文件的有效性(是否有丟失或損壞的數據)
            3、對用戶密碼的加密
            4、在哈希函數中計算散列值
            從上邊的主要用途中我們看到，由於算法的某些不可逆特徵，在加密應用上有較好的安全性。通過使用MD5加密算法，我們輸入一個任意長度的字節串，都會生成一個128位的整數。所以根據這一點MD5被廣泛的用作密碼加密。下面我就像大家演示一下怎樣進行密碼加密。
        */

        #endregion

        private byte[] GetComplexCombineArray(string account, string pwd)
		{
			List<byte> arrays = new List<byte>();
			byte[] accountBytes = Encoding.Unicode.GetBytes(account);
			byte[] pwdBytes = pwd == null? new byte[0] : Encoding.Unicode.GetBytes(pwd);
			int maxLength = accountBytes.Length > pwdBytes.Length ? accountBytes.Length : pwdBytes.Length;

            for (int i = 0; i < maxLength; i++)
            {
                if (i < accountBytes.Length)
                {
					arrays.Add(accountBytes[i]);
                }

                if (i < pwdBytes.Length)
                {
					arrays.Add(pwdBytes[i]);
                }
            }

			return arrays.ToArray();
		}

		public string ToMD5Hash(string source)
        {
            using (MD5 md5Hash = new MD5CryptoServiceProvider())
            {
				byte[] bytes = Encoding.Default.GetBytes(source);//將要加密的字符串轉換爲字節數組
				byte[] encryptdata = md5Hash.ComputeHash(bytes);//將字符串加密後也轉換爲字符數組
				return Convert.ToBase64String(encryptdata);//將加密後的字節數組轉換爲加密字符串
			}
        }

		/// <summary>
		/// 創建哈希字符串適用於任何 MD5 哈希函數 （在任何平臺） 上創建 32 個字符的十六進制格式哈希字符串
		/// </summary>
		/// <param name="account"></param>
		/// <param name="pwd"></param>
		/// <returns></returns>
		public string EncryptData(string account, string pwd)
		{
            using (MD5 md5Hash = MD5.Create())
            {
				byte[] input = this.GetComplexCombineArray(account, pwd);
				byte[] computedHash = md5Hash.ComputeHash(input);

				StringBuilder sb = new StringBuilder();
				sb.Clear();

                for (int i = 0; i < computedHash.Length; i++)
                {
					sb.Append(computedHash[i].ToString("x2"));

				}

				string hash = sb.ToString();
				return hash;
            }					
		}

        /// <summary>
        /// 獲取16位md5加密
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Get16MD5One(string source)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

                //轉換成字符串，並取9到25位
                string sBuilder = BitConverter.ToString(data, 4, 8);

                //BitConverter轉換出來的字符串會在每個字符中間產生一個分隔符，需要去除掉
                sBuilder = sBuilder.Replace("-", "");
                return sBuilder.ToString().ToUpper();
            }
        }

        //// <summary>
        /// </summary>
        /// <param name="source">需要加密的明文</param>
        /// <returns>返回32位加密結果，該結果取32位加密結果的第9位到25位</returns>      
        public string Get32MD5Two(string source)
        {
            using (MD5 md5Hash = new MD5CryptoServiceProvider())
            {
                //獲取密文字節數組
                byte[] bytResult = md5Hash.ComputeHash(Encoding.Default.GetBytes(source));

                //轉換成字符串，32位
                string strResult = BitConverter.ToString(bytResult);

                //BitConverter轉換出來的字符串會在每個字符中間產生一個分隔符，需要去除掉
                strResult = strResult.Replace("-", "");
                return strResult.ToUpper();
            }
        }

        //// <summary>
        /// </summary>
        /// <param name="source">需要加密的明文</param>
        /// <returns>返回16位加密結果，該結果取32位加密結果的第9位到25位</returns>
        public string Get16MD5Two(string source)
        {

            using (MD5 md5Hash = new MD5CryptoServiceProvider())
            {
                //獲取密文字節數組
                byte[] bytResult = md5Hash.ComputeHash(Encoding.Default.GetBytes(source));

                //轉換成字符串，並取9到25位
                string strResult = BitConverter.ToString(bytResult, 4, 8);

                //BitConverter轉換出來的字符串會在每個字符中間產生一個分隔符，需要去除掉
                strResult = strResult.Replace("-", "");
                return strResult.ToUpper();
            }
        }

    }
}
