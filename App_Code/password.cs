using System;

using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace mypass
{
    //工职校应用各应用平台加密解密字符串方法
    //
    //加密传送-解密     webpwd为解密后的值，Request["webPwd"].ToString()为被解密的值，cdivtc为密钥
                    // mypass.RC4Crypto jm = new mypass.RC4Crypto();
       
                    //webpwd = jm.Decrypt((String)Request["webPwd"].ToString(), "cdivtc");
    //加密传送-加密    webpwd为加密后的值，Request["webPwd"].ToString()为被加密的值，cdivtc为密钥
                    // mypass.RC4Crypto jm = new mypass.RC4Crypto();
                    //webpwd = jm.Encrypt((String)Request["webPwd"].ToString(),"cdivtc");

    //各系统传送示例： //webUserName 为UUM的中用户名，webPwd为使用该方法加密过的字符串，并用UrlEncode转个码，防止特殊字符,loginok为自动登陆标记,tourl为登陆成功后转到的页面

    //学生平台
    // http://www.cdivtc.com:10000/admin/default.aspx?webUserName=zhangming1&webPwd="+HttpUtility.UrlEncode("@@x1ds3rbs==")+"&loginok=auot&jm=des&tourl=admin/default.aspx
    //教务平台
    //http://www.cdivtc.com:10001/defaultuum.aspx?webUserName=zhangming1&webPwd="+HttpUtility.UrlEncode("@@x1ds3rbs==")+"&loginok=auot&jm=des&tourl=default.aspx
    
   


    public class RC4Crypto : CryptoBase
    {
        static public RC4Crypto RC4 = new RC4Crypto();


        public static string jiam()
        {
            Random pwd = new Random();
            //产生一个4位数的固定码
            string pwd1=pwd.Next(1000, 9999).ToString();
            //获取2位的天和2位的小时数
            string day=DateTime.Now.Day.ToString();
            if(day.Length==1)day="0"+day;
            string hour=DateTime.Now.Hour.ToString();
            if(hour.Length==1)day="0"+hour;

            //加密 固定码+(固定码+天+小时)*固定码  注意+都为字符串连接
            string pwd2 = pwd1 + (Convert.ToInt32(pwd1 + day + hour) * Convert.ToInt32(pwd1)).ToString();

            return pwd2;
        }
        public static bool jiem(string username,string sysname,string pwd)
        {
            if (pwd.Length > 10)
            {
                try
                {
                    //获取固定密钥
                    string jiem1 = pwd.Substring(0, 4);
                    //获取当前天和小时
                    string day = DateTime.Now.Day.ToString();
                    if (day.Length == 1) day = "0" + day;
                    string hour1 = DateTime.Now.Hour.ToString();
                    string hour2=(Convert.ToInt32( DateTime.Now.Hour.ToString())-1).ToString();
                    string hour3 = (Convert.ToInt32(DateTime.Now.Hour.ToString()) +1).ToString();
                    if (hour1.Length == 1) hour1 = "0" + hour1;
                    if (hour2.Length == 1) hour2 = "0" + hour2;
                    if (hour3.Length == 1) hour3 = "0" + hour3;
                    //解密
                    string jiem2 = (Convert.ToInt64(pwd.Substring(4, pwd.Length - 4)) / Convert.ToInt64(jiem1)).ToString();
             
                    if (jiem2.Length == 8)
                    {
                        if (jiem1 != jiem2.Substring(0, 4)) return false;
                        if (jiem2.Substring(4, 2) == day && (jiem2.Substring(6, 2) == hour1||jiem2.Substring(6, 2) == hour2||jiem2.Substring(6, 2) == hour3))
                        {
                            //写单点登陆日志
                            //...略
                            //返回验证为真
                            return true;

                        }
                       
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }


        public override Byte[] EncryptEx(Byte[] data, String pass)
        {

            if (data == null || pass == null) return null;
            Byte[] output = new Byte[data.Length];
            Int64 i = 0;
            Int64 j = 0;
            Byte[] mBox = GetKey(Encode.GetBytes(pass), 256);

            // 加密
            for (Int64 offset = 0; offset < data.Length; offset++)
            {
                i = (i + 1) % mBox.Length;
                j = (j + mBox[i]) % mBox.Length;
                Byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;
                Byte a = data[offset];
                //Byte b = mBox[(mBox[i] + mBox[j] % mBox.Length) % mBox.Length];
                // mBox[j] 一定比 mBox.Length 小，不需要在取模
                Byte b = mBox[(mBox[i] + mBox[j]) % mBox.Length];
                output[offset] = (Byte)((Int32)a ^ (Int32)b);
            }

            return output;
        }

        public override Byte[] DecryptEx(Byte[] data, String pass)
        {
            return EncryptEx(data, pass);
        }

        /// <summary>
        /// 打乱密码
        /// </summary>
        /// <param name="pass">密码</param>
        /// <param name="kLen">密码箱长度</param>
        /// <returns>打乱后的密码</returns>
        static private Byte[] GetKey(Byte[] pass, Int32 kLen)
        {
            Byte[] mBox = new Byte[kLen];

            for (Int64 i = 0; i < kLen; i++)
            {
                mBox[i] = (Byte)i;
            }
            Int64 j = 0;
            for (Int64 i = 0; i < kLen; i++)
            {
                j = (j + mBox[i] + pass[i % pass.Length]) % kLen;
                Byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;
            }
            return mBox;
        }
    }
    //[StandardModule]
    internal sealed class EncryptRijndael
    {
        public static byte[] _iv = new byte[] { 0xa3, 0xe4, 0xdf, 0x13, 0x8b, 0xb7, 0x4b, 0xe2, 60, 0x61, 0xe5, 0xbc, 220, 0xef, 0xde, 0x52 };
        public static byte[] _key = new byte[] { 
            0x12, 0xad, 0x9c, 0x42, 0x54, 0xd5, 0x45, 0x40, 0xe1, 0x53, 150, 0xef, 220, 0x29, 7, 70, 
            14, 0x30, 0x5d, 0x6d, 0x4a, 60, 0x12, 0xf7, 0xc1, 0xdf, 7, 0x83, 0xf7, 180, 0x6b, 12
         };

        public class CryptoFactory
        {
            public EncryptRijndael.ICrypto MakeCryptographer(string type)
            {
                EncryptRijndael.ICrypto crypto;
                string str = type.ToLower();
                if (str == "rijndael")
                {
                    crypto = new EncryptRijndael.Rijndael();
                }
                else
                {
                    crypto = new EncryptRijndael.Rijndael();
                }
                return crypto;
            }
        }

        public interface ICrypto
        {
            int BlockSize();
            string Decrypt(string data);
            string Encrypt(string data);
            int KeySize();
        }

        public class Rijndael : EncryptRijndael.ICrypto
        {
            public int BlockSize()
            {
                RijndaelManaged managed = new RijndaelManaged();
                return managed.BlockSize;
            }

            public string Decrypt(string data)
            {
                string str;
                try
                {


                    byte[] buffer = Convert.FromBase64String(data);
                    MemoryStream stream2 = new MemoryStream(buffer, 0, buffer.Length);
                    RijndaelManaged managed = new RijndaelManaged();
                    CryptoStream stream = new CryptoStream(stream2, managed.CreateDecryptor(EncryptRijndael._key, EncryptRijndael._iv), CryptoStreamMode.Read);
                    str = new StreamReader(stream).ReadToEnd();
                }
                catch (Exception exception1)
                {

                    //              ProjectData.SetProjectError(exception1);
                    Exception exception = exception1;
                    throw exception;
                    //            ProjectData.ClearProjectError();
                }
                return str;
            }

            public string Encrypt(string data)
            {
                string str;
                try
                {
                    byte[] bytes = new UTF8Encoding().GetBytes(data);
                    MemoryStream stream2 = new MemoryStream();
                    RijndaelManaged managed = new RijndaelManaged();
                    CryptoStream stream = new CryptoStream(stream2, managed.CreateEncryptor(EncryptRijndael._key, EncryptRijndael._iv), CryptoStreamMode.Write);
                    stream.Write(bytes, 0, bytes.Length);
                    stream.FlushFinalBlock();
                    str = Convert.ToBase64String(stream2.GetBuffer(), 0, (int)stream2.Length);
                }
                catch (Exception exception1)
                {
                    //          ProjectData.SetProjectError(exception1);
                    Exception exception = exception1;
                    throw exception;
                    //        ProjectData.ClearProjectError();
                }
                return str;
            }

            public int KeySize()
            {
                RijndaelManaged managed = new RijndaelManaged();
                return managed.KeySize;
            }
        }
    }

    /// <summary>
    /// 加密类基类
    /// </summary>
    public class CryptoBase
    {

        //随机字符串生成器的主要功能如下： 
        //1、支持自定义字符串长度
        //2、支持自定义是否包含数字
        //3、支持自定义是否包含小写字母
        //4、支持自定义是否包含大写字母
        //5、支持自定义是否包含特殊符号
        //6、支持自定义字符集

        ///<summary>
        ///生成随机字符串
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public string GetRnd(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!$()*,-./:;<=>?[^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }


        /// <summary>
        /// 编码转换器，用于字节码和字符串之间的转换，默认为本机编码
        /// </summary>
        static public Encoding Encode = Encoding.Default;
        public enum EncoderMode { Base64Encoder, HexEncoder };

        /// <summary>
        /// 带编码模式的字符串加密
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="pass">密码</param>
        /// <param name="em">编码模式</param>
        /// <returns>加密后经过编码的字符串</returns>
        public String Encrypt(String data, String pass, CryptoBase.EncoderMode em)
        {
            if (data == null || pass == null) return null;
            if (em == EncoderMode.Base64Encoder)
                return Convert.ToBase64String(EncryptEx(Encode.GetBytes(data), pass));
            else
                return ByteToHex(EncryptEx(Encode.GetBytes(data), pass));
        }

        /// <summary>
        /// 带编码模式的字符串解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="pass">密码</param>
        /// <param name="em">编码模式</param>
        /// <returns>明文</returns>
        public String Decrypt(String data, String pass, CryptoBase.EncoderMode em)
        {
            if (data == null || pass == null) return null;
            if (em == EncoderMode.Base64Encoder)
                return Encode.GetString(DecryptEx(Convert.FromBase64String(data), pass));
            else
                return Encode.GetString(DecryptEx(HexToByte(data), pass));
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="pass">密码</param>
        /// <returns>加密后经过默认编码的字符串</returns>
        public String Encrypt(String data, String pass)
        {
            if (data == null || data.Trim() == "") { return ""; }
            //产生一个随机值做为第一次密码
            try
            {
                if (data.Trim().Length > 0)
                {
                    string sjz = GetRnd(5,true,true,true,false, "");
                    return "@@" + sjz + Encrypt(data, pass + sjz, EncoderMode.Base64Encoder);
                }
                else
                {
                    return data;
                }
            }
            catch(Exception e)
            {
                return "加密失败："+e.Message+data;
            }

        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">要解密的经过编码的数据</param>
        /// <param name="pass">密码</param>
        /// <returns>明文</returns>
        public String Decrypt(String data, String pass)
        {

            String StrCrypt = data.Trim();
            if (data.Length > 7)
            {
                if (data.Substring(0, 2) == "@@")
                {
                    StrCrypt = Decrypt(data.Substring(7, data.Length - 7), pass + data.Substring(2, 5), EncoderMode.Base64Encoder);

                }
            }
            #region 二次解密
            StrCrypt = StrCrypt.Trim();
            if (StrCrypt.Length > 8)
            {
                if (StrCrypt.Substring(StrCrypt.Length - 8, 8) == "~@~$~&~^")
                {
                    //解密

                    EncryptRijndael._iv = new byte[] { 0xa3, 0xe4, 0xdf, 0x13, 0x8b, 0xb7, 0x4b, 0xe2, 60, 0x61, 0xe5, 0xbc, 220, 0xef, 0xde, 0x52 };
                    EncryptRijndael._key = new byte[] { 0x12, 0xad, 0x9c, 0x42, 0x54, 0xd5, 0x45, 0x40, 0xe1, 0x53, 150, 0xef, 220, 0x29, 7, 70, 14, 0x30, 0x5d, 0x6d, 0x4a, 60, 0x12, 0xf7, 0xc1, 0xdf, 7, 0x83, 0xf7, 180, 0x6b, 12 };
                    EncryptRijndael.CryptoFactory factory = new EncryptRijndael.CryptoFactory();
                    return factory.MakeCryptographer("rijndael").Decrypt(StrCrypt.Substring(0, StrCrypt.Length - 8));

                }
            }
            #endregion

            //非本系统加密数据返回原值
            return StrCrypt;

            //return Decrypt(data, pass, EncoderMode.Base64Encoder);
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="pass">密钥</param>
        /// <returns>密文</returns>
        virtual public Byte[] EncryptEx(Byte[] data, String pass) { return null; }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="pass">密码</param>
        /// <returns>明文</returns>
        virtual public Byte[] DecryptEx(Byte[] data, String pass) { return null; }

        static public Byte[] HexToByte(String szHex)
        {
            // 两个十六进制代表一个字节
            Int32 iLen = szHex.Length;
            if (iLen <= 0 || 0 != iLen % 2)
            {
                return null;
            }
            Int32 dwCount = iLen / 2;
            UInt32 tmp1, tmp2;
            Byte[] pbBuffer = new Byte[dwCount];
            for (Int32 i = 0; i < dwCount; i++)
            {
                tmp1 = (UInt32)szHex[i * 2] - (((UInt32)szHex[i * 2] >= (UInt32)'A') ? (UInt32)'A' - 10 : (UInt32)'0');
                if (tmp1 >= 16) return null;
                tmp2 = (UInt32)szHex[i * 2 + 1] - (((UInt32)szHex[i * 2 + 1] >= (UInt32)'A') ? (UInt32)'A' - 10 : (UInt32)'0');
                if (tmp2 >= 16) return null;
                pbBuffer[i] = (Byte)(tmp1 * 16 + tmp2);
            }
            return pbBuffer;
        }

        static public String ByteToHex(Byte[] vByte)
        {
            if (vByte == null || vByte.Length < 1) return null;
            StringBuilder sb = new StringBuilder(vByte.Length * 2);
            for (int i = 0; i < vByte.Length; i++)
            {
                if ((UInt32)vByte[i] < 0) return null;
                UInt32 k = (UInt32)vByte[i] / 16;
                sb.Append((Char)(k + ((k > 9) ? 'A' - 10 : '0')));
                k = (UInt32)vByte[i] % 16;
                sb.Append((Char)(k + ((k > 9) ? 'A' - 10 : '0')));
            }
            return sb.ToString();
        }
    }

    interface ICrypto
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">要加密的数据</param>
        /// <param name="pass">密钥</param>
        /// <returns>密文</returns>
        Byte[] EncryptEx(Byte[] data, String pass);

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">要解密的数据</param>
        /// <param name="pass">密码</param>
        /// <returns>明文</returns>
        Byte[] DecryptEx(Byte[] data, String pass);
    }

}