using System.Linq;
using Domain;
using NUnit.Framework;

namespace TestsNUnit
{
    public class Tests
    {
        private ShoppingCart _cart;
        private Product _jeans;
        private Product _shirt;

        [SetUp]
        public void Setup()
        {
            _cart = new ShoppingCart();
            _jeans = new Product("Jeans", 20);
            _shirt = new Product("Shirt", 10);
        }

        [Test]
        public void CustomerCanAddProductA()
        {
            var productA = new Product("A", 1);
            _cart.AddProducts(productA);
            Assert.AreEqual(1, _cart.GetProducts().Count());
            Assert.AreEqual(_cart.GetProducts().First(), productA);
        }

        [Test]
        public void CustomerCanAddProductB()
        {
            var productB = new Product("B", 1);
            _cart.AddProducts(productB);
            Assert.AreEqual(1, _cart.GetProducts().Count());
            Assert.AreEqual(_cart.GetProducts().First(), productB);
        }

        [Test]
        public void CustomerCanAddMultipleOfTheSameType()
        {
            var product1 = new Product("A", 1);
            var product2 = new Product("A", 2);
            
            _cart.AddProducts(product1);
            _cart.AddProducts(product2);
            
            Assert.IsTrue(_cart.GetProducts().Contains(product1));
            Assert.IsTrue(_cart.GetProducts().Contains(product2));
        }

        [Test]
        public void CustomerCanAddDifferentProductTypes()
        {
            var productA = new Product("A", 1);
            var productB = new Product("B", 2);
            
            _cart.AddProducts(productA);
            _cart.AddProducts(productB);
            
            Assert.IsTrue(_cart.GetProducts().Contains(productA));
            Assert.IsTrue(_cart.GetProducts().Contains(productB));
        }

        [Test]
        public void CustomerCanRetrieveCartPrice()
        {
            _cart.AddProducts(_shirt, _jeans);
            
            Assert.AreEqual(_shirt.Cost + _jeans.Cost, _cart.GetTotal());
        }

        [Test]
        public void CustomerCanRetrieveCartPrice2()
        {
            _cart.AddProducts(_shirt, _jeans, _jeans);
            
            Assert.AreEqual(_shirt.Cost + _jeans.Cost + _jeans.Cost, _cart.GetTotal());
        }

        [Test]
        public void CustomerGetsThreeJeansForThePriceOfTwo()
        {
            _cart.AddProducts(_jeans, _jeans);
            
            Assert.AreEqual(_jeans.Cost + _jeans.Cost, _cart.GetTotal());

            _cart.AddProducts(_jeans);
            
            Assert.AreEqual(_jeans.Cost + _jeans.Cost, _cart.GetTotal());
        }

        [Test]
        public void CustomerGetsSixJeansForThePriceOfFour()
        {
            _cart.AddProducts(_jeans, _jeans, _jeans, _jeans, _jeans, _jeans);
            
            Assert.AreEqual(_jeans.Cost * 4, _cart.GetTotal());
        }

        [Test]
        public void Customer_FourJeans_TwoShirts_AppliesJeansDiscount()
        {
            _cart.AddProducts(_jeans, _jeans, _jeans, _jeans, _shirt, _shirt);

            var discountedJeans = _jeans.Cost * 2;
            
            Assert.AreEqual(discountedJeans + _jeans.Cost + _shirt.Cost*2, _cart.GetTotal());
        }

        [Test]
        public void Customer_SixJeans_TwoShirts_AppliesJeansDiscount()
        {
            _cart.AddProducts(_jeans, _jeans, _jeans, _jeans, _jeans, _jeans, _shirt, _shirt);
            
            var discountedJeans = _jeans.Cost * 2;

            Assert.AreEqual(discountedJeans*2 + _shirt.Cost*2, _cart.GetTotal());
        }

        [Test]
        public void Customer_TwoJeans_TwoShirts_AppliesShirtJeansDiscount()
        {
            _cart.AddProducts(_jeans, _jeans, _shirt, _shirt);

            var discountedShirt = _shirt.Cost - 5;
            var discountedJeans = _jeans.Cost - 10;
            
            Assert.AreEqual(_jeans.Cost + _shirt.Cost + discountedJeans + discountedShirt, _cart.GetTotal());
        }

        [Test]
        public void Customer_FiveJeans_TwoShirts_AppliesBothDiscounts()
        {
            _cart.AddProducts(_jeans, _jeans, _jeans, _jeans, _jeans, _shirt, _shirt);

            var discountedShirt = _shirt.Cost - 5;
            var discountedJeans = _jeans.Cost - 10;
            var discountedJeansGroup = _jeans.Cost * 2;
            
            Assert.AreEqual(_jeans.Cost + _shirt.Cost + discountedJeans + discountedShirt + discountedJeansGroup, _cart.GetTotal());
        }

        [Test]
        public void Customer_FiveJeans_FourShirts_AppliesShirtJeansDiscountOnlyOnce()
        {
            _cart.AddProducts(_jeans, _jeans, _jeans, _jeans, _jeans, _shirt, _shirt, _shirt, _shirt);
            
            var discountedShirt = _shirt.Cost - 5;
            var discountedJeans = _jeans.Cost - 10;
            var discountedJeansGroup = _jeans.Cost * 2;
            
            Assert.AreEqual(_jeans.Cost + _shirt.Cost*3 + discountedJeans + discountedShirt + discountedJeansGroup, _cart.GetTotal());
        }
    }
}