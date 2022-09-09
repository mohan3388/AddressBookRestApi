using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;

namespace AddressBook
{
    [TestClass]
    public class Employee
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
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
                Console.WriteLine("FirstName " + e.FirstName + ", Lastame: " + e.LastName + ", Address: " + e.Address + ", City: " + e.City + ", State: " + e.State + ", Zip: " + e.Zip + ", Phone: " + e.Phone + ", Email" + e.Email);
            }
        }
        [TestMethod]
        public void AddEmployee()
        {
            RestRequest request = new RestRequest("/employees",Method.POST);
            JObject jobject = new JObject();
            jobject.Add("FirstName","Vivek");
            jobject.Add("LastName", "Singh");
            jobject.Add("Address", "nehru nagar");
            jobject.Add("City", "korba");
            jobject.Add("State", "CG");
            jobject.Add("Zip", 490020);
            jobject.Add("Phone", "7898625487");
            jobject.Add("Email", "Vivek@123gmail.com");
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Vivek",dataResponse.FirstName);
            Assert.AreEqual("Singh", dataResponse.LastName);
            Assert.AreEqual("nehru nagar", dataResponse.Address);
            Assert.AreEqual("korba", dataResponse.City);
            Assert.AreEqual("CG", dataResponse.State);
            Assert.AreEqual(490020, dataResponse.Zip);
            Assert.AreEqual("7898625487", dataResponse.Phone);
            Assert.AreEqual("Vivek@123gmail.com", dataResponse.Email);

            System.Console.WriteLine(response.Content);
        }
        [TestMethod]
        public void onCallingPutApi_returnEmployees()
        {
            RestRequest request = new RestRequest("/employees/2", Method.PUT);
            JObject jobject = new JObject();
            jobject.Add("FirstName", "Mohan");
            jobject.Add("LastName", "Sahu");
            jobject.Add("Address", "nehru nagar");
            jobject.Add("City", "korba");
            jobject.Add("State", "CG");
            jobject.Add("Zip", 490020);
            jobject.Add("Phone", "7898625487");
            jobject.Add("Email", "Vivek@123gmail.com");
            request.AddParameter("application/json", jobject, ParameterType.RequestBody);
            var response=client.Execute(request);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Mohan", dataResponse.FirstName);
            Assert.AreEqual("Sahu", dataResponse.LastName);
            Assert.AreEqual("nehru nagar", dataResponse.Address);
            Assert.AreEqual("korba", dataResponse.City);
            Assert.AreEqual("CG", dataResponse.State);
            Assert.AreEqual(490020, dataResponse.Zip);
            Assert.AreEqual("7898625487", dataResponse.Phone);
            Assert.AreEqual("Vivek@123gmail.com", dataResponse.Email);
            Console.WriteLine(response.Content);
        }
       
    }
}