using CalculatedPropertiesSolution.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatedPropertiesSolution.Module
{
  public  class ProductsViewController : ObjectViewController<ListView, Product>
    {

        SimpleAction generateOrdersAction;
        public ProductsViewController()
        {
            generateOrdersAction = new SimpleAction(this, $"{GetType().FullName}.{nameof(generateOrdersAction)}", DevExpress.Persistent.Base.PredefinedCategory.Edit)
            {
                Caption = "Generate order",
                ImageName = "BO_Skull",

                SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
            };
            generateOrdersAction.Execute += GenerateOrdersAction_Execute;
        }

        private void GenerateOrdersAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var product = ObjectSpace.GetObject<Product>(ViewCurrentObject);
            var total1 = product.OrdersTotal;
            genereateOrders();
            ObjectSpace.CommitChanges();
            View.Refresh();
            var total2 = product.OrdersTotal;
            //DoSomethingWithTotalValue(total2); // here i get wrong value
        }

        void genereateOrders()
        {
            var product = ObjectSpace.GetObject<Product>(ViewCurrentObject);
            var order = ObjectSpace.CreateObject<Order>();
            order.Description = "First order";
            order.Total = 20;
            product.Orders.Add(order);
            order.Product = product;
            var order2 = ObjectSpace.CreateObject<Order>();
            order2.Description = "Second order";
            order2.Total = 30;
            product.Orders.Add(order2);

            order.Product = product;
        }
    }
}
