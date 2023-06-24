﻿using GHPT.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GHPT.Utils
{
    public static  class ClientUtil
    {
        public static async Task<ResponsePayload> Ask(string prompt, double temperature=0.7)
        {
            var url = "https://api.openai.com/v1/chat/completions";

            AskPayload payload = new AskPayload(
                ConfigUtil.GptModel,
                prompt,
                temperature);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {ConfigUtil.GptToken}");

            var response = await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<ResponsePayload>(result);
        }
    }
}