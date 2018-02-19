using AGLCodeChallenge.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AGLCodeChallenge
{
    
    class ProcessJson
    {
        public void DisplayPets(string url)
        {
            DisplayDatafromService(url);
        }
        /// <summary>
        /// Reads the data from JSON service using httpclient.
        /// Displays according to the requested format.
        /// </summary>
        /// <param name="uri"></param>
        private async void DisplayDatafromService(string uri)
        {
            string jsonData = null;
            if (!String.IsNullOrEmpty(uri))
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        var response = await httpClient.GetAsync(uri.Trim());
                        if (response.IsSuccessStatusCode)
                        {
                            jsonData = await response.Content.ReadAsStringAsync();
                        }
                    }
                     DisplayPetsList(jsonData);

                }
                catch (Exception ex)
                {
                    WriteMessage(ex.Message);
                    StopApp();
                }
            }
        }

        /// <summary>
        /// Displays list of pets in alphabetical order under owner's gender.
        /// </summary>
        /// <param name="jsonData"></param>
        private void DisplayPetsList(string jsonData)
        {   try
            { 
                if (!String.IsNullOrWhiteSpace(jsonData))
                {

                    List<People> peopleList = FetchPeopleData(jsonData);
                    var MaleCatList = RetrivePetsByType("Male", peopleList, "cat");
                    var FemaleCatList = RetrivePetsByType("FeMale", peopleList, "cat");

                    MaleCatList.Sort();
                    DisplayElements(MaleCatList, "Male");
                    FemaleCatList.Sort();
                    DisplayElements(FemaleCatList, "Female");
                }
                else
                {
                    WriteMessage("Json object is empty");
                    StopApp();
                }
            }
            catch (Exception ex)
            {
                WriteMessage(ex.Message);
                StopApp();
            }
            
        }

        private void DisplayElements(IEnumerable<string> nameList, string gender)
        {
            if (nameList.Any())
            {
                Console.WriteLine(gender);
                nameList.ToList().ForEach(mcats => Console.WriteLine($"\t{mcats}"));
                Console.WriteLine();
                Console.WriteLine();

            }
            else
            {
                WriteMessage("List is empty");
            }
        }

        /// <summary>
        /// Retrives the pets information from List<people> 
        /// according to pettype and owner's gender
        /// </summary>
        /// <param name="gender"></param>
        /// <param name="peopleList"></param>
        /// <param name="pettype"></param>
        /// <returns>String list</returns>
        private List<string> RetrivePetsByType(string gender, List<People> peopleList,string pettype)
        {
            List<string> petByGenderlist = new List<string>();
            try
            {

                if (!(String.IsNullOrEmpty(gender) && String.IsNullOrEmpty(pettype)) && (peopleList.Count > 0))
                {
                    petByGenderlist = peopleList.Where(person => person.Gender.ToLower().Equals(gender.ToLower()) && person.Pets != null)
                                                     .Select(person => person.Pets.Where(pet => pet.Type.ToLower().Equals(pettype.ToLower()))
                                                     .Select(pet => pet.Name)).SelectMany(k => k).ToList();
                }
                else
                {
                    
                }
            }
            catch(Exception ex)
            {
                WriteMessage(ex.Message);
                StopApp();
            }

            return petByGenderlist;

        }
        /// <summary>
        /// Deserialize data extracted from JSON string. 
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns>List of People object</returns>
        private List<People> FetchPeopleData(string jsonData)
        {
            List<People> peopleList= null;
            if (!String.IsNullOrEmpty(jsonData))
            {
                
                try
                {
                    peopleList = JsonConvert.DeserializeObject<List<People>>(JToken.Parse(jsonData).ToString());
                }
                catch(Exception ex)
                {
                    WriteMessage(ex.Message);
                    StopApp();
                }
            }

            return peopleList;
        }

        protected void StopApp()
        {
            Environment.Exit(0);
        }

        protected void WriteMessage(string message)
        {
            if(!string.IsNullOrEmpty(message))
            {
                Console.WriteLine(message);
                Console.ReadLine();
                
            }
        }
        
        
    }
}
