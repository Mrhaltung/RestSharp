using Microsoft.VisualStudio.TestTools.UnitTesting;
using AddressBook_RestSharp;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Generic;
using RestSharp;

namespace MSTESTRestSharp
{
    [TestClass]
    public class MSTEST
    {
        RestClient client;

        [TestInitialize]
        public void Setup()
        {
            client = new RestClient(" http://localhost:4000 ");
        }
        private IRestResponse GetAddressBook()
        {
            RestRequest request = new RestRequest("AddressBook", Method.GET);

            IRestResponse response = client.Execute(request);
            return response;
        }

        [TestMethod]
        public void OnCallingGetAPI_ReturnAddressBookList()
        {
            IRestResponse response = GetAddressBook();
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            List<AddressBook> AddressBookList = JsonConvert.DeserializeObject<List<AddressBook>>(response.Content);
            Assert.AreEqual(3, AddressBookList.Count);

            foreach (AddressBook addressBook in AddressBookList)
            {
                Console.WriteLine("id : " + addressBook.id + "\t" + "FirstName : " + addressBook.FirstName +
                    "\t" + "LastName : " + addressBook.LastName + "\t" + "Address : " + addressBook.Address +
                    "\t" + "City : " + addressBook.City + "\t" + "State : " + addressBook.State +
                    "\t" + "Zipcode : " + addressBook.Zipcode + "\t" + "PhoneNumber : " + addressBook.PhoneNumber +
                    "\t" + "Email : " + addressBook.Email);
            }
        }

        [TestMethod]
        public void OnCallingPostAPI_ReturnAddressBookObject()
        {
            RestRequest request = new RestRequest("/AddressBook", Method.POST);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("id", "9");
            jsonObj.Add("FirstName", "Tilak");
            jsonObj.Add("LastName", "Chandekar");
            jsonObj.Add("Address", "A");
            jsonObj.Add("City", "C");
            jsonObj.Add("State", "M");
            jsonObj.Add("Zipcode", 4);
            jsonObj.Add("PhoneNumber", "4872392");
            jsonObj.Add("Email", "tilak@gmail.com");

            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            AddressBook addressBook = new AddressBook();
            Assert.AreEqual("Tilak", addressBook.FirstName);
            Assert.AreEqual("Chandekar", addressBook.LastName);
            Assert.AreEqual("A", addressBook.Address);
            Assert.AreEqual("C", addressBook.City);
            Assert.AreEqual("M", addressBook.State);
            Assert.AreEqual("4872392", addressBook.PhoneNumber);
            Assert.AreEqual("tilak@gmail.com", addressBook.Email);
        }

        [TestMethod]
        public void OnCallingPostAPIForAEmployeeListWithMultipleEMployees_ReturnEmployeeObject()
        {
            List<AddressBook> addressBooks = new List<AddressBook>();
            addressBooks.Add(new AddressBook { FirstName = "Vidhi", LastName = "Chandekar", Address = "A", City = "C", State = "M", Zipcode = 4, PhoneNumber = "85536", Email = "radha@gmail.com" });

            foreach (var addressBook in addressBooks)
            {
                RestRequest request = new RestRequest("/AddressBook", Method.POST);
                JsonObject jsonObj = new JsonObject();
                jsonObj.Add("FirstName", addressBook.FirstName);
                jsonObj.Add("LastName", addressBook.LastName);
                jsonObj.Add("Address", addressBook.Address);
                jsonObj.Add("City", addressBook.City);
                jsonObj.Add("State", addressBook.State);
                jsonObj.Add("Zipcode", addressBook.Zipcode);
                jsonObj.Add("PhoneNumber", addressBook.PhoneNumber);
                jsonObj.Add("Email", addressBook.Email);
                request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
                AddressBook addressbook = JsonConvert.DeserializeObject<AddressBook>(response.Content);
                Assert.AreEqual(addressBook.FirstName, addressbook.FirstName);
                Assert.AreEqual(addressBook.LastName, addressbook.LastName);
                Console.WriteLine(response.Content);
            }
        }

        [TestMethod]
        public void OnCallingPutAPI_ReturnContactObjects()
        {
            RestRequest request = new RestRequest("/AddressBook/2", Method.PUT);
            JsonObject jsonObj = new JsonObject();
            jsonObj.Add("FirstName", "Shikhar");
            jsonObj.Add("LastName", "Dhawan");
            jsonObj.Add("PhoneNumber", "7858070934");
            jsonObj.Add("Address", "indian cricket");
            jsonObj.Add("City", "delhi");
            jsonObj.Add("State", "Inida");
            jsonObj.Add("Zipcode", 5);
            jsonObj.Add("Email", "sr7@gmail.com");

            request.AddParameter("application/json", jsonObj, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            AddressBook address = JsonConvert.DeserializeObject<AddressBook>(response.Content);
            Assert.AreEqual("Shikhar", address.FirstName);
            Assert.AreEqual("Dhawan", address.LastName);
            Assert.AreEqual(5, address.Zipcode);
            Console.WriteLine(response.Content);
        }


        [TestMethod]
        public void OnCallingDeleteAPI_ReturnSuccessStatus()
        {
            RestRequest request = new RestRequest("/AddressBook/5", Method.DELETE);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Console.WriteLine(response.Content);
        }
    }
}
