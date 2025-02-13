using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FYB
{
    class JSONReader
    {
        public string Token { get; set; }

        public async Task Read()
        {
            using (StreamReader reader = new StreamReader("properties.json"))
            {
                string json = await reader.ReadToEndAsync();
                JSONLayout properties = JsonConvert.DeserializeObject<JSONLayout>(json);

                Token = properties.Token;
            }
        }
    }

    internal sealed class JSONLayout
    {
        public string Token { get; set; }
    }
}
