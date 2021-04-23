using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestSharpTest
{
    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:3000");
        }

        private IRestResponse getEmployeeList()
        {
            RestRequest request = new RestRequest("/employees", Method.GET);

            //act

            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void onCallingGETApi_ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();

            //assert
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(5, dataResponse.Count);
            foreach (var item in dataResponse)
            {
                System.Console.WriteLine("id: " + item.id + "Name: " + item.name + "Salary: " + item.Salary);
            }
        }

        [TestMethod]
        public void givenEmployee_OnPost_ShouldReturnAddedEmployee()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Clark");
            jObjectbody.Add("Salary", "15000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Clark", dataResponse.name);
            Assert.AreEqual(15000, dataResponse.Salary);

        }

        [TestMethod]
        public void givenEmployees_OnPost_ShouldReturnAddedEmployees()
        {
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jObjectbody = new JObject();
            List<Employee> list = new List<Employee>()
            {
            new Employee() { name = "Prashik", Salary = 999999999 },
            new Employee() { name = "Jaware", Salary = 999999998 },
            new Employee() { name = "Emperor", Salary = 900000000 }
            };

            jObjectbody.Add("name", list[0].name);
            jObjectbody.Add("Salary", list[0].Salary);

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);
            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual(list[0].name, dataResponse.name);
            Assert.AreEqual(list[0].Salary, dataResponse.Salary);

            RestRequest request1 = new RestRequest("/employees", Method.POST);
            JObject jObjectbody1 = new JObject();
            jObjectbody1.Add("name", list[1].name);
            jObjectbody1.Add("Salary", list[1].Salary);

            request1.AddParameter("application/json", jObjectbody1, ParameterType.RequestBody);
            //act
            IRestResponse response1 = client.Execute(request1);
            Assert.AreEqual(response1.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse1 = JsonConvert.DeserializeObject<Employee>(response1.Content);
            Assert.AreEqual(list[1].name, dataResponse1.name);
            Assert.AreEqual(list[1].Salary, dataResponse1.Salary);

            RestRequest request2 = new RestRequest("/employees", Method.POST);
            JObject jObjectbody2 = new JObject();
            jObjectbody2.Add("name", list[2].name);
            jObjectbody2.Add("Salary", list[2].Salary);

            request2.AddParameter("application/json", jObjectbody2, ParameterType.RequestBody);
            //act
            IRestResponse response2 = client.Execute(request2);
            Assert.AreEqual(response2.StatusCode, System.Net.HttpStatusCode.Created);
            Employee dataResponse2 = JsonConvert.DeserializeObject<Employee>(response2.Content);
            Assert.AreEqual(list[2].name, dataResponse2.name);
            Assert.AreEqual(list[2].Salary, dataResponse2.Salary);


        }

        [TestMethod]
        public void givenEmployee_OnUpdate_ShouldReturnUpdatedEmployee()
        {
            RestRequest request = new RestRequest("/employees/update/2", Method.PUT);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "David");
            jObjectbody.Add("Salary", "1500000");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            //act
            IRestResponse response = client.Execute(request);
            Assert.AreEqual(response.StatusCode, System.Net.HttpStatusCode.OK);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("David", dataResponse.name);
            Assert.AreEqual(1500000, dataResponse.Salary);
            Console.WriteLine(response.Content);
        }
    }

}
