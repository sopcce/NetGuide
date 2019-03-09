
##  生成machineKey密

### [参考资料](http://support.microsoft.com/kb/312906?wa=wsignin1.0)


ASP.NET 配置概述

```charp
using System;
using System.Text;
using System.Security.Cryptography;

namespace Crypto
{
    public class KeyCreator
    {
        /// <summary>
        /// 生成machineKey密钥
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>
        /// 参数：
        /// 第一个参数是用于创建 decryptionKey 属性的字节数。
        /// 第二个参数是用于创建 validationKey 属性的字节数。
        /// 注意：所创建的十六进制字符串的大小是从命令行传入值的大小的两倍。例如，如果您为密钥指定 24 字节，则转换后相应的字符串长度为 48 字节。
        /// decryptionKey 的有效值为 8 或 24。此属性将为数据加密标准 (DES) 创建一个 16 字节密钥，或者为三重 DES 创建一个 48 字节密钥。
        /// validationKey 的有效值为 20 到 64。此属性将创建长度从 40 到 128 字节的密钥。
        /// 代码的输出是一个完整的<machineKey>元素，您可以将其复制并粘贴到Machine.config文件中。
        /// </remarks>
        public static void Main(String[] args)
        {
            String[] commandLineArgs = System.Environment.GetCommandLineArgs();
            string decryptionKey = CreateKey(System.Convert.ToInt32(commandLineArgs[1]));
            string validationKey = CreateKey(System.Convert.ToInt32(commandLineArgs[2]));

            Console.WriteLine("<machineKey validationKey=\"{0}\" decryptionKey=\"{1}\" validation=\"SHA1\"/>", validationKey, decryptionKey);
        }

        static String CreateKey(int numBytes)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[numBytes];

            rng.GetBytes(buff);
            return BytesToHexString(buff);
        }

        static String BytesToHexString(byte[] bytes)
        {
            StringBuilder hexString = new StringBuilder(64);

            for (int counter = 0; counter < bytes.Length; counter++)
            {
                hexString.Append(String.Format("{0:X2}", bytes[counter]));
            }
            return hexString.ToString();
        }
    }
}
```
### 格式：
```xml
<machineKey validationKey="AutoGenerate|value[,IsolateApps]"
            decryptionKey="AutoGenerate|value[,IsolateApps]"
            validation="SHA1|MD5|3DES"/>
```
### 样例：
```xml
<machineKey validationKey="77A439696CB986680CEE71CB179BBFFA75AA0FE3AB875B278EE8C54536F2B364E1BDAB809BA26D4263C33863D29B4040CD55D9665E8002D26F04A80C701A4067" decryptionKey="79378FA6BD4BE839D0B8C1E94367A820C77F38FA9CD8C7F0" validation="SHA1"/>
```
