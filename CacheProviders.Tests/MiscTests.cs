using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace CacheProviders.Tests
{
    public class MiscTests
    {
        [Fact]
        public void TestStringToByte()
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes("Hello World");
            Assert.Equal(bytes, Helpers.ObjectToByteArray("Hello World"));
        }

        [Fact]
        public void TestByteToString()
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes("Hello World");
            Assert.Equal("Hello World", Helpers.ByteArrayToObject<string>(bytes));
        }

        [Fact]
        public void TestObjectToByte()
        {
            var obj = new Person
            {
                FirstName = "Royston",
                LastName = "Malcomber"
            };
            var bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
            Assert.Equal(bytes, Helpers.ObjectToByteArray(obj));
        }

        [Fact]
        public void TestByteToObject()
        {
            var obj = new Person
            {
                FirstName = "Royston",
                LastName = "Malcomber"
            };
            
            var bytes = JsonSerializer.SerializeToUtf8Bytes(obj);
            Assert.Equal(obj.FirstName, Helpers.ByteArrayToObject<Person>(bytes).FirstName);
            Assert.Equal(obj.LastName, Helpers.ByteArrayToObject<Person>(bytes).LastName);
        }

        [Fact]
        public void TestNoCacheDelegate()
        {
            var returnValue = "Hello World";
            var noCacheDelegate = new NoCacheDelegate<string>(() => returnValue);
            
            Assert.Equal(returnValue, noCacheDelegate());
        }
        
        [Fact]
        public async Task TestNoCacheAsyncDelegate()
        {
            var returnValue = "Hello World";
            var noCacheDelegate = new NoCacheDelegateAsync<string>(() => Task.FromResult(returnValue));
            
            Assert.Equal(returnValue, await noCacheDelegate());
        }

        public class Person
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
}