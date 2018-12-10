using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Security.Cryptography;
using System.Security.AccessControl;
using ZetaCompressionLibrary;
using CML.Helper.Core;

namespace CML.Helper.Utilities
{
    public class ProtectData
    {
        const string onriginKey = "hungpt11";
        public static string EncryptData(string data)
        {
            if (!data.IsNull())
            {
                byte[] dataIn;
                byte[] arrKey;

                /* Convert data string to bytes */
                dataIn = UTF8Encoding.UTF8.GetBytes(data);

                /* Hash string */
                MD5CryptoServiceProvider MD5Hash;
                MD5Hash = new MD5CryptoServiceProvider();
                arrKey = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(onriginKey));
                MD5Hash.Clear();

                /* Encrypt */
                TripleDESCryptoServiceProvider DESCrypt;
                DESCrypt = new TripleDESCryptoServiceProvider();
                DESCrypt.Key = arrKey;
                DESCrypt.Mode = CipherMode.ECB;
                DESCrypt.Padding = PaddingMode.PKCS7;

                /* Encryption */
                ICryptoTransform cTransform;
                cTransform = DESCrypt.CreateEncryptor();

                /* Return string encrypted */
                byte[] dataOut;
                dataOut = cTransform.TransformFinalBlock(dataIn, 0, dataIn.Length);
                DESCrypt.Clear();

                if (!dataOut.IsNull())
                {
                    return Convert.ToBase64String(dataOut, 0, dataOut.Length);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        public static string DecryptData(string data)
        {
            if (!data.IsNull())
            {
                byte[] dataIn;
                byte[] arrKey;

                /* Convert data string to bytes */
                dataIn = Convert.FromBase64String(data);

                /* Hash string */
                MD5CryptoServiceProvider MD5Hash;
                MD5Hash = new MD5CryptoServiceProvider();
                arrKey = MD5Hash.ComputeHash(UTF8Encoding.UTF8.GetBytes(onriginKey));
                MD5Hash.Clear();

                /* Encrypt */
                TripleDESCryptoServiceProvider DESCrypt;
                DESCrypt = new TripleDESCryptoServiceProvider();
                DESCrypt.Key = arrKey;
                DESCrypt.Mode = CipherMode.ECB;
                DESCrypt.Padding = PaddingMode.PKCS7;

                /* Encryption */
                ICryptoTransform cTransform;
                cTransform = DESCrypt.CreateDecryptor();

                /* Return string encrypted */
                byte[] dataOut;
                dataOut = cTransform.TransformFinalBlock(dataIn, 0, dataIn.Length);
                DESCrypt.Clear();

                if (!dataOut.IsNull())
                {
                    return UTF8Encoding.UTF8.GetString(dataOut);
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
        public static bool Compression(string data, ref byte[] byteData)
        {
            try
            {
                data = EncryptData(data);
                byteData = CompressionHelper.CompressString(data);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
        public static bool Decompression(byte[] byteData, ref string data)
        {
            try
            {
                data = CompressionHelper.DecompressString(byteData);
                data = DecryptData(data);
                return true;
            }
            catch (Exception ex)
            {
                ErrorLog.Log(ex);
                return false;
            }
        }
    }
}
