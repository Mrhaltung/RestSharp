using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeRestSharp;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using RestSharp;

namespace MSTESTRestSharp
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void SetUp()
        {
            client = new RestClient(" http://localhost:4000");
        }
        private IRestResponse GetEmployeeList()
        {
            RestRequest request = new RestRequest("employee", Method.GET);

            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void OnCallingGetAPI_ReturnEmployeeList()
        {
            IRestResponse response = GetEmployeeList();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<Employee> employeeList = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            Assert.AreEqual(16, employeeList.Count);
            foreach (Employee emp in employeeList)
            {
                Console.WriteLine("id: " + emp.Id + "\t" + "name: " + emp.Name + "\t" + "salary: " + emp.Salary);
            }
        }

        [TestMethod]
        public void OnCallingPostAPI_ReturnEmployeeObject()
        {
            RestRequest request = new RestRequest("/employee", Method.POST);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("name", "Ritik");
            jsonObj.Add("salary", "50000");
            jsonObj.Add("id", "11");

            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created );
            Employee emp = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Ritik", emp.Name);
            Assert.AreEqual("50000", emp.Salary);
            Console.WriteLine(response.Content);
        }

        [TestMethod]
        public void OnCallingPostAPIForAEmployeeListWithMultipleEMployees_ReturnEmployeeObject()
        {
            List<Employee> employeeList = new List<Employee>();
            employeeList.Add(new Employee { Name = "Radha", Salary = "85536" });
            employeeList.Add(new Employee { Name = "Watson", Salary = "120123" });
            employeeList.Add(new Employee { Name = "Christiano Ronaldo", Salary = "123456" });

            foreach (var emp in employeeList)
            {

                RestRequest request = new RestRequest("employee", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("name", emp.Name);
                jsonObj.Add("salary", emp.Salary);

                request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                Employee employee = JsonConvert.DeserializeObject<Employee>(response.Content);
                Assert.AreEqual(emp.Name, employee.Name);
                Assert.AreEqual(emp.Salary, employee.Salary);
                Console.WriteLine(response.Content);
            }
        }

        [TestMethod]
        public void OnCallingPutAPI_ReturnEmployeeObject()
        {
            RestRequest request = new RestRequest("employee", Method.POST);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("name", "Radha");
            jsonObj.Add("salary", "65000");
            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Employee employee = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("Radha", employee.Name);
            Assert.AreEqual("65000", employee.Salary);
            Console.WriteLine(response.Content);
        }

        [TestMethod]
        public void OnCallingDeleteAPI_ReturnSuccessStatus()
        {
            RestRequest request = new RestRequest("/employee/10", Method.DELETE);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine(response.Content);
        }
    }
}


