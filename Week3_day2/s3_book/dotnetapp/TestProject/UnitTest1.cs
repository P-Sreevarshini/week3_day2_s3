using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        public void S1_Model_Class_Book_Exists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type playerType = assembly.GetType(typeName);
            Assert.IsNotNull(playerType);
        }

        [Test]
        public void S1_Model_Class_Category_Exists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Category";
            Assembly assembly = Assembly.Load(assemblyName);
            Type playerType = assembly.GetType(typeName);
            Assert.IsNotNull(playerType);
        }

        [Test]
        public void S1_Model_Class_ShoppingCart_Exists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.ShoppingCart";
            Assembly assembly = Assembly.Load(assemblyName);
            Type playerType = assembly.GetType(typeName);
            Assert.IsNotNull(playerType);
        }

        [Test]
        public void S1_Model_Class_ShoppingCartItem_Exists()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.ShoppingCartItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type playerType = assembly.GetType(typeName);
            Assert.IsNotNull(playerType);
        }
        [Test]
        public void S1_Test_DetailsViewFile_Exists()
        {
            string indexPath = Path.Combine(@"D:\dotnet_DayWise_Week3\Day2\Book\dotnetapp\dotnetapp\Views\Book\", "Details.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Details.cshtml view file does not exist.");
        }

        [Test]
        public void S1_Test_IndexViewFile_Exists()
        {
            string indexPath = Path.Combine(@"D:\dotnet_DayWise_Week3\Day2\Book\dotnetapp\dotnetapp\Views\Category\", "Index.cshtml");
            bool indexViewExists = File.Exists(indexPath);

            Assert.IsTrue(indexViewExists, "Details.cshtml view file does not exist.");
        }

        [Test]
        public void S3_Book_Properties_Have_RequiredAttribute_Title()
        {
            var count = 0;
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type employeeType = assembly.GetType(typeName);

            //Type employeeType = typeof(dotnetapp.Models.Book);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Title")
                {
                    var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                    Assert.NotNull(requiredAttribute, $"{property.Name} should have a RequiredAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void S3_Book_Properties_Have_RequiredAttribute_Author()
        {
            var count = 0;
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type employeeType = assembly.GetType(typeName);

            //Type employeeType = typeof(dotnetapp.Models.Book);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Author")
                {
                    var requiredAttribute = property.GetCustomAttribute<RequiredAttribute>();
                    Assert.NotNull(requiredAttribute, $"{property.Name} should have a RequiredAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void S3_Book_Properties_Have_RangeAttribute_Price()
        {
            var count = 0;
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type employeeType = assembly.GetType(typeName);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Price")
                {
                    var rangeAttribute = property.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                    Assert.NotNull(rangeAttribute, $"{property.Name} should have a RangeAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void S3_ShoppingCartItem_Properties_Have_RangeAttribute_Quantity()
        {
            var count = 0;
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.ShoppingCartItem";
            Assembly assembly = Assembly.Load(assemblyName);
            Type employeeType = assembly.GetType(typeName);
            PropertyInfo[] properties = employeeType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Quantity")
                {
                    var rangeAttribute = property.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                    Assert.NotNull(rangeAttribute, $"{property.Name} should have a RangeAttribute.");
                    count++;
                    break;
                }
            }
            if (count == 0)
            {
                Assert.Fail();
            }
        }

        // Test to Check Book Models Property Title Exists with correcct datatype string    
        [Test]
        public void Book_Title_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BookType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BookType.GetProperty("Title");
            Assert.IsNotNull(propertyInfo, "Property Title does not exist in Book class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Title in Book class is not of type string");
        }

        // Test to Check Book Models Property Author Exists with correcct datatype string    
        [Test]
        public void Book_Author_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BookType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BookType.GetProperty("Author");
            Assert.IsNotNull(propertyInfo, "Property Author does not exist in Book class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Author in Book class is not of type string");
        }

        // Test to Check Book Models Property Price Exists with correcct datatype decimal    
        [Test]
        public void Book_Price_PropertyExists_ReturnExpectedDataTypes_decimal()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BookType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BookType.GetProperty("Price");
            Assert.IsNotNull(propertyInfo, "Property Price does not exist in Book class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(decimal), expectedType, "Property Price in Book class is not of type decimal");
        }



        // Test to Check Book Models Property Description Exists with correcct datatype string    
        [Test]
        public void Book_Description_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BookType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BookType.GetProperty("Description");
            Assert.IsNotNull(propertyInfo, "Property Description does not exist in Book class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property Description in Book class is not of type string");
        }

        // Test to Check Book Models Property CoverImage Exists with correcct datatype string    
        [Test]
        public void Book_CoverImage_PropertyExists_ReturnExpectedDataTypes_string()
        {
            string assemblyName = "dotnetapp";
            string typeName = "dotnetapp.Models.Book";
            Assembly assembly = Assembly.Load(assemblyName);
            Type BookType = assembly.GetType(typeName);
            PropertyInfo propertyInfo = BookType.GetProperty("CoverImage");
            Assert.IsNotNull(propertyInfo, "Property CoverImage does not exist in Book class");
            Type expectedType = propertyInfo.PropertyType;
            Assert.AreEqual(typeof(string), expectedType, "Property CoverImage in Book class is not of type string");
        }

    }
}