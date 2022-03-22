using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTA.Model.Interface
{
    public interface IEncryptService
    {
        #region README
        /*
            可逆：RSA,AES,DES等
            不可逆：常見的MD5，SHAD等
         */

        #endregion
        string EncryptData(string account, string pwd);
        string ToMD5Hash(string source);
        string Get16MD5One(string source);
        string Get32MD5Two(string source);
        string Get16MD5Two(string source);
    }
}
