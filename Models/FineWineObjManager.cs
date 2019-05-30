using System;
using System.Collections.Generic;

namespace Ansien.Api.LogAnalyzer
{
    class FineWineObjManager
    {


 
        public static Dictionary<string, int> GetMostQueriedCat(List<FineWineCategory> cats, List<FineWineLog> logs)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();

            for (int i = 0; i < logs.Count; i++)
            {
                foreach (string query in logs[i].NaturalLangQuery)
                {
                    for (int j = 1; j < cats.Count; j++)
                    {
                        if (cats[j].Value.Contains(query))
                        {
                            string key = $"{cats[j].Type},{cats[j].Value}";
                            if (dict.ContainsKey(key))
                                dict[key] += 1;
                            else
                                dict.Add(key, 1);
                        }
                        else
                        {
                            if (cats[j].Synonyms.Contains(query))
                            {
                                string key = $"{cats[j].Type},{cats[j].Value}";
                                if (dict.ContainsKey(key))
                                    dict[key] += 1;
                                else
                                    dict.Add(key, 1);
                            }
                        }
                    }
                }
            }

            return dict;
        }
    }
}
