using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedPropertiesSolution.Module.BusinessObjects
{
    class MyTests
    {


        [Test]
        public void Test()
        {
            Assert.IsTrue(!string.IsNullOrEmpty(connectionString));
            Assert.IsNotNull(objectSpace);

            var product = objectSpace.CreateObject<Product>();
            product.Name = "Gacie";

            var order = objectSpace.CreateObject<Order>();
            order.Description = "First order";
            order.Total = 20;

            Assert.AreEqual(0, product.OrdersTotal);
            Assert.AreEqual(0, product.OrdersCount);
            product.Orders.Add(order);

            Assert.AreEqual(1, product.Orders.Count);
            Assert.AreEqual(0, product.OrdersTotal); // here should be 20
            Assert.AreEqual(0, product.OrdersCount); // here should be 1 

          


            order.Product = product;
            Assert.AreEqual(0, product.OrdersTotal);
            Assert.AreEqual(0, product.OrdersCount);
            Assert.AreEqual(1, product.Orders.Count);



            var order2 = objectSpace.CreateObject<Order>();
            order2.Description = "Second order";
            order2.Total = 33;

            product.Orders.Add(order2);

            Assert.AreEqual(2, product.Orders.Count); //here should be 2
            Assert.AreEqual(20, product.OrdersTotal); //here should be 53
            Assert.AreEqual(1, product.OrdersCount);




            order.Product = product;
            Assert.AreEqual(20, product.OrdersTotal); //here should be 53
            Assert.AreEqual(1, product.OrdersCount); //here should be 2
            Assert.AreEqual(2, product.Orders.Count);

            objectSpace.CommitChanges();

            order.Product = product;
            Assert.AreEqual(20, product.OrdersTotal); //here should be 53
            Assert.AreEqual(1, product.OrdersCount); //here should be 2
            Assert.AreEqual(2, product.Orders.Count);




        }
        #region setup



        IObjectSpace objectSpace;
        XPObjectSpaceProvider directProvider;
        string connectionString;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            XpoDefault.Session = null;

            connectionString = InMemoryDataStoreProvider.ConnectionString;
            directProvider = new XPObjectSpaceProvider(connectionString, null);
            objectSpace = directProvider.CreateObjectSpace();

            //XafTypesInfo.Instance.RegisterEntity(typeof(Produkt));
            XafTypesInfo.Instance.RegisterEntity(typeof(Order));
            XafTypesInfo.Instance.RegisterEntity(typeof(Product));
        }
        [SetUp]
        public void SetUp()
        {

            var a = 1;


        }
        #endregion
    }
}
