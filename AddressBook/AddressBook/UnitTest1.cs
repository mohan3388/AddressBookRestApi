using Newtonsoft.Json;
using RestSharp;

namespace AddressBook
{
    [TestClass]
    public class Employee
    {
        public string FirstName { get; set; }
        public string Lastame { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public long Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        RestClient client;
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }

        private IRestResponse getEmployee()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);
            IRestResponse restResponse = client.Execute(request);
            return restResponse;
        }

        [TestMethod]
        public void onCallingGetApi_returnEmployees()
        {
            IRestResponse restResponse = getEmployee();
            Assert.AreEqual(restResponse.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(restResponse.Content);
            Assert.AreEqual(1, dataResponse.Count());
            foreach (Employee e in dataResponse)
            {
                Console.WriteLine("FirstName " + e.FirstName + ", Lastame: " + e.Lastame + ", Address: " + e.Address + ", City: " + e.City + ", State: " + e.State + ", Zip: " + e.Zip + ", Phone: " + e.Phone + ", Email" + e.Email);
            }
        }
    }
}