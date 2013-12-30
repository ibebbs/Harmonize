using System;

namespace Bebbs.Harmonize.With
{
    public static class Base64
    {
        public static string Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error decoding base64 data: " + e.Message);
            }
        }

        public static string Decode(byte[] data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                int charCount = utf8Decode.GetCharCount(data, 0, data.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(data, 0, data.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error decoding base64 data: " + e.Message);
            }
        }
    }
}
