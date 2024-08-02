using System.Text;

namespace MinRenovasjonProxy.Services.Tests
{
    internal class TestData
    {
        public const string fraksjoner_response_sample1 = "fraksjoner_response_sample1";
        public const string tommekalender_response_sample1 = "tommekalender_response_sample1";

        public static string GetResourceAsString(string resourceKey)
        {
            try
            {
                var obj = Properties.Resources.ResourceManager.GetObject(resourceKey);
                return Encoding.UTF8.GetString((byte[])obj);
            }
            catch (InvalidOperationException ioex)
            {
                using (var ms = Properties.Resources.ResourceManager.GetStream(resourceKey))
                using (var streamReader = new StreamReader(ms))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}
