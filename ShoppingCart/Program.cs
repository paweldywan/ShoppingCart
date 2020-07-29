using ShoppingCart.Business.Commands;
using ShoppingCart.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    class Program
    {
        static void Main(string[] args)
        {
            _ = args;


            var shoppingCartRepository = new ShoppingCartRepository();
            var productsRepository = new ProductsRepository();

            var product = productsRepository.FindBy("SM7B");

            var addToCartCommand = new AddToCartCommand(shoppingCartRepository,
                productsRepository,
                product);

            var increaseQuantityCommand = new ChangeQuantityCommand(ChangeQuantityCommand.Operation.Increase,
                shoppingCartRepository,
                productsRepository,
                product);

            var manager = new CommandManager();
            manager.Invoke(addToCartCommand);
            manager.Invoke(increaseQuantityCommand);
            manager.Invoke(increaseQuantityCommand);
            manager.Invoke(increaseQuantityCommand);
            manager.Invoke(increaseQuantityCommand);

            //shoppingCartRepository.Add(product);
            //shoppingCartRepository.IncreaseQuantity(product.ArticleId);
            //shoppingCartRepository.IncreaseQuantity(product.ArticleId);
            //shoppingCartRepository.IncreaseQuantity(product.ArticleId);
            //shoppingCartRepository.IncreaseQuantity(product.ArticleId);

            PrintCart(shoppingCartRepository);

            manager.Undo();

            PrintCart(shoppingCartRepository);


            Console.ReadKey();
        }

        static void PrintCart(ShoppingCartRepository shoppingCartRepository)
        {
            Console.WriteLine(shoppingCartRepository);
        }
    }
}
