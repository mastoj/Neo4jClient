using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using NUnit.Framework;

namespace DateTimeWat
{
    [TestFixture]
    public class SerializationTest
    {
        private GraphClient _client;

        [Test]
        public void SerializationError()
        {
            _client.Connect();
            _client.BeginTransaction();
            var data = new SomeType() { Date = DateTime.Today, Name = "Yolo" };
            _client
                .Cypher
                .Create("(n:SomeType {data})")
                .WithParam("data", data)
                .ExecuteWithoutResults();

            var actual = _client.Cypher.Match("(n:SomeType {Name:'Yolo'})").Return(n => n.As<SomeType>()).Results.First();
            Assert.AreEqual(actual, data);

            _client.EndTransaction();
        }

        [Test]
        public void SerializationOK()
        {
            _client.Connect();
            var data = new SomeType() { Date = DateTime.Today, Name = "Yolo" };
            _client
                .Cypher
                .Create("(n:SomeType {data})")
                .WithParam("data", data)
                .ExecuteWithoutResults();

            var actual = _client.Cypher.Match("(n:SomeType {Name:'Yolo'})").Return(n => n.As<SomeType>()).Results.First();
            Assert.AreEqual(actual, data);
        }

        [SetUp]
        public void Setup()
        {
            var url = new Uri("http://localhost:7474/db/data");
            _client = new Neo4jClient.GraphClient(url, "neo4j", "xxxxxx");
        }


        [TearDown]

        public void TearDown()
        {
            _client.Cypher.Match("(n:SomeType)").Delete("n").ExecuteWithoutResults();
        }
    }
}
