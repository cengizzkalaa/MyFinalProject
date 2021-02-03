using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products; // Classın içinde ama metodların dışında tanımladıgımız için buna global degişken denir.
        public InMemoryProductDal()
        {
            //Oracele,Sql Server , POstgres, MongoDb gibi veri tabanlarından bu tarz veriler geliyormuş gibi şuan simule ettik.
            _products = new List<Product> 
            {
                new Product{ProductId=1 , CategoryId=1 , ProductName="Bardak", UnitPrice =15 , UnitsInStock=15},
                new Product{ProductId=2 , CategoryId=1 , ProductName="Kamera", UnitPrice =500 , UnitsInStock=3},
                new Product{ProductId=3 , CategoryId=2 , ProductName="Telefon", UnitPrice =1500 , UnitsInStock=2},
                new Product{ProductId=4 , CategoryId=2 , ProductName="Klavye", UnitPrice =150 , UnitsInStock=65},
                new Product{ProductId=5 , CategoryId=2 , ProductName="Fare", UnitPrice =85 , UnitsInStock=1}

            };
        }
        public void Add(Product product) // Businessten bana gönderilen ürünü veri tabanına ekliyorum yani List<Product> _products; buraya
        {
            _products.Add(product);
        }

        public void Delete(Product product) // Listeye product eklemek
        {
            //LINQ - Languae Integrated Query
            //Lambda
            //Product productToDelete = null;  // uzun yol bu elimdeki elemanları dolaşıp eğer eşitse benim gönderdiğim id ye eşitse silincek elemanı bulmak
            //foreach (var p in _products)
            //{
                //if (product.ProductId==p.ProductId) // benim gonderdıgım gonderdıgım productın product ile p nin product id si eşitse
                //{
                    //productToDelete = p;
                //}
           // }

            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId==product.ProductId); // p tek tek dolaşırken verdigimiz takma isim. Yukarıdakı gibi uzun uzun yazman yerine LINQ ile böyle yazarız.
            // her p için o an dolaştıgımız ürünün ıd si benım parametre ile gönderdigim ürünün id sine eşitse onun referansını producttodelete eşitle

            _products.Remove(productToDelete);

        }

        public List<Product> GetAll() // veritabanındaki datayı business e vermemiz lazım
        {
            return _products; // Bütün veritabanını döndürüyoruz
        }

        public void Uptade(Product product)
        {
            // Gönderdiğim ürün id'sine sahip olan listediki ürünü bul
            Product productToUptade = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUptade.ProductName = product.ProductName;
            productToUptade.CategoryId = product.CategoryId;
            productToUptade.UnitPrice = product.UnitPrice;
            productToUptade.UnitsInStock = product.UnitsInStock;

        }
        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList(); // where koşulu içindeki şarta uyan butun elemanları yeni bir liste haline getirir ve durdurur.
        }
    }
}
