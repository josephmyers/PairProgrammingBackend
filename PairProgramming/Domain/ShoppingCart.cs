using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ShoppingCart
    {
        private List<Product> _products = new List<Product>();

        public void AddProducts(params Product[] product)
        {
            _products.AddRange(product);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }

        public int GetTotal()
        {
            var itemsCopy = _products.ToList();
            var discountForJeansOnly = GetDiscountForJeansOnly(itemsCopy);
            var discountForShirtsAndJeans = GetDiscountForShirtsJeansSets(itemsCopy);
            var totalBeforeDiscount = _products.Sum(p => p.Cost);
            
            return totalBeforeDiscount - discountForJeansOnly - discountForShirtsAndJeans;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemsCopy">Copy of undiscounted items. Discounted items will be removed from the list.</param>
        /// <returns></returns>
        private int GetDiscountForJeansOnly(ICollection<Product> itemsCopy)
        {
            var numJeans = GetNumberOfJeans(itemsCopy);
            int numberOfJeansGroupings = numJeans / 3;
            var costOfOnePairOfJeans = GetFirstJeansProduct(itemsCopy).Cost;
            var discountAmount = numberOfJeansGroupings * costOfOnePairOfJeans;

            var numberOfJeansToRemove = numberOfJeansGroupings * 3;
            for (var i = 0; i < numberOfJeansToRemove; i++)
            {
                itemsCopy.Remove(GetFirstJeansProduct(itemsCopy));
            }
            
            return discountAmount;
        }

        private int GetDiscountForShirtsJeansSets(ICollection<Product> itemsCopy)
        {
            var numJeans = GetNumberOfJeans(itemsCopy);
            var numShirts = GetNumberOfShirts(itemsCopy);

            var discount = 15;

            int numPairsOfShirts = numShirts / 2;
            int numPairsOfJeans = numJeans / 2;

            var numJeansShirtsPairs = new List<int> { numPairsOfShirts, numPairsOfJeans }.Min();

            for (var i = 0; i < numJeansShirtsPairs; i++)
            {
                itemsCopy.Remove(GetFirstJeansProduct(itemsCopy));
                itemsCopy.Remove(GetFirstJeansProduct(itemsCopy));
                itemsCopy.Remove(GetFirstShirtProduct(itemsCopy));
                itemsCopy.Remove(GetFirstShirtProduct(itemsCopy));
            }
            
            return discount * numJeansShirtsPairs;
        }

        private int GetNumberOfShirts(ICollection<Product> itemsCopy)
        {
            return itemsCopy.Count(p => p.Title.Equals("Shirt"));
        }

        private Product GetFirstShirtProduct(ICollection<Product> itemsCopy)
        {
            return itemsCopy.FirstOrDefault(p => p.Title.Equals("Shirt"));
        }

        private int GetNumberOfJeans(ICollection<Product> itemsCopy)
        {
            return itemsCopy.Count(p => p.Title.Equals("Jeans"));
        }

        private Product GetFirstJeansProduct(ICollection<Product> itemsCopy)
        {
            return itemsCopy.FirstOrDefault(p => p.Title.Equals("Jeans"));
        }
    }

    
}