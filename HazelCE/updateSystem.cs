using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace HazelCE
{
    public class UpdateSystem
    {
        public static async Task<int> CheckForUpdate()
        {
            GeneralInfo info = new GeneralInfo();
            string URL = "https://raw.githubusercontent.com/tacozyt/hazelce/main/HazelCE/version.json";
            using HttpClient client = new()
            {
                BaseAddress = new Uri(URL)
            };
            versionJson WebRequest = await client.GetFromJsonAsync<versionJson>(URL);
            string JsonFile = "version.json";
            using FileStream openStream = File.OpenRead(JsonFile);
            versionJson DeserializedJson = await JsonSerializer.DeserializeAsync<versionJson>(openStream);
            if(WebRequest?.updateStream == DeserializedJson?.updateStream){
                if(WebRequest?.ver != DeserializedJson?.ver){
                    if(WebRequest?.build > DeserializedJson?.build){
                        Console.WriteLine($"There is a new HazelCE update available!\nCURRENT >> {DeserializedJson?.updateStream} {DeserializedJson?.ver} (Build {DeserializedJson?.build})\nAVAILABLE >> {WebRequest?.updateStream} {WebRequest?.ver} (Build {WebRequest?.build})\nDownload from https://github.com/tacozyt/hazelce/releases\n");
                    }
                }
            }
            return await Task.Run(() => { return 1; });
        }
    }
}