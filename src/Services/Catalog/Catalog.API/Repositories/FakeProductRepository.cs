using Catalog.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        private static List<Product> Products = new List<Product>
        {
            new Product()
            {
                Id = "602d2149e773f2a3990b47f5",
                Name = "IPhone X",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-1.png",
                Price = 950.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Id = "602d2149e773f2a3990b47f6",
                Name = "Samsung 10",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-2.png",
                Price = 840.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Id = "602d2149e773f2a3990b47f7",
                Name = "Huawei Plus",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-3.png",
                Price = 650.00M,
                Category = "White Appliances"
            },
            new Product()
            {
                Id = "602d2149e773f2a3990b47f8",
                Name = "Xiaomi Mi 9",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-4.png",
                Price = 470.00M,
                Category = "White Appliances"
            },
            new Product()
            {
                Id = "602d2149e773f2a3990b47f9",
                Name = "HTC U11+ Plus",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-5.png",
                Price = 380.00M,
                Category = "Smart Phone"
            },
            new Product()
            {
                Id = "602d2149e773f2a3990b47fa",
                Name = "LG G7 ThinQ",
                Summary = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus. Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ut, tenetur natus doloremque laborum quos iste ipsum rerum obcaecati impedit odit illo dolorum ab tempora nihil dicta earum fugiat. Temporibus, voluptatibus.",
                ImageFile = "product-6.png",
                Price = 240.00M,
                Category = "Home Kitchen"
            }
        };

        public async Task CreateProduct(Product product)
        {
            Products.Add(product);
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleted = Products.RemoveAll(item => item.Id == id);

            return deleted > 0;
        }

        public async Task<Product> GetProduct(string id)
        {
            return Products.Find(t => t.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return Products.ToList();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            return Products.Where(item => item.Category == categoryName).ToList();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return Products.Where(item => item.Name == name).ToList();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var itemToUpdate = Products.Find(item => item.Id == product.Id);

            if(itemToUpdate is null)
            {
                return false;
            }

            itemToUpdate.Description = product.Description;
            itemToUpdate.Category = product.Category;
            itemToUpdate.ImageFile = product.ImageFile;
            itemToUpdate.Name = product.Name;
            itemToUpdate.Price = product.Price;

            return true;
        }
    }
}
