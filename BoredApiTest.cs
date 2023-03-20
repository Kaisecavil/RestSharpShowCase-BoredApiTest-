using Newtonsoft.Json;
using RestSharp;
using RestSharpShowCase.Models;
using System;
using System.Net;


namespace RestSharpShowCase
{
    public class Tests
    {
        private readonly string _boredApiUrl = "https://www.boredapi.com";
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void StatusCodeTest()
        {
            // arrange
            RestClient client = new RestClient(_boredApiUrl);
            RestRequest request = new RestRequest("/api/activity/", Method.Get);

            // act
            RestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ContentTypeTest()
        {
            // arrange
            RestClient client = new RestClient(_boredApiUrl);
            RestRequest request = new RestRequest("/api/activity/", Method.Get);

            // act
            RestResponse response = client.Execute(request);

            // assert
            Assert.That(response.ContentType, Is.EqualTo("application/json"));
        }

        [TestCase("1", "diy", HttpStatusCode.OK, TestName = "Check status code valid query")]
        [TestCase("-1", "recreational", HttpStatusCode.OK, TestName = "Check status code invalid query")]
        public void StatusCodeTest(int countOfParticipants, string type, HttpStatusCode expectedHttpStatusCode)
        {
            // arrange
            RestClient client = new RestClient(_boredApiUrl);
            RestRequest request = new RestRequest($"/api/activity?participants={countOfParticipants}&type={type}", Method.Get);

            // act
            RestResponse response = client.Execute(request);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(expectedHttpStatusCode));
        }

        [TestCase(0, 1, TestName = "Check accessibility range from 0 to 1")]
        [TestCase(-1, 0, TestName = "Check accessibility range from -1 to 0")]
        public void AccessibilityRangeTest(double minAccessibility, double maxAccessibility)
        {
            // arrange
            RestClient client = new RestClient(_boredApiUrl);
            RestRequest request = new RestRequest($"/api/activity?minaccessibility={minAccessibility}&maxaccessibility={maxAccessibility}", Method.Get);
            
            // act
            RestResponse response = client.Execute(request);
            Activity activity = (Activity)JsonConvert.DeserializeObject<Activity>(response.Content);
            
            //assert
            Assert.IsTrue(minAccessibility <= activity.Accessibility && activity.Accessibility <= maxAccessibility);

        }

        [TestCase(10, 15, "No activity found with the specified parameters", TestName = "Check error for accessibility range from 10 to 15")]
        [TestCase(-1, -3, "No activity found with the specified parameters", TestName = "Check error for accessibility range from -1 to -3")]
        public void InvalidParametrsTest(double minAccessibility, double maxAccessibility, string expectedErrorMessage)
        {
            // arrange
            RestClient client = new RestClient(_boredApiUrl);
            RestRequest request = new RestRequest($"/api/activity?minaccessibility={minAccessibility}&maxaccessibility={maxAccessibility}", Method.Get);

            // act
            var response = client.Execute(request);
            APIError actualError = JsonConvert.DeserializeObject<APIError>(response.Content);

            //assert
            Assert.That(actualError.Error, Is.EqualTo(expectedErrorMessage));
        }

        [TestCase(1000000, "Make a new friend", TestName = $"Check activity description by key = 1000000")]
        [TestCase(9999999, "Resolve a problem you've been putting off", TestName = $"Check activity description by key = 9999999")]
        public void ActivityByKeyTest(long key, string expectedActivityDesctiption)
        {
            // arrange
            RestClient client = new RestClient(_boredApiUrl);
            RestRequest request = new RestRequest($"/api/activity?key={key}", Method.Get);

            // act
            var response = client.Execute(request);  
            /*Execute<Activity> doesn't work in this situation 
             * because of property names colliding with class name, so 
             * we have to use [Json property("property name")] annotations 
             * to deserialize objects correctly with help of JsonConvert class*/ 
            var activity = JsonConvert.DeserializeObject<Activity>(response.Content);

            //assert
            Assert.That(activity.ActivityDescription, Is.EqualTo(expectedActivityDesctiption));
          
        }
    }
}