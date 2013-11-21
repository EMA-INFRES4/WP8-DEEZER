using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;

namespace deezer_objects
{
    public class Deezer
    {
        private string DeezerURL = "http://api.deezer.com/editorial/{0}/charts";

        private string DeezerFinderURL = "http://api.deezer.com/search/autocomplete?q={0}";

        public async void getTopTracks(int type, Action<TracksResponse> callback){
            var data = await this.DoRequestAsync(string.Format(DeezerURL, type));
            string result = data.ReadToEnd();
            System.Diagnostics.Debug.WriteLine(result);
            TracksResponse response = JsonConvert.DeserializeObject <TracksResponse>(result);
            callback(response);
        }

        public async void findTracks(string query, Action<TracksResponse> callback)
        {
            query = HttpUtility.UrlEncode(query);
            var data = await this.DoRequestAsync(string.Format(DeezerFinderURL, query));
            string result = data.ReadToEnd();
            System.Diagnostics.Debug.WriteLine(result);
            TracksResponse response = JsonConvert.DeserializeObject<TracksResponse>(result);
            callback(response);
        }

        private async Task<System.IO.TextReader> DoRequestAsync(WebRequest req)
        {
            var task = Task.Factory.FromAsync((cb, o) => ((HttpWebRequest)o).BeginGetResponse(cb, o), res => ((HttpWebRequest)res.AsyncState).EndGetResponse(res), req);
            var result = await task;
            var resp = result;
            var stream = resp.GetResponseStream();
            var sr = new System.IO.StreamReader(stream);
            return sr;
        }

        private async Task<System.IO.TextReader> DoRequestAsync(string url)
        {
            HttpWebRequest req = HttpWebRequest.CreateHttp(url);
            req.AllowReadStreamBuffering = true;
            var tr = await DoRequestAsync(req);
            return tr;
        }
    
    }
}
