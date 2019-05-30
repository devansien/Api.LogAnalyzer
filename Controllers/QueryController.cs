using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ansien.Api.LogAnalyzer.Controllers
{
    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        // GET api/values
        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return (await Task.Run(() =>
            {
                List<FineWineCategory> cats = ConvertFineWineJsonToObj<FineWineCategory>("finewinecats.json");
                List<FineWineLog> logs = ConvertFineWineJsonToObj<FineWineLog>("finewinelogs.json");

                TimeSpan timeSpan;
                DateTime logTime = DateTime.Parse("2019-05-20T17:33:25.687+12:00");

                switch (id)
                {
                    case 7:
                        timeSpan = new TimeSpan(7, 0, 0, 0);
                        break;
                    case 48:
                        timeSpan = new TimeSpan(2, 0, 0, 0);
                        break;
                    case 24:
                        timeSpan = new TimeSpan(1, 0, 0, 0);
                        break;
                    case 12:
                        timeSpan = new TimeSpan(0, 12, 0, 0);
                        break;
                    default:
                        timeSpan = new TimeSpan(0, 12, 0, 0);
                        break;
                }

                DateTime minTime = logTime.Subtract(timeSpan);


                Dictionary<string, int> topUser = new Dictionary<string, int>();

                for (int i = 0; i < logs.Count; i++)
                {
                    if (DateTime.Compare(minTime, logs[i].Timestamp) < 0)
                    {
                        if (topUser.ContainsKey(logs[i].CustomerId.ToString()))
                            topUser[logs[i].CustomerId.ToString()] += 1;
                        else
                            topUser.Add(logs[i].CustomerId.ToString(), 1);
                    }
                }

                var sortedQuery = from entry in FineWineObjManager.GetMostQueriedCat(cats, logs) orderby entry.Value descending select entry;

                int counter = 0;
                foreach (KeyValuePair<string, int> pair in sortedQuery)
                {
                    if (counter < 10)
                        Console.WriteLine("{0}: {1}", pair.Key, pair.Value);

                    counter++;
                }

                var sortedUser = from entry in topUser orderby entry.Value descending select entry;

                ResponseWrapper response = new ResponseWrapper
                {
                    TopUser = sortedUser.First().Key,
                    TopUserCounter = sortedUser.First().Value.ToString(),
                    TopCategory = sortedQuery.First().Key,
                    TopCategoryCounter = sortedQuery.First().Value.ToString()
                };





                //FineWineCategory category = new FineWineCategory
                //{
                //    Type = "a",
                //    Value = "b",
                //    Synonyms = new HashSet<string> { "v" }
                //};

                return new JsonResult(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
            }));
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        static List<T> ConvertFineWineJsonToObj<T>(string jsonFilePath)
        {
            List<T> objects;

            using (StreamReader reader = new StreamReader(jsonFilePath))
            {
                string json = reader.ReadToEnd();
                objects = JsonConvert.DeserializeObject<List<T>>(json);
            }

            return objects;
        }
    }
}
